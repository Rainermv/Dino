using System;
using UnityEngine;

public class WaterNode {

	public int index;

	public float x;
	public float y;
	public float velocity;
	public float acceleration;



	public float deltaLeft;
	public float deltaRight;
}

public class WaterMesh {

	public GameObject meshObject;
	public Mesh mesh;

	public GameObject colliderObject;

}

public class WaterBodyComponent : MonoBehaviour
{

	public GameObject splash;
	public Material mat;
	public GameObject waterPrefab;


	const float SPRING_CONSTANT = 0.02f;
	const float DAMPING = 0.04f;
	const float SPREAD = 0.05f;

	float baseHeight;
	float left;
	float bottom;

	float z = 0;

	//protected WaterBody body;



	WaterNode[] waterNodes;
	WaterMesh[] waterMeshes;
	//float[] xPositions;
	//float[] yPositions;
	//float[] velocities;
	//float[] accelerations;
	//GameObject[] meshObjects;
	//Mesh[] meshes;

	//GameObject[] colliders;

	LineRenderer body;

	void Start(){

		SpawnWater (-10, 20, 0, -3);

	}


	public void SpawnWater(float Left, float Width, float Top, float Bottom){

		int edgeCount = Mathf.RoundToInt (Width) * 5;
		int nodeCount = edgeCount + 1;
	
		body = gameObject.AddComponent<LineRenderer> ();
		body.material = mat;
		body.material.renderQueue = 1000;
		body.SetVertexCount (nodeCount);
		body.SetWidth (0.1f, 0.1f);

		waterNodes = new WaterNode[ nodeCount ];
		waterMeshes = new WaterMesh[ edgeCount ];
		//yPositions = new float[ nodeCount ];
		//velocities = new float[ nodeCopunt ];
		//accelerations = new float[] 

		baseHeight = Top;
		bottom = Bottom;
		left = Left;

		// Create the water physics nodes
		for (int i = 0; i < nodeCount; i++){

			WaterNode waterNode = new WaterNode ();

			waterNode.index = i;

			waterNode.y = Top;
			waterNode.x = Left + Width * waterNode.index / edgeCount;
			waterNode.acceleration = 0;
			waterNode.velocity = 0;


			body.SetPosition (waterNode.index, new Vector3 (waterNode.x, waterNode.y, z));

			waterNodes [i] = waterNode;

		}

		// Create the water meshes
		for (int i = 0; i < edgeCount; i++){

			Vector3[] vertices = new Vector3[4];
			vertices [0] = new Vector3 (waterNodes [i].x, waterNodes[i].y, z);
			vertices [1] = new Vector3 (waterNodes [i + 1].x, waterNodes [i + 1].y, z);
			vertices [2] = new Vector3 (waterNodes [i].x, Bottom, z);
			vertices [3] = new Vector3 (waterNodes [i + 1].x, Bottom, z);

			Vector2[] uvs = new Vector2[4];
			uvs [0] = new Vector2 (0, 1);
			uvs [1] = new Vector2 (1, 1);
			uvs [2] = new Vector2 (0, 0);
			uvs [3] = new Vector2 (1, 0);

			/*
			 * 0 -- 1 
			 * | \  |
			 * |  \	|
			 * 2 -- 3
			*/
			int[] triangles = new int[6] { 0, 1, 3, 3, 2, 0 };



			Mesh mesh = new Mesh ();
			mesh.vertices = vertices;
			mesh.uv = uvs;
			mesh.triangles = triangles;

			GameObject meshObject = Instantiate (waterPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			meshObject.GetComponent<MeshFilter> ().mesh = mesh;
			meshObject.transform.parent = transform;

			GameObject colliderObject = new GameObject ();
			colliderObject.name = "Trigger";
			colliderObject.transform.parent = transform;
			colliderObject.transform.position = new Vector3 (Left + Width * (i + 0.5f) / edgeCount, Top - 0.5f, z);
			colliderObject.transform.localScale = new Vector3 (Width / edgeCount, 1, 1);
			//colliderObject.AddComponent<WaterDetector> ();
			Collider2D box = colliderObject.AddComponent<BoxCollider2D> ();
			box.isTrigger = true;

			WaterMesh waterMesh = new WaterMesh ();

			waterMesh.meshObject = meshObject;
			waterMesh.mesh = mesh;
			waterMesh.colliderObject = colliderObject;

		}
	}

	public void UpdateMeshes(){

		for (int i = 0; i < waterMeshes.Length; i++){

			Vector3[] vertices = new Vector3[4];
			vertices [0] = new Vector3 (waterNodes [i].x, waterNodes[i].y, z);
			vertices [1] = new Vector3 (waterNodes [i + 1].x, waterNodes [i + 1].y, z);
			vertices [2] = new Vector3 (waterNodes [i].x, bottom, z);
			vertices [3] = new Vector3 (waterNodes [i + 1].x, bottom, z);

			print (waterMeshes [i].mesh.vertices);

			waterMeshes [i].mesh.vertices = vertices;

		}
	}

	void FixedUpdate(){ 

		// Hooke's Law with the Euler method to find positions, accelerations and velocities
		for (int i = 0; i < waterNodes.Length; i++) {

			WaterNode node = waterNodes [i];

			float force = SPRING_CONSTANT * (node.y - baseHeight) + node.velocity * DAMPING;

			node.acceleration = -force; // add force to the acceleration
			node.y += node.velocity; // add velocity to the position
			node.velocity += node.acceleration; // add acceleration to the velocity

			body.SetPosition (i, new Vector3 (node.x, node.y, z)); // set the final position

			// Reset the deltas to begin wave propagation
			node.deltaLeft = 0;
			node.deltaRight = 0;
		}

		// Wave propagation
		for (int j = 0; j < 8; j++) {

			for (int i = 0; i < waterNodes.Length; i++) {

				// Check the heights of the nearby nodes, adjust velocities and record height differences
				if (i > 0) {

					waterNodes [i].deltaLeft = SPREAD * (waterNodes[i].y - waterNodes[i-1].y);
					waterNodes [i - 1].velocity += waterNodes [i].deltaLeft;
				}

				if (i < waterNodes.Length - 1) {

					waterNodes [i].deltaRight = SPREAD * (waterNodes [i].y - waterNodes [i + 1].y);
					waterNodes [i + 1].velocity += waterNodes [i].deltaRight;
				}
			}
		}

		// Apply a difference in position
		for (int i = 0; i < waterNodes.Length; i++) {

			if (i > 0) {
				waterNodes [i - 1].y += waterNodes [i].deltaLeft;
			}

			if (i < waterNodes.Length - 1) {
				waterNodes [i + 1].y += waterNodes [i].deltaRight;
			}
		}

		UpdateMeshes ();

	}
}


