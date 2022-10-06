using System;

namespace Asteroids.Enemy
{
    [Flags]
    public enum EnemyType
    {
        None = 0,
        
        // Asteroids.
        Asteroid = 1 << 1,
        AsteroidSmall = 1 << 2,
        AsteroidMedium = 1 << 3,
        AsteroidHuge = 1 << 4,
        
        UFO = 1 << 5,
    }
}