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

        //0 to 2
        //GenerateIceWall (1);
        //GenerateIceWall (2);
        //GenerateIceWall (3);
        GenerateSnowPlatform(1);
        GenerateSnowPlatform(2);
        GenerateSnowPlatform(3);
        //GenerateSnowPlatform(6);

        // 3 to 6
        GenerateSnowPlatform (3);
		GenerateSnowPlatform (4);
		GenerateSnowPlatform (5);
		GenerateSnowPlatform (6);

	}

	private void GenerateIceWall(int height){

		TileType[,] t = new TileType[1,height];

		for (int i = 0; i < height; i++) {
			t [0, i] = TileType.BLOCKER;
		}

		templates.Add (t);

	}

	private void GenerateSnowPlatform(int width){

		width = Mathf.Max (width, 3);

		TileType[,] t = new TileType[width,1];

		t [0, 0] = TileType.PLATFORM_LEFT;
		for (int i = 1; i < width -1; i++) {
			t [i,0] = TileType.PLATFORM_CENTER;
		}
		t [width-1, 0] = TileType.PLATFORM_RIGHT;

		templates.Add (t);


	}
		

	public TileType[,] GetRandom(){

		int index = 0;

		switch (World.GetInstance().GENERATION_STRATEGY) {

		case PlatformGenerationStrategy.Air:
			index = Random.Range (3, 7);
			break;
		

		case PlatformGenerationStrategy.Ground:
			index = Random.Range (0, 3);
			break;

		
		}

		return templates[index];

	}





}
