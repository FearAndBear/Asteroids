using Asteroids.DI;
using Asteroids.Enemy;
using UnityEngine;

namespace Asteroids.Factory
{
    public class EnemyFactory : IFactory<EnemyBase>
    {
        private DiContainer _diContainer;
        private Transform _container;

        private EnemyBase _prefab;

        public EnemyFactory(DiContainer diContainer, Transform container, EnemyBase prefab)
        {
            _diContainer = diContainer;
            _container = container;

            _prefab = prefab;
        }

        public EnemyBase Create()
        {
            return Create(_prefab);
        }

        public EnemyBase Create(EnemyBase customPrefab)
        {
            var newObj = Object.Instantiate(customPrefab, _container);
            _diContainer.Inject(newObj);
            
            return newObj;
        }
    }
}