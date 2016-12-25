using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

public class CharacterComponent : ActorComponent {

	//int ignoreMask = 8; // Character layer
	RaycastHit2D[] raycasts;

	Character character;

	protected Animator anim;

	protected AnimationClipPlayable currentAnimation;
	protected CharacterAnimationState currentCharacterState;

	private RaycastHit2D floorCast;

	private Vector2 floorRayOrigin;

	protected Vector2 dynamicVelocity = Vector2.zero;

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

		Bounds bounds = getBounds ();
		Vector2 offset = colls [0].offset;

		floorRayOrigin = new Vector2 (offset.x, bounds.min.y - transform.position.y);

	}

	protected void SetState(CharacterAnimationType stateType){

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


		character.isTouchingFloor = raycastFloor();	
	}

	public void ActionJump(){

		rb.AddForce( (actor as Character).jumpForce);

	}

	bool raycastFloor() {

		Vector2 trvc2 = transform.position;
		Vector2 rayOrigin = floorRayOrigin + trvc2; 

		floorCast = Physics2D.Linecast (rayOrigin, rayOrigin + (Vector2.down * 0.5f), 1 <<  Layers.FLOORS);

		Debug.DrawLine (rayOrigin, rayOrigin + (Vector2.down * 0.5f), Color.yellow);

		//if (floorCast.collider != null && (floorCast.collider.tag == "FLOOR" || floorCast.collider.tag == "PLATFORM")) {
		if (floorCast.collider != null) {
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
