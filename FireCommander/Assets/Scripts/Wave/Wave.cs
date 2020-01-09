using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Wave : MonoBehaviour {

	[SerializeField]
	private GameObject nextWave;
	[SerializeField]
	private Text waveText;

	protected GameObject canvas;
	protected int count;
	float waitTime = 2f;
	// Use this for initialization
	void Start () {

		count = transform.childCount;
		canvas = GameObject.FindGameObjectWithTag("UI");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void EnemyDecrement() {
		count--;
		if (count == 0) {
			if (nextWave != null)
				StartCoroutine(NextWave());
		}
	}

	IEnumerator NextWave() {

		var text = Instantiate(waveText);
		text.transform.parent = canvas.transform;
		text.text = nextWave.transform.name;

		yield return new WaitForSeconds(waitTime);

		Instantiate(nextWave);
		Destroy(this.gameObject);

	}

}
