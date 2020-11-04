using UnityEngine;

namespace Units
{
    public abstract  class AttackingUnit : Unit
    {
        [SerializeField] public int attack;
        [SerializeField] public int attackSpeed;

        public abstract void PlayAttackAnimation();
        
        public override void UpdateOverride()
        {
            Debug.Log(InRange());
            if (InRange())
            {
                Debug.Log("ATTACKING!");
                Invoke("Attack",attackSpeed);
            }
        }
        public virtual void Attack(Damageable damageable)
        {
            PlayAttackAnimation();
            damageable.TakeDamage(attack);
        }
    }
}