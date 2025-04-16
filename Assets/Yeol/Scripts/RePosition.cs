using UnityEngine;


public class RePosition : MonoBehaviour
{
    #region Variables
    Collider2D coll;
    #endregion
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area")) return;
        Vector3 playerDir = GameManager.Instance.player.moveInput;
        switch(transform.tag)
        {
            case "Enemy":
                if(coll.enabled)
                {
                    transform.Translate(playerDir * 10.24f + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
            break;
        }
    }
}
