using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region Variables
    public enum InfoType { Exp, Level,kill,Health,Floor}
    public InfoType type;

    public Text mytext;
    public Slider mySlider;
    #endregion
    private void LateUpdate()
    {
        switch((int)type)
        {
            case 0:
                float curExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp;
                mySlider.value = curExp / maxExp;
                break;
            case 1:
                mytext.text = "Lv." + GameManager.Instance.level.ToString("F0");
                break;
            case 2:
                mytext.text = GameManager.Instance.CurrentFloor.currentKills.ToString("F0");
                break;
            case 3:
                float curHealth = GameManager.Instance.health;
                float maxHealth = GameManager.Instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
            case 4:
                mytext.text = GameManager.Instance.CurrentFloor.floorIndex.ToString() + "층";
                Text killTarget = transform.GetChild(0).GetComponent<Text>();
                killTarget.text = "목표 수 : " + GameManager.Instance.CurrentFloor.killTarget.ToString();
                break;
        }
    }
}
