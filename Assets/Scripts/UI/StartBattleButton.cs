using System;
using UnityEngine;

public class StartBattleButton : MonoBehaviour
{
    public Action<bool> buttonListeners;

    public void OnStartBattleButtonClick()
    {
        if (buttonListeners != null)
        {
            buttonListeners.Invoke(true);
        }

        gameObject.SetActive(false);
    }
}