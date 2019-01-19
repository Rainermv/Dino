using UnityEngine;
using System.Collections;
using System;

public class PickupComponent : ActorComponent
{

    private Pickup pickup;


    // Use this for initialization
    protected override void Start()
    {
        pickup = actor as Pickup;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    internal void TriggerPickup(PlayerComponent playerComponent)
    {
        
        pickup.TriggerPickup(playerComponent.PlayerActor);
        Destroy(gameObject);
    }
}
