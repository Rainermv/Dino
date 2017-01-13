using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testEdge : MonoBehaviour {

	private EdgeCollider2D edge;

	Vector2[] points = new Vector2[2];

	// Use this for initialization
	void Start () {

		edge = GetComponent<EdgeCollider2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void recalculateEdge(float x_start, float x_end){

		points[0].x = x_start;
		points[1].x = x_end;

		edge.Reset ();
		edge.points = points;

	}
}
