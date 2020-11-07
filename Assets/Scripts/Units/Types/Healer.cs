using Data;
using UnityEngine;

namespace Units.Types
{
    public class Healer : SupportUnit
    {
        [SerializeField] private UnitStats stats;
        public override void PlayDeathAnimation()
        {
            
        }

        public override void PlayDamageAnimation()
        {
        }

        public override bool IsSupportClass()
        {
            return true;
        }

        public override void PlaySupportAnimation()
        {
            
        }
        
        public override void Start()
        {
            base.Start();
            InitializeStats();
        }
        
        private void InitializeStats()
        {
            HP = stats.HP;
            speed = stats.speed;
            stopDistance = stats.stopDistance;
        }
    }
}