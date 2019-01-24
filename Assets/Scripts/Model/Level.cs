using UnityEngine;
using System.Collections;

public class Level  {

    private int distanceTarget;
    private int number;

    private bool active = true;

    public Level(int _distanceTarget, int _number) {

        this.DistanceTarget = _distanceTarget;
        this.Number = _number;

    }

    public int DistanceTarget {
        get {
            return distanceTarget;
        }

        set {
            distanceTarget = value;
        }
    }

    public int Number {
        get {
            return number;
        }

        set {
            number = value;
        }
    }

    public bool Active {
        get {
            return active;
        }

        set {
            active = value;
        }
    }
}
