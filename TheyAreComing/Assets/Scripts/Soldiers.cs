using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers : MonoBehaviour
{
    [SerializeField]
    public Camera mainCamera;

    private Rigidbody rigidbody;

    private bool turned = false;
    private bool autoMove = false;
    private bool stop = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void CheckIsStop()
    {

    }

    private void AutoMove()
    {

    }


    private void FollowCamera()
    {
        if (!turned)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 position = new Vector3(transform.position.x, transform.position.y, cameraPosition.z + 7.0f);
            transform.position = position;
        }
        else
        {

        }
    }


    private void Update()
    {
        CheckIsStop();
        if (!stop)
        {
            if (!autoMove)
            {
                FollowCamera();
            }
            else
            {
                AutoMove();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AutoMovePoint"))
        {
            autoMove = true;
        }

        if (collision.gameObject.CompareTag("Turn"))
        {
            autoMove = false;
            turned = true;
        }
    }

}
