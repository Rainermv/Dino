﻿using UnityEngine;
using System.Collections;

public class Player : Character  {
	
	public int maxJumps;
	public int jumps;

	public float stableXPosition;
	public float recomposeXSpeed;
	public float recomposeDistance;


	public Player(){

		this.tag = "PLAYER";
		this.layer = 8;

		this.tint = Color.white;

		this.name = "Santa";
		this.spriteKey = null;

		this.animationKeys.Add(CharacterAnimationState.RUN, "santa_run");
		this.animationKeys.Add(CharacterAnimationState.JUMP, "santa_jump");
		this.initialState = CharacterAnimationState.RUN;


		this.size = new Vector2(1 , 1);

		this.startingPosition = new Vector2( 
			world.SCREEN_LEFT -2,
			world.FLOOR_Y + size.y / 2
		);

		stableXPosition = world.SCREEN_LEFT + world.SCREEN_WIDTH / 2;

		this.isKinematic = false;
		this.velocity = new Vector2(0,0);

		recomposeXSpeed = 2;
		recomposeDistance = 5;

		affectedByWorldMovement = false;

		constrainMovement = false;
		constrainRotation = true;

		jumpForce = new Vector2(0,500);
		maxJumps = jumps = 3;

		physicsMaterialKey = "Player";
	
	}
}
