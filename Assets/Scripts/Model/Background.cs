using UnityEngine;
using System.Collections;

public class Background : Actor {

	public Background(){

		spriteKey = "Background/BG";
		depth = 0;

        name = "Background";
        indestructable = true;

		//worldMovementMultiplier = 0.5f;
		//affectedByWorldMovement = false;

		hasRigidbody = false;
	}
}
