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
    [Description("战斗势力")]
    BattleForce,
    [Description("战斗组")]
    BattleGroup,
}

public abstract class Property {
    protected PropertyType _type;

    public PropertyType Type => _type;

    public Property(PropertyType type) {
        _type = type;
    }
}

public class ValueProperty : Property {
    protected float _base;

    public float Base {
        get => _base;

        set {
            _base = value;
            _value = _a * value + _b;
            _value = _value < _min ? _min : (_value > _max ? _max : _value);
        }
    }

    protected float _a;

    public float A {
        get => _a;

        set {
            _a = value;
            _value = value * _base + _b;
            _value = _value < _min ? _min : (_value > _max ? _max : _value);
        }
    }

    protected float _b;

    public float B {
        get => _b;

        set {
            _b = value;
            _value = _a * _base + value;
            _value = _value < _min ? _min : (_value > _max ? _max : _value);
        }
    }

    protected float _min;

    public float Min {
        get => _min;

        set {
            _min = value;
            _value = _a * _base + _b;
            if (_value < value) {
                _value = value;
            }
        }
    }

    protected float _max;

    public float Max {
        get => _max;

        set {
            _max = value;
            _value = _a * _base + _b;
            if (_value > value) {
                _value = value;
            }
        }
    }

    protected float _value;

    public float Value => _value;

    public ValueProperty(PropertyType type, float @base, float min = 0f, float max = 999999f, float a = 1f, float b = 0f)
        : base(type) {
        _base = @base;
        _min = min;
        _max = max;
        _a = a;
        _b = b;
        _value = a * @base + b;
    }
}

public class PropertyCollection {
    protected Dictionary<PropertyType, Property> _properties;

    public PropertyCollection() {
        _properties = new Dictionary<PropertyType, Property>();
    }

    public PROPERTY Add<PROPERTY>(PROPERTY prop) where PROPERTY : Property {
        _properties.Add(prop.Type, prop);
        return prop;
    }

    public void Remove(PropertyType type) {
        _properties.Remove(type);
    }

    public ValueProperty Get(PropertyType type) {
        return _properties.TryGetValue(type, out Property ret) ? ret as ValueProperty : null;
    }

    public ValueProperty this[PropertyType type] => Get(type);

    public PROPERTY Get<PROPERTY>(PropertyType type) where PROPERTY : Property {
        return _properties.TryGetValue(type, out Property ret) ? ret as PROPERTY : null;
    }
}

public class SimpleProperty<TYPE> : Property {
    protected TYPE _value;

    public TYPE Value { get => _value; set => _value = value; }

    public SimpleProperty(PropertyType type, TYPE value)
        : base(type) {
        _value = value;
    }
}

public class MaxValueProperty : Property {
    protected float _current;

    public float Current => _current;

    protected ValueProperty _max;

    protected bool _overflow;

    public bool Overflow {
        get => _overflow;
        set {
            if (value == _overflow || value == false) {
                return;
            }
            if (_current > _max.Value) {
                _current = _max.Value;
            }
            _overflow = value;
        }
    }

    public MaxValueProperty(PropertyType type, float @base, float max = 999999f, float a = 1f, float b = 0f, bool overflow = false)
        : base(type) {
        _max = new ValueProperty(type, @base, float.Epsilon, max, a, b);
        _overflow = overflow;
    }

    public float Base {
        get => _max.Base;
        set {
            _max.Base = value;
        }
    }

    public float A {
        get => _max.A;
        set {
            _max.A = value;
        }
    }

    public float B {
        get => _max.B;
        set {
            _max.B = value;
        }
    }

    public float Min {
        get => _max.Min;
        set {
            _max.Min = value;
        }
    }

    public float Max {
        get => _max.Max;
        set {
            _max.Max = value;
        }
    }
}