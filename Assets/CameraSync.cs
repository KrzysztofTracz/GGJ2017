using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSync : NetworkBehaviour {

	// Use this for initialization
	void Start ()
    {
        if(OfflineGame.Instance.isActiveAndEnabled)
        {
            AttachMeTo(CameraController.Instance.transform);
            CameraController.Instance.isServer = true;
        }
        else
        {
            if (!isServer)
            {
                AttachToMe(CameraController.Instance.transform);
                CameraController.Instance.isServer = false;
            }
            else
            {
                AttachMeTo(CameraController.Instance.transform);
                CameraController.Instance.isServer = true;
            }
        }
	}
	
	void AttachMeTo(Transform t)
    {
        transform.SetParent(t);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    void AttachToMe(Transform t)
    {
        t.SetParent(transform);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }
}
