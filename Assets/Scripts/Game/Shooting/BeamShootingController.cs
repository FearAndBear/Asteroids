using Asteroids.DI;
using Asteroids.Factory;
using Asteroids.Projectiles;
using Asteroids.Shooting;
using Asteroids.Shooting.Arguments;
using Asteroids.ObjectPool;
using UnityEngine;

namespace Asteroids.Player
{
    public class BeamShootingController : ShootingControllerBase<EmptyShootingArguments>
    {
        private const float Cooldown = 6;

        private float _beamDelay = 1;
        private float _beamCastTimer;
        
        private readonly Transform _sourceObject;

        private BeamProjectile _beamObject;
        
        public bool IsReload => CurrentAmmo < MaxAmmo;
        public bool IsNoAmmo => CurrentAmmo == 0;
        public bool IsShooting => _beamCastTimer > 0;

        public int CurrentAmmo { get; private set; }
        public int MaxAmmo => 3;
        public float CooldownTimer { get; private set; }
        
        public BeamShootingController(Transform projectileContainer, BeamProjectile beamProjectilePrefab, DiContainer diContainer, Transform sourceObject) : base()
        {
            var projectileFactory = new ProjectileFactory(diContainer, projectileContainer, beamProjectilePrefab);
            ProjectilesPool = new ObjectPool<ProjectileBase>(projectileFactory);
            _sourceObject = sourceObject;

            CurrentAmmo = MaxAmmo;
            CooldownTimer = Cooldown;
        }

        protected override void Update()
        {
            base.Update();

            if (IsReload)
            {
                CooldownTimer -= Time.deltaTime;

                if (CooldownTimer <= 0)
                {
                    CooldownTimer = Cooldown;
                    CurrentAmmo++;
                }
            }

            if (IsShooting)
            {
                _beamCastTimer -= Time.deltaTime;
                if (_beamCastTimer <= 0)
                {
                    _beamObject.gameObject.SetActive(false);
                    ProjectilesPool.ReturnObjectToPool(_beamObject);
                }
            }
        }

        public override bool Shoot(EmptyShootingArguments args)
        {
            if (!IsNoAmmo && !IsShooting)
            {
                _beamObject = ProjectilesPool.GetObject() as BeamProjectile;
                _beamObject.Init(new BeamProjectileArgs(_sourceObject));
                
                _beamCastTimer = _beamDelay;
                _beamObject.gameObject.SetActive(true);
                
                CurrentAmmo--;
            }

            return false;
        }

        public void ForceReset()
        {
            CurrentAmmo = MaxAmmo;
            CooldownTimer = Cooldown;

            _beamCastTimer = 0;
            if (!Equals(_beamObject, null))
            {
                _beamObject.gameObject.SetActive(false);
                ProjectilesPool.ReturnObjectToPool(_beamObject);
            }
        }
    }
}