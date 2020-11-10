using Data;
using UnityEngine;

namespace Units.Types
{
    public class Footman : AttackingUnit
    {
        [SerializeField] private UnitStats stats;

        private Vector2Lerper vector2Lerper;
        private bool playingAttackAnimation;
        private Vector2 initialAttackPosition;

        public override void PlayAttackAnimation()
        {
            playingAttackAnimation = true;
            initialAttackPosition = transform.position;
            vector2Lerper = new Vector2Lerper(initialAttackPosition,
                initialAttackPosition - GetAttackingDirection() / 3, 0.3f);
            vector2Lerper.SetValues(initialAttackPosition, initialAttackPosition - GetAttackingDirection() / 2, true);
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
            transform.position = vector2Lerper.CurrentValue;
            if (vector2Lerper.Reached)
            {
                transform.position = initialAttackPosition;
                playingAttackAnimation = false;
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