using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

public class Zombie : Character  {

	//public float pullbackVelocity;

	public string AIKey = "Zombie";

	public Zombie ()
	{

		this.tag = "ENEMY";
		this.layer = this.layer = Layers.CHARACTERS;

		CollisionPullbackVelocity = -20f;

		this.sortingOrder = 5;

		//this.tint = Color.white;

		this.name = "ZombieMale";
		this.spriteKey = null;

		AddCharacterState( CharacterAnimationType.Running, "zombie_walk", Vector2.zero);
		AddCharacterState( CharacterAnimationType.Jumping, "zombie_idle", Vector2.zero);
		AddCharacterState( CharacterAnimationType.Idling, "zombie_idle", Vector2.zero);
		AddCharacterState( CharacterAnimationType.Dead, "zombie_dead", new Vector2(0.515f, -0.165f));


		//this.animationKeys.Add(CharacterAnimationType.RUN, "zombie_walk");
		//this.animationKeys.Add(CharacterAnimationType.JUMP, "zombie_idle");
		//this.animationKeys.Add(CharacterAnimationType.IDLE, "zombie_idle");
		//this.animationKeys.Add(CharacterAnimationType.DEAD, "zombie_dead");
		this.initialStateType = CharacterAnimationType.Idling;

		float width = 0.8f;
		float height = 1.3f;

		//float offset_x = -0.3f;

		SetCollider (new Vector2 (width, height), new Vector2 (0f, -0.17f), "Enemy");

		this.startingPosition = new Vector2( 
			world.ScreenModel.ScreenLeft -2,
			world.ScreenModel.FloorY + height
		);
			
		this.isKinematic = false;
		this.velocity = new Vector2(0,0);

		//affectedByWorldMovement = false;

		constrainMovement = false;
		constrainRotation = true;

		JumpForce = new Vector2(0,0);
	}
}


