 using UnityEngine;

public class Follw : MonoBehaviour
{
    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.player.transform.position);
    }
}
