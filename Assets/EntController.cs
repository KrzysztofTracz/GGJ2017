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
    
    [SyncVar(hook="OnPrankActiveChanged")]
    public bool PrankActive = false;

    [SyncVar]
    public bool IsFailing = false;

    private void Awake()
    {
        Player = this;
    }

    // Use this for initialization
    void Start ()
    {
        LegActive.SetActive(true);
        LegInactive.SetActive(true);

        EntSocket.Instance.Attach(transform);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if(!isServer)
        //{
        //    if (Input.GetMouseButton(0) || Input.GetKey("space"))
        //    {
        //        PrankActive = true;
        //        LegActive.SetActive(true);
        //        LegInactive.SetActive(false);
        //    }
        //    else
        //    {
        //        PrankActive = false;
        //        LegActive.SetActive(false);
        //        LegInactive.SetActive(true);
        //    }
        //}
	}

    private void LateUpdate()
    {
        if (FailIndicator == null) return; 

        if(IsFailing)
        {
            FailIndicator.SetActive(true);
            IsFailing = false;
        }
        else
        {
            FailIndicator.SetActive(false);
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
        //if (isServer)
        //{
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
        //}
    }
}
