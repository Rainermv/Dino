using UnityEngine;
using System.Collections;

public class EnemyComponent : CharacterComponent {

	public Character enemy;

	private AI enemyAI;

	public AI EnemyAI {
		set { enemyAI = value; }
	}

	/*
	public void setAI(AI enemyAI){
		this.enemyAI = enemyAI;
	}
	*/

	protected override void Start () {
		base.Start ();

		enemy = actor as Character;

		SetState (CharacterAnimationType.RUN);

		enemyAI.StartAI ( this );
	}

	protected override void Update () {
		base.Update ();

		enemyAI.UpdateAI (this);



	}

	public void setAlive(bool value){
		enemy.isAlive = value;
	}

	protected override void FixedUpdate () {
		base.FixedUpdate();


		if (currentCharacterState.animationType == CharacterAnimationType.IDLE ||
			currentCharacterState.animationType == CharacterAnimationType.DEAD) {
			
			dynamicVelocity = new Vector2 (0f, 0f);

		} else if (currentCharacterState.animationType == CharacterAnimationType.RUN) {
			
			dynamicVelocity = new Vector2 (-1f, 0f);

		}

		rb.velocity = new Vector2 (world.BASE_SPEED.x, rb.velocity.y) + dynamicVelocity;
	}

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        // HIT ENEMY
        if (collision.gameObject.tag == "PLAYER") {

            Vector2 contactNormal = getContactNormal(collision);

            if (contactNormal.y < 0.5) {
                StartCoroutine(BumpRoutine());
                this.setAlive(false);
            }

        }

    }

	protected override void OnCollisionExit2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);
	}

	protected override void OnCollisionStay2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);
	}

    IEnumerator BumpRoutine() {

        float t = 0;
        //Vector2 targetPosition = Vector2.down * 3f;
        //Vector2 initalPosition = transform.position;

        Vector3 scale = transform.localScale;

        transform.localScale = new Vector3(scale.x, scale.y * 0.7f, scale.z);
        yield return new WaitForSeconds(0.1f);


        transform.localScale = scale;


    }
}
