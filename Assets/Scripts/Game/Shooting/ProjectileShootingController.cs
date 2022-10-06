using Asteroids.DI;
using Asteroids.Events;
using Asteroids.Factory;
using Asteroids.ObjectPool;
using Asteroids.Projectiles;
using Asteroids.Shooting;
using Asteroids.Shooting.Arguments;
using UnityEngine;

namespace Asteroids.Player
{
    public class ProjectileShootingController : ShootingControllerBase<DefaultShootingArguments>
    {
        private DiContainer _diContainer;
        private Transform _projectileContainer;

        private ProjectileFactory _projectileFactory;

        public ProjectileShootingController(Transform projectileContainer, ShotgunProjectile shotgunProjectilePrefab, DiContainer diContainer) : base()
        {
            _projectileContainer = projectileContainer;
            _diContainer = diContainer;
            
            _projectileFactory = new ProjectileFactory(_diContainer, _projectileContainer, shotgunProjectilePrefab);
            ProjectilesPool = new ObjectPool<ProjectileBase>(_projectileFactory);
        }

        public override bool Shoot(DefaultShootingArguments args)
        {
            var projectile = ProjectilesPool.GetObject() as ShotgunProjectile;
            projectile.transform.position = args.From;
            projectile.OnDestroyProjectile += OnProjectileDestroy;
            projectile.Init(new ShotgunProjectileArgs(args.Direction));

            return true;
        }

        private void OnProjectileDestroy(ShotgunProjectile projectile)
        {
            projectile.OnDestroyProjectile -= OnProjectileDestroy;

            ProjectilesPool.ReturnObjectToPool(projectile);
        }

        protected override void OnReplayGame(ReplayGameEvent @event)
        {
            ProjectilesPool.DisableAllActiveObjects(x => 
                ((ShotgunProjectile)x).OnDestroyProjectile -= OnProjectileDestroy);
        }
    }
}