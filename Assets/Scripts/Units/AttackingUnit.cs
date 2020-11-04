using UnityEngine;

namespace Units
{
    public class AttackingUnit : Unit
    {
        [SerializeField] public int attack;
        [SerializeField] public int attackSpeed;

        public virtual void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}