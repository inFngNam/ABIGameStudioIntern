using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		List<int> randomNumbers = new List<int>();
		int totalBlack = 0;
		
		while(randomNumbers.Count < gameObject.transform.childCount - 1 || totalBlack == 0)
		{
			if (totalBlack == 0 && randomNumbers.Count == gameObject.transform.childCount - 1)
			{
				randomNumbers = new List<int>();
			}

			int index = Random.Range(0, gameObject.transform.childCount);
			if (!randomNumbers.Contains(index))
			{
				GameObject child = gameObject.transform.GetChild(index).gameObject;
				if (Random.Range(0, 3) != 1)
				{
					var childRenderer = child.GetComponent<Renderer>();
					childRenderer.material.SetColor("_Color", Color.black);
					child.tag = "Undestroyable";
					totalBlack++;
				}
				randomNumbers.Add(index);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
        	transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
	}
}