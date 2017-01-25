using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    private bool swipeLeft = false;
    private bool swipeRight = false;
    private bool tapped = false;

	//Used to prevent menu navigation from causing your player to move when resuming
	//...Basically a nasty hack to get around an annoying bug.
	public bool InputEnabled = true;

    void Start()
    {
        dragDistance = Screen.width * 15 / 100; //dragDistance is 15% of screen width
    }

    void Update () {
        //Don't move if game paused
        if (Time.timeScale > 0 && InputEnabled)
        {
            DetectTouches();

            Player_Actions player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Actions>();

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || swipeRight)
            {
                player.MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || swipeLeft)
            {
                player.MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.Space) || tapped)
            {
                player.Wave();
            }
        }
    }

    void DetectTouches()
    {
        swipeLeft = false;
        swipeRight = false;
        tapped = false;

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 15% of the screen width
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            swipeRight = true;
                        }
                        else
                        {   //Left swipe
                            swipeLeft = true;
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    tapped = true;
                }
            }
        }
    }

	public void DisableInput()
	{
		InputEnabled = false;
	}

	public void EnableInput()
	{
		InputEnabled = true;
	}
}
