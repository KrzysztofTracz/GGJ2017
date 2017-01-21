using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkPlayer : NetworkBehaviour
{
    private void Update()
    {
        if (!isServer)
        {
            bool stomp = false;

            if (Input.GetMouseButton(0) || Input.GetKey("space"))
            {
                stomp = true;
            }

            CmdStomp(stomp);
            EntController.Player.OnPrankActiveChanged(stomp);
        }
    }

    [Command]
    public void CmdStomp(bool flag)
    {
        EntController.Player.OnPrankActiveChanged(flag);
    }
}
