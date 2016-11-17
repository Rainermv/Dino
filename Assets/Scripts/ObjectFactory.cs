﻿using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

public class ObjectFactory  {

	private int id = 0;
	private _SpriteLibrary spriteLibrary;

	#region SINGLETON

	private static ObjectFactory instance;
	private ObjectFactory(){
		spriteLibrary = _SpriteLibrary.getInstance();
	}
	public static ObjectFactory getInstance(){	
		if (instance == null){
			instance = new ObjectFactory();

		}	
		return instance;
	}

	#endregion

	#region BUILDERS

	public PlayerComponent buildPlayer(){

		Player playerModel = new Player();

		GameObject actorGameObject = createGameObject(playerModel);


		Animator anim = addAnimator (actorGameObject, playerModel);
		SpriteRenderer spriteRendererComponent = addSpriteRenderer(actorGameObject, playerModel);
		BoxCollider2D collider = addBoxCollider2D(actorGameObject, playerModel);
		Rigidbody2D rb = addRigidbody2D(actorGameObject, playerModel);



		return addPlayerComponent(actorGameObject, playerModel);
	}
		
	public ActorComponent buildActor(){

		Actor actorModel = new Platform();

		GameObject actorGameObject = createGameObject(actorModel);

		SpriteRenderer spriteRendererComponent = addSpriteRenderer(actorGameObject, actorModel);
		BoxCollider2D collider = addBoxCollider2D(actorGameObject, actorModel);
		Rigidbody2D rb = addRigidbody2D(actorGameObject, actorModel);

		return addActorComponent(actorGameObject, actorModel);
			
	}

	public ActorComponent buildAerialPlatform(){

		Platform platformModel = Platform.AerialPlatform();

		GameObject actorGameObject = createGameObject(platformModel);

		foreach (PlatformTile tile in platformModel.tiles){
			
			GameObject tileGameObject = new GameObject();
			tileGameObject.name = "platform tile - " + tile.tileId;

			SpriteRenderer sr = tileGameObject.AddComponent<SpriteRenderer>();
			sr.sprite = Resources.Load<Sprite> ("Sprites/Tiles/" + tile.tileId);

			tileGameObject.transform.parent = actorGameObject.transform;
			tileGameObject.transform.localPosition = tile.position;

		}

		//SpriteRenderer spriteRendererComponent = addSpriteRenderer(actorGameObject, platformModel);
		BoxCollider2D collider = addBoxCollider2D(actorGameObject, platformModel);
		Rigidbody2D rb = addRigidbody2D(actorGameObject, platformModel);

		return addActorComponent(actorGameObject, platformModel);

	}

	#endregion

	private GameObject createGameObject(Actor actor){

		GameObject actorGameObject = new GameObject();
		actorGameObject.name = actor.name + " - ID: " + id++;
		actorGameObject.tag = actor.tag;

		actorGameObject.layer = actor.layer;

		actorGameObject.transform.position = actor.startingPosition;
		actorGameObject.transform.localScale = actor.scale;

		return actorGameObject;
	}



	private SpriteRenderer addSpriteRenderer(GameObject obj, Actor actor){


		SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
		sr.sprite = Resources.Load<Sprite> ("Sprites/" + actor.spriteKey);
		//sr.sprite = null;
		sr.color = actor.tint;

		return sr;

	}

	private BoxCollider2D addBoxCollider2D(GameObject obj, Actor actor){

		BoxCollider2D coll = obj.AddComponent<BoxCollider2D>();

		coll.size = actor.colliderSize;

		coll.offset = actor.colliderOffset;
		coll.sharedMaterial = Resources.Load<PhysicsMaterial2D> ("PhysicsMaterials/" + actor.physicsMaterialKey);

		return coll;

	}

	private Rigidbody2D addRigidbody2D(GameObject obj, Actor actor){

		Rigidbody2D rb = obj.AddComponent<Rigidbody2D> ();
		rb.isKinematic = actor.isKinematic;
		//rb.

		if (actor.constrainMovement){
			rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezePositionX;
		}
		if (actor.constrainRotation){
			rb.constraints = rb.constraints ^ RigidbodyConstraints2D.FreezeRotation;
		}
		


		return rb;
	}

	private Animator addAnimator(GameObject obj, Character character){

		Animator anim = obj.AddComponent<Animator> ();

		//AnimationClip clip = Resources.Load<AnimationClip> ("Animations/" + character.name + "/" + character.animationKeys[0] );

		//var clipPlayable = AnimationClipPlayable.Create (clip);

		//anim.Play (clipPlayable);

		return anim;

	}

	private ActorComponent addActorComponent(GameObject obj, Actor actor){

		ActorComponent act = obj.AddComponent<ActorComponent>();
		act.actor = actor;
		act.actor.id = id++;

		return act;

	}

	private PlayerComponent addPlayerComponent(GameObject obj, Player actorPlayer){

		PlayerComponent pl = obj.AddComponent<PlayerComponent>();
		pl.actor = actorPlayer;
		pl.actor.id = id++;

		return pl;

	}
	
	
}
