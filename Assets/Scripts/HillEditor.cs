using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Hill))]
public class HillEditor : Editor
{
	Transform handleTransform;
	Quaternion handleRotation;
	Hill hill;
	private void OnSceneGUI()
	{
		hill = target as Hill;
		if (!hill.isActiveAndEnabled) return;
		handleTransform = hill.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		Vector2 top = ShowPoint(0);
		Vector2 start = ShowPoint(1);
		Vector2 control = ShowPoint(2);
		Vector2 end = ShowPoint(3);

		Handles.color = Color.gray;
		Handles.DrawLine(start, control);
		Handles.DrawLine(control, end);

		Handles.color = Color.red;
		Vector2[] points = hill.ComputePoints();
		Vector2 lineStart = top,
		lineEnd;
		for (int i = 1; i <= hill.steps; i++)
		{
			lineEnd = handleTransform.TransformPoint(points[i]);
			Handles.DrawLine(lineStart, lineEnd);
			lineStart = lineEnd;
		}
	}

	private Vector2 ShowPoint(int i)
	{
		Vector2 point = handleTransform.TransformPoint(hill.controlPoints[i]);
		EditorGUI.BeginChangeCheck();
		point = Handles.DoPositionHandle(point, handleRotation);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(hill, "Move Point");
			EditorUtility.SetDirty(hill);
			hill.controlPoints[i] = handleTransform.InverseTransformPoint(point);
		}
		return point;
	}

    
}
