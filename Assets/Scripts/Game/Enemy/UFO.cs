using Asteroids.DI.Attributes;
using Asteroids.MovingControllers;
using Asteroids.Player;

namespace Asteroids.Enemy
{
    public class UFO : EnemyBase
    {
        [Inject] private PlayerShip _playerShip;

        public void Init()
        {
            MovingController = new FollowToTargetMovingController(transform, Speed, _playerShip.transform);
        }
    }
}