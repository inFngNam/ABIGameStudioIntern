using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
	[SerializeField]
	public PhysicMaterial physicMaterial;
	private bool onFire;
	private bool force;

	// Start is called before the first frame update
	void Start()
	{
		int totalLayers = LevelVariables.getTotalLayers();
		gameObject.transform.position = new Vector3(0, (float) ((totalLayers + 5) * 0.35) + gameObject.transform.localScale.y / 2, -2);
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetButton("Fire1"))
		{
			onFire = true;
			GetComponent<Collider>().material = null;
			GetComponent<Rigidbody>().AddForce(transform.up * -5f);
		}
		
		if (!Input.GetButton("Fire1"))
		{
			onFire = false;
			GetComponent<Collider>().material = physicMaterial;
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.tag == "Layer")
		{
			if (onFire)
			{
				if (collision.gameObject.tag == "Destroyable")
				{
					Destroy(collision.gameObject.transform.parent.gameObject);
				}
				else if (collision.gameObject.tag == "Undestroyable")
				{
					LevelVariables.reset();
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
			else
			{
				GetComponent<Rigidbody>().AddForce(transform.up * 5f);
			}
		} 
		else if (collision.gameObject.tag == "Ground")
		{	
			Debug.Log("IN");
			LevelVariables.levelUp();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
