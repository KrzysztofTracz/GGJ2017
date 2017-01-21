using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{

	public ScoreManager scoreManager;
	public UnityEngine.UI.Text viewsField;
	public UnityEngine.UI.Text likesField;
	public UnityEngine.UI.Text dislikesField;
	public UnityEngine.UI.Text log;
	public UnityEngine.UI.Text subsField;
	public UnityEngine.UI.Text timeField;
	public float logClearInterval;

	float logClearTimer;
	// Use this for initialization
	void Start ()
	{
		viewsField = GameObject.Find ("ViewsField").GetComponent<Text> ();
		likesField = GameObject.Find ("LikesField").GetComponent<Text> ();
		dislikesField = GameObject.Find ("DislikesField").GetComponent<Text> ();
		log = GameObject.Find ("Log").GetComponent<Text> ();
		subsField = GameObject.Find ("SubsField").GetComponent<Text> ();
		timeField = GameObject.Find ("TimeField").GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update ()
	{
		float t = scoreManager.currentRoundDuration;
		timeField.text = String.Format ("{0:00}:{1:00}", (int)(t / 60), (int)(t % 60)); 
		viewsField.text = Mathf.CeilToInt (scoreManager.views).ToString ();
		likesField.text = Mathf.CeilToInt (scoreManager.likes).ToString ();
		dislikesField.text = Mathf.CeilToInt (scoreManager.dislikes).ToString ();
		subsField.text = "x" + Mathf.CeilToInt (scoreManager.subs).ToString ();

		Slider slider = GameObject.Find ("Slider").GetComponent<Slider>();
		slider.value = scoreManager.currentRoundDuration / scoreManager.maxDuration;

		if (scoreManager.dangerWarning) {
			log.text = "DANGER";
		}
		if (scoreManager.isFailed ()) {
			log.text = "FAIL!";
		}

		if (logClearTimer > logClearInterval) {
			logClearTimer = 0;
			log.text = "";
		}
		logClearTimer += Time.deltaTime;
			
	}
}
