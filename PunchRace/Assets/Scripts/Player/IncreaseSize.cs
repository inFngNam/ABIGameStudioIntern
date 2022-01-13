using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour
{
    [SerializeField]
    private int totalIncreaseTime = 3;
    
    [SerializeField]
    private float increaseValue = 0.25f;

    private int totalAteFoods = 0;

    private void OnControllerColliderHit(ControllerColliderHit hitCollision)
    {
        if (hitCollision.gameObject.CompareTag("Food"))
        {
            hitCollision.transform.SendMessage("Eaten");
            if (totalIncreaseTime > 0)
            {
                transform.localScale += Vector3.one * increaseValue;
                totalIncreaseTime--;
            }
            totalAteFoods++;
        }
    }
}
