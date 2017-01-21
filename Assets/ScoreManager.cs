using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public float viewsExponentScale = 0.01f;
	public float likesExponentScale = 0.01f;
	public float difficultyScale= 0.0001f;
	public float difficultyIncrementPerSecond = 0.0001f;
	public float currentRoundDuration;
	public bool roundActive = true;
	public float maxDuration = 300;

	public bool footInWater;

	public float views=0;
	public float likes=0;
	public float dislikes =0;
	public float subs = 0;
//	float latestDurationInWater;
	float currentDurationInWater = .0f;
	float enterTime;
	bool currentFail;
	public bool dangerWarning;
	public float scoreMultiplier;
	public float oneSecondTimer = 0;

	// Use this for initialization
	void Start ()
	{
		footInWater = false;
//		latestDurationInWater = 0;

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


		oneSecondTimer += Time.deltaTime;

		currentRoundDuration += Time.deltaTime;

		if (oneSecondTimer > 1) {			
			subs = scoreMultiplier * 100 + UnityEngine.Random.Range(-50,50);
		}

		// increase difficulty with time
		difficultyScale += difficultyIncrementPerSecond * Time.deltaTime;

		if (footInWater) {			
			// increment round duration counter
			if(EntController.Player.IsBeingSpotted) {
				currentFail = true;
				currentDurationInWater = 0.0f;
				scoreMultiplier = 1;	
				if (oneSecondTimer > 1) {			
					subs = scoreMultiplier * 100 + UnityEngine.Random.Range (-50, 50);
				}
			} 
			float newViews = 0;
			float newLikes = 0;
			if (EntController.Player.IsVisibleInCamera) {
				currentDurationInWater += Time.deltaTime;
				newViews = scoreMultiplier * (Mathf.Exp (viewsExponentScale * currentDurationInWater) - 1);
				newLikes = scoreMultiplier * (Mathf.Exp (likesExponentScale * currentDurationInWater) - 1);
				if (currentFail == false) {
					views += newViews;
					likes += newLikes;
				}


			}
			dislikes += difficultyScale * Mathf.Exp(difficultyScale * currentRoundDuration - 1);
			if(EntController.Player.IsBeingSpotted) {				
				dislikes += Random.Range(0, 100f * difficultyScale);
			} 


		}
		if (oneSecondTimer > 1) {
			oneSecondTimer = 0;
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
