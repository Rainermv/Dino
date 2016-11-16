using UnityEngine;
using System.Collections;

public class ObstacleBox : Actor {

	public ObstacleBox() {
		
		this.tag = "PLATFORM";
		this.layer = 7;

		this.tint = Color.red;

		this.name = "Box";
		this.spriteKey = "Box";
		this.size = new Vector2( Random.Range(1,5) , Random.Range(1,3));

		this.startingPosition = new Vector2( world.X_SPAWN, Random.Range(world.FLOOR_Y, world.CELLING_Y));
		
		this.isKinematic = true;
		this.velocity = new Vector2(0,0);

		affectedByWorldMovement = true;
	}
}
