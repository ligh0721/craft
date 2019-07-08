using System.Collections.Generic;
using System.Collections;

public class Skill : ITrigger {
    protected string _name;

    public string Name => _name;

    protected Unit _owner;

    public Unit Owner => _owner;

    protected float _cooldown;

    public float Cooldown => _cooldown;

    protected float _cooldownLeft;

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
        _cooldownLeft = cooldown;
    }

    public void AddToUnit(Unit unit) {
        _owner = unit;
        OnAdd();
    }

    public void RemoveFromUnit() {
        OnRemove();
        _owner = null;
    }

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
