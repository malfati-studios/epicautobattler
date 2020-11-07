using System;
using UnityEngine;

namespace Units.Types
{
    public class Footman : AttackingUnit
    {
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
            throw new System.NotImplementedException();
        }
    }
}