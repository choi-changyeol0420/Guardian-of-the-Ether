using UnityEngine;

public abstract class WeaponSpawner : MonoBehaviour
{
    #region Variables
    [HideInInspector]public int level;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    protected PlayerController player;
    #endregion
    private void Awake()
    {
        player = GameManager.Instance.player;
    }
    public virtual void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        //Property Set
        damage = data.baseDamage;
        count = data.baseCount;
        for(int i = 0; i < GameManager.Instance.pool.prefabs.Length;i++)
        {
            if(data.projectile == GameManager.Instance.pool.prefabs[i])
            {
                prefabId = i;
            }
        }
        
    }
    public abstract void WeaponUpdate();
    public virtual void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
    }
    private void Update()
    {
        WeaponUpdate();
    }
}
