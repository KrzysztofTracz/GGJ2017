using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObjectsSpawner : NetworkBehaviour
{

    public static NetworkObjectsSpawner Instance = null;

    public List<Transform> Sockets = new List<Transform>();
    public List<GameObject> Objects = new List<GameObject>();
    public List<bool> WithClientAuthority = new List<bool>();

    public NetworkConnection Client = null;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(GameObject gameObject)
    {
        NetworkServer.Spawn(gameObject);
    }

    public void Spawn()
    {
        for (int i = 0; i < Sockets.Count; i++)
        {
            var obj = Instantiate(Objects[i], Sockets[i].position, Sockets[i].rotation);

            NetworkServer.Spawn(obj);

            if (WithClientAuthority[i])
            {
                var ni = obj.GetComponent<NetworkIdentity>();
                ni.AssignClientAuthority(Client);
            }
            else
            {
                NetworkServer.Spawn(obj);
            }
        }
    }

    public void OfflineSpawn()
    {
        for (int i = 0; i < Sockets.Count; i++)
        {
            var obj = Instantiate(Objects[i], Sockets[i].position, Sockets[i].rotation);
        }
    }
}
