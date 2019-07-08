public enum AttackType
{
    Physical = 1,
    Magic = 2,
}

public class AttackData
{
    protected AttackType _type;

    public AttackType Type
    {
        get
        {
            return _type;
        }
    }

    protected float _value;

    public float Value
    {
        get
        {
            return _value;
        }
    }

    public AttackData(AttackType type, float value)
    {
        _type = type;
        _value = value;
    }
}
