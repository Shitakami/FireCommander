using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	[SerializeField]
	public List<ActionData> actions;
	protected Wave wave;
	Fire fireScript;
	// Use this for initialization
	void Start() {

		if (this.transform.parent != null)
			wave = transform.parent.GetComponent<Wave>();

		fireScript = GameObject.FindGameObjectWithTag("MissileGun").GetComponent<Fire>();

		StartCoroutine(MyUpdate());
	}

	protected virtual IEnumerator MyUpdate() {

		for (int i = 0; i < actions.Count; ++i) {

			switch (actions[i].actionType) {
				case Type.stop:
					yield return StartCoroutine(Stop(actions[i]));
					break;

				case Type.move:
					yield return StartCoroutine(Move(actions[i]));
					break;

				case Type.moveWithiTween:
					yield return StartCoroutine(MoveWithiTween(actions[i]));
					break;


			}
		}

		if (wave != null)
			wave.EnemyDecrement();
		fireScript.RemoveTarget(this.gameObject);
		Destroy(this.gameObject);
		

	}

	protected IEnumerator Stop(ActionData action) {
		
		yield return new WaitForSeconds(action.time);
	}

	protected virtual IEnumerator Move(ActionData action) {

		Vector3 speed = action.ToPosition - this.transform.position;
		speed /= action.time;

		float time = 0;
		
		while (time < action.time) {

			Vector3 pos = this.transform.position + speed * Time.deltaTime;
			this.transform.position = pos;

			yield return null;

			time += Time.deltaTime;

		}

	}


	protected IEnumerator MoveWithiTween(ActionData action) {

		
		iTween.MoveTo(this.gameObject, iTween.Hash(
			"x", action.ToPosition.x,
			"y", action.ToPosition.y,
			"z", action.ToPosition.z,
			"time", action.time));

		yield return new WaitForSeconds(action.time);
	}


}
