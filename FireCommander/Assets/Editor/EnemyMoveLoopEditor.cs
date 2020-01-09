using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
	

[CustomEditor(typeof(EnemyMoveLoop))]
public class EnemyMoveLoopEditor : Editor {

	ReorderableList reorderableList;
	int i;

	float randMin = -30;
	float randMax = 30;

	void OnEnable() {

		var prop = serializedObject.FindProperty("actions");
		EnemyMoveLoop t = target as EnemyMoveLoop;
		reorderableList = new ReorderableList(t.actions, typeof(ActionData));
		reorderableList.displayAdd = true;
		reorderableList.displayRemove = true;

		reorderableList.drawHeaderCallback = (rect) =>
		EditorGUI.LabelField(rect, prop.displayName);

		reorderableList.elementHeight = 65;
		reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
			// var element = prop.GetArrayElementAtIndex(index);
			var data = t.actions[index];
			//	rect.height -= 4;
			//	rect.y += 2;

			/*
			var displayName = string.Format("{0} : {1} : {2}",
				element.FindPropertyRelative("actionType").type,
				element.FindPropertyRelative("ToPosition").vector3Value,
				element.FindPropertyRelative("time").floatValue
				);
			EditorGUI.LabelField(rect, displayName);
			*/




			/*
			element.vector3Value = EditorGUI.Vector3Field(rect, "ToPosition", element.vector3Value);
			rect.y += 20;
			rect.height = 20;
			element.floatValue = EditorGUI.FloatField(rect, "time", element.floatValue);
			*/

			rect.height -= 4;
			rect.y += 2;
			rect.width = 120;
			data.actionType = (Type)EditorGUI.EnumPopup(rect, data.actionType);

			rect.y += 20;
			rect.width = 500;
			if (data.actionType != Type.stop) {

				rect.height = 30;

				data.ToPosition = EditorGUI.Vector3Field(rect, "ToPosition", data.ToPosition);
				rect.y += 20;
			}

			rect.height = 17;
			data.time = EditorGUI.FloatField(rect, "time", data.time);



		};


		reorderableList.onAddCallback += (list) => {

			prop.arraySize++;

			list.index = prop.arraySize - 1;

			t.actions.Add(new ActionData());
			Undo.RegisterCompleteObjectUndo(target, "Chenge ToPosition");

		};

		reorderableList.onRemoveCallback += (list) => {

			prop.arraySize--;
			list.index = prop.arraySize - 1;
			t.actions.RemoveAt(list.index);
			Undo.RegisterCompleteObjectUndo(target, "Chenge ToPosition");
		};

		// クリックした添え字を取得
		reorderableList.onMouseUpCallback = (list) => {

			i = list.index;

		};

		reorderableList.onChangedCallback = (list) => {

			Undo.RegisterCompleteObjectUndo(target, "Change property");
		};

	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI();


		serializedObject.Update();

		reorderableList.DoLayoutList();

		randMin = EditorGUILayout.FloatField("Random Min", randMin);
		randMax = EditorGUILayout.FloatField("Random Max", randMax);
		if (GUILayout.Button("RandPosition")) {
			EnemyMoveLoop t = target as EnemyMoveLoop;
			var data = t.actions[i];
			if (data.actionType != Type.stop) {
				Vector3 pos = new Vector3(
					Random.Range(randMin, randMax),
					Random.Range(randMin, randMax),
					Random.Range(randMin, randMax));
				data.ToPosition = pos;
				Undo.RegisterCompleteObjectUndo(target, "Chenge ToPosition");
			}


		}

		if (GUILayout.Button("RandAdd")) {
			EnemyMoveLoop t = target as EnemyMoveLoop;
			for (int i = 0; i < t.actions.Count; i++) {
				var data = t.actions[i];
				if (data.actionType != Type.stop) {
					Vector3 pos = new Vector3(
					Random.Range(randMin, randMax),
					Random.Range(randMin, randMax),
					Random.Range(randMin, randMax));
					data.ToPosition += pos;
					Undo.RegisterCompleteObjectUndo(target, "Change ToPosition");

				}
			}


		}

		if (GUILayout.Button("MultMinus")) {

			EnemyMoveLoop t = target as EnemyMoveLoop;
			for (int i = 0; i < t.actions.Count; i++) {
				var data = t.actions[i];
				data.ToPosition *= -1;

			}
			Undo.RegisterCompleteObjectUndo(target, "Change ToPosition");
		}

		serializedObject.ApplyModifiedProperties();
	}


}
