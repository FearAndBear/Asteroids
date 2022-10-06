using UnityEngine;

namespace Asteroids.Projectiles
{
    public class BeamProjectileArgs : ProjectileArgumentsBase
    {
        public Transform Target { get; }

        public BeamProjectileArgs(Transform target)
        {
            Target = target;
        }
    }
}