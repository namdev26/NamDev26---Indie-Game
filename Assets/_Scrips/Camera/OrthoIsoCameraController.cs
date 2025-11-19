using UnityEngine;

public class OrthoIsoCameraController : MonoBehaviour
{
    public float panSpeed = 0.5f;
    public float zoomSpeed = 20f;
    public float minSize = 5f;
    public float maxSize = 40f;

    private Vector3 dragOrigin;
    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Pan();
        Zoom();
    }

    void Pan()
    {
        if (Input.GetMouseButtonDown(1))
            dragOrigin = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - dragOrigin;

            Vector3 right = cam.transform.right;   // ngang theo camera
            Vector3 forward = cam.transform.forward;

            right.y = 0;
            forward.y = 0;
            right.Normalize();
            forward.Normalize();

            Vector3 move =
                (-delta.x * right +
                 -delta.y * forward) * panSpeed * Time.deltaTime;

            transform.position += move;

            dragOrigin = Input.mousePosition;
        }
    }

    void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
    }
}
