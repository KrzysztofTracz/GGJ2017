using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntSocket : MonoBehaviour {

    public static EntSocket Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    public void Attach(Transform t)
    {
        t.SetParent(transform);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
