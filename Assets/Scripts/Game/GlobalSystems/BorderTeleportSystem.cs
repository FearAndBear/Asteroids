using Asteroids.Player;
using Asteroids.DI.Attributes;
using UnityEngine;

namespace Asteroids.GlobalSystems
{
    public class BorderTeleportSystem
    {
        [Inject] private PlayerShip _playerShip;
        
        private Camera _targetCamera;
        
        public BorderTeleportSystem()
        {
            UpdateSystem.Updates += Update;
            _targetCamera = Camera.main;
        }

        private void Update()
        {
            if (_playerShip)
                TeleportObjectFromUnderScreen(_playerShip.transform);
        }

        private void TeleportObjectFromUnderScreen(Transform target)
        {
            var targetPos = target.position;
            var screenSize = new Vector2(_targetCamera.pixelWidth, _targetCamera.pixelHeight);
            
            var upperRightScreenPos = _targetCamera.ScreenToWorldPoint(screenSize);
            var bottomLeftScreenPos = _targetCamera.ScreenToWorldPoint(Vector3.zero);
            var targetScreenPos = _targetCamera.WorldToScreenPoint(targetPos);

            if (targetScreenPos.x < 0)
            {
                targetPos.x = upperRightScreenPos.x;
            }
            else if (targetScreenPos.x > screenSize.x)
            {
                targetPos.x = bottomLeftScreenPos.x;
            }
            
            if (targetScreenPos.y < 0)
            {
                targetPos.y = upperRightScreenPos.y;
            }
            else if (targetScreenPos.y > screenSize.y)
            {
                targetPos.y = bottomLeftScreenPos.y;
            }

            target.position = targetPos;
        }
    }
}