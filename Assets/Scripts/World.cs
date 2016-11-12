using UnityEngine;
using System.Collections;

public class World {

	private static World instance;
	
	public Vector2 BASE_SPEED = new Vector2 (-5f, 0);
		
	public float X_SPAWN = 10f;
	
	public float SCREEN_BOTTOM = -5f;
	public float SCREEN_TOP = 5f;
	

	private World(){
		
	}
	
	public static World getInstance(){
		
		if (instance == null){
			instance = new World();
		}
		
		return instance;
		
	}
	
	
	
}
