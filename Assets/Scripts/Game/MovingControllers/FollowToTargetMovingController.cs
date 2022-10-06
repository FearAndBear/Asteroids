using Asteroids.GlobalSystems;
using UnityEngine;

namespace Asteroids.MovingControllers
{
    public class FollowToTargetMovingController : IMovingController
    {
        public float Speed { get; }
        public Transform Transform { get; }
        public Transform Target { get; }
        public Vector2 Direction { get; private set; }

        public FollowToTargetMovingController(Transform transform, float speed, Transform target)
        {
            Transform = transform;
            Speed = speed;
            Target = target;
            UpdateSystem.Updates += UpdateMethod;
        }
        
        private void UpdateMethod()
        {
            Vector2 currentPos = Transform.position;
            
            if (!Equals(Target, null) && Target.gameObject.activeSelf)
            {
                Vector2 targetPos = Target.position;
                Direction = (targetPos - currentPos).normalized;
            }
            
            Transform.position = Vector2.MoveTowards(currentPos, currentPos + Direction, Speed * Time.deltaTime);
        }
        
        public void Dispose()
        {
            UpdateSystem.Updates -= UpdateMethod;
        }
    }
}