using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : Wave {

	[SerializeField]
	private bool isChild;

	GameObject boss;
	BossHP bossHP;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag("Hatch");
		if (isChild)
			this.transform.parent = boss.transform;
		bossHP = boss.GetComponent<BossHP>();
		count = transform.childCount;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void EnemyDecrement() {
		count--;

		if (count == 0) {
			StartCoroutine(bossHP.Rest());
			// 即Destroyするとバグが発生するので5秒後に実行
			Destroy(this.gameObject, 5);
		}
	}
}
