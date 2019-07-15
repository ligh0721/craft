using System.Collections.Generic;
using System.Collections;

public class Skill : ITrigger {
    protected string _name;

    public string Name => _name;

    protected Unit _owner;

    public Unit Owner => _owner;

    protected float _cooldown;

    public float Cooldown {
        get => _cooldown;
        set {
            ValueProperty cdr = _owner.GetProperty(PropertyType.CooldownReduction);
            float cd_rate = cdr != null ? 1.00f - cdr.Value : 1.00f;
            
            _cooldownElapsed *= value / _cooldown;
            _cooldown = value;
        }
    }

    public float RealCooldown {
        get {
            ValueProperty cdr = _owner.GetProperty(PropertyType.CooldownReduction);
            return cdr != null ? (1.00f - cdr.Value) * _cooldown : _cooldown;
        }
    }

    protected float _cooldownElapsed;

    protected TargetEffective _effective;

    public TargetEffective Effective => _effective;

    protected TargetRange _range;

    public TargetRange Range => _range;

    protected TriggerType[] _triggerTypes;

    public TriggerType[] TriggerTypes => _triggerTypes;

    public Skill(string name, float cooldown, TargetEffective effective, TargetRange range, params TriggerType[] triggerTypes) {
        _name = name;
        _owner = null;
        _cooldown = cooldown;
        _effective = effective;
        _range = range;
        _triggerTypes = triggerTypes;
        _cooldownElapsed = 0;
    }

    public void AddToUnit(Unit unit) {
        _owner = unit;
        OnAdd();
    }

    public void RemoveFromUnit() {
        OnRemove();
        _owner = null;
    }

    public void StartCoolingdown() {
        _cooldownElapsed = 0;
    }

    public void Update(float delta) => _cooldownElapsed += delta;

    protected void OnAdd() {

    }

    protected void OnRemove() {

    }

    public bool OnAttackTarget(Unit target, AttackData attackData) {
        throw new System.NotImplementedException();
    }

    public bool OnDamageTarget(Unit target, float value) {
        throw new System.NotImplementedException();
    }
}

public class ActiveSkill : Skill {
    public ActiveSkill(string name, float cooldown, TargetEffective effective, TargetRange range, params TriggerType[] triggerTypes)
        : base(name, cooldown, effective, range, triggerTypes) {

    }

    public void Cast() {

    }

    public void Cast(Unit one) {

    }

    public void Cast(BattleGroup group) {

    }

    public void Cast(BattleForce force) {

    }

    public void Cast(BattleField all) {

    }
}
