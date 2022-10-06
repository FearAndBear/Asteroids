using System;
using System.Linq;
using Asteroids.GlobalSystems;
using Asteroids.DI;
using Asteroids.DI.Attributes;
using Asteroids.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Enemy
{
    public class EnemiesController : MonoBehaviour
    {
        private const int MaxEnemiesInWave = 3;

        private const int MaxTimeBetweenWaves = 4;
        private const int MinTimeBetweenWaves = 2;

        [SerializeField] private EnemyGroup[] enemiesGroupsPrefabs;
        
        [Inject] private DiContainer _diContainer;

        private EnemyCreator[] _enemyCreators;
        private float _timer = 3;

        private void Awake()
        {
            _enemyCreators = new EnemyCreator[enemiesGroupsPrefabs.Length];
            
            for (var i = 0; i < enemiesGroupsPrefabs.Length; i++)
            {
                for (var x = 0; x < enemiesGroupsPrefabs[i].Enemies.Length; x++)
                {
                    _enemyCreators[i] = new EnemyCreator(_diContainer, transform, enemiesGroupsPrefabs[i].Enemies[x]);
                }
            }
            
            EventBus.Subscribe<EnemyDestroyedEvent>(OnEnemyDestroyed);
            EventBus.Subscribe<ReplayGameEvent>(OnReplayGame);
            
            UpdateSystem.Updates += UpdateMethod;
        }

        private void UpdateMethod()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = Random.Range(MinTimeBetweenWaves, MaxTimeBetweenWaves + 1);
                GenerateEnemyWave(Random.Range(1, MaxEnemiesInWave + 1));
            }
        }

        private void GenerateEnemyWave(int enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                CreateRandomEnemy();
            }
        }

        private EnemyBase CreateRandomEnemy(bool isAutoInit = true)
        {
            return _enemyCreators[Random.Range(0, _enemyCreators.Length)].Create(isAutoInit);
        }

        private EnemyBase CreateRandomEnemy(EnemyType type, bool isAutoInit = true)
        {
            if (!type.Equals(EnemyType.None))
            {
                var targetCreators = _enemyCreators.Where(x => x.EnemyType.HasFlag(type)).ToArray();

                if (targetCreators.Length > 0)
                {
                    return targetCreators[Random.Range(0, targetCreators.Length)].Create(isAutoInit);
                }
            }

            return null;
        }

        private void OnEnemyDestroyed(EnemyDestroyedEvent @event)
        {
            if (@event.DestructionReason != DestructionReason.Player)
                return;
            
            if (@event.EnemyBase is Asteroid asteroid && !asteroid.PartialAsteroids.Equals(EnemyType.None))
            {
                var asteroidPos = asteroid.transform.position;

                int count = Random.Range(2, 4);
                for (int i = 0; i < count; i++)
                {
                    var newAsteroid = CreateRandomEnemy(asteroid.PartialAsteroids, false) as Asteroid;
                    newAsteroid.transform.position = asteroidPos;
                    newAsteroid.Init(Utils.VectorUtils.RandomizeVectorRotationByAngle(asteroid.MovingDirection, 60));
                }
            }
        }

        private void OnReplayGame(ReplayGameEvent @event)
        {
            foreach (var enemyCreator in _enemyCreators)
            {
                enemyCreator.DisableAll();
                _timer = 3;
            }
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<EnemyDestroyedEvent>(OnEnemyDestroyed);
            EventBus.Unsubscribe<ReplayGameEvent>(OnReplayGame);
        }

        [Serializable]
        private class EnemyGroup
        {
            [SerializeField] private EnemyType _group;
            [SerializeField] private EnemyBase[] _enemies;

            public EnemyType Group => _group;
            public EnemyBase[] Enemies => _enemies;
        }
    }
}