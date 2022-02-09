using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float positionY;
    private float transformHeight = 0.1f;
    private float transformSpeed = 2.0f;

    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
        positionY = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, positionY + Mathf.Sin(Time.time * transformSpeed) * transformHeight, transform.position.z);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
