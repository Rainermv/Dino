using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Player : Character  {
	
        public int MaxJumps;
        private int _availableJumps;

        public float StableXPosition;
        public float RecomposeXSpeed;
        public float RecomposeDistance;

        public float DynamicVelocityAdjust;

        public bool IsInvincible = true;

        public int AvailableJumps {
            get => _availableJumps;
            set => _availableJumps = Mathf.Clamp(value, 0, MaxJumps);
        }

        public Vector2 SlamForce { get; set; }

        public Player(){

            this.tag = "PLAYER";
            this.layer = this.layer = Layers.CHARACTERS;

            this.sortingOrder = 10;

            //this.tint = Color.white;

            //this.name = "Santa";
            //this.spriteKey = "Santa/Run (1)";

            //addCharacterState(CharacterAnimationType.RUN, "santa_run", Vector2.zero);
            //addCharacterState(CharacterAnimationType.JUMP, "santa_jump",Vector2.zero);

            this.name = "Dino";
            this.spriteKey = "Dino/Run (1)";

            AddCharacterState(CharacterAnimationType.Running, "dino_run", Vector2.zero);
            AddCharacterState(CharacterAnimationType.Jumping, "dino_jump", Vector2.zero);

            this.initialStateType = CharacterAnimationType.Running;

            float width = 0.8f;
            float height = 1.5f;

            float offset_x = -0.3f;

            SetCollider (new Vector2 (width, height), new Vector2 (offset_x, -0.1f), "Player");

            this.startingPosition = new Vector2( 
                world.ScreenModel.ScreenLeft -2,
                world.ScreenModel.FloorY + height
            );

            StableXPosition = world.ScreenModel.ScreenMidpoint - world.ScreenModel.ScreenWidth * 0.05f;

            this.isKinematic = false;
            this.velocity = new Vector2(0,0);

            //recomposeXSpeed = 2;
            RecomposeXSpeed = 5;
            RecomposeDistance = 6;

            DynamicVelocityAdjust = 1f;

            //affectedByWorldMovement = false;

            constrainMovement = false;
            constrainRotation = true;

            JumpForce = new Vector2(0,550);
            MaxJumps = AvailableJumps = 3;

            SlamForce = new Vector2(0,-1050);

        }
    }
}
