using System.Collections.Generic;
using System.ComponentModel;

public enum PropertyType {
    [Description("未设置")]
    None,
    [Description("力量，影响负重能力以及物理攻击的威力")]
    Strength,
    [Description("敏捷，影响速度")]
    Agility,
    [Description("智力，影响武器掌握速度以及魔法攻击的威力")]
    Intelligence,
    [Description("体力，影响最大生命值的多少")]
    Vitality,
    //Mentality,
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
    [Description("暴击率")]
    CriticalRate,
    [Description("暴击伤害")]
    CriticalDamage,
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
            _y = _a * value + _b;
            _y = _y < _min ? _min : (_y > _max ? _max : _y);
        }
    }

    protected float _a;

    public float A {
        get => _a;

        set {
            _a = value;
            _y = value * _x + _b;
            _y = _y < _min ? _min : (_y > _max ? _max : _y);
        }
    }

    protected float _b;

    public float B {
        get => _b;

        set {
            _b = value;
            _y = _a * _x + value;
            _y = _y < _min ? _min : (_y > _max ? _max : _y);
        }
    }

    protected float _min;

    public float Min {
        get => _min;

        set {
            _min = value;
            _y = _a * _x + _b;
            if (_y < value) {
                _y = value;
            }
        }
    }

    protected float _max;

    public float Max {
        get => _max;

        set {
            _max = value;
            _y = _a * _x + _b;
            if (_y > value) {
                _y = value;
            }
        }
    }

    protected float _y;

    public float Y => _y;

    public Property(PropertyType type, float x, float min = 0f, float max = 999999f, float a = 1f, float b = 0f)
    {
        _type = type;
        _x = x;
        _min = min;
        _max = max;
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

    public Property Get(PropertyType type) {
        return _properties.TryGetValue(type, out Property ret) ? ret : null;
    }

    public Property this[PropertyType type] => Get(type);
}
