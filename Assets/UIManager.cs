using UnityEngine;
using System.Collections;
using System;

public class UIManager : MonoBehaviour {

	public ScoreManager scoreManager;
	public UnityEngine.UI.Text viewsField;
	public UnityEngine.UI.Text likesField;
	public UnityEngine.UI.Text dislikesField;
	public UnityEngine.UI.Text log;
	public UnityEngine.UI.Text multiplierField;
	public UnityEngine.UI.Text timeField;
	public float logClearInterval;

	float logClearTimer;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		float t = scoreManager.currentRoundDuration;
		timeField.text = String.Format ("{0:00}:{1:00}", (int)(t / 60), (int)(t % 60)); 
		viewsField.text = Mathf.CeilToInt(scoreManager.views).ToString();
		likesField.text = Mathf.CeilToInt(scoreManager.likes).ToString();
		dislikesField.text = Mathf.CeilToInt(scoreManager.dislikes).ToString();
		multiplierField.text = "x" + Mathf.RoundToInt(scoreManager.scoreMultiplier).ToString();

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
