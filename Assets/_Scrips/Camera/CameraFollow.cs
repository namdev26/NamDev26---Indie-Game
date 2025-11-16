using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform target;

    [Header("Settings Camera")]
    public Vector3 offset = new Vector3(0, 0, 0);
    public float flowSpeed = 5f;
    public float lookSpeed = 10f;

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position + offset,
            flowSpeed * Time.deltaTime
        );

        Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            lookSpeed * Time.deltaTime
        );
    }

}
