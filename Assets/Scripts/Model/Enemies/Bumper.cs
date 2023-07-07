using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

public class Bumper : Character {

    public string AIKey = "Zombie";

    const int BASES = 2;
    const int FACES = 3;

    public Bumper() {

        this.tag = "ENEMY";
        this.layer = this.layer = Layers.CHARACTERS;

        this.sortingOrder = 5;

        //this.tint = Color.white;

        this.name = "Bumper";
        //this.spriteKey = "Bumper/base" + Random.Range(0, BASES);

        this.spriteKey = "Bumper/Base0";
        AddCharacterState(CharacterAnimationType.Running, "default", Vector2.zero);
        AddCharacterState(CharacterAnimationType.Jumping, "default", Vector2.zero);
        AddCharacterState(CharacterAnimationType.Idling, "default", Vector2.zero);
        AddCharacterState(CharacterAnimationType.Dead, "default", Vector2.zero);

        SpriteComposition face = new SpriteComposition("Bumper/Face" + Random.Range(0, FACES), 6);
        face.addState(CharacterAnimationType.Falling, "Bumper/FaceFalling");
        face.addState(CharacterAnimationType.Dead, "Bumper/FaceColliding");

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

        SetCollider(new Vector2(width, height), new Vector2(offsetX, offsetY), "Enemy");

        this.startingPosition = new Vector2(
            world.ScreenModel.ScreenLeft - 2,
            world.ScreenModel.FloorY + height
        );

        this.isKinematic = false;
        this.velocity = new Vector2(0, 0);

        //affectedByWorldMovement = false;

        constrainMovement = false;
        constrainRotation = true;

        JumpForce = new Vector2(0, 0);

        CollisionPullbackVelocity = -20f;
    }
}
