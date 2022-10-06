using Asteroids.GlobalSystems;
using Asteroids.MovingControllers;
using UnityEngine;

namespace Asteroids.MoveControllers
{
    public class LinearMovingController : IMovingController
    {
        public float Speed { get; }
        public Transform Transform { get; }

        public Vector2 Direction { get; }

        public LinearMovingController(Transform transform, float speed, Vector3 direction)
        {
            Transform = transform;
            Speed = speed;

            Direction = direction;
            UpdateSystem.Updates += UpdateMethod;
        }
        
        private void UpdateMethod()
        {
            Vector2 pos = Transform.position;
            Transform.position = Vector2.MoveTowards(pos, pos + Direction, Speed * Time.deltaTime);
        }

        public void Dispose()
        {
            UpdateSystem.Updates -= UpdateMethod;
        }
    }
}