using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour,IDamageable
{
    #region Variables
    public EnemyState enemyState;
    public float currentHp;
    private Rigidbody2D player;

    private Rigidbody2D enemyRb;
    public Animator animator;
    #endregion
    private void OnEnable()
    {
        player = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentHp = enemyState.maxHp;
        enemyRb = GetComponent<Rigidbody2D>();
        if(!enemyRb.freezeRotation)
        {
            enemyRb.freezeRotation = true;
        }
        animator.SetFloat("MoveY", -1); //현재 상태 앞모습
    }
    private void FixedUpdate()
    {
        if (player == null) return;
        Vector2 direction = (player.position - enemyRb.position).normalized;
        Vector2 nextVec = direction * enemyState.moveSpeed * Time.fixedDeltaTime;
        enemyRb.MovePosition(enemyRb.position + nextVec);
        enemyRb.linearVelocity = Vector2.zero;
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
    }
    public void TakeDamage(float Damage)
    {
        currentHp -= Damage;
        if(currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }
}
