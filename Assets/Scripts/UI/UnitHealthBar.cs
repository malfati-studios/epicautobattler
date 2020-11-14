using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitHealthBar : MonoBehaviour
    {
        [SerializeField] private Image fill = null;
        public void UpdateBar(int maxHP, int currentHP)
        {
            fill.fillAmount = (float) currentHP / (float) maxHP;
        }
    }
}
