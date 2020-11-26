using System;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitButton : MonoBehaviour
    {
        public Action<GameObject> buttonListeners;
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
            unitImage.sprite = prefab.GetComponent<MovingUnit>().GetSprite();
            Color tmp = unitImage.color;
            tmp.a = 1f;
            unitImage.color = tmp;
        }

        public void OnUnitButtonClick()
        {
            if (buttonListeners != null)
            {
                buttonListeners.Invoke(unitPrefab);
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

        public void NotifyUnitCreated()
        {
            unitCount--;
            RefreshCount();
            if (unitCount == 0)
            {
                Deactivate();
            }
        }

        public void Deactivate()
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}