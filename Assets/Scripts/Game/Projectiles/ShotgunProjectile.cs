using System;
using Asteroids.HitSystem;
using Asteroids.Events;
using Asteroids.MoveControllers;
using UnityEngine;

namespace Asteroids.Projectiles
{
    public class ShotgunProjectile : ProjectileBaseWithArgs<ShotgunProjectileArgs>
    {
        public event Action<ShotgunProjectile> OnDestroyProjectile; 

        [SerializeField] private float speed;
        [SerializeField] private float lifeTime = 5;
        
        private float _timer;

        protected override void Awake()
        {
            base.Awake();
            EventBus.Subscribe<DamageReceivedEvent>(OnHitReceived);
        }

        public override void Init(ShotgunProjectileArgs args)
        {
            _timer = lifeTime;
            MovingController = new LinearMovingController(transform, speed, args.Direction);
        } 

        protected override void UpdateMethod()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                Destroy();
            }
        }

        private void OnHitReceived(DamageReceivedEvent @event)
        {
            if (@event.HitReceiver.gameObject == gameObject)
                Destroy();
        }
        
        private void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            MovingController.Dispose();
            OnDestroyProjectile?.Invoke(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventBus.Unsubscribe<DamageReceivedEvent>(OnHitReceived);
        }
    }
}