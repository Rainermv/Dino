using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Director;

public class CharacterComponent : ActorComponent {

	//int ignoreMask = 8; // Character layer
	Character character;

	protected Animator anim;

	protected AnimationClipPlayable currentAnimation;
	protected CharacterAnimationState currentCharacterState;

	private Dictionary<string, CharacterRay> characterRays =  new Dictionary<string, CharacterRay> (); 
	//private Dictionary<string, RaycastHit2D> raycasts = new Dictionary<string, RaycastHit2D> (); 
	//private Dictionary<string, Vector2> raycastOrigins = new Dictionary<string, Vector2> (); 	//private RaycastHit2D floorCast;
	//private Dictionary<string, Vector2> raycastDirections = new Dictionary<string, Vector2> (); 
	//private RaycastHit2D cliffCast;

	//private Vector2 floorRayOrigin;
	//private Vector2 cliffRayOrigin;

	protected Vector2 dynamicVelocity = Vector2.zero;

	public CharacterAnimationState CurrentCharacterState{
		get { return currentCharacterState; } 
	}

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();

		anim = GetComponentsInChildren<Animator> ()[0];

	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		character = actor as Character;

		SetState (character.initialStateType);

		//Bounds bounds = getBounds ();

		BoxCollider2D collider = colls [0] as BoxCollider2D;
		Vector2 offset = collider.offset;

		characterRays.Add("floorCast", new CharacterRay (
			//new Vector2(offset.x, bounds.min.y - transform.position.y),  
			new Vector2(offset.x,-collider.size.y /2),  
			Vector2.down * 0.5f, 
			Color.yellow ));

		characterRays.Add("cliffCast", new CharacterRay (
			new Vector2(offset.x + collider.size.x /2, -collider.size.y /2),  
			new Vector2(0.5f, -0.5f), 
			Color.red ));

		//raycastOrigins.Add ("cliffCast", new Vector2 (bounds.min.x , bounds.min.y - transform.position.y));

	

	}

	public void Flip(){

		character.direction = -character.direction;

		transform.localScale = new Vector3(
			transform.localScale.x * character.direction,
			transform.localScale.y,
			transform.localScale.z
		);

	}

	public void SetState(CharacterAnimationType stateType){

		if (currentAnimation.IsValid()) {
			currentAnimation.Destroy ();
		}

		currentCharacterState = character.animationStates [stateType];
		//string animKey = character.animationKeys [state];
		currentAnimation = AnimationFactory.getInstance ().loadAnimation (character.name, currentCharacterState.animationKey);

		anim.gameObject.transform.localPosition = currentCharacterState.animationOffset;

		//print (anim == null);

		anim.Play (currentAnimation);

		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
	}


	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		character.isTouchingFloor = Raycast("floorCast");
		character.isOnCliff = Raycast("cliffCast");
	}

	public void ActionJump(){

		rb.AddForce( (actor as Character).jumpForce);

	}


	bool Raycast(string key){

		CharacterRay characterRay = characterRays [key];

		Vector2 trvc2 = transform.position;

		Vector2 rayOrigin = characterRay.origin * character.direction + trvc2 ; 
		Vector2 rayDirection = characterRay.direction * character.direction  + rayOrigin; 

		characterRay.raycast = Physics2D.Linecast (rayOrigin, rayDirection, 1 <<  Layers.FLOORS);

		Debug.DrawLine (rayOrigin, rayDirection, characterRay.debugColor);

		//if (floorCast.collider != null && (floorCast.collider.tag == "FLOOR" || floorCast.collider.tag == "PLATFORM")) {
		if (characterRay.raycast.collider != null) {
			return true;
		}

		return false;

	}


	/*
	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		if (collision.gameObject.tag == "FLOOR"){
			character.isTouchingFloor = this.isTouchingFloor;
		}
	}

	protected override void OnCollisionExit2D(Collision2D collision){
		base.OnCollisionExit2D(collision);

		if (collision.gameObject.tag == "FLOOR"){
			character.isTouchingFloor = this.isTouchingFloor;
		}
	}
	*/


}
