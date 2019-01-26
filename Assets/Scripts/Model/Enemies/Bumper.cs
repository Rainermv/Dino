using UnityEngine;
using System.Collections;

public class Bumper : Character {

    public string AIKey = "Zombie";

    const int BASES = 2;
    const int FACES = 3;

    public Bumper() {

        this.tag = "ENEMY";
        this.layer = this.layer = Layers.CHARACTERS;

        this.depth = 5;

        //this.tint = Color.white;

        this.name = "Bumper";
        //this.spriteKey = "Bumper/base" + Random.Range(0, BASES);

        this.spriteKey = "Bumper/Base0";
        addCharacterState(CharacterAnimationType.RUN, "default", Vector2.zero);
        addCharacterState(CharacterAnimationType.JUMP, "default", Vector2.zero);
        addCharacterState(CharacterAnimationType.IDLE, "default", Vector2.zero);
        addCharacterState(CharacterAnimationType.DEAD, "default", Vector2.zero);

        SpriteComposition face = new SpriteComposition("Bumper/Face" + Random.Range(0, FACES), 6);
        face.addState(CharacterAnimationType.FALLING, "Bumper/FaceFalling");
        face.addState(CharacterAnimationType.DEAD, "Bumper/FaceColliding");

        face.constrainRotation = true;
        face.constrainMovement = true;
        face.hasRigidbody = false;
        face.indestructable = false;

        face.startingPosition = new Vector2(0, 0.1f);

        childrenSprites.Add(face);



        float width = 0.9f;
        float height = 0.9f;

        float offsetX = 0f;
        float offsetY = 0f;
        //float offset_x = -0.3f;

        setCollider(new Vector2(width, height), new Vector2(offsetX, offsetY), "Enemy");

        this.startingPosition = new Vector2(
            world.SCREEN_LEFT - 2,
            world.FLOOR_Y + height
        );

        this.isKinematic = false;
        this.velocity = new Vector2(0, 0);

        //affectedByWorldMovement = false;

        constrainMovement = false;
        constrainRotation = true;

        jumpForce = new Vector2(0, 0);

        collisionPullbackVelocity = -20f;
    }
}
