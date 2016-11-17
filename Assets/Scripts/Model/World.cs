﻿using UnityEngine;
using System.Collections;

public class World {

	private static World instance;

	public Vector2 GRAVITY = new Vector2 (0, -20);
	
	public Vector2 BASE_SPEED = new Vector2 (-5f, 0);
	public float BASE_SPEED_ANIM_MULTIPLIER = 0.3f;

	public float SPAWN_TIMER = 2;

	public float DIFICULTY_MULTIPLIER = 1.1f;
	public float DIFICULTY_MULTIPLIER_TIME = 5f;

	public float SCREEN_TOP = -5f;
	public float SCREEN_BOTTOM = 5f;
	
	public float FLOOR_Y = -4.5f;
	public float CELLING_Y = 4.5f;
	//public float FLOOR_Y = 4.5f;

	public float SCREEN_LEFT = -9f;
	public float SCREEN_RIGHT = 9f;
	public float SCREEN_WIDTH;

	public float X_SPAWN = 16f;
	public float X_REMOVE = -16f;

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
