using Asteroids.Enemy;
using UnityEngine;

namespace Asteroids.GlobalSystems
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider2D;
        private void Awake()
        {
            var camera = Camera.main;
            var screenSize = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));
            _collider2D.size = screenSize * 2;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (Equals(other, null)) return;

            var enemy = other.GetComponent<EnemyBase>();
            if (enemy != null && enemy.gameObject.activeSelf)
            {
                enemy.Destroy(DestructionReason.DeadZone);
            }
        }
    }
}