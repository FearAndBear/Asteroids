using System;
using Asteroids.HitSystem;
using Asteroids.DI;
using Asteroids.DI.Attributes;
using Asteroids.Events;
using Asteroids.GlobalSystems;
using Asteroids.Projectiles;
using Asteroids.Shooting.Arguments;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerShip : MonoBehaviour
    {
        [Space]
        [SerializeField] private Transform projectileContainer;
        [SerializeField] private ShotgunProjectile shotgunProjectilePrefab;
        [SerializeField] private BeamProjectile beamPrefab;
        
        [Inject] private DiContainer _diContainer;

        private MoveController _moveController;
        private ProjectileShootingController _projectileShootingController;
        private BeamShootingController _beamShootingController;

        public float Speed => _moveController.CurrentSpeed;

        public int MaxBeamAmmo => _beamShootingController.MaxAmmo;
        public int CurrentBeamAmmo => _beamShootingController.CurrentAmmo;
        public float CooldownBeamLeft => _beamShootingController.CooldownTimer;

        public bool BeamIsReload => _beamShootingController.IsReload; 
        
        private void Awake()
        {
            _moveController = new MoveController(transform);
            _projectileShootingController = new ProjectileShootingController(projectileContainer, shotgunProjectilePrefab, _diContainer);
            _beamShootingController = new BeamShootingController(projectileContainer, beamPrefab, _diContainer, transform);
        }

        private void OnEnable()
        {
            EventBus.Subscribe<DamageReceivedEvent>(OnDamageReceivedEvent);
            UpdateSystem.Updates += UpdateMethod;
        }

        private void OnDamageReceived()
        {
            gameObject.SetActive(false);
            EventBus.Invoke(new PlayerDestroyedEvent());
        }

        private void OnDamageReceivedEvent(DamageReceivedEvent @event)
        {
            if (@event.HitReceiver.transform == transform)
            {
                OnDamageReceived();
            }
        }

        private void UpdateMethod()
        {
            if (Input.GetButton("Horizontal"))
            {
                _moveController.Rotation(Input.GetAxisRaw("Horizontal") * Vector2.right);
            }

            if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                _moveController.MoveForward();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _projectileShootingController.Shoot(new DefaultShootingArguments(transform.position, transform.up));
            }

            if (Input.GetKey(KeyCode.G))
            {
                _beamShootingController.Shoot(new EmptyShootingArguments());
            }
        }

        private void OnDisable()
        {
            _moveController.ForceStop();
            _beamShootingController.ForceReset();

            EventBus.Unsubscribe<DamageReceivedEvent>(OnDamageReceivedEvent);
            UpdateSystem.Updates -= UpdateMethod;
        }

        private void OnDestroy()
        {
            _moveController.Dispose();
            _projectileShootingController.Dispose();
            _beamShootingController.Dispose();
        }
    }
}