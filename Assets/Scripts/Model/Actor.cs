using UnityEngine;
using System.Collections;

public abstract class Actor {

	public string tag;
	public int layer;

	public Color tint;
	
	public string name;
	public string spriteKey;
	public Vector2 size;
	
	//public bool useGravity;
	public bool isKinematic;
	public Vector2 velocity;

	public bool affectedByWorldMovement;
}
