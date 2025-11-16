//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    [Header("References")]
//    [SerializeField] private Animator animator;

//    [Header("Movement Settings")]
//    [SerializeField] private float moveSpeed = 5f;

//    private PlayerMovement movement;
//    private PlayerInputHandler inputHandler;
//    private PlayerAnimator animController;

//    private bool isJumping = false;

//    private void Awake()
//    {
//        inputHandler = new PlayerInputHandler();
//        animController = new PlayerAnimator(animator);
//    }

//    private void Start()
//    {
//        movement = new PlayerMovement(transform, moveSpeed);
//    }

//    private void Update()
//    {
//        // Đọc input di chuyển
//        Vector2 moveDir = inputHandler.ReadMoveInput();
//        movement.Move(moveDir);

//        // Input nhảy
//        if (inputHandler.ReadJumpInput() && !isJumping)
//        {
//            animController.TriggerJump();
//            isJumping = true;
//        }

//        // Giả lập reset jump (bạn có thể thay bằng OnGround check)
//        if (isJumping && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
//            isJumping = false;

//        animController.UpdateAnimation(movement.IsMoving, isJumping);
//    }
//}
