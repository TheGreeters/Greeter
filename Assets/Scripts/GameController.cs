using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static float GameSpeed = 1f;

    public static float Satisfaction;
    public static int Score;

    public const float MaxSpeed = 4f; //Let's be reasonable now

	// Use this for initialization
	void Start () {

		Satisfaction = 50f;
        GameSpeed = 1f;
		Score = 0;
		
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

    public static void AddSatisfaction(int value)
    {
        Satisfaction = Mathf.Clamp(Satisfaction + value, 0, 100);

        GameObject.Find("SatisfactionLabel").GetComponent<Text>().text = "Satisfaction: " + ((int)Satisfaction).ToString();
        GameObject bar = GameObject.Find("SatisfactionFill");
        bar.GetComponent<Image>().color = Color.HSVToRGB(0.30f * (Satisfaction / 100), 1, 1);
        RectTransform barTransform = bar.GetComponent<RectTransform>();
		barTransform.offsetMin = new Vector2(barTransform.offsetMin.x, 0);
		float realHeight = -barTransform.rect.height + barTransform.sizeDelta.y;
		barTransform.offsetMax = new Vector2(barTransform.offsetMax.x, realHeight - (Mathf.Round(Satisfaction) * (realHeight / 100f)));

		if(Mathf.Round(Satisfaction) >= 100)
		{
			Achieve.UnlockAchievement(Achievement.MaxSatisfaction);
		}
    }
}
