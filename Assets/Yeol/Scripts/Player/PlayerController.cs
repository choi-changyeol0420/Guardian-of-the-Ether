using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 3f;
    private Rigidbody2D playerRb;
    public Vector2 moveInput;
    private Animator animator;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveY", -1);     //현재 상태 앞모습
    }
    private void FixedUpdate()
    {
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
}
