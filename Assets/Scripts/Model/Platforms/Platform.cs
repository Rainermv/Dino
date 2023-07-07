using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : Actor {

	public int width;
	public int height;

	public TileType[,] template;

	public List<PlatformTile> tiles = new List<PlatformTile>();
	public Platform() {

		this.layer = Layers.FLOORS;

		this.tag = "PLATFORM";
		this.spriteKey = null;

		this.isKinematic = true;
		this.velocity = new Vector2(0,0);

		//affectedByWorldMovement = true;
	}

	public void setRandomPosition(){
		this.startingPosition = new Vector2( world.XSpawn, Random.Range(world.ScreenModel.FloorY, world.ScreenModel.CellingY));
	}

	public void setToFloor(float xPosition){
		this.startingPosition = new Vector2(xPosition, world.ScreenModel.FloorY );
	}

	public static Platform FloorPlatform(FloorPlatformType type, float xPosition){

		Platform floorPlatform = new Platform ();
		floorPlatform.name = "GroundPlatform";
		floorPlatform.sortingOrder = 7;

		TileType[,] template = FloorPlatformTemplateGenerator.getInstance().Get(type);

		floorPlatform.setWithTemplate (template);
		floorPlatform.addEdgeCollider ();
		//floorPlatform.addBoxCollider ();

		floorPlatform.setToFloor (xPosition);

		return floorPlatform;

	}

	public static Platform AerialPlatform(PlatformGenerationStrategy platformGenerationStrategy){

		Platform aerialPlatform = new Platform ();
		aerialPlatform.name = "AirPlatform";
		aerialPlatform.sortingOrder = 3;

		TileType[,] template = AerialPlatformTemplateGenerator.getInstance().GetRandom (platformGenerationStrategy);

		aerialPlatform.setWithTemplate (template);
        aerialPlatform.addBoxCollider ();
        //aerialPlatform.addEdgeCollider();

        EffectorInfo effector = new EffectorInfo();
        effector.type = EffectorType.Platform;
        effector.oneWay = true;
        effector.surfaceArc = 75;

        aerialPlatform.effectors.Add(effector);

        aerialPlatform.setRandomPosition ();

		return aerialPlatform;
	}

	void setWithTemplate(TileType[,] template){

		this.template = template;

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
			
	}

	public void addEdgeCollider(){

		ColliderInfo colliderInfo = new ColliderInfo ();
		colliderInfo.type = ColliderType.Edge;
		colliderInfo.size = Vector2.zero;
		colliderInfo.offset = Vector2.zero;

		for (int x = 0; x < width; x++) {
			colliderInfo.size.x += template [x, 0].size.x;
		}

		for (int y = 0; y < height; y++) {
			colliderInfo.size.y += template [0, y].size.y;
		}


		for (int x = 0; x < width; x++) {
			colliderInfo.offset.x += template [x, 0].offset.x * (template [x, 0].size.x / colliderInfo.size.x) ;
		}


		for (int y = 0; y < height -1; y++) {
			colliderInfo.offset.y += template [0, y].size.y;
		}
		colliderInfo.offset.y += template [0, 0].size.y * 0.5f;

		colliders.Add (colliderInfo);


	}

	public void addBoxCollider(){

		ColliderInfo colliderInfo = new ColliderInfo ();
		colliderInfo.type = ColliderType.Box;
		colliderInfo.size = Vector2.zero;
		colliderInfo.offset = Vector2.zero;

		for (int x = 0; x < width; x++) {
			colliderInfo.size.x += template [x, 0].size.x;
		}
		for (int y = 0; y < height; y++) {
			colliderInfo.size.y += template [0, y].size.y;
		}

		for (int x = 0; x < width; x++) {
			colliderInfo.offset.x += template [x, 0].offset.x * (template [x, 0].size.x / colliderInfo.size.x) ;
		}
		for (int y = 0; y < height; y++) {
			colliderInfo.offset.y += template [0, y].offset.y * (template [0, y].size.y / colliderInfo.size.y) ;
		}
			
		colliders.Add (colliderInfo);

	}
		
}
