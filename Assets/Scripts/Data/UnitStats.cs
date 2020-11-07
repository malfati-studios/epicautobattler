using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="unitStats", menuName = "Unit Stats", order = 0)]
    public class UnitStats : ScriptableObject
    {
        public int HP;
        public float speed;
        public float stopDistance;
        public int attack;
        public float attackRange;
        public int millisBetweenAttacks;
        public int supportStrength;
        public int millisBetweenSupports;
    }
}