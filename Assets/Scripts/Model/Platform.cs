using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : Actor {

	public int width;
	public int height;

	public List<PlatformTile> tiles = new List<PlatformTile>();
	public Platform() {

		this.layer = Layers.FLOORS;
		this.name = "Platform";
		this.tag = "PLATFORM";
		this.spriteKey = null;

		this.isKinematic = true;
		this.velocity = new Vector2(0,0);

		affectedByWorldMovement = true;
	}

	public void setRandomPosition(){
		this.startingPosition = new Vector2( world.X_SPAWN, Random.Range(world.FLOOR_Y, world.CELLING_Y));
	}


	public static Platform AerialPlatform(){

		Platform aerialPlatform = new Platform ();

		TileType[,] template = AerialPlatformTemplateGenerator.getInstance().GetRandom ();

		aerialPlatform.setWithTemplate (template);

		aerialPlatform.setRandomPosition ();

		return aerialPlatform;
	}

	void setWithTemplate(TileType[,] template){

		this.width = template.GetLength (0);
		this.height = template.GetLength (1);

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				PlatformTile tile = new PlatformTile (template[x,y]);

				float w = width;
				float h = height;

				tile.position = new Vector2 (

					(-w/ 2)  + x * scale.x + tile.size.x /2,
					(-h/ 2)  + y * scale.y + tile.size.y /2

				);

				this.tiles.Add(tile);
			}
		}

		this.colliderSize = Vector2.zero;

		for (int x = 0; x < width; x++) {
			this.colliderSize.x += template [x, 0].size.x;
		}
		for (int y = 0; y < height; y++) {
			this.colliderSize.y += template [0, y].size.y;
		}

	}
		
}
