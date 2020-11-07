using System;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitButton : MonoBehaviour
    {
        public Action<Unit> buttonListeners;
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private int unitCount;

        private Image unitImage;
        private TextMeshProUGUI unitCountTxt;
      
        public void SetUnitCount(int count)
        {
            unitCount = count;
            RefreshCount();
        }

        public void SetUnitPrefab(GameObject prefab)
        {
            unitPrefab = prefab;
            unitImage.sprite = prefab.transform.GetChild(0).GetComponent<Sprite>();
        }

        public void OnUnitButtonClick()
        {
            if (buttonListeners != null)
            {
                buttonListeners.Invoke(unitPrefab.GetComponent<Unit>());
            }
            unitCount--;
            RefreshCount();
            if (unitCount == 0)
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        
        private void Awake()
        {
           unitImage = gameObject.transform.GetChild(0).GetComponent<Image>();
           unitCountTxt = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        private void RefreshCount()
        {
          unitCountTxt.text = unitCount.ToString();
        }
    }
}
