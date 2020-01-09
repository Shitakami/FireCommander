using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

public class StudyLINQ : MonoBehaviour {

	public GameObject cube;

	// Use this for initialization
	void Start () {
		List<GameObject> cubes = new List<GameObject>();

		Enumerable.Range(0, 100)
				.Select(_ => Instantiate(cube, UnityEngine.Random.insideUnitSphere, Quaternion.identity))
				.Where(x => x.transform.position.sqrMagnitude < 25)
				.Select(x => x.GetComponent<MeshRenderer>().material)
				.ToList()
				.ForEach(x => x.SetColor("_Main", new Color(1, 0, 0)));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
