using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayLogin : MonoBehaviour {
	
	public Sprite LoggedInImage;
	public Sprite LoggedOutImage;

	private void Start()
	{
		if(Social.Active.GetType() == typeof(PlayGamesPlatform) && Social.localUser.authenticated)
		{
			gameObject.GetComponent<Image>().sprite = LoggedInImage;
		}
		else
		{
			gameObject.GetComponent<Image>().sprite = LoggedOutImage;
		}
	}

	public void Activate()
	{
		GameObject GPM = GameObject.Find("GooglePlayManager");
		if(GPM != null)
		{
			GPM.GetComponent<GooglePlayManager>().ToggleLogin();
		}
	}
}
