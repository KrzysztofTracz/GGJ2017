using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public float viewsExponentScale = 0.01f;
	public float likesExponentScale = 0.01f;
	public float difficultyScale;
	public float difficultyIncrementPerSecond = 0.01f;
	public float currentRoundDuration;
	public bool roundActive = true;

	public bool footInWater;

	public float views;
	public float likes;
	public float dislikes;
//	float latestDurationInWater;
	float currentDurationInWater;
	float enterTime;
	bool currentFail;
	public bool dangerWarning;
	public float scoreMultiplier;

	// Use this for initialization
	void Start ()
	{
		footInWater = false;
//		latestDurationInWater = 0;
		enterTime = 0;
		views = 0;

//		failCheckTimer = 0;
		difficultyScale = 0.01f;
		currentRoundDuration = .0f;
		scoreMultiplier = 1;
		dangerWarning = false;
	}
	
	// Update is called once per frame
	void LateUpdate ()
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
		currentRoundDuration += Time.deltaTime;

		// increase difficulty with time
		difficultyScale += difficultyIncrementPerSecond * Time.deltaTime;

		if (footInWater) {			
			// increment round duration counter
			Debug.Log ("c");
			if(EntController.Player.IsBeingSpotted) {
				currentFail = true;
				currentDurationInWater = 0.0f;
				scoreMultiplier = 1;
			} 
			float newViews = 0;
			float newLikes = 0;
			if (EntController.Player.IsVisibleInCamera) {
				currentDurationInWater += Time.deltaTime;
				newViews = scoreMultiplier * Mathf.Exp (viewsExponentScale * currentDurationInWater) - 1;
				newLikes = scoreMultiplier * Mathf.Exp (likesExponentScale * currentDurationInWater) - 1;
				if (currentFail == false) {
					views += newViews;
					likes += newLikes;
				}
			}
			if (currentFail) {
				dislikes += Mathf.RoundToInt (Mathf.Exp (viewsExponentScale * currentDurationInWater) - 1);
			} else {
				dislikes += Mathf.RoundToInt ( (newLikes>1 ? newLikes : 1) / 10);
			}	
		}
	}

	// called when entering fountain
	// start counting time
	public void FootEnter ()
	{
		footInWater = true;
		currentFail = false;
		enterTime = Time.realtimeSinceStartup;
	}

	// called when leaving fountain
	public void FootExit ()
	{	
		if (currentFail == false && EntController.Player.IsVisibleInCamera) {
			scoreMultiplier += 1;
		}
		footInWater = false;
		currentFail = false;
//		latestDurationInWater = Time.realtimeSinceStartup - enterTime;
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
