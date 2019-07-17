using System.Collections.Generic;
using System.ComponentModel;

public enum PropertyType {
    [Description("未设置")]
    None,
    [Description("活着的")]
    Alive,
    [Description("力量，影响负重能力以及物理攻击的威力")]
    Strength,
    [Description("敏捷，影响速度")]
    Agility,
    [Description("智力，影响武器掌握速度以及魔法攻击的威力")]
    Intelligence,
    [Description("体力，影响最大生命值的多少")]
    Vitality,
    //Mentality,
    [Description("生命值")]
    Health,
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
    [Description("战斗势力结盟掩码")]
    BattleForceAllyMask,
    [Description("战斗组")]
    BattleGroup,
}

public abstract class Property {
}

public interface IFloatValue {
    float Value { get; }
}

public interface IIntValue {
    int Value { get; }
}

public interface IBoolValue {
    bool Value { get; }
}

public interface IStrValue {
    string Value { get; }
}

public class ValueProperty : Property, IFloatValue {
    protected float _base;

    public float Base {
        get => _base;

        set {
            _value = _a * value + _b;
            _value = _value < _min ? _min : (_value > _max ? _max : _value);
            _base = value;
        }
    }

    protected float _a;

    public float A {
        get => _a;

        set {
            _value = value * _base + _b;
            _value = _value < _min ? _min : (_value > _max ? _max : _value);
            _a = value;
        }
    }

    protected float _b;

    public float B {
        get => _b;

        set {
            _value = _a * _base + value;
            _value = _value < _min ? _min : (_value > _max ? _max : _value);
            _b = value;
        }
    }

    protected float _min;

    public float Min {
        get => _min;

        set {
            if (_value < value) {
                _value = value;
            }
            _min = value;
        }
    }

    protected float _max;

    public float Max {
        get => _max;

        set {
            if (_value > value) {
                _value = value;
            }
            _max = value;
        }
    }

    protected float _value;

    public float Value => _value;

    public ValueProperty(float @base, float min = 0, float max = 999999, float a = 1, float b = 0) {
        _base = @base;
        _min = min;
        _max = max;
        _a = a;
        _b = b;
        _value = a * @base + b;
    }
}

public class SimpleProperty<TYPE> : Property {
    protected TYPE _value;

    public TYPE Value { get => _value; set => _value = value; }

    public SimpleProperty(TYPE value) {
        _value = value;
    }
}

public class FloatProperty : SimpleProperty<float>, IFloatValue {
    public FloatProperty(float value)
        : base(value) {
    }
}

public class IntProperty : SimpleProperty<int>, IIntValue {
    public IntProperty(int value)
        : base(value) {
    }
}

public class BoolProperty : SimpleProperty<bool>, IBoolValue {
    public BoolProperty(bool value)
        : base(value) {
    }
}

public class StrProperty : SimpleProperty<string>, IStrValue {
    public StrProperty(string value)
        : base(value) {
    }
}

public class MaxValueProperty : Property, IFloatValue {
    protected float _current;

    public float Current {
        get => _current;
        set => _current = (_overflow == false && value > _max.Value) ? _max.Value : value;
    }

    protected ValueProperty _max;

    protected bool _overflow;

    public bool Overflow {
        get => _overflow;
        set {
            if (value == _overflow) {
                return;
            }
            if (value == false && _current > _max.Value) {
                _current = _max.Value;
            }
            _overflow = value;
        }
    }

    public MaxValueProperty(float @base, float max = 999999, float a = 1, float b = 0, bool overflow = false) {
        _max = new ValueProperty(@base, float.Epsilon, max, a, b);
        _overflow = overflow;
        _current = _max.Value;
    }

    public float Base {
        get => _max.Base;
        set {
            float old = _max.Value;
            _max.Base = value;
            _current *= _max.Value / old;
        }
    }

    public float A {
        get => _max.A;
        set {
            float old = _max.Value;
            _max.A = value;
            _current *= _max.Value / old;
        }
    }

    public float B {
        get => _max.B;
        set {
            float old = _max.Value;
            _max.B = value;
            _current *= _max.Value / old;
        }
    }

    public float Max {
        get => _max.Max;
        set {
            float old = _max.Value;
            _max.Max = value;
            _current *= _max.Value / old;
        }
    }

    public float Value => _max.Value;
}

public class PropertyCollection {
    protected Dictionary<PropertyType, Property> _props;

    public PropertyCollection() {
        _props = new Dictionary<PropertyType, Property>();
    }

    public PROPERTY AddProperty<PROPERTY>(PropertyType type, PROPERTY prop) where PROPERTY : Property {
        _props.Add(type, prop);
        return prop;
    }

    public void RemoveProperty(PropertyType type) => _props.Remove(type);

    public Property GetProperty(PropertyType type) => _props.TryGetValue(type, out Property ret) ? ret : null;

    public Property this[PropertyType type] => GetProperty(type);

    public PROPERTY GetProperty<PROPERTY>(PropertyType type) where PROPERTY : Property => _props.TryGetValue(type, out Property ret) ? ret as PROPERTY : null;

    public float GetFloatValue(PropertyType type, float @default = 0) => _props.TryGetValue(type, out Property ret) ? (ret as IFloatValue).Value : @default;

    public int GetIntValue(PropertyType type, int @default = 0) => _props.TryGetValue(type, out Property ret) ? (ret as IIntValue).Value : @default;

    public bool GetBoolValue(PropertyType type, bool @default = false) => _props.TryGetValue(type, out Property ret) ? (ret as IBoolValue).Value : @default;

    public string GetStrValue(PropertyType type, string @default = "") => _props.TryGetValue(type, out Property ret) ? (ret as IStrValue).Value : @default;
}
