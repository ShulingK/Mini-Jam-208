using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private Pool _pool;

    public Pool Pool { get => _pool; set => _pool = value; }

    public void Release()
    {
        Pool.ReturnToPool(this);
    }
}
