using UnityEngine;

public class Staff : WeaponSpawner
{
    #region Variables
    float timer;
    #endregion
    public override void Init(ItemData data)
    {
        base.Init(data);
        speed = 1f;
    }

    public override void WeaponUpdate()
    {
        timer += Time.deltaTime;
        if(timer > speed)
        {
            timer = 0;
            Fire();
        }
    }
    void Fire()
    {
        if (!player.scanner.nearestTarget) return;
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector2 dir = (targetPos - transform.position).normalized;
        Transform staff = GameManager.Instance.pool.Get(prefabId).transform;
        staff.position = transform.position;
        staff.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        staff.GetComponent<Weapon>().Init(damage, count, dir);
    }
}
