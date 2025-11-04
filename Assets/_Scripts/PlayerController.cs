using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2Int startGridPos = new Vector2Int(0, 0);

    private PlayerMovement movement;
    private PlayerInputHandler inputHandler;
    private PlayerAnimator animController;

    private bool isJumping = false;

    private void Awake()
    {
        inputHandler = new PlayerInputHandler();
        animController = new PlayerAnimator(animator);
    }

    private void Start()
    {
        if (gridManager == null)
        {
            Debug.LogError("⚠️ Chưa gán GridManager cho PlayerController!");
            return;
        }

        if (animator == null)
            animator = GetComponent<Animator>();

        movement = new PlayerMovement(gridManager, transform, startGridPos, moveSpeed);
    }

    private void Update()
    {
        Vector2Int moveDir = inputHandler.ReadMoveInput();
        if (moveDir != Vector2Int.zero)
            movement.TryMove(moveDir);

        if (inputHandler.ReadJumpInput() && !movement.IsMoving && !isJumping)
        {
            Vector3 forward = transform.forward;
            Vector2Int jumpDir = WorldToGridDirection(forward);
            if (movement.TryMove(jumpDir))
            {
                animController.TriggerJump();
                isJumping = true;
            }
        }

        movement.UpdateMovement(Time.deltaTime);

        if (!movement.IsMoving && isJumping)
            isJumping = false;

        animController.UpdateAnimation(movement.IsMoving, isJumping);
    }

    private Vector2Int WorldToGridDirection(Vector3 forward)
    {
        forward.y = 0;
        forward.Normalize();

        if (Vector3.Dot(forward, Vector3.right) > 0.7f) return Vector2Int.right;
        if (Vector3.Dot(forward, Vector3.left) > 0.7f) return Vector2Int.left;
        if (Vector3.Dot(forward, Vector3.forward) > 0.7f) return Vector2Int.up;
        if (Vector3.Dot(forward, Vector3.back) > 0.7f) return Vector2Int.down;
        return Vector2Int.zero;
    }
}
