using System.Collections.Generic;


public class Unit {
    protected string _name;

    public string Name => _name;

    protected PropertyCollection _props;
    protected TriggerCollection _triggers;
    protected Dictionary<string, Skill> _skills;

    protected float _health;

    //protected Property _maxHealth;
    //public Property MaxHealth => _maxHealth;

    //protected Property _physicAttack;
    //public Property PhysicAttack => _physicAttack;

    //protected Property _magicAttack;
    //public Property MagicAttack => _magicAttack;

    public Unit(string name) {
        _name = name;
        _props = new PropertyCollection();
        _triggers = new TriggerCollection();

        _props.Add(new ValueProperty(PropertyType.Vitality, 10));
        _props.Add(new ValueProperty(PropertyType.Strength, 10));
        _props.Add(new ValueProperty(PropertyType.Intelligence, 10));
        _props.Add(new ValueProperty(PropertyType.Agility, 10));

        // BattleProperties
        _props.Add(new ValueProperty(PropertyType.MaxHealth, 100, 1));
        _props.Add(new ValueProperty(PropertyType.PhysicAttack, 10, 1));
        _props.Add(new ValueProperty(PropertyType.MagicAttack, 10f, 1));
        _props.Add(new ValueProperty(PropertyType.PhysicDefense, 0));
        _props.Add(new ValueProperty(PropertyType.MagicDefense, 0));
        _props.Add(new ValueProperty(PropertyType.Speed, 100, max: 2000));
        _props.Add(new ValueProperty(PropertyType.CriticalRate, 0.00f, max: 1.00f));
        _props.Add(new ValueProperty(PropertyType.CriticalDamage, 1.50f, 1.00f));
        _props.Add(new ValueProperty(PropertyType.CooldownReduction, 0, -10.0f, 0.80f));

        _props.Add(new SimpleProperty<int>(PropertyType.BattleForce, 0));
        _props.Add(new SimpleProperty<int>(PropertyType.BattleGroup, 0));
        UpdateBattleProperties();
    }

    public void AddProperty(Property prop) => _props.Add(prop);

    public void RemoveProperty(PropertyType type) => _props.Remove(type);

    public ValueProperty GetProperty(PropertyType type) => _props.Get(type);

    public Property GetProperty<PROPERTY>(PropertyType type) where PROPERTY : Property => _props.Get<PROPERTY>(type);

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
        _props[PropertyType.MaxHealth].Base = _props[PropertyType.Vitality].Value * 10.0f;
        _props[PropertyType.PhysicAttack].Base = _props[PropertyType.Strength].Value * 1.0f;
        _props[PropertyType.MagicAttack].Base = _props[PropertyType.Intelligence].Value * 1.0f;
        _props[PropertyType.Speed].Base = _props[PropertyType.Agility].Value * 10.0f;

        _health = _props[PropertyType.MaxHealth].Value;
    }

    public bool Dead => (_health <= 0f);

    public void Attack() {

    }
}
