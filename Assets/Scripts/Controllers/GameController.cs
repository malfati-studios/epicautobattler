using System;
using System.Collections.Generic;
using Data;
using Units;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        [SerializeField] private StartingConfiguration startingConfiguration;
        [SerializeField] private int currentLvl = 0;
        [SerializeField] private int currentCredits;
        [SerializeField] private Dictionary<UnitType, int> unitCredits;
        [SerializeField] private GameObject battleControllerPrefab;
        [SerializeField] private GameObject battleUIControllerPrefab;

        private BattleController currentBattleController;
        private BattleUIController currentBattleUiController;

        private bool waitingForBattleLevelLoad;

        public void StartGame()
        {
            currentCredits = startingConfiguration.startingCredits;
            unitCredits = new Dictionary<UnitType, int>();
            unitCredits.Add(UnitType.Archer, startingConfiguration.startingArchers);
            unitCredits.Add(UnitType.Footman, startingConfiguration.startingFootmen);
            unitCredits.Add(UnitType.Healer, startingConfiguration.startingHealers);
            SceneController.instance.LoadSceneInstant(1);
        }

        public void OnMapButtonPressed(int mapLvl)
        {
            currentLvl = mapLvl;
            waitingForBattleLevelLoad = true;
            SceneController.instance.LoadSceneWithTransition(mapLvl + 1);
        }

        public BattleController GetCurrentBattleController()
        {
            return currentBattleController;
        }

        private void SetUpNewBattle()
        {
            BattleController battleController = Instantiate(battleControllerPrefab).GetComponent<BattleController>();
            BattleUIController battleUiController =
                Instantiate(battleUIControllerPrefab).GetComponent<BattleUIController>();

            currentBattleController = battleController;
            currentBattleUiController = battleUiController;

            battleUiController.Initialize(currentBattleController);
            battleController.Initialize(currentBattleUiController);
        }

        private void Start()
        {
            SceneController.instance.levelLoaded += OnLevelLoaded;
        }

        private void OnLevelLoaded(bool boolean)
        {
            if (waitingForBattleLevelLoad)
            {
                SetUpNewBattle();
            }
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