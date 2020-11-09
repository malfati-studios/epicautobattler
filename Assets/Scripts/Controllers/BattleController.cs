using System.Collections.Generic;
using Units;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private int allEnemyUnitsCount;
        [SerializeField] private int allPlayerUnitsCount;

        [SerializeField] private List<Unit> alivePlayerUnits = new List<Unit>();
        [SerializeField] private List<Unit> aliveEnemyUnits = new List<Unit>();

        [SerializeField] private Dictionary<UnitType, int> currentUnitCredits;

        private BattleUIController battleUIController;

        public void Initialize(BattleUIController battleUIController)
        {
            currentUnitCredits = new Dictionary<UnitType, int>();
            currentUnitCredits[UnitType.Footman] = 10;
            currentUnitCredits[UnitType.Healer] = 10;
            this.battleUIController = battleUIController;
            this.battleUIController.ConfigureButtons(currentUnitCredits);
            ScanUnits();
            StartBattle();
        }

        public Unit GetNearestAlly(Unit unit)
        {
            if (unit.faction == Faction.PLAYER)
            {
                return UnitDistanceHelper.GetClosestUnit(unit, alivePlayerUnits);
            }

            return UnitDistanceHelper.GetClosestUnit(unit, aliveEnemyUnits);
        }

        public Unit GetNearestEnemy(Unit unit)
        {
            if (unit.faction == Faction.PLAYER)
            {
                return UnitDistanceHelper.GetClosestUnit(unit, aliveEnemyUnits);
            }

            return UnitDistanceHelper.GetClosestUnit(unit, alivePlayerUnits);
        }

        public void NotifyDeath(Unit go)
        {
            if (go.faction == Faction.PLAYER)
            {
                alivePlayerUnits.Remove(go.GetComponent<Unit>());
            }
            else
            {
                aliveEnemyUnits.Remove(go.GetComponent<Unit>());
            }

            battleUIController.RefreshArmyBarsUI(alivePlayerUnits.Count, allPlayerUnitsCount,
                aliveEnemyUnits.Count, allEnemyUnitsCount);
        }

        public void NotifyNewUnit(Unit u)
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
                Unit u = goUnit.GetComponent<Unit>();
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