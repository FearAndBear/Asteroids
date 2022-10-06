using System;
using UnityEngine;

namespace Asteroids.GlobalSystems
{
    public class UpdateSystem : MonoBehaviour
    {
        public static event Action Updates;
        public static event Action FixedUpdates;
        public static event Action LateUpdates;

        private void FixedUpdate()
        {
            FixedUpdates?.Invoke();
        }

        private void Update()
        {
            Updates?.Invoke();
        }

        private void LateUpdate()
        {
            LateUpdates?.Invoke();
        }

        private void OnDestroy()
        {
            Updates = null;
            FixedUpdates = null;
            LateUpdates = null;
        }
    }
}
