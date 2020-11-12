using Data;
using UnityEngine;

namespace Units.Types
{
    public class Footman : AttackingMovingUnit
    {
        [SerializeField] private UnitStats stats;

        private Vector2Lerper vector2Lerper;
        private bool playingAttackAnimation;

        public override void PlayAttackAnimation()
        {
            animator.enabled = false;
            playingAttackAnimation = true;
            vector2Lerper = new Vector2Lerper(new Vector2(0f, 0f),
                -GetAttackingDirection() / 5, 0.3f);
            vector2Lerper.SetValues(new Vector2(0f, 0f),
                -GetAttackingDirection() / 5, true);
        }

        public override bool IsSupportClass()
        {
            return false;
        }

        protected override void Update()
        {
            if (playingAttackAnimation)
            {
                UpdateAttackAnimation();
            }

            base.Update();
        }


        public override void Attack()
        {
            base.Attack();
        }

        public override void PlayDeathAnimation()
        {
        }

        public override void PlayDamageAnimation()
        {
        }

        protected override void Start()
        {
            base.Start();
            InitializeStats();
        }

        private void UpdateAttackAnimation()
        {
            vector2Lerper.Update();
            transform.GetChild(0).localPosition = vector2Lerper.CurrentValue;
            if (vector2Lerper.Reached)
            {
                transform.GetChild(0).localPosition = new Vector2(0, 0);
                playingAttackAnimation = false;
                animator.enabled = true;
            }
        }

        private void InitializeStats()
        {
            HP = stats.HP;
            speed = stats.speed;
            stopDistance = stats.stopDistance;
            attack = stats.attack;
            millisBetweenAttacks = stats.millisBetweenAttacks;
        }
    }
}