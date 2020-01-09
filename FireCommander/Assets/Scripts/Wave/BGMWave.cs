using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMWave : Wave {

	[SerializeField]
	new GameObject audio;

	// Use this for initialization
	void Start () {
		count = transform.childCount;
		canvas = GameObject.FindGameObjectWithTag("UI");
		StartCoroutine(PlayNewAudio());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator PlayNewAudio() {
		GameObject oldAudio = GameObject.FindGameObjectWithTag("BGM");
		if(oldAudio != null)
			yield return StartCoroutine(oldAudio.GetComponent<BGMController>().StopBGM());
		Instantiate(audio);

	}

}
