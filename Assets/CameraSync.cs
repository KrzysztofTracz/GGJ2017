using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSync : NetworkBehaviour {

	// Use this for initialization
	void Start ()
    {
	    if(!isServer)
        {
            CameraController.Instance.transform.SetParent(transform);
            CameraController.Instance.transform.localPosition = Vector3.zero;
            CameraController.Instance.transform.localRotation = Quaternion.identity;
            CameraController.Instance.transform.localScale = Vector3.one;

            CameraController.Instance.isServer = false;
            //CameraController.Instance.isServer = true;
        }
        else
        {
            transform.SetParent(CameraController.Instance.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            //CameraController.Instance.isServer = false;
            CameraController.Instance.isServer = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
