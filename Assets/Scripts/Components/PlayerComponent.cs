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

		if (currentCharacterState.animationType == CharacterAnimationType.RUN && player.isTouchingFloor == false) {
			SetState(CharacterAnimationType.JUMP);
		}
		else if (currentCharacterState.animationType == CharacterAnimationType.JUMP && player.isTouchingFloor == true) {
			SetState(CharacterAnimationType.RUN);
		}

		currentAnimation.speed = rb.velocity.x + Mathf.Abs(world.BASE_SPEED.x * world.BASE_SPEED_ANIM_MULTIPLIER);
			
							
	}

	// Update is called once per frame
	protected override void FixedUpdate () {
		base.FixedUpdate();

		if (transform.position.x < player.stableXPosition && player.isTouchingFloor) {

			float distance = player.stableXPosition - transform.position.x;
			float vel_x = player.recomposeXSpeed * distance / player.recomposeDistance;

			rb.velocity = new Vector2 (vel_x, rb.velocity.y) + dynamicVelocity;

		} else {
			rb.velocity = new Vector2 (0, rb.velocity.y) + dynamicVelocity;
		}
			
		if (transform.position.x < world.SCREEN_DEATH_X || transform.position.y < world.SCREEN_DEATH_Y) {

			world.BASE_SPEED = Vector2.zero;
			rb.velocity = Vector2.zero;

		}

		dynamicVelocity = Vector2.MoveTowards (dynamicVelocity, Vector2.zero, player.dynamicVelocityAdjust);

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

		// HIT ENEMY
		if (collision.gameObject.tag == "ENEMY") {

			Vector2 contactNormal = getContactNormal (collision);

			if (contactNormal.y >= 0.5) {

				ActionJump ();
				player.jumps += 1;

				collision.gameObject.SendMessage ("Kill");

			} 

			else if (contactNormal.x <= 0.5) {
				dynamicVelocity.x = collision.gameObject.GetComponent<EnemyComponent>().enemy.pullbackVelocity;
			}

		}

		// HIT FLOOR
		if (collision.gameObject.tag == "FLOOR" || collision.gameObject.tag == "PLATFORM"  ){
					
			// Get a jump if the player jump on the top of a floor
			if (getContactNormal(collision).y >= 0.75){
				player.jumps = player.maxJumps;
				//rb.drag = 1000;
			}
		
		}
			
	}

	Vector3 getContactNormal(Collision2D collision){

		return collision.contacts [0].normal;

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
