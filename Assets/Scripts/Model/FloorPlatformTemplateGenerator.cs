using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FloorPlatformTemplateGenerator  {

	private List<TileType[,]> templates = new List<TileType[,]>() ;

	static int SIZE = 3;

	private static FloorPlatformTemplateGenerator instance;

	private FloorPlatformTemplateGenerator(){
		Generate ();
	}
	public static FloorPlatformTemplateGenerator getInstance(){
		if (instance == null){
			instance = new FloorPlatformTemplateGenerator();
		}
		return instance;
	}

	private void Generate(){



	}
		

	private void GenerateLeftBorder(){

		TileType[,] t = new TileType[SIZE,1];

		t [0, 0] = TileType.SNOW_PLATFORM_LEFT;
		for (int i = 1; i < SIZE; i++) {
			t [i,0] = TileType.SNOW_PLATFORM_CENTER;
		}
		//t [SIZE-1, 0] = TileType.SNOW_PLATFORM_RIGHT;

		templates.Add (t);

	}
		

	public TileType[,] GetRandom(){

		//return templates [1];

		return templates[ Random.Range(0, templates.Count)];

	}





}
