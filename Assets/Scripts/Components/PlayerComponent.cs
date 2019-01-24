using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class PlayerComponent : CharacterComponent {

	public Player PlayerActor { get; set; }

	private ActorComponent floorTouched;

    public delegate void JumpChange(int jumps);
    public JumpChange jumpChange;

    public delegate void PlayerDeath();
    public PlayerDeath playerDeath;

    Action playerDiedCallback;

    protected override void Awake(){
		base.Awake();	

		rb = GetComponent<Rigidbody2D>();
				
	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start();

		//rb.drag = 100;

		PlayerActor = actor as Player;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();

		if (currentCharacterState.animationType == CharacterAnimationType.RUN && PlayerActor.isTouchingFloor == false) {
			SetState(CharacterAnimationType.JUMP);
		}
		else if (currentCharacterState.animationType == CharacterAnimationType.JUMP && PlayerActor.isTouchingFloor == true) {
			SetState(CharacterAnimationType.RUN);
		}

        //anim.speed = rb.velocity.x + Mathf.Abs(world.BASE_SPEED.x * world.BASE_SPEED_ANIM_MULTIPLIER);

        //currentPlayableAnimation.speed = rb.velocity.x + Mathf.Abs(world.BASE_SPEED.x * world.BASE_SPEED_ANIM_MULTIPLIER);

        // The player is invincible before he enters the screen
        if (transform.position.x > world.SCREEN_LEFT) {
			PlayerActor.isInvincible = false;
		}


    }

	// Update is called once per frame
	protected override void FixedUpdate () {
		base.FixedUpdate();

        float distance = PlayerActor.stableXPosition - transform.position.x;
        float vel_x = PlayerActor.recomposeXSpeed * distance / PlayerActor.recomposeDistance;

        if (!world.LevelCompleted) {

            if (transform.position.x < PlayerActor.stableXPosition && PlayerActor.isTouchingFloor) {

                rb.velocity = new Vector2(vel_x, rb.velocity.y) + dynamicVelocity;

            } else {
                rb.velocity = new Vector2(0, rb.velocity.y) + dynamicVelocity;
            }

        } else{

           

            // RUN TO LEVEL FINISH
            PlayerActor.stableXPosition = world.SCREEN_RIGHT * 1.5f;
            PlayerActor.recomposeXSpeed *= 1.1f;

            rb.velocity = new Vector2(vel_x, rb.velocity.y) + dynamicVelocity;

            PlayerActor.isInvincible = true;

        }

        dynamicVelocity = Vector2.MoveTowards (dynamicVelocity, Vector2.zero, PlayerActor.dynamicVelocityAdjust);

        // FINISHES LEVEL
        if (world.LevelCompleted && transform.position.x > world.SCREEN_RIGHT * 1.2f) {

            world.finishLevel();

            GameObject.Destroy(gameObject);
        }

        if (!PlayerActor.isInvincible &&
            (transform.position.x < world.SCREEN_DEATH_X || transform.position.y < world.SCREEN_DEATH_Y)) {

            //world.BASE_SPEED = Vector2.zero;
            //rb.velocity = Vector2.zero;

            playerDeath();
            playerDiedCallback();

            world.StopMoving();

            GameObject.Destroy(gameObject);

        }


    }

	
	public void PlayerJump(){
		
		if (PlayerActor.jumps > 0){

			//rb.drag = 0;

			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(PlayerActor.jumpForce);
			
			PlayerActor.jumps--;

            jumpChange(PlayerActor.jumps);

        }


	}
	
	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		// HIT ENEMY
		if (collision.gameObject.tag == "ENEMY") {

            if (PlayerActor.isInvincible) {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                return;

            }

			Vector2 contactNormal = getContactNormal (collision);

			if (contactNormal.y >= 0.5) {

                ActionJump (1.5f);
				PlayerActor.jumps += 1;

                jumpChange(PlayerActor.jumps);

                collision.gameObject.SendMessage ("Kill");

			} 

			else if (contactNormal.x <= 0.5) {
				dynamicVelocity.x = collision.gameObject.GetComponent<EnemyComponent>().enemy.collisionPullbackVelocity;
			}

		}

		// HIT FLOOR
		if (collision.gameObject.tag == "FLOOR" || collision.gameObject.tag == "PLATFORM"  ){
					
			// Get a jump if the player jump on the top of a floor
			if (getContactNormal(collision).y >= 0.75){
				PlayerActor.jumps = PlayerActor.maxJumps;

                jumpChange(PlayerActor.jumps);
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

    protected override void OnTriggerEnter2D(Collider2D collider) {
        base.OnTriggerEnter2D(collider);

        if (collider.tag == "PICKUP")
        {
            PickupComponent pickup = collider.GetComponent<PickupComponent>();
            pickup.TriggerPickup(this);
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

    public void registerCharacterDiedCallback(Action action) {

        playerDiedCallback += action;

    }

}
