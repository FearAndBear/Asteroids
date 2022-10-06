using Asteroids.DI;
using Asteroids.Projectiles;
using UnityEngine;

namespace Asteroids.Factory
{
    public class ProjectileFactory : IFactory<ProjectileBase>
    {
        private DiContainer _diContainer;
        private Transform _container;

        private ProjectileBase _prefab;

        public ProjectileFactory(DiContainer diContainer, Transform container, ProjectileBase prefab)
        {
            _diContainer = diContainer;
            _container = container;

            _prefab = prefab;
        }

        public ProjectileBase Create()
        {
            return Create(_prefab);
        }

        public ProjectileBase Create(ProjectileBase obj)
        {
            var newObj = Object.Instantiate(obj, _container);
            _diContainer.Inject(newObj);

            return newObj;
        }
    }
}