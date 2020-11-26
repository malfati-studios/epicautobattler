using JetBrains.Annotations;
using UnityEngine;

namespace Controllers
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController instance = null;
        [SerializeField] private AudioSource clickSound = null;
        [SerializeField] private AudioSource healSound = null;
        [SerializeField] private AudioSource punchFX1 = null;
        [SerializeField] private AudioSource punchFX2 = null;
        [SerializeField] private AudioSource punchFX3 = null;
        [SerializeField] private AudioSource arrowFlyFX = null;
        [SerializeField] private AudioSource arrowHitFX1 = null;
        [SerializeField] private AudioSource arrowHitFX2 = null;
        [SerializeField] private AudioSource battleMusic = null;
        [SerializeField] [NotNull] private AudioSource dieFX1 = null;
        [SerializeField] private AudioSource dieFX2 = null;
        [SerializeField] private AudioSource dieFX3 = null;


        private void Awake()
        {
            Initialize();
        }

        public void PlayClickSound()
        {
            clickSound.Play();
        }

        public void PlayHealSound()
        {
            healSound.Play();
        }

        public void PlayBattleMusic()
        {
            battleMusic.Play();
        }
        
        public void StopBattleMusic()
        {
            battleMusic.Stop();
        }
        
        public void PlayArrowFly()
        {
            arrowFlyFX.Play();
        }

        public void PlayPunchSound()
        {
            int rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    punchFX1.Play();
                    break;
                case 1:
                    punchFX2.Play();
                    break;
                case 2:
                    punchFX3.Play();
                    break;
            }
        }
        
        public void PlayDeathSound()
        {
            int rand = Random.Range(0, 5);
            switch (rand)
            {
                case 0:
                    dieFX1.Play();
                    break;
                case 1:
                    dieFX2.Play();
                    break;
                case 2:
                    dieFX3.Play();
                    break;
                case 3:
                    dieFX2.Play();
                    break;
                case 4:
                    dieFX3.Play();
                    break;
            }
        }
        
        public void PlayArrowHitSound()
        {
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    arrowHitFX1.Play();
                    break;
                case 1:
                    arrowHitFX2.Play();
                    break;
            }
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