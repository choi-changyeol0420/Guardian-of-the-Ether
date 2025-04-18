using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    #region Variables
    public ItemData data;
    public int level;
    public WeaponSpawner weapon;

    public Image icon;
    public Text textlevel;
    private Button upButton;
    [SerializeField]private float nextDamage;
    private int nextCount;
    #endregion
    private void Start()
    {
        icon.sprite = data.itemIcon;
        upButton = GetComponent<Button>();
        if (data.itemType == ItemData.ItemType.Heal)
        {
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(2.7f, 5.4f);
        }
        OnClick();
        upButton.onClick.AddListener(()=>OnClick());
    }
    private void LateUpdate()
    {
        textlevel.text = "Lv." + level;
    }
    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = data.itemType == ItemData.ItemType.Range ? newWeapon.AddComponent<Staff>() : newWeapon.AddComponent<Sword>();
                    weapon.Init(data);
                }
                else
                {
                    float linear = level * 1.3f;
                    float curve = Mathf.Pow(level, 1.1f);
                    nextDamage += Mathf.RoundToInt(data.baseDamage + linear + curve);
                    weapon.LevelUp(nextDamage, 1);
                }
                break;
            case ItemData.ItemType.Shield:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }
        level++;
        if(level ==  50)
        {
            upButton.interactable = false;
        }
    }
}
