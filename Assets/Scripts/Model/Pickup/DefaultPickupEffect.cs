using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

public class DefaultPickupEffect : PickupEffect
{
    public override void OnPickup(Player player)
    {
        Debug.Log("Picked up blank");
    }
}
