using UnityEngine;

namespace Units
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] public int HP;

        public abstract void PlayDeathAnimation();
        public abstract void PlayDamageAnimation();
        
        public bool TakeDamage(int damage)
        {
            HP -= damage;
            PlayDamageAnimation();
            if (HP < 1)
            {
                Die();
                return true;
            }
            return false;
        }

        private void Die()
        {
            BattleController.instance.NotifyDeath(gameObject);
            PlayDeathAnimation();
            Invoke("DestroyInstance", 1f);
        }

        private void DestroyInstance()
        {
            Destroy(gameObject);
        }
    }
}