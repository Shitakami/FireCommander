using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenningSystem : MonoBehaviour {

	[SerializeField]
	private Text titleText;

	[SerializeField]
	private float titleTime;

	[SerializeField]
	private Text startText;


	// Use this for initialization
	void Start () {
		StartCoroutine(TitleOpenning());
	}

	IEnumerator TitleOpenning() {
		Color color = titleText.color;
		color.a = 0;
		titleText.color = color;
		
		yield return new WaitForSeconds(titleTime);

		int count = 70;
		var d = 1.0 / count;
		for (int i = 0; i < count; ++i) {
			color.a += (float)d;
			titleText.color = color;
			yield return null;
		}
		StartCoroutine(StartText());
	}

	IEnumerator StartText() {

		var color = startText.color;
		color.a = 0;
		startText.color = color;

		int count = 50;
		var d = 1.0 / count;
		while (true) {

			for (int i = 0; i < count; ++i) {
				color.a += (float)d;
				startText.color = color;
				yield return null;
			}

			d *= -1;
		}

	}

}
