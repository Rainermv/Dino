using UnityEngine;
using System.Collections;

public class Player : Character  {
	
	public int maxJumps;
	public int jumps;

	public Player(){

		this.tag = "PLAYER";
		this.layer = 8;

		this.tint = Color.blue;

		this.name = "Player";
		this.spriteKey = "player";
		this.size = new Vector2(1 , 2);

		this.startingPosition = new Vector2( 
			world.SCREEN_LEFT + world.SCREEN_WIDTH /5 + 5,
			world.FLOOR_Y
		);

		this.isKinematic = false;
		this.velocity = new Vector2(0,0);

		affectedByWorldMovement = false;

		constrainMovement = false;
		constrainRotation = true;
		
		jumpForce = new Vector2(0,500);
		maxJumps = jumps = 3;
	
	}
}
