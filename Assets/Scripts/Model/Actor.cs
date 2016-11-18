using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ColliderType {

	Box,
	Circle

}

public class ColliderInfo{

	public ColliderType type = ColliderType.Box;
	public Vector2 size = new Vector2(1,1);
	public Vector2 offset = new Vector2(0,0);
	public string materialKey = "Default";
	public bool trigger = false;

}

public abstract class Actor {

	protected World world = World.getInstance();

	public int id = 0;
	public Vector2 startingPosition = Vector2.zero;

	public string tag = "Untagged";
	public int layer = Layers.FLOORS;

	public Color tint = Color.white;
	
	public string name = "Generic Actor";
	public string spriteKey = "default";
	public int depth = 0;

	public Vector2 scale = new Vector2(1,1);

	public List<ColliderInfo> colliders = new List<ColliderInfo> ();
	
	public bool isKinematic = false;
	public Vector2 velocity = Vector2.zero;

	public bool constrainRotation = false;
	public bool constrainMovement = false;

	public bool affectedByWorldMovement = true;
	public float worldMovementMultiplier = 1;

	public bool hasRigidbody = true;

}
