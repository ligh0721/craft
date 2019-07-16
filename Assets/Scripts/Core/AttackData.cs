public enum AttackType {
    None,
    Physical,
    Magic,
}

public class AttackData {
    protected float _physical;

    public float Physical {
        get => _physical;
        set {
            _physical = value;
        }
    }

    protected float _magic;

    public float Magic {
        get => _magic;

        set {
            _magic = value;
        }
    }

    protected readonly bool _critical;

    public bool Critical => _critical;

    public AttackData(float physical, float magic, bool critical) {
        _physical = physical;
        _magic = magic;
        _critical = critical;
    }
}
