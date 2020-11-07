using System.Collections.Generic;
using Units;

namespace Utils
{
    public class UnitDistanceHelper
    {
        public static Unit GetClosestUnit(Unit unit, List<Unit> units)
        {
            if (units.Count == 0) return null;

            Unit closestOtherUnit = null;
            float closestDistance = float.PositiveInfinity;

            foreach (var otherUnit in units)
            {
                if (AreTheSameUnit(unit, otherUnit)) continue;
                
                float currentDistance = GetDistanceToUnit(unit, otherUnit);
                if (currentDistance < closestDistance)
                {
                    closestOtherUnit = otherUnit;
                    closestDistance = currentDistance;
                }
            }

            return closestOtherUnit;
        }

        private static float GetDistanceToUnit(Unit a, Unit b)
        {
            return (a.transform.position - b.transform.position).magnitude;
        }

        private static bool AreTheSameUnit(Unit unit, Unit otherUnit)
        {
            return unit.gameObject.GetInstanceID() == otherUnit.gameObject.GetInstanceID();
        }
    }
}