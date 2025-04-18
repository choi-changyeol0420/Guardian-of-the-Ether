using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables
    public float damage;
    public int per;
    public float timer;
    public float lifetime = 2f;
    public SpriteRenderer curretWeapon;

    public Rigidbody2D rigid;
    #endregion
    public void Init(float damage, int per, Vector2 dir)
    {
        this.damage = damage;
        this.per = per;
        if(per > -1)
        {
            rigid.linearVelocity = dir.normalized * 8f;
        }
    }
    private void Update()
    {
        if (per == -1) return;
        timer += Time.deltaTime;
        if(timer >= lifetime)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) return;
        per--;

        if(per == -1)
        {
            gameObject.SetActive(false);
            rigid.linearVelocity = Vector2.zero;
        }
    }
}
