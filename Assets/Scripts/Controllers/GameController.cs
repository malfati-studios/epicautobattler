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
        
        //Level Configs
        [SerializeField] private List<LevelConfiguration> levelConfigurations;

        // Currency and credits
        [SerializeField] private int currentLvl = 0;
        [SerializeField] private int currentGold;
        [SerializeField] private Dictionary<UnitType, int> unitCredits;

        // Battle controllers
        [SerializeField] private GameObject battleLogicControllerPrefab;
        [SerializeField] private GameObject battleUIControllerPrefab;
        private BattleLogicController currentBattleLogicController;
        private BattleUIController currentBattleUiController;

        private bool waitingForBattleLevelLoad;

        public void StartGame()
        {
            currentGold = startingConfiguration.startingGold;
            unitCredits = new Dictionary<UnitType, int>();
            unitCredits.Add(UnitType.ARCHER, startingConfiguration.startingArchers);
            unitCredits.Add(UnitType.FOOTMAN, startingConfiguration.startingFootmen);
            unitCredits.Add(UnitType.HEALER, startingConfiguration.startingHealers);
            SceneController.instance.LoadMapMenu();
        }


        public void LoadBattle(int mapLvl)
        {
            currentLvl = mapLvl;
            waitingForBattleLevelLoad = true;
            SceneController.instance.LoadLvlScene(mapLvl);
        }

        public void OnStoreButtonPressed()
        {
            SceneController.instance.LoadStoreScene();
        }

        public BattleLogicController GetCurrentBattleController()
        {
            return currentBattleLogicController;
        }

        public void NotifyLevelWon()
        {
            currentGold += levelConfigurations[currentLvl - 1].goldReward;
            SceneController.instance.LoadMapScreenWithDelay();
        }

        public void NotifyLevelLost()
        {
            ClearGameProgress();
            AudioController.instance.StopBattleMusic();
            SceneController.instance.RestartGame();
        }

        public Dictionary<UnitType, int> GetUnitCredits()
        {
            return unitCredits;
        }

        public int GetCurrentGold()
        {
            return currentGold;
        }

        public void FinishedShopping(Dictionary<UnitType, int> newUnitCredits, int newGold)
        {
            unitCredits = newUnitCredits;
            currentGold = newGold;
            SceneController.instance.LoadMapMenu();
        }

        private void SetUpNewBattle()
        {
            BattleLogicController battleLogicController =
                Instantiate(battleLogicControllerPrefab).GetComponent<BattleLogicController>();
            BattleUIController battleUiController =
                Instantiate(battleUIControllerPrefab).GetComponent<BattleUIController>();

            currentBattleLogicController = battleLogicController;
            currentBattleUiController = battleUiController;

            battleUiController.Initialize(currentBattleLogicController, unitCredits, levelConfigurations[currentLvl-1]);
            battleLogicController.Initialize(currentBattleUiController, unitCredits);
            waitingForBattleLevelLoad = false;
        }


        private void OnLevelLoaded(string lvlName)
        {
            if (waitingForBattleLevelLoad)
            {
                SetUpNewBattle();
            }

            if (lvlName == SceneController.MAIN_MENU_SCENE)
            {
                AudioController.instance.PlayBattleMusic();
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
            AudioController.instance.PlayBattleMusic();
        }

        public void EnterStore()
        {
            SceneController.instance.LoadStoreScene();
        }

        private void ClearGameProgress()
        {
            currentLvl = 0;
            currentGold = 0;
            unitCredits = null;
            currentBattleLogicController = null;
            currentBattleUiController = null;
            waitingForBattleLevelLoad = false;
        }
    }
}