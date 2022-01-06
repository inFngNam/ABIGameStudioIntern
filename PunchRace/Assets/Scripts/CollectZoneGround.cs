using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectZoneGround : MonoBehaviour
{
    [SerializeField]
    public int totalFoodsAtSameTime = 0;

    [SerializeField]
    public GameObject foodTemplate;
    
    private Hashtable positions;
    private Vector3 uniqueVector3 = new Vector3(-30f, 1.5f, -30f);

    private float startX = -5f;
    private float startZ;
    private float endX;
    private float endZ;

    // Start is called before the first frame update
    private void Start()
    {
        if (totalFoodsAtSameTime == 0)
        {
            totalFoodsAtSameTime = 4;
        }
        
        // Get startX, startZ, endX, endZ
     
        // foods
        InitFoods();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            var test = GameObject.FindGameObjectsWithTag("Food");
            GameObject a = test[0];
//            a.GetComponent<Food>.GetIndex();
            RemoveEatenFood(a);
            Destroy(test[0]);
        }
    }

    private void InitFoods()
    {
        positions = new Hashtable();

        // For unique value
        positions.Add(-1, uniqueVector3);

        for (int index = 0; index < totalFoodsAtSameTime; index++)
        {
            SpawnFood(index);
        }

    }

    public void RemoveEatenFood(GameObject eatenFood)
    {   
        int index = eatenFood.GetComponent<Food>().GetIndex();
        SpawnFood(index);
    }

    private void SpawnFood(int index)
    {
        positions.Remove(index);
        Vector3 position = uniqueVector3;
        while (positions.ContainsValue(position)) {
            Debug.Log(position.x);
            position.z = 0f;
            position.x = (float) startX;
        }
        
        Debug.Log(position);

        positions.Add(index, position);
        GameObject food = Instantiate(foodTemplate, position, foodTemplate.transform.rotation);
        food.GetComponent<Food>().SetIndex(index);
        food.transform.localScale = new Vector3(5f, 5f, 5f);
        food.tag = "Food";
        startX++;
    }

}
