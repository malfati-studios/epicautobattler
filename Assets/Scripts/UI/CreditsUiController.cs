using Controllers;
using UnityEngine;

public class CreditsUiController : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        SceneController.instance.LoadMainMenu();
    }
}