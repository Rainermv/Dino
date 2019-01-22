using UnityEngine;

public enum ColliderType {

    Box,
    Circle,
    Edge

}

public class ColliderInfo {

    public ColliderType type = ColliderType.Box;
    public Vector2 size = new Vector2(1, 1);
    public Vector2 offset = new Vector2(0, 0);
    public string materialKey = "Default";
    public bool trigger = false;
    public bool usedByEffector = true;

}
