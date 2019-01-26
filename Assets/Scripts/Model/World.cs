using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class World {

	private static World instance;

    private List<Level> levels = new List<Level>();

    private Action OnlevelFinished;

    private Level currentLevel = null;

	public Vector2 GRAVITY = new Vector2 (0, -18);
	
	public Vector2 BASE_SPEED = new Vector2 (-3f, 0);
	public float BASE_SPEED_ANIM_MULTIPLIER = 0.3f;

	public float SPAWN_TIMER = 2;

    //public float DIFICULTY_MULTIPLIER = 1.1f;
    public float DIFFICULTY_INCREASE = 1;
	public float DIFICULTY_INCREASE_TIME = 15f;

	public float SCREEN_TOP = 5f;
	public float SCREEN_BOTTOM = -5f;
	
	public float FLOOR_Y = -4.5f;
	public float CELLING_Y = 2.5f;//4.5f;
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

    Action starsPickedValueChanged;

    private int _STARS_PICKED = 10;

    public float PLATFORM_FREQUENCY = 8;

	public int GROUND_CHUNK_MIN = 1;
	public int GROUND_CHUNK_MAX = 3;

	public int FLOOR_PLATFORM_SIZE = 5;

	public List<PlatformGenerationStrategy> GENERATION_STRATEGY_LIST = new List<PlatformGenerationStrategy>();

	public PlatformGenerationStrategy GENERATION_STRATEGY = PlatformGenerationStrategy.Ground;
	//public PlatformGenerationStrategy GENERATION_STRATEGY = PlatformGenerationStrategy.Air;

	public float STRATEGY_CHANGE_FREQUENCY = 25f;
	public float STRATEGY_CHANGE_CHANCE = 0.5f;

	public float ENEMY_SPAWN_CHANCE_PLATFORMS = 1f;
    public float ENEMY_SPAWN_CHANCE_GROUND = 1f;

    public float PROP_SPAWN_CHANCE_GROUND = 1f;

    public float PICKUP_SPAWN_CHANCE_PLATFORMS = 0.5f;
    public float PICKUP_SPAWN_CHANCE_GROUND = 0.2f;

    public int LEVEL_FINISH_DEFAULT_STRATEGY_DISTANCE = 20;

    public int STARS_PICKED {
        get {
            return _STARS_PICKED;
        }

        set {
            _STARS_PICKED = value;

            if (starsPickedValueChanged != null) {
                starsPickedValueChanged();
            }
        }
    }

    public float DistanceTargetRatio {
        get {
            return TRAVEL_DISTANCE / currentLevel.DistanceTarget;
        }
    }

    public bool IsDistanceTargetResetStrategy {
        get {
            return (currentLevel.DistanceTarget - (int)TRAVEL_DISTANCE) < LEVEL_FINISH_DEFAULT_STRATEGY_DISTANCE;
        }
    }

    public bool LevelCompleted {
        get {
            return TRAVEL_DISTANCE > currentLevel.DistanceTarget;
        }
    }

    public bool LevelFinished {
        get {
            return currentLevel.Active == false;
        }
    }

    

    private World(int loadLevel){
		
		SCREEN_WIDTH = SCREEN_RIGHT - SCREEN_LEFT;
		SCREEN_MIDPOINT = SCREEN_LEFT + SCREEN_WIDTH / 2;

		SCREEN_DEATH_X = SCREEN_LEFT - 1f;
		SCREEN_DEATH_Y = SCREEN_BOTTOM - 5f;

		GENERATION_STRATEGY_LIST.Add (PlatformGenerationStrategy.Ground);
		GENERATION_STRATEGY_LIST.Add (PlatformGenerationStrategy.Air);

        // Generating levels

        levels.Add(new Level(50,  levels.Count));
        levels.Add(new Level(150, levels.Count));
        levels.Add(new Level(300, levels.Count));

        if (loadLevel >= levels.Count ) {
            Debug.LogError("Level index out of range - " + loadLevel);
        } else {
            currentLevel = levels[loadLevel];
        }

        
    }

	public void RandomStrategy(){

        //TEST
        GENERATION_STRATEGY = GENERATION_STRATEGY_LIST[1];

        int id = UnityEngine.Random.Range (0, GENERATION_STRATEGY_LIST.Count);
		GENERATION_STRATEGY = GENERATION_STRATEGY_LIST [id];

	}

    public void ResetStrategy() {

        GENERATION_STRATEGY = GENERATION_STRATEGY_LIST[0];
    }

	public static World GetInstance(){
		if (instance == null){
			instance = new World(0);
		}
		return instance;
	}

    // Refactor this to make things faster
	public static World Restart(bool nextLevel){

        

        int levelNumber = instance.currentLevel.Number;

        if (nextLevel) {
            levelNumber += 1;
        }

        instance = null;
        instance = new World (levelNumber);

		return instance;

	}

	public void update(){

		TRAVEL_DISTANCE += BASE_SPEED.magnitude * Time.deltaTime;

	}

    public void registerStarsPickedValueChange(Action callback) {

        starsPickedValueChanged += callback;
    }

    public void StopMoving() {

        BASE_SPEED = Vector2.zero;

    }

    public void finishLevel() {

        currentLevel.Active = false;

        if (OnlevelFinished != null) {
            OnlevelFinished();
        }
       
    }

    public void registerOnLevelFinished(Action act) {
        OnlevelFinished += act;
    }
	
	
	
}
