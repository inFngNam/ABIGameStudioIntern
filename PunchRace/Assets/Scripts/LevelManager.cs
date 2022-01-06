using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int requiredFoods;
    private int totalEnemies;
    private int level;

    // Start is called before the first frame update
    private void Start()
    {       
    	requiredFoods = GameManager.Instance.GetRequiredFoods();
        totalEnemies = GameManager.Instance.GetTotalEnemies();
        level = GameManager.Instance.GetLevel();
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
