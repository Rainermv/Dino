using UnityEngine;
using System.Collections;

public class Player : Character  {
	
	public int maxJumps;
	public int jumps;

	public Player(){

		affectedByWorldMovement = false;
		
		jumpForce = new Vector2(0,500);
		maxJumps = jumps = 3;
	
	}
}
