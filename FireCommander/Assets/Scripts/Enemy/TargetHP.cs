using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TargetHP : MonoBehaviour {

	public GameObject explosion;
	public int HP;
	public ReactiveProperty<int> hp = new ReactiveProperty<int>();

	protected bool shooted = false;
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
				ScoreManager.Instance.destroyCount++;
				Destroy(this.gameObject);
			});
		
	}

	public void SetShooted() {
		shooted = true;
	}


	public bool GetShooted() {

		return shooted;

	}

	}
