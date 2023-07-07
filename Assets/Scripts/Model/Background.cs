using UnityEngine;
using System.Collections;

public class Background : Actor {

	public Background(){

		spriteKey = "Background/BG";
		sortingOrder = 0;

        name = "Background";
        indestructable = true;

		//worldMovementMultiplier = 0.5f;
		//affectedByWorldMovement = false;

		hasRigidbody = false;
	}
}
