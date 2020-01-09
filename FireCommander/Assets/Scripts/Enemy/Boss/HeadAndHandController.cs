using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAndHandController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Down() {
		animator.SetBool("Down", true);
	}

	public void Wake() {
		animator.SetBool("Down", false);
	}

}
