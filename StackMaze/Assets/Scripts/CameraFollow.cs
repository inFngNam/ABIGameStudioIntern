using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public float smoothSpeed = 0.125f;

    [SerializeField]
    public Transform target;

    [SerializeField]
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
