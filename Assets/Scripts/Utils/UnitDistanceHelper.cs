using System.Collections.Generic;
using Units;

namespace Utils
{
    public class UnitDistanceHelper
    {
        public static MovingUnit GetClosestUnit(MovingUnit movingUnit, List<MovingUnit> units)
        {
            if (units.Count == 0) return null;

            MovingUnit closestOtherMovingUnit = null;
            float closestDistance = float.PositiveInfinity;

            foreach (var otherUnit in units)
            {
                if (AreTheSameUnit(movingUnit, otherUnit)) continue;
                
                float currentDistance = GetDistanceToUnit(movingUnit, otherUnit);
                if (currentDistance < closestDistance)
                {
                    closestOtherMovingUnit = otherUnit;
                    closestDistance = currentDistance;
                }
            }

            return closestOtherMovingUnit;
        }

        private static float GetDistanceToUnit(MovingUnit a, MovingUnit b)
        {
            return (a.transform.position - b.transform.position).magnitude;
        }

        private static bool AreTheSameUnit(MovingUnit movingUnit, MovingUnit otherMovingUnit)
        {
            return movingUnit.gameObject.GetInstanceID() == otherMovingUnit.gameObject.GetInstanceID();
        }
    }
}