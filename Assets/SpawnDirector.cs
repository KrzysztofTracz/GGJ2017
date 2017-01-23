using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDirector : MonoBehaviour {

    public List<ActorSpawner> Spawners = new List<ActorSpawner>();

    public AnimationCurve SpawnersActivity = new AnimationCurve();

    public AnimationCurve SpawnDelayMin = new AnimationCurve();
    public AnimationCurve SpawnDelayMax = new AnimationCurve();
    public AnimationCurve SpawnCivil = new AnimationCurve();

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
        ElapsedTime += Time.deltaTime;

        var currentTime = ElapsedTime / TotalTime;

        var activeSpawners = SpawnersActivity.Evaluate(currentTime);
        var spawnDelayMin = SpawnDelayMin.Evaluate(currentTime);
        var spawnDelayMax = SpawnDelayMax.Evaluate(currentTime);
        var spawnCivil = SpawnCivil.Evaluate(currentTime);

        for (int i=0;((float)i)<activeSpawners;i++)
        {
            if(!Spawners[i].gameObject.activeSelf)
            {
                Spawners[i].gameObject.SetActive(true);
            }

            Spawners[i].SpawnDelayMin = spawnDelayMin;
            Spawners[i].SpawnDelayMax = spawnDelayMax;
            Spawners[i].SpawnCivilChance = spawnCivil;
        }
    }
}
