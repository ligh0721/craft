using System.Collections.Generic;


public class Unit {
    protected string _name;

    public string Name => _name;

    protected PropertyCollection _props;
    protected TriggerCollection _triggers;
    protected Dictionary<string, Skill> _skills;

    protected SimpleProperty<bool> _alive;
    protected MaxValueProperty _health;

    public Unit(string name) {
        _name = name;
        _props = new PropertyCollection();
        _triggers = new TriggerCollection();

        _alive = _props.AddProperty(new SimpleProperty<bool>(PropertyType.Alive, true));

        _props.AddProperty(new ValueProperty(PropertyType.Vitality, 10));
        _props.AddProperty(new ValueProperty(PropertyType.Strength, 10));
        _props.AddProperty(new ValueProperty(PropertyType.Intelligence, 10));
        _props.AddProperty(new ValueProperty(PropertyType.Agility, 10));

        // BattleProperties
        _health = _props.AddProperty(new MaxValueProperty(PropertyType.Health, 100));
        _props.AddProperty(new ValueProperty(PropertyType.PhysicAttack, 10, 1));
        _props.AddProperty(new ValueProperty(PropertyType.MagicAttack, 10f, 1));
        _props.AddProperty(new ValueProperty(PropertyType.PhysicDefense, 0));
        _props.AddProperty(new ValueProperty(PropertyType.MagicDefense, 0));
        _props.AddProperty(new ValueProperty(PropertyType.Speed, 100, max: 2000));
        _props.AddProperty(new ValueProperty(PropertyType.CriticalRate, 0.00f, max: 1.00f));
        _props.AddProperty(new ValueProperty(PropertyType.CriticalDamage, 1.50f, 1.00f));
        _props.AddProperty(new ValueProperty(PropertyType.CooldownReduction, 0, -10.0f, 0.80f));

        _props.AddProperty(new SimpleProperty<int>(PropertyType.BattleForce, 0));
        _props.AddProperty(new SimpleProperty<int>(PropertyType.BattleGroup, 0));
        UpdateBattleProperties();
    }

    public void AddProperty(Property prop) => _props.AddProperty(prop);

    public void RemoveProperty(PropertyType type) => _props.RemoveProperty(type);

    public Property GetPropertyObject(PropertyType type) => _props.GetProperty(type);

    public PROPERTY GetPropertyObject<PROPERTY>(PropertyType type) where PROPERTY : Property => _props.GetProperty<PROPERTY>(type);

    public float GetFloatProperty(PropertyType type, float @default = 0) => _props.GetFloatValue(type, @default);

    public int GetIntProperty(PropertyType type, int @default = 0) => _props.GetIntValue(type, @default);

    public bool GetBoolProperty(PropertyType type, bool @default = false) => _props.GetBoolValue(type, @default);

    public string GetStrProperty(PropertyType type, string @default = "") => _props.GetStrValue(type, @default);

    public Skill AddSkill(Skill skill) {
        skill.AddToUnit(this);
        foreach (var triggerType in skill.TriggerTypes) {
            _triggers.Add(triggerType, skill);
        }
        _skills.Add(skill.Name, skill);
        return skill;
    }

    public void RemoveSkill(string name) {
        if (_skills.TryGetValue(name, out Skill skill) == false) {
            return;
        }
        _skills.Remove(name);
        foreach (var triggerType in skill.TriggerTypes) {
            _triggers.Remove(triggerType, skill);
        }
        skill.RemoveFromUnit();
    }

    public void UpdateBattleProperties() {
        _props.GetProperty<MaxValueProperty>(PropertyType.Health).Base = _props.GetProperty<ValueProperty>(PropertyType.Vitality).Value * 10.0f;
        _props.GetProperty<ValueProperty>(PropertyType.PhysicAttack).Base = _props.GetProperty<ValueProperty>(PropertyType.Strength).Value * 1.0f;
        _props.GetProperty<ValueProperty>(PropertyType.MagicAttack).Base = _props.GetProperty<ValueProperty>(PropertyType.Intelligence).Value * 1.0f;
        _props.GetProperty<ValueProperty>(PropertyType.Speed).Base = _props.GetProperty<ValueProperty>(PropertyType.Agility).Value * 10.0f;
    }

    public bool Alive => _alive.Value;

    public void Attack() {

    }
}
