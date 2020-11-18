using System;
using Controllers;
using Data;
using UnityEngine;

namespace Units.Types
{
    public class Healer : MovingUnit
    {
        [SerializeField] private UnitStats stats;

        [SerializeField] public int millisBetweenSupports;

        private DateTime lastHealTime = DateTime.Now;
        private GameObject healFX;
        private int healAmount;

        public void Heal()
        {
            lastHealTime = DateTime.Now;
            PlayHealAnimation();
            target.ReceiveHeal(healAmount);
        }

        public override void SearchForTarget()
        {
            if (!target)
            {
                target = battleLogicController.GetNearestHurtAlly(this);

                if (target != null)
                {
                    target.deathListeners += OnTargetDeath;
                }
            }
        }

        protected override void Update()
        {
            base.Update();

            if (HasTarget() && !target.IsHurt())
            {
                ClearTarget();
            }
            
            if (HasTarget() && InRange() && CheckLastHealTime())
            {
                Heal();
            }
        }

        private bool CheckLastHealTime()
        {
            return DateTime.Now > lastHealTime.AddMilliseconds(millisBetweenSupports);
        }
        
       

        protected override void Start()
        {
            base.Start();
            InitializeStats();
            healFX = transform.GetChild(1).gameObject;
        }

        public override void PlayDeathAnimation()
        {
            animator.enabled = false;
            StopMoving();
        }

        public override void PlayDamageAnimation()
        {
        }

        private void PlayHealAnimation()
        {
            GameObject FXGO = Instantiate(healFX, target.transform.position, Quaternion.identity);
            FXGO.GetComponent<ParticleSystem>().Play();
            AudioController.instance.PlayHealSound();
        }

        public override bool IsSupportClass()
        {
            return true;
        }

        public void PlaySupportAnimation()
        {
            AudioController.instance.PlayHealSound();
        }
        
        private void InitializeStats()
        {
            HP = stats.HP;
            currentHP = HP;
            speed = stats.speed;
            stopDistance = stats.stopDistance;
            healAmount = stats.supportStrength;
            millisBetweenSupports = stats.millisBetweenSupports;
        }
    }
}