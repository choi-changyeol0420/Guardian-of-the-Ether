using System.Collections;
using System.ComponentModel;
using UnityEngine;
using static EnemySpawner;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour,IDamageable
{
    #region Variables
    public float maxHealth;
    public float damage;
    public float moveSpeed;
    public int exp = 5;
    public float health;
    private Rigidbody2D enemyRb;
    private Collider2D coll;
    public Animator animator;
    private bool isDie;

    private Rigidbody2D player;
    WaitForFixedUpdate wait;
    #endregion
    private void OnEnable()
    {
        player = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isDie = false;
        coll.enabled = true;
        enemyRb.simulated = true;
        animator.SetInteger("State", 0);
        health = maxHealth;
    }
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        enemyRb = GetComponent<Rigidbody2D>();
        if(!enemyRb.freezeRotation)
        {
            enemyRb.freezeRotation = true;
        }
        animator.SetFloat("MoveY", -1); //현재 상태 앞모습
        wait = new WaitForFixedUpdate();
    }
    private void FixedUpdate()
    {
        if (player == null || isDie) return;
        Vector2 direction = (player.position - enemyRb.position).normalized;
        Vector2 nextVec = direction * moveSpeed * Time.fixedDeltaTime;
        
        if( direction != Vector2.zero)
        {
            animator.SetInteger("State", 1);
            animator.SetFloat("MoveY", direction.y);
            animator.SetFloat("MoveX", direction.x);
        }
        else
        {
            animator.SetInteger("State", 0);
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", 0);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HurtBlend")) return;
        enemyRb.MovePosition(enemyRb.position + nextVec);
        enemyRb.linearVelocity = Vector2.zero;
    }
    public void TakeDamage(float Damage)
    {
        health -= Damage;
        StartCoroutine(KnockBack());
        if (health > 0)
        {
            animator.SetInteger("State", 3);
        }
        else
        {
            isDie = true;
            coll.enabled = false;
            enemyRb.simulated = false;
            animator.SetInteger("State", 4);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp(exp);
        }
    }
    IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        enemyRb.AddForce(dirVec.normalized * 0.5f, ForceMode2D.Impulse);
        
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    public void Init(SpawnData data)
    {
        maxHealth = data.health;
        health = data.health;
        moveSpeed = data.speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon")) return;
        TakeDamage(collision.GetComponent<Weapon>().damage);
    }
}
