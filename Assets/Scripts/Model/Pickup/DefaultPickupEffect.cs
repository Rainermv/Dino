using UnityEngine;
using System.Collections;

public class DefaultPickupEffect : PickupEffect
{
    public override void OnPickup(Player player)
    {
        Debug.Log("Picked up blank");
    }
}
