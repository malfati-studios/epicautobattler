using UnityEngine;

namespace Units
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] public int HP;

        public bool TakeDamage(int damage)
        {
            HP -= damage;
            return HP < 1;
        }
    }
}