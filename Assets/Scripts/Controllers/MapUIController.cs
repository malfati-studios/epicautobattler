using Controllers;
using TMPro;
using Units;
using UnityEngine;

public class MapUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI footmapCreditsNumber;
    [SerializeField] private TextMeshProUGUI archerCreditsNumber;
    [SerializeField] private TextMeshProUGUI healerCreditsNumber;
    [SerializeField] private TextMeshProUGUI gold;


    public void Start()
    {
        var unitCredits = GameController.instance.GetUnitCredits();
        footmapCreditsNumber.text = unitCredits[UnitType.FOOTMAN].ToString();
        archerCreditsNumber.text = unitCredits[UnitType.ARCHER].ToString();
        gold.text = GameController.instance.GetCurrentGold().ToString();
    }
}