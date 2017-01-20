using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotationMovement = 5.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private Vector3 baseRotation = Vector3.zero; 

    // Use this for initialization
    void Start ()
    {
        baseRotation = transform.rotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        rotationY += RotationMovement * Input.GetAxis("Mouse X");
        rotationX -= RotationMovement * Input.GetAxis("Mouse Y");

        transform.rotation = Quaternion.Euler(baseRotation + new Vector3(rotationX, rotationY, 0.0f));
    }
}
