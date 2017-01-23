using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : Cutscenka
{
    float delay = 5.0f;

	protected override void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R) || (delay -= Time.deltaTime) < 0.0f)
        {
            Application.LoadLevel("root");
        }
	}
	protected override void OnEnable() {
		base.OnEnable ();
		EntController.Player.RoundEnded = true;
        RootManager.Instance.ReactionScript.enabled = false;

    }
}
