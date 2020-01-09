using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager> {

	public List<GameObject> waves;
	int count;
	public int destroyCount;

	// Use this for initialization
	void Start () {
		count = 0;
		destroyCount = 0;
		for (int i = 0; i < waves.Count; ++i)
			count += waves[i].transform.childCount;
	}

	public float CalcScore() {
		float score = (float)destroyCount / count;
		destroyCount = 0;
		return score;
	}

}
