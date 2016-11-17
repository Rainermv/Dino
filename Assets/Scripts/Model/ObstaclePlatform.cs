using UnityEngine;
using System.Collections;

public class ObstaclePlatform : Actor {

	public ObstaclePlatform() {
		
		this.tag = "PLATFORM";
		this.layer = Layers.FLOORS;

		this.tint = Color.red;

		this.name = "Box";
		this.spriteKey = "Box";

		Vector2 size = new Vector2( Random.Range(1,5) , Random.Range(1,3));
		this.scale = size;
		//this.colliderSize = sie

		this.startingPosition = new Vector2( world.X_SPAWN, Random.Range(world.FLOOR_Y, world.CELLING_Y));
		
		this.isKinematic = true;
		this.velocity = new Vector2(0,0);

		affectedByWorldMovement = true;
	}
}
