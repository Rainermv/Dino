using UnityEngine;
using System.Collections;

public class World {

	private static World instance;

	public Vector2 GRAVITY = new Vector2 (0, -20);
	
	public Vector2 BASE_SPEED = new Vector2 (-5f, 0);
	public float SPAWN_TIMER = 2;

	public float MULTIPLIER = 1.1f;
	public float MULT_TIMER = 30f;
		
	public float X_SPAWN = 10f;

	public float SCREEN_TOP = -5f;
	public float SCREEN_BOTTOM = 5f;
	
	public float FLOOR_Y = -4.5f;
	public float CELLING_Y = 4.5f;
	//public float FLOOR_Y = 4.5f;

	public float SCREEN_LEFT = -9f;
	public float SCREEN_RIGHT = 9f;
	public float SCREEN_WIDTH;

	public float TRAVEL_DISTANCE;

	public float OBSTACLE_FREQUENCY = 10;

	private World(){
		SCREEN_WIDTH = Mathf.Abs (SCREEN_LEFT) + Mathf.Abs (SCREEN_RIGHT);
	}
	public static World getInstance(){
		if (instance == null){
			instance = new World();
		}
		return instance;
	}

	public void update(){

		TRAVEL_DISTANCE += BASE_SPEED.magnitude * Time.deltaTime;

	}
	
	
	
}
