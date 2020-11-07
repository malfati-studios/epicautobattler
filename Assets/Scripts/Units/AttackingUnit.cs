using System;
using UnityEngine;

namespace Units
{
    public abstract class AttackingUnit : Unit
    {
        [SerializeField] public int attack;
        [SerializeField] public int millisBetweenAttacks;

        private DateTime lastAttackTime = DateTime.Now;

        public abstract void PlayAttackAnimation(Vector2 attackingDirection);

        public virtual void Attack()
        {
            lastAttackTime = DateTime.Now;
            PlayAttackAnimation(GetAttackingDirection());
            bool died = target.GetComponent<Damageable>().TakeDamage(attack);
            if (died)
            {
                ClearTarget();
            }
        }

        public new virtual void Update()
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

        private Vector2 GetAttackingDirection()
        {
            return (target.transform.position - transform.position).normalized;
        }
    }
}