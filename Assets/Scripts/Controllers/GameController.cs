using System.Collections.Generic;
using Data;
using Units;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;

        [SerializeField] private StartingConfiguration startingConfiguration;
        [SerializeField] private int currentLvl = 0;
        [SerializeField] private int currentCredits;
        [SerializeField] private Dictionary<UnitType, int> unitCredits;

        public void StartGame()
        {
            currentCredits = startingConfiguration.startingCredits;
            unitCredits.Add(UnitType.Archer, startingConfiguration.startingArchers);
            unitCredits.Add(UnitType.Footman, startingConfiguration.startingFootmen);
            unitCredits.Add(UnitType.Healer, startingConfiguration.startingHealers);
            SceneController.instance.LoadScene(1);
        }

        public void StartLevel1()
        {
            SceneController.instance.LoadScene(2);
            currentLvl = 1;
        }
        
        public void StartLevel2()
        {
            SceneController.instance.LoadScene(3);
            currentLvl = 2;

        }
        
        public void StartLevel3()
        {
            SceneController.instance.LoadScene(4);
            currentLvl = 3;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}