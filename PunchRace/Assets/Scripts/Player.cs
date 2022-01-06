using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int a = 0;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (a == 100) 
        {
            // Debug.Log("Level: " + GameManager.Instance.GetLevel());
            // Debug.Log("Foods: " + GameManager.Instance.GetRequiredFoods());
            Debug.Log("Enemies: " + GameManager.Instance.GetTotalEnemies());
            GameManager.Instance.LevelUp();
            a=0;
        }
        a++;
    }
}
