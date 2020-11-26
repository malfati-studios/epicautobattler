using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="startingConfiguration", menuName = "Starting Configuration", order = 0)]
    public class StartingConfiguration : ScriptableObject
    {
        public int startingGold;
        public int startingFootmen;
        public int startingHealers;
        public int startingArchers;
    }
}
