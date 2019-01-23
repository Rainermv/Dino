using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prop : Actor {

    public Prop(int type = -1) {

        if (type == -1) {
            type = Random.Range(0, 8);
        }

        this.depth = 1;

        this.name = "Prop" + type;
        this.spriteKey = "Props/prop" + type;

        this.hasRigidbody = false;
        this.isKinematic = true;

        


    }
}
