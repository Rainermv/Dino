using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

public class CharacterComponent : ActorComponent {

	//int ignoreMask = 8; // Character layer
	RaycastHit2D[] raycasts;

	Character character;

	protected Animator anim;

	protected AnimationClipPlayable currentAnimation;
	protected CharacterAnimationState state;

	private RaycastHit2D floorCast;

	private Vector2 floorRayOrigin;


	// Use this for initialization
	protected override void Awake () {
		base.Awake ();

		anim = GetComponent<Animator> ();

	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		character = actor as Character;

		SetState (character.initialState);

		floorRayOrigin = new Vector2 (coll.offset.x, coll.bounds.min.y - transform.position.y);

	}

	protected void SetState(CharacterAnimationState newState){
		state = newState;
		string animKey = character.animationKeys [state];
		currentAnimation = AnimationFactory.getInstance ().loadAnimation (character.name, animKey);

		anim.Play (currentAnimation);
	}


	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		currentAnimation.speed = rb.velocity.x + Mathf.Abs(world.BASE_SPEED.x * world.BASE_SPEED_ANIM_MULTIPLIER);
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
