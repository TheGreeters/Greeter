using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour {
    public Vector3 target = new Vector3(0, 0, 0);
    public float speed = 1.0f;
    public Animator anim = new Animator();

    private Vector3 position;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        position = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        // Testing
        if (transform.position.z < 0)
        {
            Debug.Log("test");
            anim.SetInteger("State", 2);
        }
    }
}