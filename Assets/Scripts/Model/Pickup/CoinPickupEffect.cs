using UnityEngine;
using System.Collections;

public class CoinPickupEffect : PickupEffect
{
    public override void OnPickup(Player player)
    {
        Debug.Log("Picked up a coin");
    }
}
