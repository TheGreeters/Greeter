using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour {

    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;

    public float Rate = 1f;

    private float TimeSinceSpawn = 0f;
    private float MinChance = 0f;

 
	void Update () {
        //Don't do anything if the game is paused
        if (Time.timeScale > 0)
        {
            TimeSinceSpawn += Time.deltaTime;
            MinChance += Mathf.Exp(2 * Rate) * Random.Range(0.6f, 1f) * TimeSinceSpawn / 1000;
            if (Random.Range(MinChance, 1f) >= 1)
            {
                Spawn();
                TimeSinceSpawn = 0f;
                //Every now and then, don't reset minChance completely so you may get more bunches of customers
                if(Random.Range(0f, 1f) >= 0.90f)
                {
                    MinChance = MinChance / 2;
                }
                MinChance = 0f;
            }
            if (Random.Range(0, 300) >= 299) //0.33% chance (E.g. Rate is updated randomly once every ~300 frames)
            {
                Rate = Mathf.Clamp(Rate + Random.Range(-0.2f, 0.3f), 1f, 3f);
            }
        }
    }

    void Spawn()
    {
        Component[] spawner = gameObject.GetComponentsInChildren(typeof(Spawner));
        int index = UnityEngine.Random.Range(0, spawner.Length);
        Spawner spawnScript = (Spawner)spawner[index];
        spawnScript.Spawn();
    }
}
