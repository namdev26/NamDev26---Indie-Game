using UnityEngine;
using UnityEngine.EventSystems;

public class OrthoIsoCameraController : MonoBehaviour
{
    [Header("Camera Control")]
    public float panSpeed = 0.5f;
    public float zoomSpeed = 20f;
    public float minSize = 5f;
    public float maxSize = 40f;

    [Header("Lock When UI Open")]
    public bool uiOpen = false;   // G?i true khi m? Panel, false khi ?óng Panel

    private Vector3 dragOrigin;
    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (uiOpen) return;   // N?u ?ang m? UI ? khóa camera hoàn toàn

        Pan();
        Zoom();
    }

    void Pan()
    {
        // N?u chu?t ?ang n?m trên UI ? không cho pan
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(1))
            dragOrigin = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - dragOrigin;

            Vector3 right = cam.transform.right;
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
        // N?u chu?t ?ang n?m trên UI ? không zoom
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.001f)
        {
            cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
        }
    }
}
