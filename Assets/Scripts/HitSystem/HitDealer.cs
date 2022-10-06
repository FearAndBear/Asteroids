using UnityEngine;

namespace Asteroids.HitSystem
{
    public class HitDealer : MonoBehaviour
    {
        [SerializeField] private HitFilter targets;

        public HitFilter Targets => targets;
    }
}