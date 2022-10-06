using System;

namespace Asteroids.HitSystem
{
    [Flags] 
    public enum HitFilter
    {
        None = 0,
        Player = 1 << 1,
        Enemy = 1 << 2,
    }
}