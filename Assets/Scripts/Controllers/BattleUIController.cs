using System.Collections.Generic;
using UI;
using Units;
using UnityEngine;

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

        private BattleLogicController battleLogicController;
        private UnitCreator unitCreator;

        public void Initialize(BattleLogicController battleLogicController, Dictionary<UnitType, int> unitCredits)
        {
            var unitCreatorGO = Instantiate(unitCreatorPrefab);
            unitCreator = unitCreatorGO.GetComponent<UnitCreator>();
            unitCreator.Initialize();
            unitCreator.unitCreateEvent += OnUnitCreation;

            playerBar = GameObject.FindGameObjectWithTag("PlayerBar").GetComponent<ArmyBar>();
            enemyBar = GameObject.FindGameObjectWithTag("EnemyBar").GetComponent<ArmyBar>();
            footmanButton = GameObject.FindGameObjectWithTag("FootmanButton").GetComponent<UnitButton>();
            healerButton = GameObject.FindGameObjectWithTag("HealerButton").GetComponent<UnitButton>();
            archerButton = GameObject.FindGameObjectWithTag("ArcherButton").GetComponent<UnitButton>();
            startBattleButton = GameObject.FindGameObjectWithTag("StartBattleButton").GetComponent<StartBattleButton>();

            footmanButton.buttonListeners += OnButtonClick;
            footmanButton.SetUnitCount(unitCredits[UnitType.FOOTMAN]);
            footmanButton.SetUnitPrefab(footmanPrefab);

            healerButton.buttonListeners += OnButtonClick;
            healerButton.SetUnitCount(unitCredits[UnitType.HEALER]);
            healerButton.SetUnitPrefab(healerPrefab);
            
            archerButton.buttonListeners += OnButtonClick;
            archerButton.SetUnitCount(unitCredits[UnitType.ARCHER]);
            archerButton.SetUnitPrefab(archerPrefab);

            startBattleButton.buttonListeners += OnStartBattleButtonClick;

            this.battleLogicController = battleLogicController;
        }

        public void RefreshArmyBarsUI(int alivePlayerUnitsCount, int allPlayerUnitsCount, int aliveEnemyUnitsCount,
            int allEnemyUnitsCount)
        {
            playerBar.UpdateBar(alivePlayerUnitsCount, allPlayerUnitsCount);
            enemyBar.UpdateBar(aliveEnemyUnitsCount, allEnemyUnitsCount);
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