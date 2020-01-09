using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TargetMove : MonoBehaviour {

	public float rotateSpeed;
	public float verticalSpeed;
	public float height;
	GameObject MainCamera;
	Vector3 axis;
	float y;
	// Use this for initialization
	void Start() {


		MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

		axis = Vector3.up;
		if(Random.value <= 0.5f)
			rotateSpeed *= -1;
		if(Random.value <= 0.5f)
			height *= -1;
		y = transform.position.y;

		this.UpdateAsObservable()
			.Subscribe(_ => {
				float newY = y + Mathf.Sin(Time.time * verticalSpeed) * height;
				transform.RotateAround(MainCamera.transform.position, axis, rotateSpeed * Time.deltaTime);
				Vector3 pos = this.transform.position;
				pos.y = newY;
				this.transform.position = pos;
			});

	}
	

}
