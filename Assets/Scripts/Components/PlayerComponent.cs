using UnityEngine;
using System.Collections;

public class PlayerComponent : CharacterComponent {

	private Player player;

	private ActorComponent floorTouched;
			
	protected override void Awake(){
		base.Awake();	

		rb = GetComponent<Rigidbody2D>();
				
	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start();

		//rb.drag = 100;

		player = actor as Player;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();

		if (state == CharacterAnimationState.RUN && player.isTouchingFloor == false) {
			SetState(CharacterAnimationState.JUMP);
		}
		else if (state == CharacterAnimationState.JUMP && player.isTouchingFloor == true) {
			SetState(CharacterAnimationState.RUN);
		}
			
							
	}

	// Update is called once per frame
	protected override void FixedUpdate () {
		base.FixedUpdate();

		if (transform.position.x < player.stableXPosition) {

			//float maxDistance = Mathf.Abs (player.startingPosition) - Mathf.Abs (player.stableXPosition);
			float distance =  Mathf.Abs(transform.position.x) - Mathf.Abs(player.stableXPosition);
			//print (distance);
			float vel_x = player.recomposeXSpeed * distance / player.recomposeDistance;
			//float vel_x = player.recomposeXSpeed;

			rb.velocity = new Vector2 (vel_x, rb.velocity.y);

		} else {
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}

	}

	
	public void PlayerJump(){
		
		if (player.jumps > 0){

			//rb.drag = 0;

			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(player.jumpForce);
			
			player.jumps--;
		}

		
	}
	
	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		if (collision.gameObject.tag == "FLOOR" || collision.gameObject.tag == "PLATFORM"  ){

			foreach (ContactPoint2D contact in collision.contacts) {
				
				// Get a jump if the player jump on the top of a floor
				if (contact.normal.y >= 0.75){
					player.jumps = player.maxJumps;
					break;
					//rb.drag = 1000;
				}
			}
		}

		if (collision.gameObject.tag == "FLOOR") {

			//rb.drag = player.floorDrag;
		}
	}

	protected override void OnCollisionExit2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		if (collision.gameObject.tag == "FLOOR"){

			//rb.drag = 0;
			
			foreach (ContactPoint2D contact in collision.contacts) {
				// Get a jump if the player jump on the top of a floor
				if (contact.normal.y >= 0.75){
					//rb.drag = 0;
				}
			}
		}
	}

	protected override void OnCollisionStay2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		if (collision.gameObject.tag == "FLOOR"){

			//rb.AddForce (new Vector2 (-player.floorDrag, 0), ForceMode2D.Impulse);

			foreach (ContactPoint2D contact in collision.contacts) {
			
				if (contact.normal.y >= 1){


					//transform.position.x += world.BASE_SPEED.x * Time.deltaTime;
				}
					
			}
		}

	}
	
}
