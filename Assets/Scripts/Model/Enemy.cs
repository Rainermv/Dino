using UnityEngine;
using System.Collections;

public class Enemy : Character  {

	public float pullbackVelocity;

	public Enemy ()
	{

		this.tag = "ENEMY";
		this.layer = this.layer = Layers.CHARACTERS;

		pullbackVelocity = -20f;

		this.depth = 5;

		//this.tint = Color.white;

		this.name = "ZombieMale";
		this.spriteKey = null;

		this.animationKeys.Add(CharacterAnimationState.RUN, "zombie_walk");
		this.animationKeys.Add(CharacterAnimationState.JUMP, "zombie_idle");
		this.animationKeys.Add(CharacterAnimationState.IDLE, "zombie_idle");
		this.animationKeys.Add(CharacterAnimationState.DEAD, "zombie_dead");
		this.initialState = CharacterAnimationState.IDLE;

		float width = 0.8f;
		float height = 1.3f;

		//float offset_x = -0.3f;

		setCollider (new Vector2 (width, height), new Vector2 (0f, -0.17f));

		this.startingPosition = new Vector2( 
			world.SCREEN_LEFT -2,
			world.FLOOR_Y + height
		);
			
		this.isKinematic = true;
		this.velocity = new Vector2(0,0);

		affectedByWorldMovement = true;

		constrainMovement = false;
		constrainRotation = true;

		jumpForce = new Vector2(0,0);
	}
}


