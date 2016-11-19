using UnityEngine;
using System.Collections;

public class Player : Character  {
	
	public int maxJumps;
	public int jumps;

	public float stableXPosition;
	public float recomposeXSpeed;
	public float recomposeDistance;


	public Player(){

		this.tag = "PLAYER";
		this.layer = this.layer = Layers.CHARACTERS;

		this.depth = 5;

		this.tint = Color.white;

		this.name = "Santa";
		this.spriteKey = null;

		this.animationKeys.Add(CharacterAnimationState.RUN, "santa_run");
		this.animationKeys.Add(CharacterAnimationState.JUMP, "santa_jump");
		this.initialState = CharacterAnimationState.RUN;

		float width = 0.8f;
		float height = 1.5f;

		float offset_x = -0.3f;

		ColliderInfo boxCollider = new ColliderInfo ();
		boxCollider.type = ColliderType.Box;
		boxCollider.size = new Vector2(width,height);
		boxCollider.offset = new Vector2(offset_x, -0.1f);
		boxCollider.materialKey = "Player";
		colliders.Add (boxCollider);

		/*
		ColliderInfo circleCollider = new ColliderInfo ();
		circleCollider.type = ColliderType.Circle;
		circleCollider.size = new Vector2(width /2, 0);
		circleCollider.offset = new Vector2(offset_x, - height / 3);
		circleCollider.materialKey = "Player";
		colliders.Add (circleCollider);
		*/

		this.startingPosition = new Vector2( 
			world.SCREEN_LEFT -2,
			world.FLOOR_Y + height
		);



		stableXPosition = world.SCREEN_MIDPOINT + world.SCREEN_WIDTH * 0.1f;

		this.isKinematic = false;
		this.velocity = new Vector2(0,0);

		recomposeXSpeed = 2;
		recomposeDistance = 6;

		affectedByWorldMovement = false;

		constrainMovement = false;
		constrainRotation = true;

		jumpForce = new Vector2(0,520);
		maxJumps = jumps = 3;
			
	}
}
