using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {


	public GameObject footSymbol; // TODO: remove when integrated
	public float timeBetweenFailChecks = 3; // TODO: remove when integrated
	public float failCheckTimer;
	public float viewsExponentScale = 0.25f;
	public float likesExponentScale = 0.1f;

	public bool footInWater;

	public int views;
	public int likes;
	public int dislikes;
	float latestDurationInWater; 
	float currentDurationInWater;
	float enterTime;
	bool currentFail;
	public int scoreMultiplier;

	// Use this for initialization
	void Start () {
		footInWater = false;
		latestDurationInWater = 0.0f;
		enterTime = 0.0f;
		views = 0;

		failCheckTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {		

		if (footInWater) {
			currentDurationInWater += Time.deltaTime;

			if (failCheckTimer > timeBetweenFailChecks) {
				currentFail = true;
				currentDurationInWater = 0.0f;
				scoreMultiplier = 1;

			}

			int newViews = scoreMultiplier * Mathf.RoundToInt(Mathf.Exp(viewsExponentScale*currentDurationInWater-1) * 10);
			int newLikes = scoreMultiplier * Mathf.RoundToInt(Mathf.Exp(likesExponentScale*currentDurationInWater-1) * 10);
			views += newViews;
			likes += newLikes;
			if (currentFail) {
				dislikes += Mathf.RoundToInt(Mathf.Exp (viewsExponentScale * currentDurationInWater - 1) * 10);
			} else {
				dislikes += Mathf.RoundToInt(newLikes/10);
			}				
		}

		// fail check (emulating being caught)
		if (failCheckTimer > timeBetweenFailChecks) {
			failCheckTimer = 0;
		}
		failCheckTimer += Time.deltaTime;
	}

	// called when entering fountain
	// start counting time 
	public void FootEnter() {
		footInWater = true;
		currentFail = false;
		enterTime = Time.realtimeSinceStartup;
		footSymbol.SetActive(true);
	}

	// called when leaving fountain
	public void FootExit() {	
		if (currentFail == false) {
			scoreMultiplier += 1;
		}
		footInWater = false;
		currentFail = false;
		latestDurationInWater = Time.realtimeSinceStartup - enterTime;
		footSymbol.SetActive(false);
	}

	public bool isFootInWater() {
		return footInWater;
	}

	public bool isFailed() {
		return currentFail;
	}
}
