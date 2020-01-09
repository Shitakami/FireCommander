using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class HandHP : TargetHP {

	// Use this for initialization
	void Start () {
		hp.Value = HP;


		hp.ObserveEveryValueChanged(_ => _.Value)
			.Where(_ => _ <= 0)
			.Subscribe(_ => {
				GameObject newExplosion = Instantiate(explosion, this.transform.position, Quaternion.identity);
				Destroy(newExplosion, 4f);
				if (transform.parent != null) {
					var wave = transform.parent.GetComponent<Wave>();
					if (wave != null)
						wave.EnemyDecrement();
				}
				
				Destroy(this.gameObject);
			});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
