using System.Collections.Generic;


public class Unit {
    protected string _name;

    public string Name => _name;

    protected PropertyCollection _props;
    protected TriggerCollection _triggers;
    public TriggerCollection Triggers => _triggers;
    protected Dictionary<string, Skill> _skills;

    protected SimpleProperty<bool> _alive;
    protected MaxValueProperty _health;

    public Unit(string name) {
        _name = name;
        _props = new PropertyCollection();
        _triggers = new TriggerCollection();

        _alive = _props.AddProperty(PropertyType.Alive, new BoolProperty(true));

        _props.AddProperty(PropertyType.Vitality, new ValueProperty(10));
        _props.AddProperty(PropertyType.Strength, new ValueProperty(10));
        _props.AddProperty(PropertyType.Intelligence, new ValueProperty(10));
        _props.AddProperty(PropertyType.Agility, new ValueProperty(10));

        // BattleProperties
        _health = _props.AddProperty(PropertyType.Health, new MaxValueProperty(100));
        _props.AddProperty(PropertyType.PhysicAttack, new ValueProperty(10, 1));
        _props.AddProperty(PropertyType.MagicAttack, new ValueProperty(10f, 1));
        _props.AddProperty(PropertyType.PhysicDefense, new ValueProperty(0));
        _props.AddProperty(PropertyType.MagicDefense, new ValueProperty(0));
        _props.AddProperty(PropertyType.Speed, new ValueProperty(100, max: 2000));
        _props.AddProperty(PropertyType.CriticalRate, new ValueProperty(0.00f, max: 1.00f));
        _props.AddProperty(PropertyType.CriticalDamage, new ValueProperty(1.50f, 1.00f));
        _props.AddProperty(PropertyType.CooldownReduction, new ValueProperty(0, -10.0f, 0.80f));

        _props.AddProperty(PropertyType.BattleForce, new IntProperty(0));
        _props.AddProperty(PropertyType.BattleGroup, new IntProperty(0));
        UpdateBattleProperties();
    }

    public void AddProperty(PropertyType type, Property prop) => _props.AddProperty(type, prop);

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
            _triggers.AddTrigger(triggerType, skill);
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
            _triggers.RemoveTrigger(triggerType, skill);
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

    public AttackData Attack(Unit target, float physicalFactorA = 1, float physicalFactorB = 0, float magicFactorA = 1, float magicFactorB = 0, float criticalRateFactor = 1) {
        float criticalRate = _props.GetFloatValue(PropertyType.CriticalRate) * criticalRateFactor;
        
        float physical = _props.GetFloatValue(PropertyType.PhysicAttack) * physicalFactorA + physicalFactorB;
        float magic = _props.GetFloatValue(PropertyType.MagicAttack) * magicFactorA + magicFactorB;
        bool critical = Utils.Chance(criticalRate);
        if (critical) {
            float criticalDamage = _props.GetFloatValue(PropertyType.CriticalDamage, 1.00f);
            physical *= criticalDamage;
            magic *= criticalDamage;
        }

        AttackData ad = new AttackData(physical, magic, critical);
        if (_triggers.TriggerOnAttackTarget(target, ad) == false) {
            return null;
        }
        return ad;
    }

    public void Damaged(Unit source, AttackData ad) {
        float physicalDef = _props.GetFloatValue(PropertyType.PhysicDefense);
        ad.Physical *= (1.00f - physicalDef / (physicalDef + 100));
        float magicDef = _props.GetFloatValue(PropertyType.MagicDefense);
        ad.Magic *= (1.00f - magicDef / (magicDef + 100));
        if (source == null || source.Triggers.TriggerOnDamageTarget(this, ad) == false) {
            return;
        }
    }
}
