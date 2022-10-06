using Asteroids.GlobalSystems;
using Asteroids.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.DI
{
    public class DiInitializer : DiInitializerBase
    {
        [FormerlySerializedAs("playerController")] [SerializeField] private PlayerShip playerShip;

        protected override void InstallBindings()
        {
            DiContainer.Bind(playerShip);
            DiContainer.Bind(DiContainer);
            DiContainer.Bind(new PlayerScoreCounterSystem());
        }
    }
}