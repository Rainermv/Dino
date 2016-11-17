using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class AerialPlatformTemplateGenerator  {

	private List<TileType[,]> templates = new List<TileType[,]>() ;

	private static AerialPlatformTemplateGenerator instance;

	private AerialPlatformTemplateGenerator(){
		Generate ();
	}
	public static AerialPlatformTemplateGenerator getInstance(){
		if (instance == null){
			instance = new AerialPlatformTemplateGenerator();
		}
		return instance;
	}

	private void Generate(){

		GenerateIceWall (1);
		GenerateIceWall (2);
		GenerateIceWall (3);

		GenerateSnowPlatform (3);
		GenerateSnowPlatform (4);
		GenerateSnowPlatform (5);
		GenerateSnowPlatform (6);

	}

	private void GenerateIceWall(int height){

		TileType[,] t = new TileType[1,height];

		for (int i = 0; i < height; i++) {
			t [0, i] = TileType.ICE;
		}

		templates.Add (t);

	}

	private void GenerateSnowPlatform(int width){

		width = Mathf.Max (width, 3);

		TileType[,] t = new TileType[width,1];

		t [0, 0] = TileType.SNOW_PLATFORM_LEFT;
		for (int i = 1; i < width -1; i++) {
			t [i,0] = TileType.SNOW_PLATFORM_CENTER;
		}
		t [width-1, 0] = TileType.SNOW_PLATFORM_RIGHT;

		templates.Add (t);


	}
		

	public TileType[,] GetRandom(){

		//return templates [1];

		return templates[ Random.Range(0, templates.Count)];

	}





}
