using System;
using UnityEngine;

public struct WaterNode {

	public int index;

	public float xPosition;
	public float yPosition;
	public float velocity;
	public float acceleration;

	public GameObject meshObject;
	public Mesh mesh;

	public GameObject collider;
}

public class WaterBodyComponent : GameObject
{

	public float Left;
	public float Width;
	public float Top;
	public float Bottom;

	public GameObject splash;
	public Material mat;
	public GameObject watermesh;


	const float SPRING_CONSTANT = 0.02f;
	const float DAMPING = 0.04f;
	const float SPREAD = 0.05f;

	float baseHeight;
	float left;
	float bottom;

	//protected WaterBody body;



	WaterNode[] nodes;
	//float[] xPositions;
	//float[] yPositions;
	//float[] velocities;
	//float[] accelerations;
	//GameObject[] meshObjects;
	//Mesh[] meshes;

	//GameObject[] colliders;

	LineRenderer body;



	void Start(){

		int edgeCount = Mathf.RoundToInt (Width) * 5;
		int nodeCount = edgeCount + 1;
	
		body = gameObject.AddComponent<LineRenderer> ();
		body.material = mat;
		body.material.renderQueue = 1000;
		body.SetVertexCount (nodeCount);
		body.SetWidth (0.1f, 0.1f);

		nodes = new WaterNode[ nodeCount ];
		//yPositions = new float[ nodeCount ];
		//velocities = new float[ nodeCopunt ];
		//accelerations = new float[] 

		baseHeight = Top;
		bottom = Bottom;
		left = Left;

		int i = 0;
		foreach (WaterNode waterNode in nodes) {

			waterNode.index = i++;

			waterNode.yPosition = Top;
			waterNode.xPosition = Left + Width * waterNode.index / edgeCount;
			waterNode.acceleration = 0;
			waterNode.velocity = 0;


			body.SetPosition( waterNode.index, new Vector3(

						}

	}
}


