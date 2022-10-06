using UnityEngine;

namespace Asteroids.Shooting.Arguments
{
    public class DefaultShootingArguments : ShootingArgumentsBase
    {
        public Vector3 From { get; }
        public Vector3 Direction { get; }

        public DefaultShootingArguments(Vector3 from, Vector3 direction)
        {
            From = from;
            Direction = direction;
        }
    }
}