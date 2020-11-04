using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;

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

        public enum Faction
        {
            PLAYER,
            ENEMY
        }
    }
}