using UnityEngine;

namespace Units.Types
{
    public class Footman : AttackingUnit
    {
        public override void PlayAttackAnimation()
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Attack(Damageable damageable)
        {
            base.Attack(damageable);
        }
    }
}