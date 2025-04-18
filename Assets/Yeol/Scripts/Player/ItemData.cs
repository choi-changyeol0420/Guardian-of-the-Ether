using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    #region Variables
    public enum ItemType {Melee,Range,Shield,Glove, Shoe,Heal }
    [Header("#Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    [Header("#Level Data")]
    public float baseDamage;
    public int baseCount;
    [Header("#Weapon")]
    public GameObject projectile;
    #endregion
}
