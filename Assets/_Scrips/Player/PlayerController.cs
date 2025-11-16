using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float runMultiplier = 1.6f;

    private Vector3 _input;

    private void Update()
    {
        GatherInput();
        Look();

        // ---- Animation ----
        float targetSpeed = 0f;

        if (_input.sqrMagnitude > 0.01f)
        {
            // Đi bộ
            targetSpeed = 0.5f;

            // Nhấn shift → chạy
            if (Input.GetKey(KeyCode.LeftShift))
                targetSpeed = 1f;
        }

        // Chuyển từ từ
        float smoothSpeed = Mathf.Lerp(animator.GetFloat("Speed"), targetSpeed, Time.deltaTime * 8f);
        animator.SetFloat("Speed", smoothSpeed);
    }


    private void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        if (_input.sqrMagnitude > 0.01f)
        {
            Vector3 lookDir = _input.ToIsometric();
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.2f);
        }
    }

    void GatherInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _input = new Vector3(h, 0, v).normalized;
    }

    void Move()
    {
        if (_input.sqrMagnitude > 0.01f)
        {
            // Get blend speed
            float animSpeed = animator.GetFloat("Speed");

            // Từ 0.5 - 1 → scale thành chạy
            float realSpeed = Mathf.Lerp(_moveSpeed, _moveSpeed * runMultiplier, animSpeed);

            Vector3 move = _input.ToIsometric() * realSpeed * Time.deltaTime;
            _rb.MovePosition(transform.position + move);
        }
    }

}
