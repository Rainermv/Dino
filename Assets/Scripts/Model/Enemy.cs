using UnityEngine;
using System.Collections;

public class Enemy : Character  {

	public float pullbackVelocity;

	public string AIKey = "Zombie";

	public Enemy ()
	{

		this.tag = "ENEMY";
		this.layer = this.layer = Layers.CHARACTERS;

		pullbackVelocity = -20f;

		this.depth = 5;

		//this.tint = Color.white;

		this.name = "ZombieMale";
		this.spriteKey = null;

		addCharacterState( CharacterAnimationType.RUN, "zombie_walk", Vector2.zero);
		addCharacterState( CharacterAnimationType.JUMP, "zombie_idle", Vector2.zero);
		addCharacterState( CharacterAnimationType.IDLE, "zombie_idle", Vector2.zero);
		addCharacterState( CharacterAnimationType.DEAD, "zombie_dead", new Vector2(0.515f, -0.165f));


		//this.animationKeys.Add(CharacterAnimationType.RUN, "zombie_walk");
		//this.animationKeys.Add(CharacterAnimationType.JUMP, "zombie_idle");
		//this.animationKeys.Add(CharacterAnimationType.IDLE, "zombie_idle");
		//this.animationKeys.Add(CharacterAnimationType.DEAD, "zombie_dead");
		this.initialStateType = CharacterAnimationType.IDLE;

		float width = 0.8f;
		float height = 1.3f;

		//float offset_x = -0.3f;

		setCollider (new Vector2 (width, height), new Vector2 (0f, -0.17f));

		this.startingPosition = new Vector2( 
			world.SCREEN_LEFT -2,
			world.FLOOR_Y + height
		);
			
		this.isKinematic = false;
		this.velocity = new Vector2(0,0);

		//affectedByWorldMovement = false;

		constrainMovement = false;
		constrainRotation = true;

		jumpForce = new Vector2(0,0);
	}
}


