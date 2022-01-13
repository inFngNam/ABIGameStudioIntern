using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRightLeg : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BotBody"))
        {
            GameObject bot = GameObject.Find("[Bot]");
            bot.GetComponent<Animator>().enabled = false;
            bot.GetComponent<CapsuleCollider>().enabled = false;
            bot.transform.SendMessage("StartReloadScene");
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Vector3 forceDirection = collision.transform.position - transform.position;
                forceDirection.Normalize();
                rigidbody.AddForceAtPosition(forceDirection * 1000f, transform.position, ForceMode.Impulse);
            }
        }
    }
}
