using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SiteScale : MonoBehaviour {

	public float angleRate;

	Transform Camera;
	float startAngle;
	Vector3 startPos;

	// Use this for initialization
	void Start () {
		Camera = GameObject.FindWithTag("MainCamera").transform;
		startAngle = Camera.transform.eulerAngles.x;
		startPos = this.transform.position;

		Observable
			.EveryUpdate()
			.Subscribe(_ => {



				
				//Vector3 ang = this.transform.localPosition;
				float x = Camera.transform.eulerAngles.x;
				if (x > 180)
					x -= 360;

				Vector3 newPos = startPos + (x - startAngle) * angleRate * Vector3.up;
				
				this.transform.position = newPos;


				Vector3 angle = this.transform.eulerAngles;
				angle.z = 0;
				this.transform.eulerAngles = angle;

			});

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
