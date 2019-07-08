public enum AttackType {
    None,
    Physical,
    Magic,
}

public class AttackData {
    protected AttackType _type;

    public AttackType Type => _type;

    protected float _value;

    public float Value => _value;

    public AttackData(AttackType type, float value) {
        _type = type;
        _value = value;
    }
}
