using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Testplatform : MonoBehaviour {

	private EdgeCollider2D edge;
	private Rigidbody2D rb;

	public GameObject childPrefab;

	public testEdge floor;

	List<GameObject> children = new List<GameObject> ();
	float count = 0;

	// Use this for initialization
	void Start () {

		//edge = GetComponent<EdgeCollider2D> ();
		rb = GetComponent<Rigidbody2D> ();

		StartCoroutine(AddChildren());
		StartCoroutine(RecalculateBounds());
	
	}
	
	// Update is called once per frame
	void Update () {

		if (children.Count > 0) {
			rb.MovePosition (transform.position + new Vector3 (-1f * Time.deltaTime, 0, 0));
		}

		foreach (GameObject obj in children) {

			if (obj.transform.position.x <= -5) {
				GameObject.Destroy (obj);
				children.Remove (obj);

				RecalculateBounds ();


				break;
			}
				

		}
	}

	private IEnumerator RecalculateBounds(){

		while (true){

			float x_start = children [0].transform.position.x - 0.5f;
			float x_end = children [children.Count - 1].transform.position.x + 0.5f;

			floor.recalculateEdge (x_start, x_end);

			yield return new WaitForSeconds (0.1f);

		}


	}

	private IEnumerator AddChildren() {
		while (true) {

			GameObject child = Instantiate (childPrefab, Vector3.zero, transform.rotation) as GameObject;

			child.transform.parent = transform;

			//Vector2 off = new Vector2( children.Count * 0.5f,0);
			Vector3 pos = Vector3.right * count;

			//pos = new Vector3 (children.Count, 0, 0);
			child.transform.localPosition = pos;

			children.Add(child.gameObject);
			count += 1;

			RecalculateBounds ();

			yield return new WaitForSeconds(1f);



		}
	}
}
