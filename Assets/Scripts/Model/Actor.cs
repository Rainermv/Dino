using UnityEngine;
using System.Collections;

public abstract class Actor {

	protected World world = World.getInstance();

	public int id = 0;

	public Vector2 startingPosition = Vector2.zero;

	public string tag = "Default";
	public int layer = 9;

	public Color tint = Color.white;
	
	public string name = "Generic Actor";
	public string spriteKey = "default";
	public Vector2 size = new Vector2(1,1);
	
	//public bool useGravity;
	public bool isKinematic = false;
	public Vector2 velocity = Vector2.zero;
	public bool constrainRotation = false;
	public bool constrainMovement = false;
	public string physicsMaterialKey = "Default";

	public bool affectedByWorldMovement = true;
}
