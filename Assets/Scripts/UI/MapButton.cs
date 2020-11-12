using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    [SerializeField] private int mapLvl;

    private void Awake()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mapLvl.ToString();
    }

    public void OnButtonClick()
    {
        GameController.instance.OnMapButtonPressed(mapLvl);
        AudioController.instance.PlayClickSound();
    }
}
