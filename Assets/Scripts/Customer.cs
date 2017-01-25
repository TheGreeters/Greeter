using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {

    [HideInInspector]
    public int Lane;
    
    public bool Greeted = false;

    public bool FailedGreeting = false;

    public bool CompletedReaction = false;

    // Low mood means they are happy, high mood means they are angry
    public int Mood;

    private const int DespawnZ = 5; //Adjust as needed depending on sprite height

    private const float Speed = 3f;

    private Vector3 target = new Vector3(0, 0, 0);

    private void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y, -100);
    }

    void Update()
    {
        if (transform.position.z <= DespawnZ)
        {
            if(!Greeted && !FailedGreeting)
            {
                GameController.AddSatisfaction(-1);
            }

            GameObject.Destroy(gameObject);
        }

        if (!CompletedReaction && Greeted)
        {
            GameObject.Find("ResponseLabel").GetComponent<Text>().text = "Good wave! Keep it up!";
            GameObject.Find("ResponseLabel").GetComponent<Text>().color = Color.green;
            UpdateAnimator(1); //wave
        }
        else if (!CompletedReaction && FailedGreeting)
        {
            if(transform.position.z < 13)
            {
                GameObject.Find("ResponseLabel").GetComponent<Text>().text = "Awkward. Too close!";
                GameObject.Find("ResponseLabel").GetComponent<Text>().color = Color.red;
            }
            else
            {
                GameObject.Find("ResponseLabel").GetComponent<Text>().text = "Awkward. Too far!";
                GameObject.Find("ResponseLabel").GetComponent<Text>().color = Color.grey;
            }
            
            UpdateAnimator(2); //shake
        }

        Animator anim = gameObject.GetComponent<Animator>();
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("person_01_walk"))
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

			//After completing a reaction and starting the walk again, set customer alpha so it is evident they have been waved at.
			//Also when the customer passes the player so it is evident they are not eligible to be waved at.
			if(CompletedReaction || transform.position.z < GameObject.FindGameObjectWithTag("Player").transform.position.z)
			{
				Color spriteColor = GetComponent<SpriteRenderer>().color;
				GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.6f);
			}
        }
        else if(!CompletedReaction && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            CompletedReaction = true;
            UpdateAnimator(0);
		}
    }

    void UpdateAnimator(int state)
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetInteger("State", state);
    }

    //public static void UpdateCustomerAudioClip()
    //{
    //    GameObject.LoadAudioClip();
    //}

    //private void LoadAudioClip()
    //{
        
    //}
}
