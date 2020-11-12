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

        [SerializeField] private List<MovingUnit> alivePlayerUnits = new List<MovingUnit>();
        [SerializeField] private List<MovingUnit> aliveEnemyUnits = new List<MovingUnit>();

        [SerializeField] private Dictionary<UnitType, int> currentUnitCredits;

        private BattleUIController battleUIController;

        public void Initialize(BattleUIController battleUIController, Dictionary<UnitType, int> unitCredits)
        {
            currentUnitCredits = new Dictionary<UnitType, int>();
            currentUnitCredits[UnitType.Footman] = unitCredits[UnitType.Footman];
            currentUnitCredits[UnitType.Healer] = unitCredits[UnitType.Healer];
            this.battleUIController = battleUIController;
            ScanUnits();
        }

        public MovingUnit GetNearestAlly(MovingUnit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return UnitDistanceHelper.GetClosestUnit(movingUnit, alivePlayerUnits);
            }

            return UnitDistanceHelper.GetClosestUnit(movingUnit, aliveEnemyUnits);
        }

        public MovingUnit GetNearestEnemy(MovingUnit movingUnit)
        {
            if (movingUnit.faction == Faction.PLAYER)
            {
                return UnitDistanceHelper.GetClosestUnit(movingUnit, aliveEnemyUnits);
            }

            return UnitDistanceHelper.GetClosestUnit(movingUnit, alivePlayerUnits);
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

        public void NotifyNewUnit(MovingUnit u)
        {
            alivePlayerUnits.Add(u);
            allPlayerUnitsCount++;
            currentUnitCredits[u.type] = currentUnitCredits[u.type] - 1;

            battleUIController.RefreshArmyBarsUI(alivePlayerUnits.Count, allPlayerUnitsCount,
                aliveEnemyUnits.Count, allEnemyUnitsCount);
        }

        public void StartBattle()
        {
            foreach (var aliveEnemyUnit in aliveEnemyUnits)
            {
                aliveEnemyUnit.SetBattleController(this);
                aliveEnemyUnit.SearchForTarget();
            }

            foreach (var alivePlayerUnit in alivePlayerUnits)
            {
                alivePlayerUnit.SetBattleController(this);
                alivePlayerUnit.SearchForTarget();
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