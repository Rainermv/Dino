using UnityEngine;
using System.Collections;

public class ObstacleBox : Obstacle {

	public ObstacleBox(){	
		this.name = "Box";
		this.spriteKey = "Box";
		this.size = new Vector2( Random.Range(1,5) , Random.Range(1,5));
		
		this.isKinematic = true;
		this.velocity = new Vector2(0,0);
	}
}
