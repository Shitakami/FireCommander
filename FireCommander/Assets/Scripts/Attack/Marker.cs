using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class Marker : MonoBehaviour {

	GameObject target;
	public bool isLock;
	public bool isInSite;

	public float lockTime;

	Vector2 rightTop;
	Vector2 leftUnder;

	Fire fireScript;
	TargetHP targetHPScript;
	GameObject MainCamera;
	new Camera camera;

	Animator _animator;
	AudioSource[] _audioSorces;
	// Use this for initialization
	void Start() {

	
		target = this.transform.parent.gameObject;
		targetHPScript = target.GetComponent<TargetHP>();

		fireScript = GameObject.FindWithTag("MissileGun").GetComponent<Fire>();
		MainCamera = GameObject.FindWithTag("MainCamera");
		camera = MainCamera.GetComponent<Camera>();
		_animator = GetComponent<Animator>();
		_audioSorces = GetComponents<AudioSource>();
		rightTop = MainCamera.GetComponent<Site>().GetRightTop();
		leftUnder = MainCamera.GetComponent<Site>().GetLeftUnder();
		isLock = false;
		isInSite = false;



		/*
		 * デバック関数
		 * スペースを押すとスクリーンポジションが出力される
		 *
		this.UpdateAsObservable()
			.Where(_ => Input.GetKeyDown(KeyCode.Space))
			.Subscribe(_ => {
				Vector2 pos = RectTransformUtility.WorldToScreenPoint(camera, target.transform.position);
				Debug.Log(pos);
				Debug.Log(IsInCamera());
				Debug.Log(IsInSite());
			});
		*/


		// ロックオンマーカーは常にプレイヤーを見る
		this.UpdateAsObservable()
			.Subscribe(_ => {
				Vector3 ang = MainCamera.transform.eulerAngles;
				ang.z = 0;
				this.transform.eulerAngles = ang;


				});


		// ロックオンされていない、サイトに入っていない状態からサイトに入るとロックオン処理をする
		this.UpdateAsObservable()
				.Where(_ => !isLock)
				.Where(_ => !isInSite)
				.Where(_ => !targetHPScript.GetShooted())
				.Where(_ => IsInCamera())
				.Where(_ => IsInSite())
				.Subscribe(_ => {
				//	Debug.Log("SiteIn");
					isInSite = true;
					MainThreadDispatcher.StartUpdateMicroCoroutine(LockTimer());
					_animator.SetBool("Next", true);
					_audioSorces[0].Play();
				});


		// サイトから出るとロックオン処理をやめる、ロックオンしているなら外す
		this.UpdateAsObservable()
			.Where(_ => isInSite)
			.Where(_ => !targetHPScript.GetShooted())
			.Where(_ => !IsInCamera() || !IsInSite())
			.Subscribe(_ => {
				//	Debug.Log("SiteOut");

				if (isLock == true) {

					fireScript.RemoveTarget(target);
					isLock = false;

				}

				isInSite = false;
				_animator.SetBool("Next", false);
				_animator.SetBool("LockOn", false);
			});

	}



	IEnumerator LockTimer() {

		float time = 0;

		while(time < lockTime) {

			yield return null;

			if (isInSite == false)
				yield break;

			time += Time.deltaTime;

		}

		isLock = true;
		//Debug.Log("LockOn!");
		if(target == null)
			yield break;
		_animator.SetBool("LockOn", true);
		_audioSorces[1].Play();
		fireScript.SetTarget(target);

	}


	bool IsInCamera() {

		Vector3 vec = target.transform.position - MainCamera.transform.position;
		Vector3 forward = MainCamera.transform.forward;

		return 0 < Vector3.Dot(vec, forward);
	}

	bool IsInSite() {

		Vector2 pos = RectTransformUtility.WorldToScreenPoint(camera, target.transform.position);

		bool retVal = (leftUnder.x < pos.x) && (pos.x < rightTop.x) &&
			(leftUnder.y < pos.y) && (pos.y < rightTop.y);


		return retVal;


	}

	public void ResetParamater(){
		isInSite = false;
		isLock = false;
		_animator.SetBool("Next", false);
		_animator.SetBool("LockOn", false);
	}

}