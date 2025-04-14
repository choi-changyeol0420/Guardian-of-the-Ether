using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    private Dictionary<string, Pool> _pools = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void CreatePool(string key, PooledObject prefab, int count)
    {
        if(!_pools.ContainsKey(key))
        {
            var pool = new Pool(prefab, count, this.transform);
            _pools.Add(key, pool);
        }
    }
    public PooledObject Spawn(string key)
    {
        if (_pools.TryGetValue(key, out var pool))
        {
            return pool.Get();
        }
        Debug.LogError($"[PoolManager] No pool with key: {key}");
        return null;
    }
    public void Despawn(PooledObject obj)
    {
        obj.ReturnToPool();
    }
}
