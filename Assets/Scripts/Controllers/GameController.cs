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

        [SerializeField] private GameObject battleLogicControllerPrefab;
        [SerializeField] private GameObject battleUIControllerPrefab;
        private BattleLogicController currentBattleLogicController;
        private BattleUIController currentBattleUiController;

        [SerializeField] private int currentLvl = 0;
        [SerializeField] private int currentCredits;
        [SerializeField] private Dictionary<UnitType, int> unitCredits;

        private bool waitingForBattleLevelLoad;

        public void StartGame()
        {
            currentCredits = startingConfiguration.startingCredits;
            unitCredits = new Dictionary<UnitType, int>();
            unitCredits.Add(UnitType.ARCHER, startingConfiguration.startingArchers);
            unitCredits.Add(UnitType.FOOTMAN, startingConfiguration.startingFootmen);
            unitCredits.Add(UnitType.HEALER, startingConfiguration.startingHealers);
            SceneController.instance.LoadSceneInstant(1);
        }

        public void OnMapButtonPressed(int mapLvl)
        {
            currentLvl = mapLvl;
            waitingForBattleLevelLoad = true;
            SceneController.instance.LoadSceneWithTransition(mapLvl + 1);
        }

        public BattleLogicController GetCurrentBattleController()
        {
            return currentBattleLogicController;
        }

        private void SetUpNewBattle()
        {
            BattleLogicController battleLogicController = Instantiate(battleLogicControllerPrefab).GetComponent<BattleLogicController>();
            BattleUIController battleUiController =
                Instantiate(battleUIControllerPrefab).GetComponent<BattleUIController>();

            currentBattleLogicController = battleLogicController;
            currentBattleUiController = battleUiController;

            battleUiController.Initialize(currentBattleLogicController, unitCredits);
            battleLogicController.Initialize(currentBattleUiController, unitCredits);
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

        private void Start()
        {
            SceneController.instance.levelLoaded += OnLevelLoaded;
        }
    }
}