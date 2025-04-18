using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSprite
{
    public string weaponName;
    public Sprite[] Sprites;
}
public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager Instance;
    public int enemyLevel;
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int nextExp = 30;
    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerController player;

    //Weapon
    public List<WeaponSprite> weapons;
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        health = maxHealth;
    }
    public void GetExp(int exp)
    {
        this.exp += exp;
        if(this.exp >= nextExp)
        {
            nextExp = Mathf.RoundToInt(nextExp * 1.2f);
            this.exp = 0;
            level++;
        }
    }
}
