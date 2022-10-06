using Asteroids.GlobalSystems;
using Asteroids.Player;
using Asteroids.DI.Attributes;
using Asteroids.Events;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Asteroids.UI
{
    public class MainUI : MonoBehaviour
    {
        [Space]
        [SerializeField] private TMP_Text playerSpeedText;
        [SerializeField] private TMP_Text playerPositionText;
        [SerializeField] private TMP_Text playerRotationText;
        
        [Space]
        [SerializeField] private TMP_Text scoreText;

        [Space]
        [SerializeField] private TMP_Text beamAmmoCountText;
        [SerializeField] private TMP_Text beamCooldownText;

        [Inject] private PlayerShip _playerShip;
        [Inject] private PlayerScoreCounterSystem _scoreCounterSystem;

        private void Awake()
        {
            UpdateSystem.Updates += UpdateMethod;
        }

        public void UpdateMethod()
        {
            if (Equals(_playerShip, null))
                return;

            scoreText.text = $"Score: {_scoreCounterSystem.Score}";
            playerSpeedText.text = $"Speed: {_playerShip.Speed}";

            playerPositionText.text = $"Position: {_playerShip.transform.position}";
            playerRotationText.text = $"Rotation: {_playerShip.transform.eulerAngles}";
            beamAmmoCountText.text = $"Beam: {_playerShip.CurrentBeamAmmo}/{_playerShip.MaxBeamAmmo}";

            beamCooldownText.text = _playerShip.BeamIsReload
                ? $"{(_playerShip.CooldownBeamLeft * 100) / 60:0.00} sec."
                : "";
        }

        private void OnDestroy()
        {
            UpdateSystem.Updates -= UpdateMethod;
        }
    }
}