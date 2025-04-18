using System.Collections;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    private WeaponSpawner[] weapons;
    public void Init(ItemData data)
    {
        name = "Gear" + data.itemId;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.baseDamage;
        
        ApplyGear();
    }
    public void LevelUp(float rate)
    {
        this.rate = rate;
    }
    IEnumerator delayWeapon()
    {
        yield return new WaitForSeconds(0.3f);
        weapons = FindObjectsByType<WeaponSpawner>(FindObjectsSortMode.None);
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
            switch (weapons.Length)
            {
                case 0:
                    weapons[0].speed += 150 * rate;
                    break;
                case 1:
                    weapons[1].speed -= rate / 50;
                    break;
            }
        }
    }
    void SpeedUp()
    {
        float speed = 1;
        GameManager.Instance.player.moveSpeed = speed + speed * rate;
    }
    void HealthUp()
    {
        float health = 10;
        GameManager.Instance.maxHealth += Mathf.RoundToInt(health);
    }
    private void Start()
    {
        StartCoroutine(delayWeapon());
    }
}
