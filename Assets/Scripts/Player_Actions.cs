using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Player_Actions : MonoBehaviour {

    int currentLane;

    public int numberOfCustomerResponsesHappy = 6;
    public int numberOfCustomerResponsesAngry = 0;
    
    public float happyZoneMin;
    public float happyZoneMax;

    public float smallHappyZoneMin;
    public float smallHappyZoneMax;

    public float largeHappyZoneMin;
    public float largeHappyZoneMax;

    public int failMultiplier = 3;
    public int successMultiplier = 1;

    //private string audioTest = "Sounds/intro_music_gjt";

    //private string audioTest = "";

    private Transform target;

    public float speed = 10.0f;

    // Use this for initialization
    void Start () {

        currentLane = 2;

        smallHappyZoneMin = gameObject.transform.position.z + 5f;
        smallHappyZoneMax = gameObject.transform.position.z + 6f;

        happyZoneMin = gameObject.transform.position.z + 4f;
        happyZoneMax = gameObject.transform.position.z + 8f;

        largeHappyZoneMin = gameObject.transform.position.z + 4f;
        largeHappyZoneMax = gameObject.transform.position.z + 8f;

	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo stateInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            if (Vector3.Distance(transform.position, targetPosition) > 0)
            {
                float step = speed * Time.deltaTime;
                gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            }
            else
            {
                target = null;
                if (!stateInfo.IsName("Player_Wave"))
                {
                    StayStill();
                }
            }
        }
        
        if(stateInfo.IsName("Player_Wave") && stateInfo.normalizedTime >= 1)
        {
            StayStill();
        }

    }

    public void MoveLeft()
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

    public void MoveRight()
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

    public void Wave()
    {
        UpdateAnimator(2);

        GameObject[] customerArray = GameObject.FindGameObjectsWithTag("CustomerLane" + (currentLane).ToString());

        bool inHappyZone = false;

        int mood = 0;

        float customerPosition;

        if (customerArray != null && customerArray.Length > 0)
        {
            GameObject lanes = GameObject.Find("Lanes");

            foreach (GameObject currentCustomer in customerArray)
            {

                mood = GetCustomerMood(currentCustomer);

                customerPosition = currentCustomer.transform.position.z;


                if (CustomerInHappyZone(currentCustomer)
                    && customerPosition + lanes.transform.position.z > gameObject.transform.position.z
                    && (!currentCustomer.GetComponent<Customer>().Greeted 
                        || !currentCustomer.GetComponent<Customer>().FailedGreeting))
                {
                    inHappyZone = true;

                    Customer customerScript = currentCustomer.GetComponent<Customer>();

                    customerScript.Greeted = true;

                    currentCustomer.tag = "CustomerWavedAt";

                    UpdateCustomerAudioClip(currentCustomer, inHappyZone);

                    break;
                }

                

            }

            if (inHappyZone)
            {
                GameController.AddScore(mood * successMultiplier);
                GameController.AddSatisfaction(mood); 
            }
            else
            {
                //GameController.AddScore(mood * failMultiplier);
                GameController.AddSatisfaction(mood * failMultiplier);

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

                //UpdateCustomerAudioClip(disappointedCustomer, inHappyZone);
            }


        }
 
    }

    int GetCustomerMood(GameObject currentCustomer)
    {
        Customer customerScript = (Customer)currentCustomer.GetComponent(typeof(Customer));

        return customerScript.Mood;
    }

    bool CustomerInHappyZone(GameObject currentCustomer)
    {
        float customerPosition = currentCustomer.transform.position.z;

        int customerMood = GetCustomerMood(currentCustomer);

        if (customerPosition > smallHappyZoneMin && customerPosition < smallHappyZoneMax)
        {
            return true;
        }
        else if (customerPosition > largeHappyZoneMin && customerPosition < largeHappyZoneMax && customerMood < 3)
        {
            return true;
        }
        else if (customerPosition > largeHappyZoneMin && customerPosition < largeHappyZoneMax && customerMood < 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void UpdateAnimator(int state)
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetInteger("State", state);
    }

    void UpdateCustomerAudioClip(GameObject currentCustomer, bool inHappyZone)
    {
        string audioClipName;
        AudioClip soundClip;
        AudioSource customerAudio = currentCustomer.GetComponent<AudioSource>();

        int clipNum;

        clipNum = UnityEngine.Random.Range(1, numberOfCustomerResponsesHappy + 1);

        audioClipName = "Sounds/voice" + clipNum;

        soundClip = Resources.Load<AudioClip>(audioClipName);

        customerAudio.clip = soundClip;

        //if (inHappyZone)
        //{
        //    clipNum = UnityEngine.Random.Range(1, numberOfCustomerResponsesHappy + 1);

        //    audioClipName = "happy" + clipNum;

        //    soundClip = Resources.Load<AudioClip>(audioClipName);

        //    customerAudio.clip = soundClip;
        //}
        //else
        //{
        //    clipNum = UnityEngine.Random.Range(1, numberOfCustomerResponsesAngry + 1);

        //    audioClipName = "angry" + clipNum;

        //    soundClip = Resources.Load<AudioClip>(audioClipName);

        //    customerAudio.clip = soundClip;
    

        customerAudio.Play();
    }
}
