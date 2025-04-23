using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    public float moveSpeed = 1f;
    private Rigidbody2D playerRb;
    public Vector2 moveInput;
    public Animator animator;
    public SpriteRenderer[] weapons;

    public Scanner scanner;
    #endregion
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveY", -1);     //현재 상태 앞모습
        scanner = GetComponent<Scanner>();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        Vector2 nextVec = moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
        playerRb.MovePosition(playerRb.position + nextVec);
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("IsMove", isMoving);

        if (isMoving)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) return;
        GameManager.Instance.health -= Time.deltaTime * 10;
        if(GameManager.Instance.health < 0)
        {
            for(int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
}
