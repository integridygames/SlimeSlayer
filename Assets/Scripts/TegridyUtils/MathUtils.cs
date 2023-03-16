using UnityEngine;

namespace TegridyUtils
{
    public static class MathUtils
    {
        public static bool IsInCone(Vector3 origin, Vector3 direction, Vector3 target, float distance, float rangeAngle, bool inPlane = false)
        {
            if (inPlane)
            {
                target.y = origin.y;
            }

            if (Vector3.Distance(origin, target) > distance)
            {
                return false;
            }

            var directionToTarget = target - origin;

            var angle = Vector3.Angle(direction, directionToTarget);
            return angle <= rangeAngle;
        }
    }
}