using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]  // Siteスクリプトが優先的に実行される
public class Site : MonoBehaviour {

	public GameObject LeftUnder;
	public GameObject RightTop;
	public new Camera camera;
	Vector2 leftUnder;
	Vector2 rightTop;

	// Use this for initialization
	void Start () {
		leftUnder = RectTransformUtility.WorldToScreenPoint(camera, LeftUnder.transform.position);
		rightTop = RectTransformUtility.WorldToScreenPoint(camera, RightTop.transform.position);
		LeftUnder.SetActive(false);
		RightTop.SetActive(false);


	}


	public Vector2 GetLeftUnder() {
		return leftUnder;
	}

	public Vector2 GetRightTop() {
		return rightTop;
	}

}
