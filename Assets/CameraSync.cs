using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSync : NetworkBehaviour
{
#if UNITY_ANDROID
    [SyncVar(hook = "OnRotationChanged")]
#endif
    public Quaternion rotation = Quaternion.identity;

#if UNITY_ANDROID
    [SyncVar(hook = "OnPositionChanged")]
#endif
    public Vector3 position = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        if(OfflineGame.Instance != null && OfflineGame.Instance.isActiveAndEnabled)
        {
            AttachMeTo(CameraController.Instance.transform);
            CameraController.Instance.isServer = true;
        }
        else
        {
            if (!isServer)
            {
                AttachToMe(CameraController.Instance.transform);
                CameraController.Instance.isServer = false;
            }
            else
            {
                AttachMeTo(CameraController.Instance.transform);
                CameraController.Instance.isServer = true;
            }
        }
	}

    private void LateUpdate()
    {
        rotation = transform.rotation;
        position = transform.position;
    }

    void AttachMeTo(Transform t)
    {
        transform.SetParent(t);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    void AttachToMe(Transform t)
    {
        t.SetParent(transform);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    public void OnRotationChanged(Quaternion value)
    {
        transform.rotation = value;
    }

    public void OnPositionChanged(Vector3 value)
    {
        transform.position = value;
    }
}
