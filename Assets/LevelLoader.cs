using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("scene0_asset_test", LoadSceneMode.Additive);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
