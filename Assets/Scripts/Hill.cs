using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Hill : MonoBehaviour
{



	public Vector2 start, control, end;

	public float height;
	public float speed = 1;
    public Vector2 endForce;
	private void OnEnable()
	{
		
	}

	public Vector2[] controlPoints = {
		new Vector2(-1,-1),
		new Vector2(0,0),
		new Vector2(1,1),
		new Vector2(2, 0)
	};

	private Vector2[] points;
	public int steps = 10;
	public Vector2[] ComputePoints()
	{
		points = new Vector2[steps + 1];
		points[0] = controlPoints[0];
		for (int i = 1; i <= steps; i++)
		{
			float t = i / (float)steps;
			points[i] = Vector3.Lerp(Vector3.Lerp(controlPoints[1], controlPoints[2], t), Vector3.Lerp(controlPoints[2], controlPoints[3], t), t);
		}
		return points;
	}

	public float PosToT(Vector2 pos)
	{
		float distance = Mathf.Abs(controlPoints[3].x - controlPoints[1].x);

		return Mathf.Abs(transform.TransformPoint(controlPoints[1]).x - pos.x) / distance;
	}
	public Vector2 GetPoint(float t)
	{
		if(t <= 0) {
			return Vector3.Lerp(transform.TransformPoint(controlPoints[0]), transform.TransformPoint(controlPoints[1]), 1+t);
		} else {
			return transform.TransformPoint(Vector3.Lerp(Vector3.Lerp(controlPoints[1], controlPoints[2], t), Vector3.Lerp(controlPoints[2], controlPoints[3], t), t));
		}
		
	}

	//Everything after this will only execute outside of editor mode
#if !UNITY_EDITOR
    
#endif
}
