public enum TargetType {
    None,
    One,
    Group,
    Force,
    All,
}

public class Skill : ITrigger {
    protected string _name;

    public string Name => _name;

    protected Unit _owner;

    public Unit Owner => _owner;

    protected float _cooldown;

    public float Cooldown {
        get => _cooldown;
        set {
            _cooldownElapsed *= value / _cooldown;
            _cooldown = value;
        }
    }

    public float RealCooldown => (1.00f - _owner.GetFloatProperty(PropertyType.CooldownReduction)) * _cooldown;

    protected float _cooldownElapsed;

    protected TriggerType[] _triggerTypes;

    public TriggerType[] TriggerTypes => _triggerTypes;

    public Skill(string name, float cooldown, params TriggerType[] triggerTypes) {
        _name = name;
        _owner = null;
        _cooldown = cooldown;
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

    public void StartCooldown() => _cooldownElapsed = 0;

    public void ResetCooldown() => _cooldownElapsed = RealCooldown;

    public void Update(float delta) => _cooldownElapsed += delta;

    public bool Ready => _cooldownElapsed >= RealCooldown;

    protected virtual void OnAdd() {

    }

    protected virtual void OnRemove() {

    }

    public virtual bool OnAttackTarget(Unit target, AttackData attackData) {
        throw new System.NotImplementedException();
    }

    public virtual bool OnDamaged(Unit target, AttackData attackData) {
        throw new System.NotImplementedException();
    }

    public virtual bool OnDamageTarget(Unit target, AttackData attackData) {
        throw new System.NotImplementedException();
    }

    public virtual void OnHpChanged(float changed) {
        throw new System.NotImplementedException();
    }
}

public class ActiveSkill : Skill {
    protected RelationFlags _targetRelation;

    public RelationFlags TargetRelation => _targetRelation;

    protected TargetType _targetType;

    public TargetType TargetType => _targetType;

    public ActiveSkill(string name, float cooldown, RelationFlags targetRelation, TargetType targetType)
        : base(name, cooldown) {
        _targetRelation = targetRelation;
        _targetType = targetType;
    }

    public bool Cast(ITargetUnits target) {
        if (!Ready) {
            return false;
        }
        if (OnCast(target) == false) {
            return false;
        }
        StartCooldown();
        OnFinishCasting(target);
        return true;
    }

    protected virtual bool OnCast(ITargetUnits target) {
        return true;
    }

    protected virtual void OnFinishCasting(ITargetUnits target) {
    }
}

public class AttackAct : ActiveSkill {
    protected AttackFactors _factors;

    public AttackAct(string name, float cooldown, TargetType targetType, AttackFactors factors)
        : base(name, cooldown, RelationFlags.Enemy, targetType) {
        _factors = factors;
    }

    protected override void OnFinishCasting(ITargetUnits target) {
        for (var it = target.GetUnitEnumerator(UnitFilter.AliveCanBeTargetedAt, _owner, _targetRelation); it.MoveNext();) {
            AttackData ad = _owner.Attack(it.Current, _factors);
            if (ad == null) {
                continue;
            }
            it.Current.Damaged(_owner, ad);
        }
    }
}
