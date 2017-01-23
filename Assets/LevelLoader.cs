using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public Text Views = null;
    public Text Likes = null;
    public Text Dislikes = null;
    public Text Subs = null;

    // Use this for initialization
    void Start () {
        SceneManager.LoadScene("scene0_asset_test", LoadSceneMode.Additive);

        try
        {
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            var data = bf.Deserialize(file) as SaveData;
            Views.text = Mathf.CeilToInt(data.views).ToString();
            Likes.text = Mathf.CeilToInt(data.likes).ToString();
            Dislikes.text = Mathf.CeilToInt(data.subs).ToString();
            Subs.text = Mathf.CeilToInt(data.dislikes).ToString();
        }
        catch
        {
            Views.text = "0";
            Likes.text = "0";
            Subs.text = "0";
            Dislikes.text = "0";
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
