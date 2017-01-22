using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NieLajki : Lajki {

    public static NieLajki Instanceeee = null;

    private void Awake()
    {
        Instanceeee = this;
    }



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
