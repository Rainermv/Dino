using UnityEngine;
using System.Collections;

public class World {

	private static World instance;
	
	public Vector2 BASE_SPEED = new Vector2 (-5f, 0);
	public float SPAWN_TIMER = 2;

	public float MULTIPLIER = 1.1f;
	public float MULT_TIMER = 30f;
		
	public float X_SPAWN = 10f;
	
	public float SCREEN_BOTTOM = -4.5f;
	public float SCREEN_TOP = 4.5f;

	public float TRAVEL_DISTANCE;

	public float OBSTACLE_FREQUENCY = 500f;

	private World(){
		
	}
	public static World getInstance(){
		if (instance == null){
			instance = new World();
		}
		return instance;
	}

	public void update(){

		TRAVEL_DISTANCE += BASE_SPEED.magnitude;

	}
	
	
	
}
