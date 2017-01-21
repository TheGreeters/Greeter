using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    public int LaneSize = 200;
    public GameObject CustomerPrefab;

    [HideInInspector]
    public int SpawnId = 0; //This will be set by the SpawnManager

    private const int PlayerWidth = 14;
    
    public void Spawn()
    {
        int offset = Random.Range((int)-(LaneSize/2) + (PlayerWidth/2), (LaneSize / 2) - (PlayerWidth / 2));
        offset += (PlayerWidth / 2) * (int)Mathf.Sign(offset);
        
        GameObject newCustomer = Instantiate(CustomerPrefab, new Vector3(gameObject.transform.position.x + offset, 0, 0), new Quaternion());
        Customer customerScript = (Customer)newCustomer.GetComponent(typeof(Customer));
        customerScript.Lane = SpawnId;
        //TODO: Set the sprite of newly spawned customer
    }
}
