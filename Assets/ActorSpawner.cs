using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorSpawner : MonoBehaviour
{
    public List<Transform> Targets = new List<Transform>();

    public GameObject PolicemanPrefab = null;
    public GameObject CivilPrefab = null;
    public GameObject DogePrefab = null;

    public float SpawnDelayMin = 3.0f;
    public float SpawnDelayMax = 5.0f;

    public float SpawnDelay = 0.0f;

    public float SpawnCivilChance = 0.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnDelay -= Time.deltaTime;

		if(SpawnDelay <= 0.0f)
        {
            Spawn();
            SpawnDelay = Random.Range(SpawnDelayMin, SpawnDelayMax);
        }
	}

    public void Spawn()
    {
        var obj = GameObject.Instantiate(PolicemanPrefab, transform.position, transform.rotation);

        if(Random.value < SpawnCivilChance)
        {
            var policemanController = obj.GetComponent<PolicemanController>();
            policemanController.Civil = true;
        }

        var agent = obj.GetComponent<ActorController>();
        agent.SetDestination(Targets[Random.Range(0, Targets.Count)].position);
    }
}
