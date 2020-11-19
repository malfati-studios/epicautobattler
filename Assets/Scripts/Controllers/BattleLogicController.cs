using System;
using System.Collections.Generic;
using System.Threading;
using Units;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class BattleLogicController : MonoBehaviour
    {
        [SerializeField] private int allEnemyUnitsCount;
        [SerializeField] private int allPlayerUnitsCount;

        ReaderWriterLock alivePlayerUnitsLock = new ReaderWriterLock();
        [SerializeField] private List<Unit> alivePlayerUnits = new List<Unit>();

        ReaderWriterLock aliveEnemyUnitsLock = new ReaderWriterLock();
        [SerializeField] private List<Unit> aliveEnemyUnits = new List<Unit>();

        [SerializeField] private Dictionary<UnitType, int> currentUnitCredits;

        private bool battleStarted;

        private BattleUIController battleUIController;

        public void Initialize(BattleUIController battleUIController, Dictionary<UnitType, int> unitCredits)
        {
            currentUnitCredits = new Dictionary<UnitType, int>();
            currentUnitCredits[UnitType.FOOTMAN] = unitCredits[UnitType.FOOTMAN];
            currentUnitCredits[UnitType.HEALER] = unitCredits[UnitType.HEALER];
            currentUnitCredits[UnitType.ARCHER] = unitCredits[UnitType.ARCHER];

            this.battleUIController = battleUIController;
            ScanUnits();
        }

        public Unit GetNearestAlly(Unit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return SafelyReadFromAlivePlayerUnits(movingUnit, UnitTargetHelper.GetClosestUnit);
            }
            else
            {
                return SafelyReadFromEnemyPlayerUnits(movingUnit, UnitTargetHelper.GetClosestUnit);
            }
        }

        public Unit GetNearestHurtAlly(Unit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return SafelyReadFromAlivePlayerUnits(movingUnit, UnitTargetHelper.GetClosestHurtUnit);
            }

            return SafelyReadFromEnemyPlayerUnits(movingUnit, UnitTargetHelper.GetClosestHurtUnit);
        }

        public Unit GetNearestEnemy(Unit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return SafelyReadFromEnemyPlayerUnits(movingUnit, UnitTargetHelper.GetClosestUnit);
            }

            return SafelyReadFromAlivePlayerUnits(movingUnit, UnitTargetHelper.GetClosestUnit);
        }

        public void NotifyDeath(MovingUnit go)
        {
            if (go.faction == Faction.PLAYER)
            {
                alivePlayerUnitsLock.AcquireWriterLock(500);
                alivePlayerUnits.Remove(go.GetComponent<Unit>());
                alivePlayerUnitsLock.ReleaseWriterLock();
            }
            else
            {
                aliveEnemyUnitsLock.AcquireWriterLock(500);
                aliveEnemyUnits.Remove(go.GetComponent<Unit>());
                aliveEnemyUnitsLock.ReleaseWriterLock();
            }

            battleUIController.RefreshArmyBarsUI(alivePlayerUnits.Count, allPlayerUnitsCount,
                aliveEnemyUnits.Count, allEnemyUnitsCount);
            CheckWinLoseConditions();
        }

        public bool BattleStarted()
        {
            return battleStarted;
        }

        public int NotifyNewUnitAndReturnCreditsLeft(Unit u)
        {
            alivePlayerUnitsLock.AcquireWriterLock(500);
            alivePlayerUnits.Add(u);
            alivePlayerUnitsLock.ReleaseWriterLock();
            
            allPlayerUnitsCount++;
            currentUnitCredits[u.type] = currentUnitCredits[u.type] - 1;

            battleUIController.RefreshArmyBarsUI(alivePlayerUnits.Count, allPlayerUnitsCount,
                aliveEnemyUnits.Count, allEnemyUnitsCount);

            InitializeUnit(u);

            return currentUnitCredits[u.type];
        }

        public void StartBattle()
        {
            battleStarted = true;
            foreach (var aliveEnemyUnit in aliveEnemyUnits)
            {
                InitializeUnit(aliveEnemyUnit);
            }

            foreach (var alivePlayerUnit in alivePlayerUnits)
            {
                InitializeUnit(alivePlayerUnit);
            }
        }

        private void InitializeUnit(Unit u)
        {
            u.SetBattleController(this);
            if (battleStarted && UnitTargetHelper.CanSearchForTarget(u))
            {
                ((MovingUnit) u).SearchForTarget();
            }
        }

        private void ScanUnits()
        {
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (var goUnit in units)
            {
                MovingUnit u = goUnit.GetComponent<MovingUnit>();
                if (u.faction == Faction.PLAYER)
                {
                    alivePlayerUnits.Add(u);
                }
                else
                {
                    aliveEnemyUnits.Add(u);
                }
            }

            allEnemyUnitsCount = aliveEnemyUnits.Count;
            allPlayerUnitsCount = alivePlayerUnits.Count;
        }

        private Unit SafelyReadFromAlivePlayerUnits(Unit u, Func<Unit, List<Unit>, Unit> callback)
        {
            Unit result;
            alivePlayerUnitsLock.AcquireReaderLock(500);
            result = callback(u, alivePlayerUnits);
            alivePlayerUnitsLock.ReleaseReaderLock();
            return result;
        }

        private Unit SafelyReadFromEnemyPlayerUnits(Unit u, Func<Unit, List<Unit>, Unit> callback)
        {
            Unit result;
            aliveEnemyUnitsLock.AcquireReaderLock(500);
            result = callback(u, aliveEnemyUnits);
            aliveEnemyUnitsLock.ReleaseReaderLock();
            return result;
        }

     
        private void CheckWinLoseConditions()
        {
            if (CheckPlayerLost())
            {
                battleUIController.ShowLoseScreen();
                GameController.instance.NotifyLevelLost();
                return;
            }

            if (CheckPlayerWon())
            {
                battleUIController.ShowWinScreen();
                GameController.instance.NotifyLevelWon();
                return;
            }
            
        }

        private bool CheckPlayerLost()
        {
            return alivePlayerUnits.Count == 0 && NoMoreCredits();
        }
     
        private bool CheckPlayerWon()
        {
            return aliveEnemyUnits.Count == 0;
        }
        
        private bool NoMoreCredits()
        {
            foreach (var unitCredit in currentUnitCredits)
            {
                if (unitCredit.Value > 0)
                {
                    return false;
                }
            }

            return true;
        }

    }
}