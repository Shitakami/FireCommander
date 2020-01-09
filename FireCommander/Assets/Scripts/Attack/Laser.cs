using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Laser : MonoBehaviour {

	public int LaserDamage;

	// Use this for initialization
	void Start () {

		this.OnTriggerEnterAsObservable()
			.Where(x => x.gameObject.tag == "Target")
			.Subscribe(x => {
				x.GetComponent<TargetHP>().hp.Value -= LaserDamage;
			});

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
