using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public abstract class Character : Actor  {

        //public Dictionary<CharacterAnimationState, string> animationKeys = new Dictionary<CharacterAnimationState,string>();

        public Dictionary<CharacterAnimationType, CharacterAnimationState> animationStates = new Dictionary<CharacterAnimationType, CharacterAnimationState>();

        public CharacterAnimationType initialStateType;

        public Vector2 JumpForce;

        public bool IsTouchingFloor = true;
        public bool IsOnCliff = false;
        public bool IsAlive = true;

        public float Direction = 1f;

        public float GravityScaleBase = 1;
        public float GravityScaleFalling = 3f;

        public float CollisionPullbackVelocity;

        protected void SetCollider(Vector2 size, Vector2 colliderOffset, string materialKey)
        {

            var boxCollider = new ColliderInfo();
            boxCollider.type = ColliderType.Box;
            //boxCollider.size = new Vector2(width,height);
            boxCollider.size = size;
            //boxCollider.offset = new Vector2(offset_x, -0.1f);
            boxCollider.offset = colliderOffset;
            boxCollider.materialKey = materialKey;
            colliders.Add(boxCollider);

        }

        protected void AddCharacterState(CharacterAnimationType animationType,  string animationKey, Vector2 animationOffset){

            this.animationStates.Add( animationType, new CharacterAnimationState (animationType, animationKey, animationOffset) );

        }

    }
}
