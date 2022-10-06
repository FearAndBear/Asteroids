using UnityEngine;

namespace Asteroids.Projectiles
{
    public class ShotgunProjectileArgs : ProjectileArgumentsBase
    {
        public Vector2 Direction { get; }

        public ShotgunProjectileArgs(Vector2 direction)
        {
            Direction = direction;
        }
    }
}