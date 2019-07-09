using System.Collections.Generic;
using System.ComponentModel;

public enum PropertyType {
    [Description("未设置")]
    None,
    [Description("力量，影响负重能力以及物理攻击的威力")]
    Strength,
    [Description("敏捷，影响负速度")]
    Agility,
    [Description("智力，影响武器掌握速度以及魔法攻击的威力")]
    Intelligence,
    [Description("体力，影响最大生命值的多少")]
    Vitality,
    Mentality,
    [Description("最大生命值")]
    MaxHealth,
    [Description("物理攻击力")]
    PhysicAttack,
    [Description("魔法攻击力")]
    MagicAttack,
    [Description("物理防御力")]
    PhysicDefense,
    [Description("魔法防御力")]
    MagicDefense,
    [Description("速度")]
    Speed,
    [Description("冷却时间减少")]
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
