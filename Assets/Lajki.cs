using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lajki : MonoBehaviour {

    public static Lajki Instance = null;

    public ParticleSystem ParticleSystem = null;

    public bool _lock = false;
    public float ShowTime = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    public void Napierdalaj()
    {
        if (_lock) return;

        if (!ParticleSystem.isPlaying)
        {
            ParticleSystem.Play();
        }

        ShowTime += Time.deltaTime;
        _lock = true;
    }

    void LateUpdate()
    {
        ShowTime -= Time.deltaTime;
        if (ShowTime <= 0)
        {
            ShowTime = 0.0f;
            ParticleSystem.Stop();
        }
        _lock = false;
    }
}
