using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World {

	private static World instance;

	public Vector2 GRAVITY = new Vector2 (0, -15);
	
	public Vector2 BASE_SPEED = new Vector2 (-3f, 0);
	public float BASE_SPEED_ANIM_MULTIPLIER = 0.3f;

	public float SPAWN_TIMER = 2;

	public float DIFICULTY_MULTIPLIER = 1.1f;
	public float DIFICULTY_MULTIPLIER_TIME = 5f;

	public float SCREEN_TOP = 5f;
	public float SCREEN_BOTTOM = -5f;
	
	public float FLOOR_Y = -4.5f;
	public float CELLING_Y = 4.5f;
	//public float FLOOR_Y = 4.5f;

	public float SCREEN_LEFT = -9f;
	public float SCREEN_RIGHT = 9f;

	public float SCREEN_WIDTH;
	public float SCREEN_MIDPOINT;

	public float SCREEN_DEATH_X;
	public float SCREEN_DEATH_Y;

	public float X_SPAWN = 16f;
	public float X_REMOVE = -16f;

	public float TRAVEL_DISTANCE;

	public float PLATFORM_FREQUENCY = 8;

	public int GROUND_CHUNK_MIN = 1;
	public int GROUND_CHUNK_MAX = 3;

	public int FLOOR_PLATFORM_SIZE = 5;

	public List<PlatformGenerationStrategy> GENERATION_STRATEGY_LIST = new List<PlatformGenerationStrategy>();

	public PlatformGenerationStrategy GENERATION_STRATEGY = PlatformGenerationStrategy.Ground;
	public float STRATEGY_CHANGE_FREQUENCY = 25f;
	public float STRATEGY_CHANGE_CHANCE = 0.5f;

	public float ENEMY_SPAWN_CHANCE_PLATFORMS = 1f;

	private World(){
		
		SCREEN_WIDTH = SCREEN_RIGHT - SCREEN_LEFT;
		SCREEN_MIDPOINT = SCREEN_LEFT + SCREEN_WIDTH / 2;

		SCREEN_DEATH_X = SCREEN_LEFT - 2f;
		SCREEN_DEATH_Y = SCREEN_BOTTOM - 5f;

		GENERATION_STRATEGY_LIST.Add (PlatformGenerationStrategy.Ground);
		GENERATION_STRATEGY_LIST.Add (PlatformGenerationStrategy.Air);


	}

	public void randomStrategy(){

		int id = Random.Range (0, GENERATION_STRATEGY_LIST.Count);
		GENERATION_STRATEGY = GENERATION_STRATEGY_LIST [id];

	}

	public static World getInstance(){
		if (instance == null){
			instance = new World();
		}
		return instance;
	}

	public static World restart(){

		instance = new World ();
		return instance;

	}

	public void update(){

		TRAVEL_DISTANCE += BASE_SPEED.magnitude * Time.deltaTime;

	}
	
	
	
}
