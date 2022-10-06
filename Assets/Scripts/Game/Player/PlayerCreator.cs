using Asteroids.Events;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerCreator : MonoBehaviour
    {
        [SerializeField] private PlayerShip playerShip;

        private void Awake()
        {
            ActivatePlayer();
            
            EventBus.Subscribe<ReplayGameEvent>(OnReplayGame);
        }

        private void OnReplayGame(ReplayGameEvent @event)
        {
            playerShip.gameObject.SetActive(false);
            ActivatePlayer();
        }
        
        private void ActivatePlayer()
        {
            playerShip.gameObject.SetActive(true);
            
            playerShip.transform.position = Vector3.zero;
            playerShip.transform.eulerAngles = Vector3.zero;
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<ReplayGameEvent>(OnReplayGame);
        }
    }
}