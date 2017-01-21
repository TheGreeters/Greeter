using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour {

    static Spawner[] Spawns = new Spawner[2];

    public float Rate = 1;

    void Start()
    {
        //Get the spawn points and add them to the array of Spawners
        for(var i = 0; i < transform.childCount; i++)
        {
            Spawns[i] = (Spawner)transform.GetChild(i).GetComponent(typeof(Spawner));
            Spawns[i].SpawnId = i + 1;
        }
    }
 
	void Update () {
        if (Random.Range(0f, 1f) >= 1 - (Rate / 100))
        {
            Component[] spawner = gameObject.GetComponentsInChildren(typeof(Spawner));
            int index = UnityEngine.Random.Range(0, spawner.Length - 1);
            Spawner spawnScript = (Spawner)spawner[index];
            spawnScript.Spawn();
        }
    }
}
