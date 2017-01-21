using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public ScoreManager scoreManager;
	public UnityEngine.UI.Text viewsField;
	public UnityEngine.UI.Text likesField;
	public UnityEngine.UI.Text dislikesField;
	public UnityEngine.UI.Text log;
	public UnityEngine.UI.Text multiplierField;
	public float logClearInterval;

	float logClearTimer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		viewsField.text = scoreManager.views.ToString();
		likesField.text = scoreManager.likes.ToString();
		dislikesField.text = scoreManager.dislikes.ToString();
		multiplierField.text = "x" + scoreManager.scoreMultiplier.ToString ();


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
