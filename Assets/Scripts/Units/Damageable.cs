using UnityEngine;

namespace Units
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] public int HP;

        public bool TakeDamage(int damage)
        {
            HP -= damage;
            Invoke("Die", 1f);
            return HP < 1;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}