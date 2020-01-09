using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "Your Score\n" + 
			"Count  " + ScoreManager.Instance.destroyCount + "\n" + 
			"<size=100>" + (int)(ScoreManager.Instance.CalcScore() * 100) + "</size>%";
	}
	

}
