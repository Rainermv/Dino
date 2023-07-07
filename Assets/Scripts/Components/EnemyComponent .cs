using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

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

		enemy = Actor as Character;

		SetState (CharacterAnimationType.Running);

		enemyAI.StartAI ( this );
	}

	protected override void Update () {
		base.Update ();

		enemyAI.UpdateAI (this);



	}

	public void setAlive(bool value){
		enemy.IsAlive = value;
	}

	protected override void FixedUpdate () {
		base.FixedUpdate();


		if (currentCharacterState.animationType == CharacterAnimationType.Idling ||
			currentCharacterState.animationType == CharacterAnimationType.Dead) {
			
			dynamicVelocity = new Vector2 (0f, 0f);

		} else if (currentCharacterState.animationType == CharacterAnimationType.Running) {
			
			dynamicVelocity = new Vector2 (-1f, 0f);

		}

		RigidBody.velocity = new Vector2 (World.BaseSpeed.x, RigidBody.velocity.y) + dynamicVelocity;
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
