using UnityEngine;

public class Movement: MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    [SerializeField]
    private float moveSpeed = 2f; 

    private CharacterController characterController;
    private Joystick joystick;
    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        joystick = FindObjectOfType<Joystick>(); 
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    { 
        float horizontalInput = joystick.Horizontal + Input.GetAxis("Horizontal");
        float verticalInput = joystick.Vertical + Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
            animator.SetBool("isRunning", true);  
        }
        else if (moveDirection == Vector3.zero)
        {
            animator.SetBool("isRunning", false);
        }

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
