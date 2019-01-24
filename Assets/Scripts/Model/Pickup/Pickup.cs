using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pickup : Actor  {

    public PickupEffect effect;

    public Pickup(PickupEffectType effectType)
    {

        switch (effectType)
        {
            case PickupEffectType.STAR:
                this.effect = new StarPickupEffect();
                this.spriteKey = "pickups/Star";
                this.name = "Star";
                break;

            default:
                this.effect = new DefaultPickupEffect();
                this.spriteKey = "pickups/default";
                this.name = "Default";
                break;
        }

        this.tag = "PICKUP";
        this.layer = this.layer = Layers.CHARACTERS;

        this.depth = 7;

        //this.tint = Color.white;

        float width = 1.1f;
        float height = 1.1f;

        //float offset_x = -0.3f;

        setCollider(new Vector2(width, height), new Vector2(0f, -0.17f), "Enemy");

        /*
        this.startingPosition = new Vector2(
            world.SCREEN_LEFT - 2,
            world.FLOOR_Y + height
        );
        */
        this.isKinematic = true;
        //this.velocity = new Vector2(0, 0);

        constrainMovement = false;
        constrainRotation = true;

        

        
    }

    internal void TriggerPickup(Player player)
    {
        effect.OnPickup(player);
    }

    private void setCollider(Vector2 size, Vector2 colliderOffset, string materialKey)
    {

        ColliderInfo boxCollider = new ColliderInfo();
        boxCollider.type = ColliderType.Box;
        boxCollider.size = size;
        boxCollider.trigger = true;
        boxCollider.offset = colliderOffset;
        boxCollider.materialKey = materialKey;
        colliders.Add(boxCollider);

    }

}
