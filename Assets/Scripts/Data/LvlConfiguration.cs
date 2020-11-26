using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="levelConfiguration", menuName = "Level Configuration", order = 0)]
    public class LevelConfiguration : ScriptableObject
    {
        public int lvlNumber;
        public int goldReward;
    }
}