using UnityEngine;
using System.Collections;

public class ObjectFactory  {

	private SpriteLibrary spriteLibrary;

	private static ObjectFactory instance;
	private ObjectFactory(){
	}
	public static ObjectFactory getInstance(){	
		if (instance == null){
			instance = new ObjectFactory();
		}	
		return instance;
	}
	
	public GameObject buildBox(Vector2 position){
	
		spriteLibrary = SpriteLibrary.getInstance();

		// Get the object information
		Obstacle obstacleModel = new ObstacleBox();
		Sprite newSprite = spriteLibrary.getSprite(obstacleModel.spriteKey);
		
		// Create the game Object
		GameObject obstacleGameObject = new GameObject();
		obstacleGameObject.name = obstacleModel.name;
		
		obstacleGameObject.transform.position = position;
		obstacleGameObject.transform.localScale = obstacleModel.size;
		
		// Add the sprite renderer
		SpriteRenderer spriteRendererComponent = obstacleGameObject.AddComponent<SpriteRenderer>();
		spriteRendererComponent.sprite = newSprite;	
		
		// Add the collider
		BoxCollider2D collider = obstacleGameObject.AddComponent<BoxCollider2D>();
		collider.size = new Vector2 (1,1);		
		
		// Add the Rigidbody component
		Rigidbody2D rb = obstacleGameObject.AddComponent<Rigidbody2D>();
		rb.isKinematic = obstacleModel.isKinematic;
		
		// Add the Actor component
		ActorObstacle actor = obstacleGameObject.AddComponent<ActorObstacle>();
		actor.setSpeed( obstacleModel.velocity );
		
		return obstacleGameObject;
			
	}
	
	
}
