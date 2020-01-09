using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMove : EnemyMove {

	[SerializeField]
	private int SecondMoveBeginIndex;

	[SerializeField]
	private int ThirdMoveBeginIndex;

	[SerializeField]
	private int FourthBeginIndex;

	private int startIndex;
	private int endIndex;

	int changeCount;

	void Start() {
		changeCount = 0;
		startIndex = SecondMoveBeginIndex;
		endIndex = ThirdMoveBeginIndex;
		if (startIndex == endIndex)
			endIndex = startIndex + 1;
		StartCoroutine(MyUpdate());
	}

	protected override IEnumerator MyUpdate() {

		for (int i = 0; i < startIndex; ++i) {

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
		

		StartCoroutine(EternalMove());

		
	}

	public IEnumerator EternalMove() {

		while (true) {
			for (int i = startIndex; i < endIndex; ++i) {

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
		}

	}

	public void ChangeMove() {

		if (changeCount == 0) {
			startIndex = ThirdMoveBeginIndex;
			endIndex = FourthBeginIndex;
		}
		else if (changeCount == 1) {

			startIndex = FourthBeginIndex;
			endIndex = actions.Count;

		}
		changeCount++;
	}

}
