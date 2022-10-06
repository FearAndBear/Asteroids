using Asteroids.GlobalSystems;
using Asteroids.MovingControllers;
using UnityEngine;

namespace Asteroids.Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected IMovingController MovingController;
        
        protected virtual void Awake()
        {
            UpdateSystem.Updates += UpdateMethod;
        }

        protected virtual void UpdateMethod() { }

        protected virtual void OnDestroy()
        {
            UpdateSystem.Updates -= UpdateMethod;
        }
    }
}