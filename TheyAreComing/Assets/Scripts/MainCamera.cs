using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    public Transform turnTarget;

    [SerializeField]
    public float speed;

    [SerializeField]
    public float rotationSpeed;

    private Rigidbody rigidbody;

    private bool turn = false;
    private bool stop = false;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateCamera();
        Move();
    }

    private void Move()
    {
        if (!stop)
        {
            if (!turn)
            {
                rigidbody.velocity = new Vector3(0.0f, 0.0f, speed * -1.0f);
            }
            else
            {
                rigidbody.velocity = new Vector3(speed * 1.0f, 0.0f, 0.0f);
            }
        }
    }

    private void RotateCamera()
    {
        if (turn)
        {
            Vector3 direction = (turnTarget.position - transform.position).normalized;
            Quaternion rotationTarget = Quaternion.LookRotation(direction);
            transform.rotation =  Quaternion.Slerp(transform.rotation, rotationTarget, rotationSpeed *  Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Turn"))
        {
            turn = true;
        }

        if (collision.gameObject.CompareTag("Stop"))
        {
            stop = true;
        }
    }
}