using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance = null;

    public float RotationMovement = 5.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public bool isServer = true;
    
    private Vector3 baseRotation = Vector3.zero;

    public Cutscenka Cutscenka = null;
    public Ending Ending = null;
    public Ending BadEnding = null;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        baseRotation = transform.rotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isServer)
        {
            rotationY += RotationMovement * Input.GetAxis("Mouse X");
            rotationX -= RotationMovement * Input.GetAxis("Mouse Y");

            if (rotationX >  90.0f) rotationX =  90.0f;
            if (rotationX < -90.0f) rotationX = -90.0f;

            transform.rotation = Quaternion.Euler(baseRotation + new Vector3(rotationX, rotationY, 0.0f));
        }
    }
}
