using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveLoop : EnemyMove {

	[SerializeField]
	private int loopBegin;

	// Use this for initialization
	void Start () {
		if (this.transform.parent != null)
			wave = transform.parent.GetComponent<Wave>();

	

		StartCoroutine(MyUpdate());
	}

	protected override IEnumerator MyUpdate() {

		int i = 0;

		while (true) {
			
			 while(i < actions.Count){
			
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

				i++;
			}

			i = loopBegin;

		}


	}
}
