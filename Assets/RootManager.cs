using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootManager : MonoBehaviour {

	GameObject ui;
	GameObject startButton;
    // Use this for initialization

    public GameObject ent = null;
    public GameObject mainCamera = null;

    public ReactionScript ReactionScript = null;
    public CommentsScript CommentsScript = null;

    public static RootManager Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        //		try {
        //			SceneManager.LoadScene ("scene0_asset_test", LoadSceneMode.Additive);
        //		}
        //		catch {
        //			Debug.Log ("Failed to load scene0_asset_test");
        //		}
        //		try {
        //			SceneManager.UnloadScene(SceneManager.GetSceneByName("scene0"));
        //		}
        //		catch {
        //			Debug.Log ("failed to unload scene0");
        //		}
        //		if(ui == null) ui = GameObject.Find ("UI 1");			
        //		if(startButton == null) 	startButton = GameObject.Find ("StartButton");
        //		ui.SetActive(true);
        //		startButton.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
//		if (EntController.Player && EntController.Player.RoundEnded && Input.GetKey ("z")) {
//			EntController.Player.RoundEnded = false;
//			Start ();
//			return;
//		}
	}

	public void StartRound() {		
		GameObject.Find ("UI 1").SetActive(false);
		
		SceneManager.LoadScene ("scene0", LoadSceneMode.Additive);        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("scene0"));

        ent.SetActive(false);
        mainCamera.SetActive(false);
        ReactionScript.enabled = true;
        CommentsScript.enabled = true;
    }
}
	
