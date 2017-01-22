using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indykator : MonoBehaviour {

    public float ShowTime = 0.0f;

    public bool _lock = false;

    public void Show()
    {
        if (_lock) return;

        gameObject.SetActive(true);
        ShowTime += Time.deltaTime;
        _lock = true;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        ShowTime -= Time.deltaTime * 0.5f;
        if(ShowTime <= 0)
        {
            ShowTime = 0.0f;
            gameObject.SetActive(false);
        }
        _lock = false;
	}

    public void Reset()
    {
        ShowTime = 0.0f;
        gameObject.SetActive(false);
    }
}
