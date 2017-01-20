using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanController : MonoBehaviour {

    public Transform Head = null;

    public float FieldOfView = 30.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var dir   = EntController.Player.Head.position - Head.position;
        var angle = Vector3.Angle(Head.forward, dir);
        if(angle < FieldOfView)
        {
            EntController.Player.InSight();
        }
    }
}
