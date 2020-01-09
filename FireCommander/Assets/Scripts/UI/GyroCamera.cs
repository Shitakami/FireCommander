using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour {

	Transform m_transform;
	readonly Quaternion _BASE_ROTATION = Quaternion.Euler(90, 0, 0);

	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
		m_transform = transform;

	}
	
	// Update is called once per frame
	void Update () {

		//transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.right) * Input.gyro.attitude * Quaternion.AngleAxis(180.0f, Vector3.forward);
		Quaternion gyro = Input.gyro.attitude;
		m_transform.rotation = _BASE_ROTATION * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
	}
}
