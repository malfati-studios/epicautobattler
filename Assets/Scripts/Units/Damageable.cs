using UnityEngine;

namespace Units
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] public int HP;

        public bool TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 1)
            {
                Invoke("Die", 1f);
                return true;
            }
            return false;
        }

        private void Die()
        {
            BattleController.instance.NotifyDeath(gameObject);
            Destroy(gameObject);
        }
    }
}