using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2Int startGridPos = new Vector2Int(0, 0); // ✅ Mặc định tile (0,0)

    private PlayerMovement movement;
    private PlayerInputHandler inputHandler;

    private void Awake()
    {
        inputHandler = new PlayerInputHandler();
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

        // ✅ Khởi tạo player tại tile (0,0)
        movement = new PlayerMovement(gridManager, transform, startGridPos, moveSpeed);
    }

    private void Update()
    {
        Vector2Int moveDir = inputHandler.ReadMoveInput();
        if (moveDir != Vector2Int.zero)
            movement.TryMove(moveDir);

        movement.UpdateMovement(Time.deltaTime);
        animator.SetBool("isWalking", movement.IsMoving);
    }
}
