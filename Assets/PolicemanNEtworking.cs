using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PolicemanNEtworking : NetworkBehaviour
{
    public PolicemanController PolicemanController = null;

#if UNITY_ANDROID
    [SyncVar(hook = "OnCivilChanged")]
#endif
    public bool IsCivilian = false;

#if UNITY_ANDROID
    [SyncVar(hook = "OnSpeedChanged")]
#endif
    public float Speed = 0.0f;

    void Update () {
		
	}

    public void OnCivilChanged(bool value)
    {
        IsCivilian = true;
        GetComponent<PolicemanController>().MakeCivilian();
    }

    public void OnSpeedChanged(float value)
    {
        Speed = value;
        PolicemanController.Animator.SetFloat("Speed", Speed);
    }
}
