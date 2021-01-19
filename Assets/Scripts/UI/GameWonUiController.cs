using Controllers;
using UnityEngine;

namespace UI
{
    public class GameWonUiController : MonoBehaviour
    {
        public void OnQuitButtonPressed()
        {
            Application.Quit();
        }
    
        public void OnPlayAgainButtonPressed()
        {
            SceneController.instance.LoadMainMenu();
        }
    }
}