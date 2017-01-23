using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveData
{
    public float views;
    public float likes;
    public float dislikes;
    public float subs;
}

public class Ending : Cutscenka
{
    float delay = 10.0f;

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


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate);

        SaveData data = new SaveData();
        data.views = ScoreManager.Instance.views;
        data.likes = ScoreManager.Instance.likes;
        data.dislikes = ScoreManager.Instance.dislikes;
        data.subs = ScoreManager.Instance.subs;

        bf.Serialize(file, data);
        file.Close();
    }
}
