using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntController : MonoBehaviour {

    public static EntController Player = null;

    public GameObject LegActive = null;
    public GameObject LegInactive = null;

    public GameObject FailIndicator = null;

    public Transform Head = null;

    public bool PrankActive { get; private set; }
    public bool IsFailing { get; private set; }

    private void Awake()
    {
        Player = this;
    }

    // Use this for initialization
    void Start ()
    {
        PrankActive = false;
        IsFailing = false;

        LegActive.SetActive(false);
        LegInactive.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            PrankActive = true;
            LegActive.SetActive(true);
            LegInactive.SetActive(false);
        }
        else
        {
            PrankActive = false;
            LegActive.SetActive(false);
            LegInactive.SetActive(true);
        }
	}

    private void LateUpdate()
    {
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
}
