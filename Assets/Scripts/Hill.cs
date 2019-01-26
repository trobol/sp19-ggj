using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
[ExecuteInEditMode]
public class Hill : MonoBehaviour
{
  
  
    
    public Vector2 start, control, end;
    
    EdgeCollider2D collider;
    private void OnEnable() {
        collider = GetComponent<EdgeCollider2D>();
    }

    public Vector2[] controlPoints = {
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(2, 0)
    };

    private Vector2[] points;
    public int steps;
    public Vector2[] ComputePoints() {
        points = new Vector2[steps+1];
        for (int i = 0; i <= steps; i++) {
            float t = i / (float)steps;
			points[i] = Vector3.Lerp(Vector3.Lerp(controlPoints[0], controlPoints[1], t), Vector3.Lerp(controlPoints[1], controlPoints[2], t), t);
		}
        collider.points = points;
        return points;
    }
    //Everything after this will only execute outside of editor mode
    #if !UNITY_EDITOR 
    
    #endif
}
