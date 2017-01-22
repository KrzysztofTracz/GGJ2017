using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indykators : MonoBehaviour {

    public static Indykators Instance = null;

    public Indykator Left  = null;
    public Indykator Right = null;
    public Indykator Up    = null;

    public void Awake()
    {
        Instance = this;
    }

    public void Reset()
    {
        Left.Reset();
        Right.Reset();
        Up.Reset();
    }
}
