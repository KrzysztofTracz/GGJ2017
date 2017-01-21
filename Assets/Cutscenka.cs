using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenka : MonoBehaviour {

    static public Cutscenka Instance = null;

    public Transform CameraSocket = null;
    public Transform PrevCameraParent = null;

    public float TotalTime = 5.0f;
    public float ElapsedTime = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    protected virtual void OnEnable()
    {
        ElapsedTime = 0.0f;
        PrevCameraParent = CameraController.Instance.transform.parent;

        CameraController.Instance.transform.SetParent(CameraSocket);
        CameraController.Instance.transform.localPosition = Vector3.zero;
        CameraController.Instance.transform.localRotation = Quaternion.identity;
        CameraController.Instance.enabled = false;
        EntController.Player.gameObject.SetActive(false);

		EntController.Player.GameplayStopped = true;
    }

    protected virtual void OnDisable()
    {
		EntController.Player.GameplayStopped = false;

        if (CameraController.Instance != null && EntController.Player != null)
        {
            CameraController.Instance.transform.SetParent(PrevCameraParent);
            CameraController.Instance.transform.localPosition = Vector3.zero;
            CameraController.Instance.transform.localRotation = Quaternion.identity;
            CameraController.Instance.enabled = true;
            EntController.Player.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        ElapsedTime += Time.deltaTime;
        if(ElapsedTime >= TotalTime)
        {
            gameObject.SetActive(false);
        }
    }
}
