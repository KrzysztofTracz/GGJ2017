using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null;

    public float viewsExponentScale = 0.02f;
	public float likesExponentScale = 0.02f;
	public float difficultyScale= 0.01f;
	public float difficultyIncrementPerSecond = 0.01f;
	public float currentRoundDuration;
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

	public SoundEmitter soundEmitter;

    private void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
	{
		soundEmitter = GameObject.Find ("SoundEmitter").GetComponent<SoundEmitter> ();
		footInWater = false;
//		latestDurationInWater = 0;

		currentRoundDuration = .0f;
		scoreMultiplier = 1;
		dangerWarning = false;
	}

	float GenerateSubs() {
		float range = 1;

		float s = 0;

		if (likes > 10)
			range = 5;
		else if (likes > 100)
			range = 50;
		else if (likes > 1000) 
			range = 500;

		range = Mathf.Min (likes, range);

		return Mathf.Min(likes, (scoreMultiplier * 100 + UnityEngine.Random.Range (range, -range)) / 10);
	}

    private float Limit(float value)
    {
        return (value > 1000.0f ? 1000.0f : value);
    }

	// Update is called once per frame
	void LateUpdate ()
	{
        if (EntController.Player == null)
        {
            return;
        }

		if (Input.GetKey ("z")) {
			EntController.Player.RoundEnded = true;
		}

        if (footInWater == false && EntController.Player.PrankActive) {
			FootEnter ();
		}
		if (footInWater && EntController.Player.PrankActive == false) {
			FootExit ();
		}

		if (EntController.Player.RoundEnded) {
			// 
		}

		// do not update values if round is over
		if (EntController.Player.GameplayStopped) {
			return; 
		}


		oneSecondTimer += Time.deltaTime;

		currentRoundDuration += Time.deltaTime;

		if (oneSecondTimer > 1) {	
			subs = GenerateSubs ();
		}

		// increase difficulty with time
		difficultyScale += difficultyIncrementPerSecond * Time.deltaTime;

		float newViews = 0;
		float newLikes = 0;
		float newDislikes = 0;
		if (footInWater) {			
			// increment round duration counter
			if(EntController.Player.IsBeingSpotted) {
				currentFail = true;
				currentDurationInWater = 0.0f;
				scoreMultiplier = 1;	
				if (oneSecondTimer > 1) {			
					subs = GenerateSubs ();
				}

                //newDislikes += Random.Range(0, 100f) / 100 * difficultyScale;
            } 

			if (EntController.Player.IsVisibleInCamera) {
				currentDurationInWater += Time.deltaTime;
				newViews = scoreMultiplier * (Mathf.Exp (viewsExponentScale * currentDurationInWater) - 1);
				newLikes = scoreMultiplier * (Mathf.Exp (likesExponentScale * currentDurationInWater) - 1);
				if (currentFail == false) {
					views += Limit(newViews);
					likes += Limit(newLikes);
				}
			}
            else
            {
                newDislikes = (Mathf.Exp((difficultyScale / 100.0f) * currentRoundDuration) - 1);
            }			

			dislikes += Limit(newDislikes);
			views += Limit(newDislikes);

        }
        else
        {
            newDislikes = (Mathf.Exp((difficultyScale/1000.0f) * currentRoundDuration) - 1);
            dislikes += Limit(newDislikes);
            views += Limit(newDislikes);
        }

		views += Time.deltaTime * Random.Range(0,100)/100;

		if (oneSecondTimer > 1) {
			oneSecondTimer = 0;
		}

        if(newLikes > 0)
        {
            Lajki.Instance.Napierdalaj();
        }

        if (newDislikes > 0)
        {
            NieLajki.Instanceeee.Napierdalaj();
        }
    }

	// called when entering fountain
	// start counting time
	public void FootEnter ()
	{		
		soundEmitter.Play(soundEmitter.foot_in_water);
		footInWater = true;
		currentFail = false;
		enterTime = Time.realtimeSinceStartup;
	}

	// called when leaving fountain
	public void FootExit ()
	{	
		soundEmitter.Play(soundEmitter.foot_outa_water);
		
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
