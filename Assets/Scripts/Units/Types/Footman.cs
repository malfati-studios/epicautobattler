using Controllers;
using Data;
using UnityEngine;

namespace Units.Types
{
    public class Footman : AttackingMovingUnit
    {
        [SerializeField] private UnitStats stats;

        private Vector2Lerper backwardsVector2Lerper;
        private Vector2Lerper forwardVector2Lerper;
        private AttackAnimationState attackAnimationState = AttackAnimationState.NOT_PLAYING;

        private enum AttackAnimationState
        {
            NOT_PLAYING,
            GOING_BACKWARDS,
            GOING_FORWARD
        }

        public override void PlayAttackAnimation()
        {
            animator.enabled = false;
            attackAnimationState = AttackAnimationState.GOING_BACKWARDS;
            backwardsVector2Lerper = new Vector2Lerper(new Vector2(0f, 0f),
                -GetAttackingDirection() / 8, 0.3f);
            backwardsVector2Lerper.SetValues(new Vector2(0f, 0f),
                -GetAttackingDirection() / 8, true);
    }

        public override bool IsSupportClass()
        {
            return false;
        }

        protected override void Update()
        {
            if (attackAnimationState != AttackAnimationState.NOT_PLAYING)
            {
                if (target)
                {
                    UpdateAttackAnimation();
                }
                else
                {
                    ResetAttackPosition();
                    attackAnimationState = AttackAnimationState.NOT_PLAYING;
                }
            }

            base.Update();
        }

        public override void PlayDeathAnimation()
        {
            animator.enabled = false;
            StopMoving();
        }

        public override void PlayDamageAnimation()
        {
        }

        private void UpdateAttackAnimation()
        {
            if (attackAnimationState == AttackAnimationState.GOING_BACKWARDS)
            {
                backwardsVector2Lerper.Update();
                transform.GetChild(0).localPosition = backwardsVector2Lerper.CurrentValue;
                if (backwardsVector2Lerper.Reached)
                {
                    attackAnimationState = AttackAnimationState.GOING_FORWARD;
                    forwardVector2Lerper = new Vector2Lerper(transform.GetChild(0).localPosition, target.transform.position - transform.position, 0.05f);
                    forwardVector2Lerper.SetValues(transform.GetChild(0).localPosition, target.transform.position - transform.position, true);
                }
            }
            else
            {
                forwardVector2Lerper.Update();
                transform.GetChild(0).localPosition = forwardVector2Lerper.CurrentValue;
                if (forwardVector2Lerper.Reached)
                {
                    attackAnimationState = AttackAnimationState.NOT_PLAYING;
                    DamageCallback();
                    AudioController.instance.PlayPunchSound();
                    //The amount of time we want the footman to be on the enemy
                    Invoke("ResetAttackPosition", 0.3f);
                }
            }
        }

        public void ResetAttackPosition()
        {
            transform.GetChild(0).localPosition = new Vector2(0, 0);
            animator.enabled = true;
        }

        protected override void InitializeStats()
        {
            HP = stats.HP;
            currentHP = HP;
            speed = stats.speed;
            stopDistance = stats.stopDistance;
            attack = stats.attack;
            millisBetweenAttacks = stats.millisBetweenAttacks;
        }
       
    }
}