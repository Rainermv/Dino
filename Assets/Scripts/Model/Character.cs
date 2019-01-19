using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : Actor  {

	//public Dictionary<CharacterAnimationState, string> animationKeys = new Dictionary<CharacterAnimationState,string>();

	public Dictionary<CharacterAnimationType, CharacterAnimationState> animationStates = new Dictionary<CharacterAnimationType, CharacterAnimationState>();

	public CharacterAnimationType initialStateType;

	public Vector2 jumpForce;

	public bool isTouchingFloor = true;
	public bool isOnCliff = false;
	public bool isAlive = true;

	public float direction = 1f;

	public float gravityScaleBase = 1;
	public float gravityScaleFalling = 3f;

    public float collisionPullbackVelocity;

    protected void setCollider(Vector2 size, Vector2 colliderOffset, string materialKey)
    {

        ColliderInfo boxCollider = new ColliderInfo();
        boxCollider.type = ColliderType.Box;
        //boxCollider.size = new Vector2(width,height);
        boxCollider.size = size;
        //boxCollider.offset = new Vector2(offset_x, -0.1f);
        boxCollider.offset = colliderOffset;
        boxCollider.materialKey = materialKey;
        colliders.Add(boxCollider);

    }

    protected void addCharacterState(CharacterAnimationType animationType,  string animationKey, Vector2 animationOffset){

		this.animationStates.Add( animationType, new CharacterAnimationState (animationType, animationKey, animationOffset) );

	}

}
