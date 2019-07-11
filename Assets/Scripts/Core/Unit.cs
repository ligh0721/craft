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

        _props.Add(new ValueProperty(PropertyType.Vitality, 10f));
        _props.Add(new ValueProperty(PropertyType.Strength, 10f));
        _props.Add(new ValueProperty(PropertyType.Intelligence, 10f));
        _props.Add(new ValueProperty(PropertyType.Agility, 10f));

        _props.Add(new ValueProperty(PropertyType.MaxHealth, 100f, 1f));
        _props.Add(new ValueProperty(PropertyType.PhysicAttack, 10f, 1f));
        _props.Add(new ValueProperty(PropertyType.MagicAttack, 10f, 1f));
        _props.Add(new ValueProperty(PropertyType.PhysicDefense, 0f));
        _props.Add(new ValueProperty(PropertyType.MagicDefense, 0f));
        _props.Add(new ValueProperty(PropertyType.Speed, 100f, max: 2000f));
        _props.Add(new ValueProperty(PropertyType.CriticalRate, 0.00f, max: 1.00f));
        _props.Add(new ValueProperty(PropertyType.CriticalDamage, 1.50f, 1.00f));
        UpdateBattleProperties();
    }

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
