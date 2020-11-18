﻿using UnityEngine;

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