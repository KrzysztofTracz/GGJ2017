using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMimic : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = CameraController.Instance.transform.position;

        var r = transform.rotation.eulerAngles;
        r.y = CameraController.Instance.transform.rotation.eulerAngles.y;

        transform.rotation = Quaternion.Euler(r);
    }
}
