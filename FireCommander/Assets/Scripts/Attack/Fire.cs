using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine.UI;

public class Fire : MonoBehaviour {

	public GameObject missile;
	public GameObject Laser;
	public float MissileVolume;
	public float LaserVolume;
	public float interval;
	public int missilePerTarget;
	const int NOTHING = 0;
	const int MISSILE = 1;
	const int LASER = 2;

	int volume;
	public Text text;

	List<GameObject> targets;

	bool isLaser;

	AudioSource microphone;
	// Use this for initialization
	void Start() {
		text.text = "Target:0";
		targets = new List<GameObject>();
		Laser.SetActive(false);
		isLaser = false;

		// マイクの初期化
		microphone = GetComponent<AudioSource>();
		microphone.clip = Microphone.Start(null, true, 999, 44100);
		microphone.loop = true;
		while(!(Microphone.GetPosition("") > 0)) { }
		microphone.Play();

		/*
		 * 記録
		 * Updateストリームには2つ種類がある
		 * 1 UpdateAsObservable
		 * 2 Observable.EveryUpdate
		 * 
		 * 1の場合はオブジェクトがdestroyされた場合OnCompletedが自動で発行されるので
		 * 寿命管理が楽である
		 * 
		 * 2の場合はパフォーマンスが良いかわりにDisposeを手動で行う必要がある
		 * また、long型が返される
		 */

		Observable
			.EveryUpdate()
			.Subscribe(_ => {
				volume = GetAverageVolume();
			});

		Observable
			.EveryUpdate()
			.Where(_ => volume == MISSILE)
			.Where(_ => isLaser == false)
			.Throttle(TimeSpan.FromMilliseconds(100))
			.ThrottleFirst(TimeSpan.FromSeconds(interval))
			.Subscribe(_ => {
				//Debug.Log("Missile");
				ShotMissile();
				});

		/* 今回はレーザーを使用しない
		Observable
			.EveryUpdate()
			.Where(_ => isLaser == false)
			.Where(_ => volume == LASER)
			.Subscribe(_ => {
				Debug.Log("Laser");
				MainThreadDispatcher.StartEndOfFrameMicroCoroutine(ShotLaswer());
				});
		*/
	}

	void Update() {
		text.text = "Target:" + targets.Count;
	}

	int GetAverageVolume() {

		float[] data = new float[256];
		float a = 0;

		microphone.GetOutputData(data, 0);
		foreach(float s in data)
			a += Mathf.Abs(s);

		float volume = a / 256.0f;

		if(volume > LaserVolume)
			return LASER;

		if(volume > MissileVolume)
			return MISSILE;

		return NOTHING;
	}

	void ShotMissile() {

		for(int i = 0; i < targets.Count; ++i) {

			for(int j = 0; j < missilePerTarget; ++j) {
				
				Vector3 pos = this.transform.position;
				pos.x += UnityEngine.Random.Range(-3, 3);
				pos.y += UnityEngine.Random.Range(-3, 3);
				pos.z += UnityEngine.Random.Range(-3, 3);
				GameObject newMissile = Instantiate(missile, pos, this.transform.rotation);
				newMissile.GetComponent<Missile>().SetTarget(targets[i]);
				targets[i].GetComponent<TargetHP>().SetShooted();
			}
		}

		targets.Clear();

	}

	public void SetTarget(GameObject target) {
		targets.Add(target);
	}

	public void RemoveTarget(GameObject target) {
		targets.Remove(target);
	}


	IEnumerator ShotLaswer() {

		isLaser = true;
		Laser.SetActive(true);
		while(GetAverageVolume() == LASER) {

			yield return null;

		}
		Laser.SetActive(false);

		float time = 0;
		while(time < interval) {
			time += Time.deltaTime;
			yield return null;
		}
		isLaser = false;
	}

}