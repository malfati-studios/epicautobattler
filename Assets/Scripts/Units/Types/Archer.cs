using Data;
using UnityEngine;

namespace Units.Types
{
    public class Archer : AttackingMovingUnit
    {
        [SerializeField] private UnitStats stats;
        // Start is called before the first frame update
        public override bool IsSupportClass()
        {
            return false;
        }

        protected override void Start()
        {
            base.Start();
            InitializeStats();
           
        }

        // Update is called once per frame
        public override void PlayAttackAnimation()
        {
        }

        protected override void Update()
        {
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
        
        private void InitializeStats()
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