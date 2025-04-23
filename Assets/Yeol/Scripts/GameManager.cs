using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSprite
{
    public string weaponName;
    public Sprite[] Sprites;
}
[System.Serializable]
public class DungeonFloor
{
    public int floorIndex;
    public int killTarget;
    public int currentKills;
    public bool isCleared;
}
public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager Instance;
    public int enemyLevel;
    [Header("# Player Info")]
    public bool isLive;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int exp;
    public int nextExp = 30;
    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerController player;

    [Header("# Weapon")]
    public List<WeaponSprite> weapons;
    public LevelUp uiLevelUp;

    [Header("# Dungeon")]
    public List<DungeonFloor> floors;
    public int floorIndex;

    public DungeonFloor CurrentFloor => floors[floorIndex];
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        health = maxHealth;
        uiLevelUp.Select(0);
    }
    public void GetExp(int exp)
    {
        this.exp += exp;
        if(this.exp >= nextExp)
        {
            nextExp = Mathf.RoundToInt(nextExp * 1.2f);
            this.exp = 0;
            level++;
            uiLevelUp.Show();
        }
    }
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;

    }
    public void OnEnemyKill()
    {
        var floor = CurrentFloor;
        if (floor.isCleared) return;
        floor.currentKills++;
        if(floor.currentKills >= floor.killTarget)
        {
            floor.isCleared = true;
            Debug.Log("클리어!");
            // 방치형 시스템 활성화
        }
    }
    public void GoToNextFloor()
    {
        if (floorIndex + 1 < floors.Count) floorIndex++;
    }
    public void GoToPreviousFloor()
    {
        if (floorIndex - 1 >= 0) floorIndex--;
    }
    public bool CanAutoBattle => CurrentFloor.isCleared;
}
