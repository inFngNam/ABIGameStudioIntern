using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
	public GameObject layerTemplate;

	// Start is called before the first frame update
	void Start()
	{
		int totalLayers = LevelVariables.getTotalLayers();
		for (int index = 0; index < totalLayers; index++)
		{
	            Vector3 position = new Vector3(0, (float) ((index + 3) * 0.35) , 0);
        	    GameObject layer = Instantiate(layerTemplate, position, layerTemplate.transform.rotation);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
