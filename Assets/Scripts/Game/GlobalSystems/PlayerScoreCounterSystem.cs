using System;
using Asteroids.Enemy;
using Asteroids.Events;

namespace Asteroids.GlobalSystems
{
    public class PlayerScoreCounterSystem : IDisposable
    {
        public event Action<int> OnUpdateScore; 
        
        public int Score { get; private set; }

        public PlayerScoreCounterSystem()
        {
            Score = 0;
            EventBus.Subscribe<EnemyDestroyedEvent>(OnEnemyDestroyed);
            EventBus.Subscribe<ReplayGameEvent>(OnReplayGame);
        }

        private void OnReplayGame(ReplayGameEvent @event)
        {
            UpdateScore(0);
        }

        private void OnEnemyDestroyed(EnemyDestroyedEvent @event)
        {
            if (@event.DestructionReason == DestructionReason.Player)
            {
                UpdateScore(Score + @event.EnemyBase.Score);
            }
        }

        private void UpdateScore(int score)
        {
            Score = score;
            OnUpdateScore?.Invoke(Score);
        }

        public void Dispose()
        {
            Score = 0;
            EventBus.Unsubscribe<EnemyDestroyedEvent>(OnEnemyDestroyed);
            EventBus.Unsubscribe<ReplayGameEvent>(OnReplayGame);
        }

        ~PlayerScoreCounterSystem()
        {
            Dispose();
        }
    }
}