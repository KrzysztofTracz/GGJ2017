using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntCutsceneAnimator : MonoBehaviour {

    public Animator Animator = null;

    // Use this for initialization
    void Start () {
        Animator.SetBool("Chill", true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
