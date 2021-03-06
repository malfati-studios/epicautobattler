﻿using System.Collections.Generic;
using Units;

namespace Utils
{
    public class UnitTargetHelper
    {
        public static Unit GetClosestUnit(Unit movingUnit, List<Unit> units)
        {
            return GetClosestUnit(movingUnit, units, false);
        }

        public static Unit GetClosestHurtUnit(Unit movingUnit, List<Unit> units)
        {
            return GetClosestUnit(movingUnit, units, true);
        }

        private static Unit GetClosestUnit(Unit movingUnit, List<Unit> units, bool searchHurtTarget = false)
        {
            if (units.Count == 0) return null;

            Unit closestOtherMovingUnit = null;
            float closestDistance = float.PositiveInfinity;

            foreach (var otherUnit in units)
            {
                if (AreTheSameUnit(movingUnit, otherUnit)) continue;
                if (searchHurtTarget && !otherUnit.IsHurt()) continue;

                float currentDistance = GetDistanceToUnit(movingUnit, otherUnit);
                if (currentDistance < closestDistance)
                {
                    closestOtherMovingUnit = otherUnit;
                    closestDistance = currentDistance;
                }
            }

            return closestOtherMovingUnit;
        }

        private static float GetDistanceToUnit(Unit a, Unit b)
        {
            return (a.transform.position - b.transform.position).magnitude;
        }

        private static bool AreTheSameUnit(Unit movingUnit, Unit otherMovingUnit)
        {
            return movingUnit.gameObject.GetInstanceID() == otherMovingUnit.gameObject.GetInstanceID();
        }

        public static bool CanSearchForTarget(Unit unit)
        {
            return unit.GetType().IsSubclassOf(typeof(MovingUnit))
                   || unit.GetType() == typeof(MovingUnit);
        }
    }
}