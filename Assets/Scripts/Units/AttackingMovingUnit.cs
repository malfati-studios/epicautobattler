using System;
using UnityEngine;

namespace Units
{
    public abstract class AttackingMovingUnit : MovingUnit
    {
        [SerializeField] public int attack;
        [SerializeField] public int millisBetweenAttacks;

        private DateTime lastAttackTime = DateTime.Now;

        public abstract void PlayAttackAnimation();

        public virtual void Attack()
        {
            lastAttackTime = DateTime.Now;
            PlayAttackAnimation();
        }

        //This functions needs to be called in the point of the animation that you want to make the damage
        protected void DamageCallback()
        {
            bool died = target.GetComponent<Unit>().TakeDamage(attack);
            if (died)
            {
                ClearTarget();
            }
        }

        protected new virtual void Update()
        {
            base.Update();
            if (HasTarget() && InRange() && CheckAttackTime())
            {
                Attack();
            }
        }

        private bool CheckAttackTime()
        {
            return DateTime.Now > lastAttackTime.AddMilliseconds(millisBetweenAttacks);
        }

        protected Vector2 GetAttackingDirection()
        {
            return (target.transform.position - transform.position).normalized;
        }
    }
}