using System.Collections.Generic;
using UnityEngine;

namespace General.Pool
{
    public class PrefabPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Queue<T> _objects = new Queue<T>();
        private readonly Transform _parentTransform;

        public PrefabPool(T prefab, int initialCapacity, Transform parentTransform)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
            for (int i = 0; i < initialCapacity; i++)
            {
                T obj = Object.Instantiate(prefab, parentTransform);
                obj.gameObject.SetActive(false);
                _objects.Enqueue(obj);
            }
        }

        public T Get()
        {
            if (_objects.Count > 0)
            {
                T obj = _objects.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                T obj = Object.Instantiate(_prefab, _parentTransform);
                return obj;
            }
        }

        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }
    }
}