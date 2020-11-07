using System.Collections.Generic;
using UI;
using Units;
using UnityEngine;

namespace Controllers
{
    public class LevelUIController : MonoBehaviour
    {
        public static LevelUIController instance;
        
        [SerializeField] private ArmyBar playerBar;
        [SerializeField] private ArmyBar enemyBar;
        [SerializeField] private UnitButton footmanButton;
        [SerializeField] private UnitButton healerButton;
        
        [SerializeField] private GameObject footmanPrefab;
        [SerializeField] private GameObject healerPrefab;
        
        public void RefreshArmyBarsUI(int alivePlayerUnitsCount, int allPlayerUnitsCount, int aliveEnemyUnitsCount, int allEnemyUnitsCount)
        {
            playerBar.UpdateBar(alivePlayerUnitsCount, allPlayerUnitsCount);
            enemyBar.UpdateBar( aliveEnemyUnitsCount, allEnemyUnitsCount);
        }
        
        public void ConfigureButtons(Dictionary<UnitType, int> currentUnitCredits)
        {
            footmanButton.buttonListeners += OnButtonClick;
            footmanButton.SetUnitCount(currentUnitCredits[UnitType.Footman]);
            footmanButton.SetUnitPrefab(footmanPrefab);

            healerButton.buttonListeners += OnButtonClick;
            healerButton.SetUnitCount(currentUnitCredits[UnitType.Healer]);
            healerButton.SetUnitPrefab(healerPrefab);
        }
        
        private void OnButtonClick(Unit unit)
        {
            BattleController.instance.NotifyNewUnit(unit);
        }

        private void Start()
        {
            playerBar = GameObject.FindGameObjectWithTag("PlayerBar").GetComponent<ArmyBar>();
            enemyBar = GameObject.FindGameObjectWithTag("EnemyBar").GetComponent<ArmyBar>();
            footmanButton = GameObject.FindGameObjectWithTag("FootmanButton").GetComponent<UnitButton>();
            healerButton = GameObject.FindGameObjectWithTag("HealerButton").GetComponent<UnitButton>();
        }

        private void Awake()
        {
            Initialize();
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
