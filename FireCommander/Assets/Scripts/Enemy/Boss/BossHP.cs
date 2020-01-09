using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BossHP : TargetHP {

	[SerializeField]
	private GameObject marker;

	[SerializeField]
	private GameObject robo;

	[SerializeField]
	private GameObject HeadAndHand;

	[SerializeField]
	private GameObject BigExplosion;

	Marker markerScript;
	HatchController hatch;
	HeadAndHandController headAndHand;
	RoboMove roboMove;
	string OpenTag = "Untagged";
	string CloseTag = "Hatch";

	Vector3 startRotation;

	bool isDamaged;

	[SerializeField]
	GameObject[] waves;

	AudioSource[] se;

	int waveIndex;
	// Use this for initialization
	void Start () {

		markerScript = marker.GetComponent<Marker>();
		hatch = robo.GetComponent<HatchController>();
		headAndHand = HeadAndHand.GetComponent<HeadAndHandController>();
		roboMove = robo.GetComponent<RoboMove>();
		startRotation = robo.transform.eulerAngles;
		hp.Value = HP;
		marker.SetActive(false);
		this.gameObject.tag = CloseTag;
		se = GetComponents<AudioSource>();
		isDamaged = false;
		waveIndex = 0;

		hp.ObserveEveryValueChanged(_ => _.Value)
			.Where(_ => _ <= 0)
			.Subscribe(_ => {

				/* TargetHPのスクリプト
				GameObject newExplosion = Instantiate(explosion, this.transform.position, Quaternion.identity);
				Destroy(newExplosion, 4f);
				if (transform.parent != null) {
					var wave = transform.parent.GetComponent<Wave>();
					if (wave != null)
						wave.EnemyDecrement();
				}
				ScoreManager.Instance.destroyCount++;
				Destroy(this.gameObject);
				*/
				isDamaged = true;
				marker.SetActive(false);
				roboMove.StopAllCoroutines();
				StartCoroutine(DieAnim());
			});


		hp.ObserveEveryValueChanged(_ => _.Value)
			.Skip(1)
			.Where(_ => _ != 0)
			.Where(_ => _ % 3 == 0)
			.Subscribe(_ => {


				shooted = false;
				marker.SetActive(false);
				markerScript.ResetParamater();
				StartCoroutine(DownAnim());
			});


		this.OnTriggerEnterAsObservable()
			.Where(x => x.tag == "Attack")
			.Where(x => this.tag == "Hatch")
			.Subscribe(x => {
				shooted = false;
				markerScript.ResetParamater();
			});

		StartCoroutine(FirstWave());
		
	}

	public void OpenHatch() {

		this.tag = OpenTag;
		marker.SetActive(true);
		hatch.OpenHatch();
		headAndHand.Down();
		se[0].Play();

	}

	public void CloseHatch() {

		this.tag = CloseTag;
		marker.SetActive(false);
		hatch.CloseHatch();
		headAndHand.Wake();
		se[1].Play();
		Instantiate(waves[waveIndex]);
	}

	IEnumerator DownAnim() {

		isDamaged = true;
		waveIndex++;
		Vector3 angleForce = new Vector3(20, 5, 2);

		Rigidbody rigidbody = robo.AddComponent<Rigidbody>();
		rigidbody.AddTorque(angleForce);
		rigidbody.useGravity = false;
		roboMove.StopAllCoroutines();
		GameObject newExplosion = Instantiate(explosion, this.transform.position, Quaternion.identity);
		this.tag = CloseTag;

		Destroy(newExplosion, 4f);

		yield return new WaitForSeconds(3f);

		rigidbody.AddTorque(angleForce * -3);
		yield return new WaitForSeconds(1f);

		Destroy(rigidbody);
		hatch.CloseHatch();
		headAndHand.Wake();
		robo.transform.eulerAngles = startRotation;
		se[1].Play();

		roboMove.ChangeMove();
		StartCoroutine(roboMove.EternalMove());
		Instantiate(waves[waveIndex]);
	}

	IEnumerator DieAnim() {

		int count = 10;

		Rigidbody rigidbody = robo.AddComponent<Rigidbody>();
		rigidbody.AddTorque(new Vector3(30, 10, 5));
		rigidbody.useGravity = true;


		for (int i = 0; i < count; i++) {
			Vector3 pos = this.transform.position;
			pos.x += Random.Range(-3, 3);
			pos.y += Random.Range(-3, 3);
			pos.z += Random.Range(-3, 3);
			GameObject newExplosion = Instantiate(explosion, pos, Quaternion.identity);
			Destroy(newExplosion, 4f);
			yield return new WaitForSeconds(0.3f);
		}
		GameObject newBigExplosion = Instantiate(BigExplosion, this.transform.position, Quaternion.identity);
		Destroy(newBigExplosion, 4f);
		var wave = robo.transform.parent.GetComponent<Wave>();
		if (wave != null)
			wave.EnemyDecrement();
		Destroy(robo.gameObject);
	}

	public IEnumerator Rest() {
		OpenHatch();
		yield return new WaitForSeconds(6f);
		
		if (isDamaged == false)
			CloseHatch();
		else
			isDamaged = false;
	}

	IEnumerator FirstWave() {
		float waitTime = 3;
		yield return new WaitForSeconds(waitTime);
		Instantiate(waves[waveIndex]);
	}

}
