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

    public CameraVisible Mesh0 = null;
    public CameraVisible Mesh1 = null;

    public Transform Fountain = null;

#if UNITY_ANDROID
    [SyncVar(hook="OnPrankActiveChanged")]
#endif
    public bool PrankActive = false;

    public bool PrankSuccess = false;

    public bool IsVisibleInCamera = false;

#if UNITY_ANDROID
    [SyncVar]
#endif
    public bool IsFailing = false;

	public bool IsBeingSpotted;

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

        Fountain = GameObject.Find("Fountain Target").transform;
    }
	
    private void Update()
    {


        if (UIController.Instance.Fail == null) return;

        PrankSuccess = false;
        IsVisibleInCamera = false;

        //Physics.SphereCast(Head.transform.position, SphereCastRadius, Head.transform.forward,)

        var dir = Fountain.position - Head.position;
        var angle = Vector3.Angle(Head.forward, dir);
        if (angle < 30.0f)
        {
            IsVisibleInCamera = true;
        }

        if (PrankActive && !IsFailing && IsVisibleInCamera)
        {
            PrankSuccess = true;
        }

        if (IsFailing)
        {
            UIController.Instance.Fail.SetActive(true);
            IsFailing = false;
        }
        else
        {
            UIController.Instance.Fail.SetActive(false);
        }

		IsBeingSpotted = false; // reset for the next frame
    }

    public void InSight()
    {
		IsBeingSpotted = true;
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
