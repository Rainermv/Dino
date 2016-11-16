using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

public class CharacterComponent : ActorComponent {

	LayerMask mask = 9; // Character layer
	RaycastHit2D[] raycasts;

	Character character;

	protected Animator anim;

	protected AnimationClipPlayable currentAnimation;
	protected CharacterAnimationState state;

	private RaycastHit2D floorCast;


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

		floorCast = Physics2D.Linecast (transform.position, getBounds ().min * 1.1f, mask.value);

		if (floorCast.collider != null && floorCast.collider.tag == "FLOOR") {
			//print ("colliding");
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
