using Controllers;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        GameController.instance.OnStoreButtonPressed();
        AudioController.instance.PlayClickSound();
    }
}
