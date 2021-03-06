﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;                          //Store a reference to the Game Object OptionsTint 
	public GameObject optionsMusicSlider;                   //Store a reference to the Game Object OptionsMusicVolSlider 
	public GameObject optionsSfxSlider;                     //Store a reference to the Game Object OptionsSfxVolSlider
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;                           //Store a reference to the Game Object PausePanel 
	public GameObject pauseMusicSlider;						//Store a reference to the Game Object MusicVolSlider 
	public GameObject pauseSfxSlider;                       //Store a reference to the Game Object SfxVolSlider
	public AudioMixer mainMixer;                            //Used to hold a reference to the AudioMixer mainMixer

	public GameObject androidResume;

	public void Start()
	{
		//Load the player's preferences for their audio levels
		SetAudioLevels setAudio = optionsPanel.GetComponent<SetAudioLevels>();
		if (PlayerPrefs.HasKey("MusicVol"))
		{
			setAudio.SetMusicLevel(PlayerPrefs.GetFloat("MusicVol"));
		}
		if (PlayerPrefs.HasKey("SfxVol"))
		{
			setAudio.SetSfxLevel(PlayerPrefs.GetFloat("SfxVol"));
		}
	}

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		float musicVol = 0f;
		mainMixer.GetFloat("musicVol", out musicVol);
		optionsMusicSlider.GetComponent<Slider>().value = musicVol;

		float sfxVol = 0f;
		mainMixer.GetFloat("sfxVol", out sfxVol);
		optionsSfxSlider.GetComponent<Slider>().value = sfxVol;

		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		float musicVol = 0f;
		mainMixer.GetFloat("musicVol", out musicVol);
		pauseMusicSlider.GetComponent<Slider>().value = musicVol;

		float sfxVol = 0f;
		mainMixer.GetFloat("sfxVol", out sfxVol);
		pauseSfxSlider.GetComponent<Slider>().value = sfxVol;

		if (Application.platform == RuntimePlatform.Android)
		{
			androidResume.SetActive(true);
		}

		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);
	}
}
