using System;
using UnityEngine;

namespace Asteroids.MovingControllers
{
    public interface IMovingController : IDisposable
    {
        float Speed { get; }
        Transform Transform { get; }
        Vector2 Direction { get; }
    }
}