using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private uint _initPoolSize;
    public uint InitPoolSize => _initPoolSize;

    [SerializeField] private PooledObject _objectToPool;

    private Stack<PooledObject> _stack;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if (_objectToPool == null) return;

        _stack = new Stack<PooledObject>();

        PooledObject instance = null;

        for (int i = 0; i < InitPoolSize; i++)
        {
            instance = Instantiate(_objectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            _stack.Push(instance);
        }
    }

    public PooledObject GetPooledObject()
    {
        if (_objectToPool == null) return null;

        // if the pool is not large enough, 
        // instantiate extra 
        if (_stack.Count == 0 )
        {
            PooledObject newInstance = Instantiate(_objectToPool);
            newInstance.Pool = this;
            return newInstance;
        }

        PooledObject nextInstance = _stack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        _stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }

}
