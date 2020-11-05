using System;
using UnityEngine;

namespace Units.Types
{
    public class Footman : AttackingUnit
    {

        private Animator animator;
        private static readonly int MovingAxis = Animator.StringToHash("MovingAxis");

        public override void PlayAttackAnimation()
        {
        }

        public override bool IsSupportClass()
        {
            return false;
        }

        public override void PlayMovingAnimation()
        {
            animator.SetInteger(MovingAxis, GetMovingAxis());
        }

        public override void StopMovingAnimation()
        {
        }

        public void Start()
        {
            animator = GetComponent<Animator>();
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
            throw new System.NotImplementedException();
        }
    }
}