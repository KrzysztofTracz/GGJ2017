using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailIndicator : MonoBehaviour {

    public static FailIndicator Instance = null;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
