using Asteroids.DI.Attributes;
using Asteroids.Events;
using Asteroids.GlobalSystems;
using Asteroids.Player;
using Asteroids.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button reloadGameButton;

        [Inject] private PlayerScoreCounterSystem _playerScoreCounterSystem;

        private void Awake()
        {
            reloadGameButton.onClick.AddListener(ReloadGame);
            
            EventBus.Subscribe<PlayerDestroyedEvent>(OnPlayerDestroyed);
        }

        private void OnPlayerDestroyed(PlayerDestroyedEvent @event)
        {
            canvasGroup.SetActive(true);
            scoreText.text = $"Score: {_playerScoreCounterSystem.Score}";
        }

        private void ReloadGame()
        {
            EventBus.Invoke(new ReplayGameEvent());
            canvasGroup.SetActive(false);
        }

        private void OnDestroy()
        {
            reloadGameButton.onClick.RemoveListener(ReloadGame);
            
            EventBus.Unsubscribe<PlayerDestroyedEvent>(OnPlayerDestroyed);
        }
    }
}