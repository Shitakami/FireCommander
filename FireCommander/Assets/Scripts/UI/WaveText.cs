using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class WaveText : MonoBehaviour {

	[SerializeField]
	private float time;
	[SerializeField]
	private float d_size;

	Text text;
	// Use this for initialization
	void Start () {

		text = GetComponent<Text>();
		MainThreadDispatcher.StartUpdateMicroCoroutine(TextAnim());
		
	}

	IEnumerator TextAnim() {

		var color = text.color;
		int count = 70;
		var d_color = 1.0f / count;

		var size = this.transform.localScale;

		for (int i = 0; i < count; ++i) {
			color.a -= d_color;
			text.color = color;

			size.x += d_size;
			size.y += d_size;
			size.z += d_size;
			this.transform.localScale = size;
			yield return null;
		}
		Destroy(this.gameObject);
	}

}
