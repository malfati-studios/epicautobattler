using System.Collections.Generic;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Controllers
{
    public class StoreUIController : MonoBehaviour
    {
        [SerializeField] private int footmanGoldCost;
        [SerializeField] private int archerGoldCost;
        [SerializeField] private int healerGoldCost;
        
        [SerializeField] private TextMeshProUGUI footmanCreditsNumber;
        [SerializeField] private TextMeshProUGUI archerCreditsNumber;
        [SerializeField] private TextMeshProUGUI healerCreditsNumber;
        [SerializeField] private TextMeshProUGUI gold;
        
        [SerializeField] private GameObject footmanButton;
        [SerializeField] private GameObject archerButton;
        [SerializeField] private GameObject healerButton;
        
        private Dictionary<UnitType, int> currentUnitCredits;
        private int currentGold;

        public void FootmanButtonPressed()
        {
            AudioController.instance.PlayClickSound();
            if (footmanGoldCost <= currentGold)
            {
                currentUnitCredits[UnitType.FOOTMAN] = currentUnitCredits[UnitType.FOOTMAN] + 1;
                currentGold -= footmanGoldCost;
                RefreshUI();
            }
        }

        public void ArcherButtonPressed()
        {
            AudioController.instance.PlayClickSound();
            if (archerGoldCost <= currentGold)
            {
                currentUnitCredits[UnitType.ARCHER] = currentUnitCredits[UnitType.ARCHER] + 1;
                currentGold -= archerGoldCost;
                RefreshUI();
            }
        }

        public void HealerButtonPressed()
        {
            AudioController.instance.PlayClickSound();
            if (healerGoldCost <= currentGold)
            {
                currentUnitCredits[UnitType.HEALER] = currentUnitCredits[UnitType.HEALER] + 1;
                currentGold -= healerGoldCost;
                RefreshUI();
            }
        }

        public void BackToMenuButtonPressed()
        {
            AudioController.instance.PlayClickSound();
            GameController.instance.FinishedShopping(currentUnitCredits, currentGold);
        }
        
        private void RefreshUI()
        {
            footmanCreditsNumber.text = currentUnitCredits[UnitType.FOOTMAN].ToString();
            archerCreditsNumber.text = currentUnitCredits[UnitType.ARCHER].ToString();
            healerCreditsNumber.text = currentUnitCredits[UnitType.HEALER].ToString();
            gold.text = currentGold.ToString();

            if (currentGold < footmanGoldCost)
            {
                footmanButton.GetComponent<Button>().interactable = false;
            }
            
            if (currentGold < archerGoldCost)
            {
                archerButton.GetComponent<Button>().interactable = false;
            }
            
            if (currentGold < healerGoldCost)
            {
                healerButton.GetComponent<Button>().interactable = false;
            }
        }

        private void Start()
        {
            currentUnitCredits = Cloner.DeepClone(GameController.instance.GetUnitCredits());
            currentGold = GameController.instance.GetCurrentGold();
            RefreshUI();
        }
    }
}