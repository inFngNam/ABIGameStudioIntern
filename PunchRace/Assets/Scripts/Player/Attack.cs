using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    private bool isKick = false;
    public bool kicked = false;
    private float kickAnimationTime = 1.15f;
    private float kickCooldown = 0.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        ReturnToIdle();
    }

    private void OnControllerColliderHit(ControllerColliderHit hitCollision)
    {
        if (hitCollision.gameObject.CompareTag("Bot") || hitCollision.gameObject.CompareTag("Box"))
        {
            Kick();
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
