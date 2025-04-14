using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    #region Variables
    private readonly Stack<PooledObject> _objects = new Stack<PooledObject>();
    private readonly PooledObject _prefab;
    private readonly Transform _parent;
    #endregion

    public Pool(PooledObject prefab, int initialCount, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        for (int i = 0; i < initialCount; i++)
        {
            var obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            obj.PoolOrigin = this;
            _objects.Push(obj);
        }
    }
    public PooledObject Get()
    {
        if(_objects.Count > 0)
        {
            var obj = _objects.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newobj = Object.Instantiate(_prefab, _parent);
            newobj.PoolOrigin = this;
            return newobj;
        }
    }
    public void Release(PooledObject obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Push(obj);
    }
}
