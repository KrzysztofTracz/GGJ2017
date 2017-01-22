using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : Cutscenka
{
	protected override void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
	protected override void OnEnable() {
		base.OnEnable ();
		EntController.Player.RoundEnded = true;
	}
}
