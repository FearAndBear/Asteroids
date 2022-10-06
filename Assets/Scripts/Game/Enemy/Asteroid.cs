using Asteroids.MoveControllers;
using UnityEngine;

namespace Asteroids.Enemy
{
    public class Asteroid : EnemyBase
    {
        [SerializeField] private EnemyType partialAsteroids;

        public Vector3 MovingDirection => MovingController.Direction;
        
        public EnemyType PartialAsteroids => partialAsteroids;

        public void Init(Vector3 movingDirection)
        {
            MovingController = new LinearMovingController(transform, Speed, movingDirection);
        }
    }
}