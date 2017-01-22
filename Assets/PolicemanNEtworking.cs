using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PolicemanNEtworking : NetworkBehaviour
{
#if UNITY_ANDROID
    [SyncVar(hook = "OnCivilChanged")]
#endif
    public bool IsCivilian = false;

	void Update () {
		
	}

    public void OnCivilChanged(bool value)
    {
        IsCivilian = true;
        GetComponent<PolicemanController>().MakeCivilian();
    }
}
