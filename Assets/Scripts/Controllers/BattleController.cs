using System.Collections.Generic;
using UI;
using Units;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController instance;

        [SerializeField] private int allEnemyUnitsCount;
        [SerializeField] private int allPlayerUnitsCount;

        [SerializeField] private List<Unit> alivePlayerUnits = new List<Unit>();
        [SerializeField] private List<Unit> aliveEnemyUnits = new List<Unit>();

        [SerializeField] private ArmyBar playerBar;
        [SerializeField] private ArmyBar enemyBar;

        [SerializeField] private Dictionary<UnitType, int> currentUnitCredits;

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

            RefreshArmyBarsUI();
        }

        public void NotifyNewUnit(Unit u)
        {
            if (u.faction == Faction.PLAYER)
            {
                alivePlayerUnits.Add(u);
                allPlayerUnitsCount++;
            }
            else
            {
                aliveEnemyUnits.Add(u);
                allEnemyUnitsCount++;
            }

            RefreshArmyBarsUI();
        }

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            ScanUnits();
        }

        private void RefreshArmyBarsUI()
        {
            playerBar.UpdateBar(alivePlayerUnits.Count, allPlayerUnitsCount);
            enemyBar.UpdateBar(aliveEnemyUnits.Count, allEnemyUnitsCount);
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

        private void Initialize()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}