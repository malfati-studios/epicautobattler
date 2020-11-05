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