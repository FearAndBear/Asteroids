using System;
using Asteroids.HitSystem;
using Asteroids.Events;
using Asteroids.MovingControllers;
using UnityEngine;

namespace Asteroids.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public event Action<EnemyBase> OnDestroyed;

        [SerializeField] protected float Speed;

        [SerializeField] private EnemyType enemyType;   
        [SerializeField] private int score;

        public EnemyType EnemyType
        {
            get => enemyType;
            protected set => enemyType = value;
        }

        public int Score => score;
        
        protected IMovingController MovingController;

        public virtual void Destroy(DestructionReason destructionReason)
        {
            OnDestroyed?.Invoke(this);
            EventBus.Invoke(new EnemyDestroyedEvent(this, destructionReason));
        }
        
        private void OnDamageReceivedEvent(DamageReceivedEvent @event)
        {
            if (@event.HitReceiver.transform == transform)
            {
                Destroy(DestructionReason.Player);
            }
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<DamageReceivedEvent>(OnDamageReceivedEvent);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<DamageReceivedEvent>(OnDamageReceivedEvent);
            MovingController?.Dispose();
        }
    }
}