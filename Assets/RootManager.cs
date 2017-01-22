using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("StartButton").SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (EntController.Player.RoundEnded) {
			Start ();
		}
	}

	public void StartRound() {		
		GameObject.Find ("UI").SetActive(false);
		SceneManager.LoadScene ("scene0", LoadSceneMode.Additive);
        SceneManager.LoadScene("scene0_asset_test", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("scene0"));
	}
}
