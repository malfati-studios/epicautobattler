using System.Collections.Generic;
using Units;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class BattleLogicController : MonoBehaviour
    {
        [SerializeField] private int allEnemyUnitsCount;
        [SerializeField] private int allPlayerUnitsCount;

        [SerializeField] private List<Unit> alivePlayerUnits = new List<Unit>();
        [SerializeField] private List<Unit> aliveEnemyUnits = new List<Unit>();

        [SerializeField] private Dictionary<UnitType, int> currentUnitCredits;

        private bool battleStarted;

        private BattleUIController battleUIController;

        public void Initialize(BattleUIController battleUIController, Dictionary<UnitType, int> unitCredits)
        {
            currentUnitCredits = new Dictionary<UnitType, int>();
            currentUnitCredits[UnitType.Footman] = unitCredits[UnitType.Footman];
            currentUnitCredits[UnitType.Healer] = unitCredits[UnitType.Healer];
            this.battleUIController = battleUIController;
            ScanUnits();
        }

        public Unit GetNearestAlly(Unit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return UnitTargetHelper.GetClosestUnit(movingUnit, alivePlayerUnits);
            }

            return UnitTargetHelper.GetClosestUnit(movingUnit, aliveEnemyUnits);
        }
        
        public Unit GetNearestHurtAlly(Unit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return UnitTargetHelper.GetClosestHurtUnit(movingUnit, alivePlayerUnits);
            }

            return UnitTargetHelper.GetClosestHurtUnit(movingUnit, aliveEnemyUnits);
        }

        public Unit GetNearestEnemy(Unit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return UnitTargetHelper.GetClosestUnit(movingUnit, aliveEnemyUnits);
            }

            return UnitTargetHelper.GetClosestUnit(movingUnit, alivePlayerUnits);
        }

        public void NotifyDeath(MovingUnit go)
        {
            if (go.faction == Faction.PLAYER)
            {
                alivePlayerUnits.Remove(go.GetComponent<MovingUnit>());
            }
            else
            {
                aliveEnemyUnits.Remove(go.GetComponent<MovingUnit>());
            }

            battleUIController.RefreshArmyBarsUI(alivePlayerUnits.Count, allPlayerUnitsCount,
                aliveEnemyUnits.Count, allEnemyUnitsCount);
        }

        public int NotifyNewUnitAndReturnCreditsLeft(Unit u)
        {
            alivePlayerUnits.Add(u);
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
    }
}