using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EntController : NetworkBehaviour {

    public static EntController Player = null;

    public GameObject LegActive = null;
    public GameObject LegInactive = null;

    public GameObject FailIndicator = null;

    public Transform Head = null;

#if UNITY_ANDROID
    [SyncVar(hook="OnPrankActiveChanged")]
#endif
    public bool PrankActive = false;

#if UNITY_ANDROID
    [SyncVar]
#endif
    public bool IsFailing = false;

    private void Awake()
    {
        Player = this;
    }

    void Start ()
    {
        LegActive.SetActive(true);
        LegInactive.SetActive(true);

        EntSocket.Instance.Attach(transform);

        Head = CameraController.Instance.transform;

        FailIndicator = GameObject.Find("Fail");
    }
	
    private void LateUpdate()
    {
        if (UIController.Instance.Fail == null) return; 

        if(IsFailing)
        {
            UIController.Instance.Fail.SetActive(true);
            IsFailing = false;
        }
        else
        {
            UIController.Instance.Fail.SetActive(false);
        }
    }

    public void InSight()
    {
        if (PrankActive) Fail();
    }

    public void Fail()
    {
        IsFailing = true;
    }

    public void OnPrankActiveChanged(bool prankActive)
    {
        PrankActive = prankActive;
        if (prankActive)
        {
            LegActive.SetActive(true);
            LegInactive.SetActive(false);
        }
        else
        {
            LegActive.SetActive(false);
            LegInactive.SetActive(true);
        }
    }
}
