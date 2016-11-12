using UnityEngine;
using System.Collections;

public class ObjectFactory  {

	private int id = 0;

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
	
	public ActorComponent buildActor(Vector2 position){
	
		spriteLibrary = SpriteLibrary.getInstance();

		// Get the object information
		Actor actorModel = new ObstacleBox();
		Sprite newSprite = spriteLibrary.getSprite(actorModel.spriteKey);

		// Create the game Object
		GameObject actorGameObject = new GameObject();
		actorGameObject.name = actorModel.name + " " + id++;
		actorGameObject.tag = actorModel.tag;

		actorGameObject.layer = actorModel.layer;
		
		actorGameObject.transform.position = position;
		actorGameObject.transform.localScale = actorModel.size;

		// Add the sprite renderer
		SpriteRenderer spriteRendererComponent = actorGameObject.AddComponent<SpriteRenderer>();
		spriteRendererComponent.sprite = newSprite;	
		
		// Add the collider
		BoxCollider2D collider = actorGameObject.AddComponent<BoxCollider2D>();
		collider.size = new Vector2 (1,1);		
		
		// Add the Rigidbody component
		Rigidbody2D rb = actorGameObject.AddComponent<Rigidbody2D>();
		rb.isKinematic = actorModel.isKinematic;

		// Add the Obstacle component
		ActorComponent actorComponent = actorGameObject.AddComponent<ActorComponent>();
		//actorComponent.setSpeed( actorModel.velocity );
		actorComponent.actor = actorModel;
		
		return actorComponent;
			
	}
	
	
}
