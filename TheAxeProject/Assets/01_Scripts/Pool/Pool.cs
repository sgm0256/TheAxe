using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Pool
    {
        private Stack<IPoolable> _pool;
        private Transform _parent;
        private PoolTypeSO _poolType;

        public Pool(PoolTypeSO poolType, Transform parent, int count)
        {
            _pool = new Stack<IPoolable>(count);
            _parent = parent;
            _poolType = poolType;

            Instantiate(count);
        }

        private void Instantiate(int count)
        {
            var asset = _poolType.prefab;

            for (int i = 0; i < count; i++)
            {
                GameObject gameObj = UnityEngine.Object.Instantiate(asset);
                gameObj.SetActive(false);
                gameObj.transform.SetParent(_parent);
                IPoolable item = gameObj.GetComponent<IPoolable>();
                item.SetUpPool(this);
                _pool.Push(item);
            }
        }

        public IPoolable Pop()
        {
            IPoolable item;
            if (_pool.Count == 0)
            {
                GameObject gameObj = UnityEngine.Object.Instantiate(_poolType.prefab, _parent);
                item = gameObj.GetComponent<IPoolable>();
                item.SetUpPool(this);
            }
            else
            {
                item = _pool.Pop();
                item.GameObject.SetActive(true);
            }
            item.ResetItem();
            return item;
        }

        public void Push(IPoolable item)
        {
            item.GameObject.transform.SetParent(_parent);
            item.GameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}
