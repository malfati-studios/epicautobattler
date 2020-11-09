using System;
using UnityEngine;

public class StartBattleButton : MonoBehaviour
{
    public Action<bool> buttonListeners;

    public void OnUnitButtonClick()
    {
        if (buttonListeners != null)
        {
            buttonListeners.Invoke(true);
        }

        gameObject.SetActive(false);
    }
}