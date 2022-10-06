using Asteroids.DI;
using Asteroids.DI.Attributes;
using UnityEngine;

namespace Asteroids.GlobalSystems
{
    public class SystemInitializer : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;

        private BorderTeleportSystem _borderTeleportSystem;
        private void Awake()
        {
            _diContainer.Inject(_borderTeleportSystem = new BorderTeleportSystem());
        }
    }
}