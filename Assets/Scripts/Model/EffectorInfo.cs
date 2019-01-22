public enum EffectorType {

    Platform,

}

public class EffectorInfo {

    public EffectorType type = EffectorType.Platform;
    public bool oneWay = true;
    internal float surfaceArc = 180;
}
