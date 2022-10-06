using System;
using Asteroids.Events;
using Asteroids.GlobalSystems;
using Asteroids.Projectiles;
using Asteroids.Shooting.Arguments;
using Asteroids.ObjectPool;

namespace Asteroids.Shooting
{
    public abstract class ShootingControllerBase<T> : IDisposable where T : ShootingArgumentsBase 
    {
        protected ObjectPool<ProjectileBase> ProjectilesPool;

        public abstract bool Shoot(T args);
        
        public T Data { get; protected set; }

        protected ShootingControllerBase()
        {
            UpdateSystem.Updates += Update;
            
            EventBus.Subscribe<ReplayGameEvent>(OnReplayGame);
        }

        protected virtual void OnReplayGame(ReplayGameEvent @event)
        {
            ProjectilesPool?.DisableAllActiveObjects();
        }

        protected virtual void Update()
        {
            
        }

        public virtual void Dispose()
        {
            UpdateSystem.Updates -= Update;
            EventBus.Unsubscribe<ReplayGameEvent>(OnReplayGame);
        }

        public void Deconstruct()
        {
            Dispose();
        }
    }
}