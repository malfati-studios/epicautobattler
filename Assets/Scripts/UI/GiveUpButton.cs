using Controllers;
using UnityEngine;

public class GiveUpButton : MonoBehaviour
{
   public void OnGiveUpButtonClick()
   {
      SceneController.instance.LoadMainMenu();
   }
}
