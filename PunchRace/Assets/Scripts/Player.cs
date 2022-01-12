using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    // For movement
    [SerializeField]
    private float moveSpeed = 2f; 

    private CharacterController characterController;
    private Joystick joystick;
    private Vector3 moveDirection;

    // For kick
    private bool isKick = false;
    public bool kicked = false;
    private float kickAnimationTime = 1.15f;
    private float kickCooldown = 0.0f;

    // For increase size
    private int totalIncreaseSizeTime = 3;
    private Vector3 increaseSizeVector = new Vector3(0.25f, 0.25f, 0.25f);

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        joystick = FindObjectOfType<Joystick>(); 
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        ReturnToIdle();
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

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            IncreaseSize();
        }
        else if (collision.gameObject.CompareTag("Bot"))
        {
            Kick();
        }
    }

    private void IncreaseSize()
    {
        if (totalIncreaseSizeTime > 0)
        {
            transform.localScale += increaseSizeVector;
            totalIncreaseSizeTime--;
        }
    }

    private void Kick()
    {
        if (!isKick)
        {
            animator.SetBool("kick", true);
            isKick = true;
            kickCooldown = 0.0f;
            kicked = true;
        }
    }

    private void ReturnToIdle()
    {
        if (isKick)
        {
            if (kickCooldown % 60 >= kickAnimationTime)
            {
                animator.SetBool("kick", false);
                kickCooldown = 0.0f;
                isKick = false;
            }
            kickCooldown += Time.deltaTime;
        }
    }
}
