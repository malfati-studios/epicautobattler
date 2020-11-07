using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ArmyBar : MonoBehaviour
    {
        [SerializeField] private Image fill = null;
        public void UpdateBar(int aliveUnits, int allUnits)
        {
            fill.fillAmount = (float) aliveUnits / (float) allUnits;
        }
    }
}