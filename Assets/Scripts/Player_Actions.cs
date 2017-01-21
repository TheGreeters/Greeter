using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Player_Actions : MonoBehaviour {

    int currentLane;

	// Use this for initialization
	void Start () {

        currentLane = 1;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Wave();
        }

    }

    void MoveLeft()
    {
        float newPosition;

        GameObject newLane;

        string newLaneName;

        newLaneName = "Lane" + (currentLane - 1).ToString();

        newLane = GameObject.Find(newLaneName);

        if (newLane != null)
        {
            currentLane--;

            Console.Write("newLane");

            newPosition = newLane.transform.position.x;

            transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        }
    }

    void MoveRight()
    {
        float newPosition;

        GameObject newLane;

        string newLaneName;

        newLaneName = "Lane" + (currentLane + 1).ToString();

        newLane = GameObject.Find(newLaneName);

        if (newLane != null)
        {
            currentLane++;

            Console.Write("newLane");

            newPosition = newLane.transform.position.x;

            transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        }

        
    }

    void Wave()
    {
        // Insert Waving code here
    }
}
