using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{


	public GameObject footSymbol;
	// TODO: remove when integrated
	public float timeBetweenFailChecks = 3;
	// TODO: remove when integrated
	public float viewsExponentScale = 0.01f;
	public float likesExponentScale = 0.01f;
	public float difficultyScale;
	public float difficultyIncrementPerSecond = 0.002f;
	public float currentRoundDuration;
	public bool roundActive = true;

	public bool footInWater;
	float failCheckTimer;

	public float views;
	public float likes;
	public float dislikes;
	float latestDurationInWater;
	float currentDurationInWater;
	float enterTime;
	bool currentFail;
	public bool dangerWarning;
	public float scoreMultiplier;

	// Use this for initialization
	void Start ()
	{
		footInWater = false;
		latestDurationInWater = 0;
		enterTime = 0;
		views = 0;

		failCheckTimer = 0;
		difficultyScale = 0.01f;
		currentRoundDuration = .0f;
		scoreMultiplier = 1;
		dangerWarning = false;
	}
	
	// Update is called once per frame
	void Update ()
	{		
		if(footInWater == false && EntController.Player.PrankActive) {
			FootEnter ();
		}
		if (footInWater && EntController.Player.PrankActive == false) {
			FootExit ();
		}

		// do not update values if round is over
		if (roundActive == false) {
			return; 
		}

		// increment round duration counter
		currentRoundDuration += Time.deltaTime;

		// increase difficulty with time
		difficultyScale += difficultyIncrementPerSecond * Time.deltaTime;

//		if (failCheckTimer > timeBetweenFailChecks) {
//			dangerWarning = false;
//		} else if (failCheckTimer > timeBetweenFailChecks - 1) {
//			// detect danger for early warning 
//			dangerWarning = true;
//		}

		if (footInWater && EntController.Player.IsVisibleInCamera ) {
			currentDurationInWater += Time.deltaTime;

			if(EntController.Player.IsFailing) {
//			if (failCheckTimer > timeBetweenFailChecks) {
				currentFail = true;
				currentDurationInWater = 0.0f;
				scoreMultiplier = 1;
			} 
			float newViews = scoreMultiplier * Mathf.Exp (viewsExponentScale * currentDurationInWater - 1);
			float newLikes = scoreMultiplier * Mathf.Exp (likesExponentScale * currentDurationInWater - 1);
			views += newViews;
			likes += newLikes;
			if (currentFail) {
				dislikes += Mathf.RoundToInt (Mathf.Exp (viewsExponentScale * currentDurationInWater - 1));
			} else {
				dislikes += Mathf.RoundToInt (newLikes / 10);
			}				
		}

		// fail check (emulating being caught)
//		if (failCheckTimer > timeBetweenFailChecks) {
//			failCheckTimer = 0;
//		}
//		failCheckTimer += Time.deltaTime;
	}

	// called when entering fountain
	// start counting time
	public void FootEnter ()
	{
		footInWater = true;
		currentFail = false;
		enterTime = Time.realtimeSinceStartup;
		footSymbol.SetActive (true);
	}

	// called when leaving fountain
	public void FootExit ()
	{	
		if (currentFail == false) {
			scoreMultiplier += 1;
		}
		footInWater = false;
		currentFail = false;
		latestDurationInWater = Time.realtimeSinceStartup - enterTime;
		footSymbol.SetActive (false);
	}

	public bool isFootInWater ()
	{
		return footInWater;
	}

	public bool isFailed ()
	{
		return currentFail;
	}
}
