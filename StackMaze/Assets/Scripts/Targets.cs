using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    private bool enable = false;

    private void Update()
    {
        if (enable)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().enabled = true;
            }
            enable = false;
        }
    }
    
    public void EnableAll()
    {
        enable = true;
    }
}
