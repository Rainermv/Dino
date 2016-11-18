using UnityEngine;
using System.Collections;

public class Background : Actor {

	public Background(){

		spriteKey = "Background/BG";
		depth = 0;

		worldMovementMultiplier = 0.5f;

		hasRigidbody = false;
	}
}
