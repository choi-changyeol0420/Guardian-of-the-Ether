using System.Collections;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    [HideInInspector]public WeaponSpawner[] weapons;
    public void Init(ItemData data)
    {
        name = "Gear" + data.itemId;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.baseDamage;
        StartCoroutine(delayWeapon());
        
    }
    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }
    IEnumerator delayWeapon()
    {
        yield return new WaitForSeconds(0.3f);
        weapons = FindObjectsByType<WeaponSpawner>(FindObjectsSortMode.None);
        ApplyGear();
    }
    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.Shield:
                HealthUp();
                break;
            case ItemData.ItemType.Glove:
                ReteUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }
    void ReteUp()
    {
        if(weapons.Length > 0)
        {
            if (weapons[0] != null)
            {
                weapons[0].speed += 150 * rate;
            }
            if (weapons.Length < 2) return;
            weapons[1].speed -= 0.015f;
        }
    }
    void SpeedUp()
    {
        GameManager.Instance.player.moveSpeed += rate;
    }
    void HealthUp()
    {
        GameManager.Instance.maxHealth += Mathf.RoundToInt(rate);
    }
}
