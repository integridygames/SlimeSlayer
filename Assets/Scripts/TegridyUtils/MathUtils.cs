using UnityEngine;

namespace TegridyUtils
{
    public static class MathUtils
    {
        public static bool IsInCone(Vector3 origin, Vector3 direction, Vector3 target, float distance, float rangeAngle,
            bool inPlane = false)
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

        /// <summary>
        /// https://discussions.unity.com/t/capsule-calculations/177989/3
        /// </summary>
        /// <param name="point"></param>
        /// <param name="capStart"></param>
        /// <param name="capEnd"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static bool IsPointInCapsule(Vector3 point, Vector3 capStart, Vector3 capEnd, float rad)
        {
//get the height of the triangle
            float[] sides = {
                Vector3.Distance(capStart, capEnd), Vector3.Distance(capEnd, point), Vector3.Distance(point, capStart)
            };
            var s = (sides[0] + sides[1] + sides[2]) / 2;
            var a = Mathf.Sqrt(s * (s - sides[0]) * (s - sides[1]) * (s - sides[2]));
            var h = a / (sides[0] * 0.5f);
//check if the height is within radius of the cylinder or the 2 spheres
            bool cap = h <= rad || sides[1] <= rad || sides[2] <= rad;
            return cap;
        }
    }
}