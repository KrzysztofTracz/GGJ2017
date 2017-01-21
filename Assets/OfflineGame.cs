using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineGame : MonoBehaviour {

    public static OfflineGame Instance = null;

    public NetworkObjectsSpawner ObjectsSpawner = null;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        ObjectsSpawner.OfflineSpawn();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (OfflineGame.Instance.isActiveAndEnabled)
        {
            bool stomp = false;

            if (Input.GetMouseButton(0) || Input.GetKey("space"))
            {
                stomp = true;
            }

            EntController.Player.OnPrankActiveChanged(stomp);
        }
    }
}
