using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour {


	public AudioSource emitter;
	public AudioSource emitter2;
	public AudioClip foot_in_water;
	public AudioClip foot_in_water_splash;
	public AudioClip foot_outa_water;
	public AudioClip combo_l;
	public AudioClip police_whistle;
	public AudioClip outro_loose;
	public AudioClip outro_win;

	// Use this for initialization
	void Start () {
		emitter = this.GetComponent<AudioSource>();
		emitter2 = GameObject.Find("SoundEmitter2").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Play(AudioClip clip) {
		emitter.clip = clip;
		emitter.Play ();
	}

	public void Play2(AudioClip clip) {
		emitter2.PlayOneShot (clip, emitter2.volume);
	}
}
