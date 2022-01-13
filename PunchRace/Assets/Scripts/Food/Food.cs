using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private float respawnTime = 2f;
    
    [SerializeField]
    private float transformSpeed = 0.5f;

    private float positionY;
    private float transformHeight = 0.1f;

    private float cooldown = 0.0f;
    private bool isEnabled = true;
    private Vector3 rotateVector = new Vector3(0f, 100f, 0f);

    private Collider collider;
    private Renderer renderer;

    private void Start()
    {
        positionY = transform.position.y;
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!isEnabled)
        {
            cooldown += Time.deltaTime;
            if (cooldown % 60 >= respawnTime)
            {
                collider.enabled = true;
                renderer.enabled = true;
                isEnabled = true;
                cooldown = 0.0f;
            }
        }
        transform.position = new Vector3(transform.position.x, positionY + Mathf.Sin(Time.time * transformSpeed) * transformHeight, transform.position.z);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isEnabled)
            {
                renderer.enabled = false;
                collider.enabled = false;
                isEnabled = false;
            }
        }
    }
}
