using System;
using Data;
using UnityEngine;

namespace Units.Types
{
    public class Footman : AttackingUnit
    {
        [SerializeField] private UnitStats stats;

        public override void PlayAttackAnimation(Vector2 attackingDirection)
        {
        }

        public override bool IsSupportClass()
        {
            return false;
        }

        public override void Update()
        {
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

        public override void Start()
        {
            base.Start();
            InitializeStats();
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