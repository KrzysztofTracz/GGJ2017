using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{
    public NetworkObjectsSpawner NetworkObjectsSpawner = null;

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        if (conn.connectionId != 0)
        {
            NetworkObjectsSpawner.Client = conn;
            NetworkObjectsSpawner.Spawn();
        }
    }
}
