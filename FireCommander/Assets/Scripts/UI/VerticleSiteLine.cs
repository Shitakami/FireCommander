using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class VerticleSiteLine : MonoBehaviour {
	new Transform camera;
	Material material;
	int propId = 0;
	// Use this for initialization
	void Start () {
		camera = transform.parent;
		material = GetComponent<Renderer>().material;

		// マテリアルにアクセスする場合はPropertyToIDをしたほうが効率が良い
		propId = Shader.PropertyToID("_Angle");
		MainThreadDispatcher.StartUpdateMicroCoroutine(Opening());
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 ang = transform.eulerAngles;
		ang.z = 0;
		transform.eulerAngles = ang;

		material.SetFloat(propId, Mathf.Abs(-camera.eulerAngles.x));

	}

	IEnumerator Opening() {

		int count = 60;

		var alpha = Shader.PropertyToID("_LineColor");
		var mat = GetComponent<Renderer>().material;
		var color = mat.GetColor(alpha);
		var d_alpha = color.a / count;
		
		for (int i = 0; i < count; ++i) {
			color.a = d_alpha * i;
			mat.SetColor(alpha, color);
			yield return null;
		}

	}

}
