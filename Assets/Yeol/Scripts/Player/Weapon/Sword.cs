using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponSpawner
{
    public override void Init(ItemData data)
    {
        base.Init(data);
        GameManager.Instance.pool.prefabs[prefabId].GetComponent<Weapon>().curretWeapon.sprite = GameManager.Instance.weapons[0].Sprites[0];
        speed = 150;
        Batch();
    }

    public override void WeaponUpdate()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.K))
        {
            LevelUp(5, 1);
        }
    }

    public override void LevelUp(float damage, int count)
    {
        base.LevelUp(damage, count);
        // count가 5 이상이면 다시 2로 초기화하고 초과된 오브젝트 비활성화
        if (GameManager.Instance.weapons[0].Sprites.Length < level) return;
        if (this.count >= 6)
        {
            level++;
            this.count = 2;
            this.damage += 5;
            // 오브젝트 풀에서 활성화된 것들 중 초과된 것 비활성화
            int activeCount = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.gameObject.activeSelf)
                {
                    activeCount++;
                    if (activeCount > this.count)
                    {
                        child.gameObject.SetActive(false);
                    }
                    child.GetComponent<Weapon>().curretWeapon.sprite = GameManager.Instance.weapons[0].Sprites[level];
                }
            }
        }

        Batch();
    }
    void Batch()
    {
        List<Transform> activeSwords = new List<Transform>();

        // 현재 활성화된 자식 오브젝트만 리스트에 추가
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                activeSwords.Add(child);
            }
        }

        // 부족한 만큼 풀에서 꺼내서 추가
        while (activeSwords.Count < count)
        {
            Transform newSword = GameManager.Instance.pool.Get(prefabId).transform;
            newSword.parent = transform;
            activeSwords.Add(newSword);
        }

        // 원형 배치
        for (int i = 0; i < count; i++)
        {
            Transform sword = activeSwords[i];

            sword.localPosition = Vector3.zero;
            sword.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360f * i / count;
            sword.Rotate(rotVec);
            sword.Translate(sword.up * 0.4f, Space.World);

            sword.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); // -1 = 무한 지속
        }
    }
}
