using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootManager : MonoBehaviour {

	GameObject ui;
	GameObject startButton;
	// Use this for initialization
	void Start () {		
		try {
			SceneManager.LoadScene ("scene0_asset_test", LoadSceneMode.Additive);
		}
		catch {
			
		}
		try {
			SceneManager.UnloadScene(SceneManager.GetSceneByName("scene0"));
		}
		catch {
			Debug.Log ("failed to unload scene");
		}
		if(ui == null) ui = GameObject.Find ("UI 1 root");			
		if(startButton == null) 	startButton = GameObject.Find ("StartButton");
		ui.SetActive(true);
		startButton.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (EntController.Player && EntController.Player.RoundEnded && Input.GetKey ("z")) {
			EntController.Player.RoundEnded = false;
			Start ();
			return;
		}
	}

	public void StartRound() {		
		GameObject.Find ("UI 1 root").SetActive(false);
		SceneManager.LoadScene ("scene0", LoadSceneMode.Additive);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("scene0"));
	}
}
	