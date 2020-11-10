using System;
using UnityEngine;

namespace Units
{
    public abstract class AttackingUnit : Unit
    {
        [SerializeField] public int attack;
        [SerializeField] public int millisBetweenAttacks;

        private DateTime lastAttackTime = DateTime.Now;

        public abstract void PlayAttackAnimation();

        public virtual void Attack()
        {
            lastAttackTime = DateTime.Now;
            PlayAttackAnimation();
            bool died = target.GetComponent<GameEntity>().TakeDamage(attack);
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