using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Actor {

    public List<SpriteComposition> childrenSprites = new List<SpriteComposition>();

    public delegate void OnChangeSpriteKey();
    public OnChangeSpriteKey onChangeSpriteKey;

	protected World world = World.GetInstance();

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
    public List<EffectorInfo> effectors = new List<EffectorInfo>();

    public bool isKinematic = false;
	public Vector2 velocity = Vector2.zero;

	public bool constrainRotation = false;
	public bool constrainMovement = false;

	//public bool affectedByWorldMovement = true;
	public float worldMovementMultiplier = 1;

	public bool hasRigidbody = true;
    public bool indestructable = false;
}
