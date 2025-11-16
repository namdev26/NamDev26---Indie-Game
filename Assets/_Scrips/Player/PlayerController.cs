using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    //[SerializeField] private float _lookSpeed = 5f;
    private Vector3 _input;

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Look() 
    {
        if (_input != Vector3.zero) 
        {
            var relative = (transform.position + _input.ToIsometric()) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
        }
    }

    void GatherInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _input = new Vector3(h, 0f, v).normalized;
    }

    void Move() 
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _moveSpeed * Time.deltaTime);
    }
}
