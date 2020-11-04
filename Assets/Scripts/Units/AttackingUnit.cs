using UnityEngine;

namespace Units
{
    public abstract  class AttackingUnit : Unit
    {
        [SerializeField] public int attack;
        [SerializeField] public int attackSpeed;

        public abstract void PlayAttackAnimation();
        
        public virtual void Attack(Damageable damageable)
        {
            PlayAttackAnimation();
            damageable.TakeDamage(attack);
        }
        
        public virtual void Update()
        {
            base.Update();
            if (InRange())
            {
                Invoke("Attack",attackSpeed);
            }
        }
       
    }
}