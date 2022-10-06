namespace Asteroids.Projectiles
{
    public abstract class ProjectileBaseWithArgs<T> : ProjectileBase where T : ProjectileArgumentsBase
    {
        public abstract void Init(T data);
    }
}