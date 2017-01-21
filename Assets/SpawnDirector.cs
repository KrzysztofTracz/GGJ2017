using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDirector : MonoBehaviour {

    public List<ActorSpawner> Spawners = new List<ActorSpawner>();

    public AnimationCurve SpawnersActivity = new AnimationCurve();

    public AnimationCurve SpawnDelayMin = new AnimationCurve();
    public AnimationCurve SpawnDelayMax = new AnimationCurve();

    public float ElapsedTime = 0.0f;
    public float TotalTime   = 5.0f * 60.0f;

    // Use this for initialization
    void Start ()
    {
		for(int i=0;i<Spawners.Count;i++)
        {
            Spawners[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
