using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.DI;
using Asteroids.Factory;
using UnityEngine;

namespace Asteroids.ObjectPool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Stack<T> _allInactiveObjects;
        private readonly List<T> _allActiveObjects;
        
        private readonly IFactory<T> _factory;

        private DiContainer _diContainer;

        public ObjectPool(IFactory<T> factory)
        {
            _factory = factory;
            _allInactiveObjects = new Stack<T>(20);
            _allActiveObjects = new List<T>(20);
        }

        public T GetObject()
        {
            T obj;
            if (_allInactiveObjects.Count > 0)
            {
                obj = _allInactiveObjects.Pop();
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = _factory.Create();
            }

            _allActiveObjects.Add(obj);
            return obj;
        }

        public void DisableAllActiveObjects(Action<T> actionBeforeInactiveObj = null)
        {
            foreach (var obj in _allActiveObjects)
            {
                actionBeforeInactiveObj?.Invoke(obj);
                obj.gameObject.SetActive(false);
                _allInactiveObjects.Push(obj);
            }
            
            _allActiveObjects.Clear();
        }

        public void ReturnObjectToPool(T obj)
        {
            if (_allInactiveObjects.Any(x => x.Equals(obj)))
            {
                Debug.LogWarning($"[ObjectPool] {obj} object already contains in pool!");    
            }
            
            obj.gameObject.SetActive(false);
            _allInactiveObjects.Push(obj);
            _allActiveObjects.Remove(obj);
        }
    }
}