using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    public GameObject CustomerPrefab;
    
    public int SpawnId = 1; //IMPORTANT: Make sure to set this unique for each spawner in the inspector

    private const float PlayerWidth = 0.75f;
    private const float LaneSize = 1.4f;
    private const int NumberOfNormalCustomerSprites = 15;

	private List<int> happySprites = new List<int>(new int[] { 3, 10 });
    private List<int> angrySprites = new List<int>(new int[] { 4, 8 });

    public void Spawn()
    {
        //Determine position to spawn at
        int offset = Random.Range((int)PlayerWidth, (int)(PlayerWidth + LaneSize));

        //Create new customer
        Vector3 position = new Vector3(gameObject.transform.position.x + offset, gameObject.transform.position.y, gameObject.transform.position.z);
        GameObject newCustomer = Instantiate(CustomerPrefab, position, new Quaternion());
        newCustomer.transform.parent = GameObject.Find("Lane" + SpawnId).transform;
        
        //Set Lane ID for simplicity
        Customer customerScript = (Customer)newCustomer.GetComponent(typeof(Customer));
        customerScript.Lane = SpawnId;

        //Choose sprite for customer
        int spriteNumber = ChooseSprite();

        //Set sprite of customer
        SpriteRenderer spriteRender = newCustomer.GetComponent<SpriteRenderer>();

        string spriteName;

        spriteName = "Sprites/person_" + (spriteNumber < 10 ? "0" + spriteNumber.ToString() : spriteNumber.ToString());
        
        spriteRender.sprite = Resources.Load<Sprite>(spriteName);

        //Set animator of customer
        Animator spriteAnimator = newCustomer.GetComponent<Animator>();

        string animatorName  = "Sprites/Customer" + (spriteNumber == 1 ? "" : spriteNumber.ToString());

        spriteAnimator.runtimeAnimatorController = Resources.Load(animatorName) as RuntimeAnimatorController;

        //Set mood for customer
        if (happySprites.IndexOf(spriteNumber) != -1)
        {
            customerScript.Mood = 1;
        }
        else if (angrySprites.IndexOf(spriteNumber) != -1)
        {
            customerScript.Mood = 3;
        }
        else
        {
            customerScript.Mood = 2;
        }

        //Set tag for customer
        newCustomer.tag = "CustomerLane" + SpawnId.ToString();
    }

    private int ChooseSprite()
    {
		//NOTE: This method relies on all special customers being sequentially numbered AFTER all normal customers.
		int result = 1;
		switch(Random.Range(0, 100)) //NOTE: excludes 100... 1% chance for each result from 0-99
		{
			case 99:
				result = 16; //Alien
				break;
			case 98:
				result = 17; //Toga Man
				break;
			default:
				//98% of the time, just spawn a normal customer
				result = Random.Range(1, NumberOfNormalCustomerSprites + 1);
				break;
		}
		return result;

    }
}
