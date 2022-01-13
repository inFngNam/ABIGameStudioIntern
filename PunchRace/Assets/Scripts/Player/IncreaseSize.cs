using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour
{
    [SerializeField]
    private int totalIncreaseTime = 3;
    
    [SerializeField]
    private float increaseValue = 0.25f;

    private void OnControllerColliderHit(ControllerColliderHit hitCollision)
    {
        if (hitCollision.gameObject.CompareTag("Food") && totalIncreaseTime > 0)
        {
            transform.localScale += Vector3.one * increaseValue;
            totalIncreaseTime--;
        }
    }
}
