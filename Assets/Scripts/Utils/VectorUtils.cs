using UnityEngine;

namespace Asteroids.Utils
{
    public static class VectorUtils
    {
        public static Vector3 RandomizeVectorRotationByAngle(Vector3 from, Vector3 to, float angleMax)
        {
            Vector3 direction = (to - from).normalized;
            return RandomizeVectorRotationByAngle(direction, angleMax);
        }

        public static Vector3 RandomizeVectorRotationByAngle(Vector3 direction, float angleMax)
        {
            return Quaternion.Euler(0, 0, Random.Range(-angleMax, angleMax)) * direction;
        }
    }
}