﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorController : MonoBehaviour {

    public NavMeshAgent NavMeshAgent = null;

    public bool hasTarget = false;

    public void SetDestination(Vector3 position)
    {
        NavMeshAgent.destination = position;
        hasTarget = true;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(hasTarget && (NavMeshAgent.destination - transform.position).magnitude < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
