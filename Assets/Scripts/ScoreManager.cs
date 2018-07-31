using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	public static int score; //Creates a Score value to store

	Text text;

	void Start()
	{
		text = GetComponent<Text> ();


		score = PlayerPrefs.GetInt ("CurrentScore");
	}

	void Update()
	{
		if (score < 0)
			score = 0;
		//If Score is lower than 0 then score is 0

		text.text = "" + score;

	}


	public static void AddPoints (int pointsToAdd)
	{
		score += pointsToAdd;
		PlayerPrefs.SetInt ("CurrentScore", score);
	}

	public static void Reset ()
	{
		score = 0;
		PlayerPrefs.SetInt ("CurrentScore", score);
	}
}