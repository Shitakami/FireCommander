using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { stop, move, moveWithiTween };

[System.Serializable]
public class ActionData {

	[SerializeField]
	public Type actionType;
	[SerializeField]
	public Vector3 ToPosition;
	[SerializeField]
	public float time;



}