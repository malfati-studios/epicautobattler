using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmyBar : MonoBehaviour
{
    private int currHp = 0;
    private int maxHp = 0;
    [SerializeField] private Image fill = null;
    [SerializeField] private TextMeshProUGUI txtHealth = null;
    public void UpdateHP(int hp)
    {
        currHp = hp;
    }

    public void ConfigureBar(int hp, int maxHp)
    {
        this.maxHp = maxHp;
        currHp = hp;
        UpdateHP(hp);
    }
}