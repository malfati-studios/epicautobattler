using Controllers;
using UnityEngine;

namespace UI
{
   public class MainMenuUIController : MonoBehaviour
   {
      public void PlayButtonPressed()
      {
         AudioController.instance.PlayClickSound();
         GameController.instance.StartGame();
      }
   }
}
