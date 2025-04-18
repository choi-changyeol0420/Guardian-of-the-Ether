using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region Variables
    public enum InfoType { Exp, Level,kill,Health}
    public InfoType type;

    public Text mytext;
    public Slider mySlider;
    #endregion
    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp;
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                mytext.text = "Lv." + GameManager.Instance.level.ToString("F0");
                break;
            case InfoType.kill:
                mytext.text = GameManager.Instance.kill.ToString("F0");
                break;
            case InfoType.Health:
                float curHealth = GameManager.Instance.health;
                float maxHealth = GameManager.Instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
