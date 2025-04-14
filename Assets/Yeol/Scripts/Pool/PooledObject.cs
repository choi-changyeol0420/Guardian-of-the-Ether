using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public Pool PoolOrigin { get; set; }

    public void ReturnToPool()
    {
        if(PoolOrigin != null)
        {
            PoolOrigin.Release(this);
        }
        else
        {
            Destroy(gameObject); // 钱 绝捞 积己等 版快
        }
    }
}
