using System.Collections.Generic;
using Data;
using TMPro;
using UI;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    // ReSharper disable once InconsistentNaming
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField] private ArmyBar playerBar;
        [SerializeField] private ArmyBar enemyBar;
        [SerializeField] private UnitButton footmanButton;
        [SerializeField] private UnitButton healerButton;
        [SerializeField] private UnitButton archerButton;
        [SerializeField] private StartBattleButton startBattleButton;

        [SerializeField] private GameObject footmanPrefab;
        [SerializeField] private GameObject healerPrefab;
        [SerializeField] private GameObject archerPrefab;
        [SerializeField] private GameObject unitCreatorPrefab;

        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;

        private BattleLogicController battleLogicController;
        private UnitCreator unitCreator;

        private LevelConfiguration levelConfiguration;

        public void Initialize(BattleLogicController battleLogicController, Dictionary<UnitType, int> unitCredits,
            LevelConfiguration levelConfiguration)
        {
            var unitCreatorGO = Instantiate(unitCreatorPrefab);
            unitCreator = unitCreatorGO.GetComponent<UnitCreator>();
            unitCreator.Initialize();
            unitCreator.unitCreateEvent += OnUnitCreation;

            playerBar = GameObject.FindGameObjectWithTag("PlayerBar").GetComponent<ArmyBar>();
            enemyBar = GameObject.FindGameObjectWithTag("EnemyBar").GetComponent<ArmyBar>();

            SetUpButtons(unitCredits);

            winScreen = GameObject.FindGameObjectWithTag("WinCondition");
            loseScreen = GameObject.FindGameObjectWithTag("LoseCondition");

            this.battleLogicController = battleLogicController;
            this.levelConfiguration = levelConfiguration;
        }

        private void SetUpButtons(Dictionary<UnitType, int> unitCredits)
        {
            footmanButton = GameObject.FindGameObjectWithTag("FootmanButton").GetComponent<UnitButton>();
            healerButton = GameObject.FindGameObjectWithTag("HealerButton").GetComponent<UnitButton>();
            archerButton = GameObject.FindGameObjectWithTag("ArcherButton").GetComponent<UnitButton>();
            startBattleButton = GameObject.FindGameObjectWithTag("StartBattleButton").GetComponent<StartBattleButton>();

            footmanButton.SetUnitPrefab(footmanPrefab);
            if (unitCredits[UnitType.FOOTMAN] > 0)
            {
                footmanButton.buttonListeners += OnButtonClick;
                footmanButton.SetUnitCount(unitCredits[UnitType.FOOTMAN]);
            }
            else
            {
                footmanButton.Deactivate();
            }

            healerButton.SetUnitPrefab(healerPrefab);
            if (unitCredits[UnitType.HEALER] > 0)
            {
                healerButton.buttonListeners += OnButtonClick;
                healerButton.SetUnitCount(unitCredits[UnitType.HEALER]);
            }
            else
            {
                healerButton.Deactivate();
            }

            archerButton.SetUnitPrefab(archerPrefab);
            if (unitCredits[UnitType.ARCHER] > 0)
            {
                archerButton.buttonListeners += OnButtonClick;
                archerButton.SetUnitCount(unitCredits[UnitType.ARCHER]);
            }
            else
            {
                archerButton.Deactivate();
            }

            startBattleButton.buttonListeners += OnStartBattleButtonClick;
        }

        public void RefreshArmyBarsUI(int alivePlayerUnitsCount, int allPlayerUnitsCount, int aliveEnemyUnitsCount,
            int allEnemyUnitsCount)
        {
            playerBar.UpdateBar(alivePlayerUnitsCount, allPlayerUnitsCount);
            enemyBar.UpdateBar(aliveEnemyUnitsCount, allEnemyUnitsCount);
        }

        public void ShowWinScreen()
        {
            winScreen.transform.GetChild(0).gameObject.SetActive(true);
            winScreen.transform.GetChild(1).gameObject.SetActive(true);
            winScreen.transform.GetChild(2).gameObject.SetActive(true);
            winScreen.transform.GetChild(3).gameObject.SetActive(true);

            winScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = string.Format("You earned {0} gold", levelConfiguration.goldReward);
        }

        public void ShowLoseScreen()
        {
            loseScreen.transform.GetChild(0).gameObject.SetActive(true);
            loseScreen.transform.GetChild(1).gameObject.SetActive(true);
        }

        private void OnUnitCreation(Unit unit)
        {
            int creditsLeft = battleLogicController.NotifyNewUnitAndReturnCreditsLeft(unit);
            switch (unit.type)
            {
                case UnitType.FOOTMAN:
                    footmanButton.NotifyUnitCreated();
                    break;
                case UnitType.HEALER:
                    healerButton.NotifyUnitCreated();
                    break;
                case UnitType.ARCHER:
                    archerButton.NotifyUnitCreated();
                    break;
            }

            if (creditsLeft == 0)
            {
                unitCreator.DisableCreation();
            }
        }

        // ReSharper disable once InconsistentNaming
        private void OnButtonClick(GameObject unitSelected)
        {
            unitCreator.ButtonClicked(unitSelected);
        }

        private void OnStartBattleButtonClick(bool boolean)
        {
            battleLogicController.StartBattle();
        }
    }
}