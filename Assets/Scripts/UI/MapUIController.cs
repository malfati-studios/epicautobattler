using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Units;
using UnityEngine;

namespace Controllers
{
    public class MapUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI footmapCreditsNumber;
        [SerializeField] private TextMeshProUGUI archerCreditsNumber;
        [SerializeField] private TextMeshProUGUI healerCreditsNumber;
        [SerializeField] private TextMeshProUGUI gold;
        [SerializeField] private GameObject emptyArmyWarning;

        [SerializeField] [NotNull] private GameObject lvl1Button;
        [SerializeField] [NotNull] private GameObject lvl2Button;
        [SerializeField] [NotNull] private GameObject lvl3Button;
        [SerializeField] [NotNull] private GameObject lvl4Button;

        private Dictionary<UnitType, int> unitCredits;
        private List<bool> wonLevels;

        public void Start()
        {
            unitCredits = GameController.instance.GetUnitCredits();
            footmapCreditsNumber.text = unitCredits[UnitType.FOOTMAN].ToString();
            archerCreditsNumber.text = unitCredits[UnitType.ARCHER].ToString();
            healerCreditsNumber.text = unitCredits[UnitType.HEALER].ToString();
            gold.text = GameController.instance.GetCurrentGold().ToString();
            emptyArmyWarning.SetActive(false);
            wonLevels = GameController.instance.GetWonLevels();
            if (wonLevels[0])
            {
                lvl1Button.transform.GetChild(1).gameObject.SetActive(true);
            }
            
            if (wonLevels[1])
            {
                lvl2Button.transform.GetChild(1).gameObject.SetActive(true);
            }
            
            if (wonLevels[2])
            {
                lvl3Button.transform.GetChild(1).gameObject.SetActive(true);
            }
            
            if (wonLevels[3])
            {
                lvl4Button.transform.GetChild(1).gameObject.SetActive(true);
            }

        }

        public void StoreButtonPressed()
        {
            AudioController.instance.PlayClickSound();
            GameController.instance.EnterStore();
        }
        
        public void QuitButtonPressed()
        {
            Application.Quit();
        }

        public void OnMapButtonPressed(int mapLvl)
        {
            if (IsArmyEmpty())
            {
                emptyArmyWarning.SetActive(true);
                Invoke("HideEmptyArmyWarning", 3f);
            }
            else
            {
                AudioController.instance.PlayClickSound();
                GameController.instance.LoadBattle(mapLvl);
            }
        }

        private void HideEmptyArmyWarning()
        {
            emptyArmyWarning.SetActive(false);
        }

        private bool IsArmyEmpty()
        {
            return unitCredits[UnitType.FOOTMAN] == 0 && unitCredits[UnitType.ARCHER] == 0 &&
                   unitCredits[UnitType.HEALER] == 0;
        }
    }
}