using UnityEngine;

namespace Controllers
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController instance = null;
        [SerializeField] private AudioSource clickSound = null;

        private void Awake()
        {
            Initialize();
        }

        public void PlayClickSound()
        {
            clickSound.Play();
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