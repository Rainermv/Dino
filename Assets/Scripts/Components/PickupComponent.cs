using UnityEngine;
using System.Collections;
using System;

public class PickupComponent : ActorComponent
{

    private Pickup pickup;

   
    float translateAnimationLimit = 0.05f;
    float rotateAnimation = 5f;

    float translateSpeed = 0.05f;
    float rotateSpeed = 0.3f;

    float translateT = 0;
    float rotateT = 0;


    // Use this for initialization
    protected override void Start()
    {
        pickup = Actor as Pickup;

        StartCoroutine(RoutineTranslateAnimation());

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

    private IEnumerator RoutineTranslateAnimation() {

        //Vector2 target = translateAnimation;

        translateT = UnityEngine.Random.Range(0, translateAnimationLimit);

        while (true) {

            translateT += translateSpeed * Time.deltaTime;
            float posY = Mathf.PingPong(translateT, translateAnimationLimit) - (translateAnimationLimit * 0.5f);


            Vector3 newPosition = new Vector3(
                transform.position.x,
                transform.position.y + posY,
                transform.position.z
            );

            transform.position = newPosition;

            yield return null;
        }
        
       

    }
}
