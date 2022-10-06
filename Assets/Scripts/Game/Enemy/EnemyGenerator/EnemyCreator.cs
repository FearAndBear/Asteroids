using Asteroids.DI;
using Asteroids.Factory;
using Asteroids.ObjectPool;
using Asteroids.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Enemy
{
    public class EnemyCreator
    {
        private ObjectPool<EnemyBase> _objectPool;
        
        private Camera _camera;

        public EnemyType EnemyType { get; }

        public EnemyCreator(DiContainer diContainer, Transform container, EnemyBase prefab)
        {
            EnemyType = prefab.EnemyType;

            _camera = Camera.main;
            var factory = new EnemyFactory(diContainer, container, prefab);
            _objectPool = new ObjectPool<EnemyBase>(factory);
        }

        public EnemyBase Create(bool isAutoInit = true)
        {
            var newEnemy = _objectPool.GetObject();
            newEnemy.OnDestroyed += OnEnemyDestroyed;

            var pos = GetRandomPosForEnemy();
            var moveDirection = VectorUtils.RandomizeVectorRotationByAngle(pos, Vector3.zero, 45);

            var shift = (pos - Vector3.zero).normalized * newEnemy.transform.lossyScale.x;

            newEnemy.transform.position = pos + shift;

            if (isAutoInit)
            {
                switch (newEnemy)
                {
                    case Asteroid asteroid:
                        asteroid.Init(moveDirection);
                        break;
                
                    case UFO ufo:
                        ufo.Init();
                        break;
                }
            }
            
            return newEnemy;
        }

        public void DisableAll()
        {
            _objectPool.DisableAllActiveObjects(x => x.OnDestroyed -= OnEnemyDestroyed);
        }
        
        private void OnEnemyDestroyed(EnemyBase enemy)
        {
            enemy.OnDestroyed -= OnEnemyDestroyed;
            _objectPool.ReturnObjectToPool(enemy);
        }
        
        private Vector3 GetRandomPosForEnemy()
        {
            Vector2 resultPos = Vector2.zero;
            Vector2 cameraSize = new Vector2(_camera.pixelWidth, _camera.pixelHeight);
            
            // Vertical, else horizontal. 
            if (Random.Range(0, 2) == 0)
            {
                resultPos.y = Random.Range(0, 2) == 0 ? cameraSize.y : 0;
                resultPos.x = Random.Range(0, cameraSize.x + 1);
            }
            else
            {
                resultPos.x = Random.Range(0, 2) == 0 ? cameraSize.x : 0;
                resultPos.x = Random.Range(0, cameraSize.y + 1);
            }

            return _camera.ScreenToWorldPoint(resultPos);
        }
    }
}