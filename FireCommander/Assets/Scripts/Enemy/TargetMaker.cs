using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TargetMaker : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start() {
		StartCoroutine(MakeTarget());
	}

	// Update is called once per frame
	void Update() {

	}

	IEnumerator MakeTarget() {

		while(true) {
			yield return new WaitForSeconds(Random.Range(3, 6));
			GameObject newT = Instantiate(target);
			Vector3 pos = this.transform.position;
			pos.y += UnityEngine.Random.Range(-5, 5);
			newT.transform.position = this.transform.position;
		}
	}
}