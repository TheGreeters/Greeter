using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static float GameSpeed = 1f;

    static float Satisfaction;
    static int Score;
    static int HighScore;

    static string gameOverMusic = "";
    static string backgroundMusic = "Sounds/intro_music_gjt";

    public const float MaxSpeed = 4f; //Let's be reasonable now

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            Satisfaction = 50f;
            Score = 0;

            //UpdateBackgroundMusic(true);
        }
        else
        {
            UpdateHighScore();

            //UpdateBackgroundMusic(false);
        }

        GameSpeed = 1f;

        UpdateScore();
        GameController.AddSatisfaction(0); //Add 0 because this causes the bar fill to update color/size


    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale > 0 && GameSpeed < MaxSpeed)
        {
            GameSpeed += Mathf.Min(Random.Range(0f, 0.0008f), MaxSpeed - GameSpeed);
            Time.timeScale = GameSpeed;
        }

        if (Satisfaction <= 0)
        {
            Satisfaction = 50;

            SceneManager.LoadScene("GameOver");
        }
	}

    public static void AddScore(int value)
    {
        Score += value;
        UpdateScore();
    }

    public static int GetScore()
    {
        return Score;
    }

    private static void UpdateScore()
    {
        GameObject.Find("ScoreLabel").GetComponent<Text>().text = "Score: " + Score;
    }

    private static void UpdateHighScore()
    {
        HighScore = GetHighScore();

        if (Score > HighScore)
        {
            //Update file holding high score
        }
    }

    public static int GetHighScore()
    {
        //Get high score from file

        return HighScore;
    }

    public static void AddSatisfaction(int value)
    {
        Satisfaction = Mathf.Clamp(Satisfaction + value, 0, 100);

        GameObject.Find("SatisfactionLabel").GetComponent<Text>().text = "Satisfaction: " + ((int)Satisfaction).ToString();
        GameObject bar = GameObject.Find("SatisfactionFill");
        bar.GetComponent<Image>().color = Color.HSVToRGB(0.30f * (Satisfaction / 100), 1, 1);
        RectTransform barTransform = bar.GetComponent<RectTransform>();
        barTransform.offsetMin = new Vector2(barTransform.offsetMin.x, 0);
        barTransform.offsetMax = new Vector2(barTransform.offsetMax.x, -400 + Mathf.Round(Satisfaction) * 4);

		if(Mathf.Round(Satisfaction) >= 100)
		{
			Achieve.UnlockAchievement(Achievement.MaxSatisfaction);
		}
    }

    void UpdateBackgroundMusic(bool gameOver)
    {
        string audioClipName;
        AudioClip soundClip;
        AudioSource customerAudio = gameObject.GetComponent<AudioSource>();

        if (gameOver)
        {
            audioClipName = gameOverMusic;

            soundClip = Resources.Load<AudioClip>(audioClipName);

            customerAudio.clip = soundClip;
        }
        else
        {
            audioClipName = backgroundMusic;

            soundClip = Resources.Load<AudioClip>(audioClipName);

            customerAudio.clip = soundClip;
        }

        customerAudio.Play();
    }


}
