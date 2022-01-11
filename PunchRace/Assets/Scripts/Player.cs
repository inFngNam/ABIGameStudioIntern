using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    [SerializeField]
    private float moveSpeed = 2f;
  
    private CharacterController characterController;
    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // get input
    }

    private void FixedUpdate()
    {
        Move(); 
    }

    private void Move()
    { 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, verticalInput) * moveSpeed;

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isRunning", true);  
        }
        else if (moveDirection == Vector3.zero)
        {
            animator.SetBool("isRunning", false);
        }

        characterController.Move(moveDirection * Time.deltaTime);

//        if (horizontalVelocity == 0 && verticalVelocity == 0)
//        {
//        }
//        else 
//        {
//            animator.SetBool("isRunning", true);
//        }
    }

    private void OnCollisionEnter(Collision collision) 
    {

    }

}
