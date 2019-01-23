using UnityEngine;
using System.Collections;

public class StarPickupEffect : PickupEffect
{
    public override void OnPickup(Player player)
    {
        World.getInstance().STARS_PICKED += 1;
    }
}
