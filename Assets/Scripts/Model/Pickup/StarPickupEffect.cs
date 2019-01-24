using UnityEngine;
using System.Collections;

public class StarPickupEffect : PickupEffect
{
    public override void OnPickup(Player player)
    {
        World.GetInstance().STARS_PICKED += 1;
    }
}
