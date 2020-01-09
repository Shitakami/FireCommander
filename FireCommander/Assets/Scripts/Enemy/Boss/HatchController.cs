using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HatchController : MonoBehaviour {

	Animator _animator;
	int id;


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenHatch() {
		_animator.SetBool("Open", true);
	}

	public void CloseHatch() {
		_animator.SetBool("Open", false);
	}

}
