using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int[] requiredFoodsOfLevels = new int[] { 5, 7, 9, 13, 15, 16, 20, 21, 23, 36 };
    private int level = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
    
    }

    public void LevelUp()
    {
        if (level < 9)
        {
            level = level + 1;
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetRequiredFoods()
    {
        return requiredFoodsOfLevels[level];
    }

    public int GetTotalEnemies()
    {
        return level < 3 ? 3 : Random.Range(3, 5);
    }
}
