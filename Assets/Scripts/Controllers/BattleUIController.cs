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
        [SerializeField] private StartBattleButton startBattleButton;
        
        [SerializeField] private GameObject footmanPrefab;
        [SerializeField] private GameObject healerPrefab;

        private BattleController battleController;

        public void Initialize(BattleController battleController)
        {
            playerBar = GameObject.FindGameObjectWithTag("PlayerBar").GetComponent<ArmyBar>();
            enemyBar = GameObject.FindGameObjectWithTag("EnemyBar").GetComponent<ArmyBar>();
            footmanButton = GameObject.FindGameObjectWithTag("FootmanButton").GetComponent<UnitButton>();
            healerButton = GameObject.FindGameObjectWithTag("HealerButton").GetComponent<UnitButton>();
            startBattleButton = GameObject.FindGameObjectWithTag("StartBattleButton").GetComponent<StartBattleButton>();
            this.battleController = battleController;
        }
        
        // ReSharper disable once InconsistentNaming
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

            startBattleButton.buttonListeners += OnStartBattleButtonClick;
        }
        
        private void OnButtonClick(Unit unit)
        {
            battleController.NotifyNewUnit(unit);
        }
        
        private void OnStartBattleButtonClick(bool boolean)
        {
            battleController.StartBattle();
        }

    }
}
