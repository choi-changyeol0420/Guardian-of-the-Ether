using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    #region Variables
    public ItemData data;
    public int level;
    public WeaponSpawner weapon;
    public Gear gear;

    public Image icon;
    public Text textlevel;
    public Text textName;
    public Text textDesc;
    private Button upButton;
    [SerializeField]private float nextDamage;
    #endregion
    private void Start()
    {
        icon.sprite = data.itemIcon;
        upButton = GetComponent<Button>();
        if (data.itemType == ItemData.ItemType.Heal)
        {
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(2.7f, 4.5f);
        }
        
        upButton.onClick.AddListener(()=>OnClick());
    }
    
    private void OnEnable()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        textlevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
        textlevel.text = "Lv." + level;
        switch((int)data.itemType)
        {
            case 0:
            case 1:
                float linear = level * 1.3f;
                float curve = Mathf.Pow(level, 1.1f);
                nextDamage += Mathf.RoundToInt(data.baseDamage + linear + curve);
                textDesc.text = string.Format(data.itemDesc, nextDamage, 1);
                if (level % 5 == 0)
                {
                    switch ((int)data.itemType)
                    {
                        case 0:
                            textDesc.text = string.Format(data.itemDesc, nextDamage, 1) + "업그레이드 시 회전체 2로 감소";
                            break;
                    }
                }
                break;
            case 2:
            case 3:
            case 5:
                textDesc.text = data.itemDesc;
                break;
            case 4:
                textDesc.text = data.itemDesc;
                break;
        }
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
                    GameManager.Instance.player.weapons[(int)data.itemType].gameObject.SetActive(true);
                    weapon.Init(data);
                }
                else
                {
                    float linear = level * 1.3f;
                    float curve = Mathf.Pow(level, 1.1f);
                    nextDamage += Mathf.RoundToInt(data.baseDamage + linear + curve);
                    weapon.LevelUp(nextDamage, 1);
                    if (level % 5 == 0)
                    {
                        if (GameManager.Instance.weapons[(int)data.itemType].Sprites.Length < weapon.level) return;
                        switch (data.itemType)
                        {
                            case ItemData.ItemType.Melee:
                                icon.sprite = GameManager.Instance.weapons[(int)ItemData.ItemType.Melee].Sprites[weapon.level];
                                GameManager.Instance.player.weapons[(int)ItemData.ItemType.Melee].sprite = GameManager.Instance.weapons[(int)ItemData.ItemType.Melee].Sprites[weapon.level];
                                break;
                            case ItemData.ItemType.Range:
                                weapon.level++;
                                icon.sprite = GameManager.Instance.weapons[(int)ItemData.ItemType.Range].Sprites[weapon.level];
                                GameManager.Instance.player.weapons[(int)ItemData.ItemType.Range].sprite = GameManager.Instance.weapons[(int)ItemData.ItemType.Range].Sprites[weapon.level];
                                break;
                        }    
                    }
                }
                break;
            case ItemData.ItemType.Shield:
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {    
                    float nextRate = data.baseDamage;
                    gear.LevelUp(nextRate);
                }
                    break;
            case ItemData.ItemType.Heal:
                if(level != 0)
                {
                    GameManager.Instance.health += Mathf.RoundToInt(data.baseDamage);
                }
                break;
        }
        level++;
        if (level == 65)
        {
            upButton.interactable = false;
        }
    }
}
