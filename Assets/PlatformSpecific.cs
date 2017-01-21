using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpecific : MonoBehaviour
{
    public GameObject Android       = null;
    public GameObject AndroidEditor = null;
    public GameObject AndroidDevice = null;

    public GameObject PC = null;

    void Awake()
    {
#if UNITY_ANDROID
        Android.SetActive(true);

        if (Application.isEditor)
        {
            AndroidEditor.SetActive(true);
        }
        else
        {
            AndroidDevice.SetActive(true);
        }
#else
        PC.SetActive(true);
#endif
    }
}
