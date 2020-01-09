using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocalMoveLoop : EnemyMoveLoop {

	// Use this for initialization
	void Start () {
		if (this.transform.parent != null)
			wave = transform.parent.GetComponent<Wave>();

		

		StartCoroutine(MyUpdate());
	}

	protected override IEnumerator Move(ActionData action) {


		Vector3 speed = action.ToPosition - this.transform.localPosition;
		speed /= action.time;

		float time = 0;

		while (time < action.time) {

			Vector3 pos = this.transform.localPosition + speed * Time.deltaTime;
			this.transform.localPosition = pos;

			yield return null;

			time += Time.deltaTime;

		}

	}

}
