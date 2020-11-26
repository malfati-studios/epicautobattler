using System.Collections.Generic;
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

        private Dictionary<UnitType, int> unitCredits;

        public void Start()
        {
            unitCredits = GameController.instance.GetUnitCredits();
            footmapCreditsNumber.text = unitCredits[UnitType.FOOTMAN].ToString();
            archerCreditsNumber.text = unitCredits[UnitType.ARCHER].ToString();
            healerCreditsNumber.text = unitCredits[UnitType.HEALER].ToString();
            gold.text = GameController.instance.GetCurrentGold().ToString();
            emptyArmyWarning.SetActive(false);
        }

        public void StoreButtonPressed()
        {
            AudioController.instance.PlayClickSound();
            GameController.instance.EnterStore();
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