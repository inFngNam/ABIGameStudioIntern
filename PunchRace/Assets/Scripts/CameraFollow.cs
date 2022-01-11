using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    public float smoothSpeed = 0.125f;

    [SerializeField]
    public Vector3 offset;

    //    private Vector3 rotationPosition = new Vector3(20f, 0, 0);

    public void Start()
    {
    //    transform.rotation = rotationPosition;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
