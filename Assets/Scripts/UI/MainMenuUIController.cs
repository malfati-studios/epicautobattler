using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
   public class MainMenuUIController : MonoBehaviour
   {
      public void PlayButtonPressed()
      {
         AudioController.instance.PlayClickSound();
         GameController.instance.StartGame();
      }
      
      public void QuitButtonPressed()
      {
         Application.Quit();
      }
      
      public void CreditsButtonPressed()
      {
        SceneController.instance.LoadCreditsScene();
      }
   }
}
