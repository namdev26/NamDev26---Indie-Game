//using UnityEngine;

//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(Rigidbody))]
//public class PlayerMovement : MonoBehaviour
//{
//    [Header("Movement Settings")]
//    public float moveSpeed = 3f;
//    public Camera mainCamera;

//    private Animator animator;
//    private Rigidbody rb;
//    private Vector3 moveDirection;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        animator = GetComponent<Animator>();

//        if (mainCamera == null)
//            mainCamera = Camera.main;

//        // Giữ nhân vật trên mặt phẳng (không ngã)
//        rb.constraints = RigidbodyConstraints.FreezeRotation;
//    }

//    void Update()
//    {
//        // Đọc input từ bàn phím
//        float h = Input.GetAxisRaw("Horizontal");
//        float v = Input.GetAxisRaw("Vertical");

//        // Di chuyển isometric: xoay input theo hướng camera
//        Vector3 inputDir = new Vector3(h, 0, v).normalized;

//        if (inputDir.magnitude >= 0.1f)
//        {
//            // Chuyển hướng camera thành hướng di chuyển trong world
//            Vector3 camForward = mainCamera.transform.forward;
//            Vector3 camRight = mainCamera.transform.right;
//            camForward.y = 0;
//            camRight.y = 0;

//            moveDirection = (camForward * v + camRight * h).normalized;
//            transform.forward = moveDirection; // Hướng nhân vật về hướng di chuyển
//        }
//        else
//        {
//            moveDirection = Vector3.zero;
//        }

//        // Cập nhật tốc độ cho Animator
//        animator.SetFloat("Speed", moveDirection.magnitude * moveSpeed);
//    }

//    void FixedUpdate()
//    {
//        // Di chuyển bằng Rigidbody
//        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
//    }
//}
