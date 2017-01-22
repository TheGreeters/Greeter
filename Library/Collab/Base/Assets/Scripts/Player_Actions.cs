using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Player_Actions : MonoBehaviour {

    int currentLane;
    
    public float happyZoneMin;

    public float happyZoneMax;

    private Transform target;

    public float speed = 10.0f;

    // Use this for initialization
    void Start () {

        currentLane = 2;

        happyZoneMin = gameObject.transform.position.z + 4f;

        happyZoneMax = gameObject.transform.position.z + 8f;

	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else
        {
            StayStill();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Wave();
        }

    }

    void MoveLeft()
    {
        string newLaneName = "Lane" + (currentLane - 1).ToString();

        GameObject newLane = GameObject.Find(newLaneName);

        if (newLane != null)
        {
            currentLane--;

            target = newLane.transform;

            UpdateAnimator(1);
        }

        
    }

    void MoveRight()
    {
        string newLaneName = "Lane" + (currentLane + 1).ToString();

        GameObject newLane = GameObject.Find(newLaneName);

        if (newLane != null)
        {
            currentLane++;

            target = newLane.transform;

            UpdateAnimator(1);
        }
    }

    void StayStill()
    {
        UpdateAnimator(0);
    }

    void Wave()
    {
        UpdateAnimator(2);

        GameObject[] customerArray = GameObject.FindGameObjectsWithTag("CustomerLane" + (currentLane).ToString());

        bool inHappyZone = false;

        if (customerArray != null && customerArray.Length > 0)
        {
            GameObject lanes = GameObject.Find("Lanes");

            foreach (GameObject currentCustomer in customerArray)
            {

                if (happyZoneMin < currentCustomer.transform.position.z 
                    && currentCustomer.transform.position.z < happyZoneMax
                    && currentCustomer.transform.position.z + lanes.transform.position.z > gameObject.transform.position.z
                    && (!currentCustomer.GetComponent<Customer>().Greeted 
                        || !currentCustomer.GetComponent<Customer>().FailedGreeting))
                {
                    inHappyZone = true;

                    Customer customerScript = currentCustomer.GetComponent<Customer>();

                    customerScript.Greeted = true;

                    currentCustomer.tag = "CustomerWavedAt";

                    break;
                }

                

            }

            if (inHappyZone)
            {
                GameController.AddScore(1);
                GameController.AddSatisfaction(1);
            }
            else
            {
                GameController.AddScore(-3);
                GameController.AddSatisfaction(-3);

                GameObject disappointedCustomer;

                disappointedCustomer = customerArray[0];

                foreach (GameObject currentCustomer in customerArray)
                {
                    if (currentCustomer.transform.position.z < disappointedCustomer.transform.position.z
                        && currentCustomer.transform.position.z + lanes.transform.position.z > gameObject.transform.position.z
                        && (!currentCustomer.GetComponent<Customer>().Greeted
                            || !currentCustomer.GetComponent<Customer>().FailedGreeting))
                    {
                        disappointedCustomer = currentCustomer;
                    }
                }

                Customer customerScript = disappointedCustomer.GetComponent<Customer>();

                customerScript.FailedGreeting = true;

                disappointedCustomer.tag = "CustomerWavedAt";
            }
        }
 
    }

    void UpdateAnimator(int state)
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetInteger("State", state);
    }
}
