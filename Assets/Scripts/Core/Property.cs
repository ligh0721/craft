using System.Collections.Generic;

public enum PropertyType {
    None,
    Strength,
    Agility,
    Intelligence,
    Vitality,
    Mentality,
    Health,
    PhysicAttack,
    MagicAttack,
    PhysicDefense,
    MagicDefense,
    Speed,
    CooldownReduction,
}

public class Property
{
    protected PropertyType _type;

    public PropertyType Type => _type;

    protected float _x;

    public float X {
        get => _x;

        set {
            _x = value;
            _y = _a * _x + _b;
        }
    }

    protected float _a;

    public float A {
        get => _a;

        set {
            _a = value;
            _y = _a * _x + _b;
        }
    }

    protected float _b;

    public float B {
        get => _b;

        set {
            _b = value;
            _y = _a * _x + _b;
        }
    }

    protected float _y;

    public float Y => _y;

    public Property(PropertyType type, float x, float a = 1.0f, float b = 0.0f)
    {
        _type = type;
        _x = x;
        _a = a;
        _b = b;
        _y = a * x + b;
    }
}

public class PropertyCollection {
    protected Dictionary<PropertyType, Property> _properties;

    public PropertyCollection() {
        _properties = new Dictionary<PropertyType, Property>();
    }

    public Property Add(Property prop) {
        _properties.Add(prop.Type, prop);
        return prop;
    }

    public void Remove(PropertyType type) {
        _properties.Remove(type);
    }

    public Property GetProperty(PropertyType type) {
        return _properties.TryGetValue(type, out Property ret) ? ret : null;
    }
}
