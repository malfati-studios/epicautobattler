﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitButton : MonoBehaviour
    {
        public Action<GameObject> buttonListeners;
        [SerializeField] private GameObject unitPrefab;

        private Image unitImage;
        private TextMeshProUGUI unitCountTxt;
        private int unitCount;
    
    
        public void SetUnitCount(int count)
        {
            unitCount = count;
            RefreshCount();
        }

        public void SetUnitPrefab(GameObject prefab)
        {
            unitPrefab = prefab;
            unitImage.sprite = prefab.GetComponent<Sprite>();
        }

        public void OnUnitButtonClick()
        {
            if (buttonListeners != null)
            {
                buttonListeners.Invoke(unitPrefab);
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
