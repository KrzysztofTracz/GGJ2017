using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisible : MonoBehaviour {

    public bool Value = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBecameInvisible()
    {
        Value = false;
    }

    public void OnBecameVisible()
    {
        Value = true;
    }
}
