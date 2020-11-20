using Controllers;
using Data;
using UnityEngine;

namespace Units.Types
{
    public class Archer : AttackingMovingUnit
    {
        [SerializeField] private UnitStats stats;
        [SerializeField] private GameObject arrow;

        // Start is called before the first frame update
        public override bool IsSupportClass()
        {
            return false;
        }

        // Update is called once per frame
        public override void PlayAttackAnimation()
        {
            animator.enabled = false;
            GameObject arrowGo = Instantiate(arrow, transform.position, Quaternion.identity);
            arrowGo.GetComponent<Arrow>().Initialize(target, this);
            AudioController.instance.PlayArrowFly();
        }

        public override void PlayDeathAnimation()
        {
            animator.enabled = false;
            StopMoving();
        }

        public override void PlayDamageAnimation()
        {
            
        }

        public void NotifyHitTarget()
        {
            DamageCallback();
            AudioController.instance.PlayArrowHitSound();
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