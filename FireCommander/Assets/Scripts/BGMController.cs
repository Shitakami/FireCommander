using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMController : MonoBehaviour {
	new AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator StopBGM() {
		int count = 50;
		float d = audio.volume / count;
		for (int i = 0; i < count; i++) {
			audio.volume -= d;
			yield return null;
		}
		Destroy(this.gameObject);
	}
}
