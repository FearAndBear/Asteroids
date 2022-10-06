using System;
using Asteroids.Events;
using UnityEngine;

namespace Asteroids.HitSystem
{
    [Serializable]
    public class HitReceiver : MonoBehaviour
    {
        [SerializeField] private HitFilter owner;

        public HitFilter Owner => owner;

        private void OnTriggerEnter2D(Collider2D other)
        {
            HitDealer hitDealer = other.GetComponent<HitDealer>();
            if (hitDealer && owner.Equals(hitDealer.Targets))
            {
                EventBus.Invoke(new DamageReceivedEvent(this, hitDealer));
            }
        }
    }
}