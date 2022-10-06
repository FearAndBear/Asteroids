using System;
using Asteroids.GlobalSystems;
using UnityEngine;

namespace Asteroids.Player
{
    public class MoveController : IDisposable
    {
        private const float MaxSpeed = 0.01f;
        private const float AccelerationSpeed = 0.03f;
        private const float DecelerationSpeed = 0.01f;

        private const float RotationSpeed = 240;

        private readonly Transform _target;

        private bool _isAcceleration;
        
        public float CurrentSpeed { get; private set; } = 0;
        
        public Vector3 Velocity { get; private set; }
        
        public MoveController(Transform target)
        {
            _target = target;

            UpdateSystem.Updates += UpdateMethod;
        }

        public void MoveForward()
        {
            Acceleration();
            _isAcceleration = true;
        }

        public void Rotation(Vector2 direction)
        {
            _target.Rotate(Vector3.back * ((direction * RotationSpeed).x * Time.deltaTime));
        }

        public void ForceStop()
        {
            CurrentSpeed = 0;
            Velocity = Vector3.zero;
        }
        
        private void UpdateMethod()
        {
            _target.position += Velocity;

            if (_isAcceleration) 
                _isAcceleration = false;
            else 
                Deceleration();
        }

        private void Acceleration()
        {
            float newSpeed = CurrentSpeed + AccelerationSpeed * Time.deltaTime;
            CurrentSpeed = Mathf.Min(MaxSpeed, newSpeed);
            Velocity += _target.up * (CurrentSpeed * Time.deltaTime);
            
            Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
        }

        private void Deceleration()
        {
            float newSpeed = CurrentSpeed - DecelerationSpeed * Time.deltaTime;
            CurrentSpeed = Mathf.Max(0, newSpeed);

            Velocity -= Velocity.normalized * (DecelerationSpeed * Time.deltaTime);
        }

        public void Dispose()
        {
            UpdateSystem.Updates -= UpdateMethod;
        }
    }
}