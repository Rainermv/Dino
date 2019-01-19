using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class CharacterComponent : ActorComponent {

   

    //int ignoreMask = 8; // Character layer
    Character character;

	protected Animator animatorComponent;

    //public AnimationClip animationClip;

    protected PlayableGraph playableGraph;
    //protected AnimationPlayableOutput playableOutput;
    //protected AnimationClipPlayable clipPlayable;

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

        Animator[] animators = GetComponentsInChildren<Animator>();

        if (animators.Length > 0) {
            animatorComponent = GetComponentsInChildren<Animator>()[0];
        }
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		character = actor as Character;

        //playableGraph = PlayableGraph.Create();
        //playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        //playableOutput = AnimationPlayableOutput.Create(playableGraph, character.name, GetComponent<Animator>());

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

    public void SetState(CharacterAnimationType stateType){

        currentCharacterState = character.animationStates[stateType];

        if (animatorComponent != null) {

            AnimationClip animationClip = AnimationFactory.getInstance().getAnimationClip(character.name, currentCharacterState.animationKey);
            AnimationPlayableUtilities.PlayClip(animatorComponent, animationClip, out playableGraph);

            animatorComponent.gameObject.transform.localPosition = currentCharacterState.animationOffset;
            animatorComponent.updateMode = AnimatorUpdateMode.UnscaledTime;

        }

        if (character.childrenSprites.Count > 0) {

            foreach (SpriteComposition sprite in character.childrenSprites) {

                sprite.changeState(stateType);

            }


        }

        

        /*
        if (clipPlayable.IsValid()) {
			clipPlayable.Destroy ();
		}

       
        
		//string animKey = character.animationKeys [state];
		clipPlayable = AnimationFactory.getInstance ().loadAnimation (playableGraph, character.name, currentCharacterState.animationKey);

		anim.gameObject.transform.localPosition = currentCharacterState.animationOffset;

        //print (anim == null);

        //anim.Play (currentAnimation);
        playableGraph.Play();


        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        */
    }


	public void Flip(){

		character.direction = -character.direction;

		transform.localScale = new Vector3(
			transform.localScale.x * character.direction,
			transform.localScale.y,
			transform.localScale.z
		);

	}

	

	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		character.isTouchingFloor = Raycast("floorCast");
		character.isOnCliff = Raycast("cliffCast");
	}

	// Update is called once per frame
	protected override void FixedUpdate () {
		base.FixedUpdate();
	
		// update character's gravity scale
		if (!character.isTouchingFloor && rb.velocity.y < 0) {
			rb.gravityScale = character.gravityScaleFalling;
		} else {
			rb.gravityScale = character.gravityScaleBase;
		}

	}

    void OnDisable(){

        // Destroys all Playables and Outputs created by the graph.

        playableGraph.Destroy();
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
