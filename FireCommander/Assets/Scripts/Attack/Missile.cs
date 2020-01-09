using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class Missile : MonoBehaviour {

	public GameObject target;
	public float speed;
	public float angle_velocity;
	public GameObject smallExplosion;
	public GameObject trail;

	Rigidbody thisRigid;

	// Use this for initialization
	void Start() {
		thisRigid = GetComponent<Rigidbody>();

		this.OnTriggerEnterAsObservable()
			.Where(x => x.gameObject == target.gameObject)
			.Where(x => x.tag != "Hatch")
			.Subscribe(x => {
				x.GetComponent<TargetHP>().hp.Value -= 1;
				Explosion();
			});

		this.OnTriggerEnterAsObservable()
			.Where(x => x.gameObject == target.gameObject)
			.Where(x => x.tag == "Hatch")
			.Subscribe(x => {
				Explosion();
			});

		this.UpdateAsObservable()
			.Where(_ => target != null)
			.Subscribe(_ => {
				// ミサイルを敵の方向に向ける
				Quaternion direction = Quaternion.LookRotation(target.transform.position - this.transform.position);
				transform.rotation = Quaternion.Slerp(this.transform.rotation, direction, angle_velocity * Time.deltaTime);

				thisRigid.velocity = speed * transform.forward;
			});

		this.UpdateAsObservable()
			.First()
			.Where(_ => target == null)
			.Subscribe(_ => {
				Destroy(this.gameObject, 3);
			});

	}

	void Explosion() {
		GameObject newEx = Instantiate(smallExplosion, this.transform.position, Quaternion.identity);
		trail.transform.parent = null;
		Destroy(newEx.gameObject, 3f);
		Destroy(trail.gameObject, 3f);
		Destroy(this.gameObject);
	}

	public void SetTarget(GameObject t) {
		target = t;
	}

}
