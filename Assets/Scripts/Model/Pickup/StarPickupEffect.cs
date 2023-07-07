using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

public class StarPickupEffect : PickupEffect
{
    public override void OnPickup(Player player)
    {
        World.GetInstance().StarsPicked += 1;
    }
}
