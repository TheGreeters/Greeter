using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    [HideInInspector]
    public int Lane = 1; //Set a default just in case

    [HideInInspector]
    public bool Greeted = false;

    private const int DespawnY = -16; //Adjust as needed depending on sprite height
    
	void Update () {
		if(transform.position.y < DespawnY)
        {
            GameObject.Destroy(gameObject);
        }
	}
}
