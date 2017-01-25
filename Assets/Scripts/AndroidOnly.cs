using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidOnly : MonoBehaviour {

	void Awake()
	{
		gameObject.SetActive(Application.platform == RuntimePlatform.Android);
	}

}
