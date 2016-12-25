using UnityEngine;
using System.Collections;

public class Player : Character  {
	
	public int maxJumps;
	public int jumps;

	public float stableXPosition;
	public float recomposeXSpeed;
	public float recomposeDistance;

	public float dynamicVelocityAdjust;

	public Player(){

		this.tag = "PLAYER";
		this.layer = this.layer = Layers.CHARACTERS;

		this.depth = 5;

		//this.tint = Color.white;

		this.name = "Santa";
		this.spriteKey = null;

		addCharacterState(CharacterAnimationType.RUN, "santa_run", Vector2.zero);
		addCharacterState(CharacterAnimationType.JUMP, "santa_jump",Vector2.zero);

		this.initialStateType = CharacterAnimationType.RUN;

		float width = 0.8f;
		float height = 1.5f;

		float offset_x = -0.3f;

		setCollider (new Vector2 (width, height), new Vector2 (offset_x, -0.1f));

		this.startingPosition = new Vector2( 
			world.SCREEN_LEFT -2,
			world.FLOOR_Y + height
		);

		stableXPosition = world.SCREEN_MIDPOINT - world.SCREEN_WIDTH * 0.05f;

		this.isKinematic = false;
		this.velocity = new Vector2(0,0);

		//recomposeXSpeed = 2;
		recomposeXSpeed = 5;
		recomposeDistance = 6;

		dynamicVelocityAdjust = 1f;

		//affectedByWorldMovement = false;

		constrainMovement = false;
		constrainRotation = true;

		jumpForce = new Vector2(0,520);
		maxJumps = jumps = 3;
			
	}
}
