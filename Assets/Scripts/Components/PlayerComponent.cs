using UnityEngine;
using System;
using System.Collections;
using Assets.Scripts.Model;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class PlayerComponent : CharacterComponent {

	public Player PlayerActor { get; set; }

	private ActorComponent floorTouched;

    private Action<int, int> _onUpdateJumpsAction;

    //public delegate void PlayerDeath();
   //public PlayerDeath playerDeath;

    Action _onPlayerDeathAction;

    protected override void Awake(){
		base.Awake();	

		RigidBody = GetComponent<Rigidbody2D>();
				
	}
    
    // Use this for initialization
    protected override void Start () {
		base.Start();

		PlayerActor = Actor as Player;
	}

    public void Initialize(Action<int, int> onUpdateJumpsAction, Action onPlayerDeath)
    {
        _onPlayerDeathAction += onPlayerDeath;
        _onUpdateJumpsAction += onUpdateJumpsAction;
    }


    // Update is called once per frame
    protected override void Update () {
		base.Update();

		switch (currentCharacterState.animationType)
        {
            case CharacterAnimationType.Running:
                if (!PlayerActor.IsTouchingFloor)
                {
                    SetState(CharacterAnimationType.Jumping);
                }
                break;
            case CharacterAnimationType.Jumping:
                if (PlayerActor.IsTouchingFloor)
                {
                    SetState(CharacterAnimationType.Running);
                }
                break;
            case CharacterAnimationType.Idling:
                break;
            case CharacterAnimationType.Dead:
                break;
            case CharacterAnimationType.Falling:
                break;
            default:
                Debug.Log(currentCharacterState.animationType);
                throw new ArgumentOutOfRangeException();
        }

        //anim.speed = rb.velocity.x + Mathf.Abs(world.BASE_SPEED.x * world.BASE_SPEED_ANIM_MULTIPLIER);

        //currentPlayableAnimation.speed = rb.velocity.x + Mathf.Abs(world.BASE_SPEED.x * world.BASE_SPEED_ANIM_MULTIPLIER);

        // The player is invincible before he enters the screen
        if (transform.position.x > World.ScreenModel.ScreenLeft) {
			PlayerActor.IsInvincible = false;
		}


    }

    public void RunToLevelFinish(float horizontalVelocity)
    {
        PlayerActor.StableXPosition = World.ScreenModel.ScreenRight * 1.5f;
        PlayerActor.RecomposeXSpeed *= 1.1f;

        RigidBody.velocity = new Vector2(horizontalVelocity, RigidBody.velocity.y) + dynamicVelocity;

        PlayerActor.IsInvincible = true;

    }

	// Update is called once per frame
	protected override void FixedUpdate () {
		base.FixedUpdate();

        var distance = PlayerActor.StableXPosition - transform.position.x;
        var horizontalVelocity = PlayerActor.RecomposeXSpeed * distance / PlayerActor.RecomposeDistance;

        if (!World.LevelCompleted) {

            if (transform.position.x < PlayerActor.StableXPosition && PlayerActor.IsTouchingFloor) {

                RigidBody.velocity = new Vector2(horizontalVelocity, RigidBody.velocity.y) + dynamicVelocity;

            } else {
                RigidBody.velocity = new Vector2(0, RigidBody.velocity.y) + dynamicVelocity;
            }

        } else{
            /*
            // RUN TO LEVEL FINISH
            
            */

        }

        dynamicVelocity = Vector2.MoveTowards (dynamicVelocity, Vector2.zero, PlayerActor.DynamicVelocityAdjust);

        /*
        // FINISHES LEVEL
        if (World.LevelCompleted && transform.position.x > World.ScreenModel.ScreenRight * 1.2f) {

            World.FinishLevel();

            GameObject.Destroy(gameObject);
        }
        */

        if (PlayerActor.IsInvincible ||
            (!(transform.position.x < World.ScreenModel.ScreenDeathX) && !(transform.position.y < World.ScreenModel.ScreenDeathY)))
            return;

        //world.BASE_SPEED = Vector2.zero;
        //rb.velocity = Vector2.zero;

        _onPlayerDeathAction();

        World.StopMoving();

        GameObject.Destroy(gameObject);


    }

	
	public void PlayerJump()
    {
        if (PlayerActor.AvailableJumps <= 0)
            return;

        RigidBody.velocity = new Vector2(RigidBody.velocity.x, 0);
        RigidBody.AddForce(PlayerActor.JumpForce);
			
        PlayerActor.AvailableJumps--;

        _onUpdateJumpsAction(PlayerActor.AvailableJumps, PlayerActor.MaxJumps);


    }

    public void PlayerSlam()
    {
        if (PlayerActor.IsTouchingFloor)
            return;

        RigidBody.velocity = new Vector2(RigidBody.velocity.x, 0);
        RigidBody.AddForce(PlayerActor.SlamForce);
    }

    protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		// HIT ENEMY
		if (collision.gameObject.tag == "ENEMY") {

            if (PlayerActor.IsInvincible) {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                return;

            }

			Vector2 contactNormal = getContactNormal (collision);

			if (contactNormal.y >= 0.5) {

                ActionJump (1.5f);
				PlayerActor.AvailableJumps += 1;

                _onUpdateJumpsAction(PlayerActor.AvailableJumps, PlayerActor.MaxJumps);

                //collision.gameObject.SendMessage ("Kill");

			} 

			else if (contactNormal.x <= 0.5) {
				dynamicVelocity.x = collision.gameObject.GetComponent<EnemyComponent>().enemy.CollisionPullbackVelocity;
			}

		}

		// HIT FLOOR
		if (collision.gameObject.tag == "FLOOR" || collision.gameObject.tag == "PLATFORM"  ){
					
			// if the player hits the top of a floor
			if (getContactNormal(collision).y >= 0.75){

                // Recharge Jumps
				PlayerActor.AvailableJumps = PlayerActor.MaxJumps;
                _onUpdateJumpsAction(PlayerActor.AvailableJumps, PlayerActor.MaxJumps);

                // Recharge Slams
            }
		
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

        _onPlayerDeathAction += action;

    }


    
}
