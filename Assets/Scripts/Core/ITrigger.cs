using System.Collections.Generic;

public enum TriggerType {
    None,
    OnAttackTarget,
    OnDamaged,
    OnDamageTarget,
    OnHpChanged,
}

public interface ITrigger {
    bool OnAttackTarget(Unit target, AttackData attackData);
    bool OnDamaged(Unit source, AttackData attackData);
    bool OnDamageTarget(Unit target, AttackData attackData);
    void OnHpChanged(float changed);
}

public class TriggerCollection {
    protected HashSet<ITrigger> _onAttackTargetTriggers;
    protected HashSet<ITrigger> _onDamagedTriggers;
    protected HashSet<ITrigger> _onDamageTargetTriggers;
    protected HashSet<ITrigger> _onHpChangedTriggers;

    public TriggerCollection() {
        _onAttackTargetTriggers = new HashSet<ITrigger>();
        _onDamagedTriggers = new HashSet<ITrigger>();
        _onDamageTargetTriggers = new HashSet<ITrigger>();
        _onHpChangedTriggers = new HashSet<ITrigger>();
    }

    public void AddTrigger(TriggerType type, ITrigger trigger) {
        switch (type) {
        case TriggerType.OnAttackTarget:
            _onAttackTargetTriggers.Add(trigger);
            break;
        case TriggerType.OnDamaged:
            _onDamagedTriggers.Add(trigger);
            break;
        case TriggerType.OnDamageTarget:
            _onDamageTargetTriggers.Add(trigger);
            break;
        case TriggerType.OnHpChanged:
            _onHpChangedTriggers.Add(trigger);
            break;
        default:
            break;
        }
    }

    public void RemoveTrigger(TriggerType type, ITrigger trigger) {
        switch (type) {
        case TriggerType.OnAttackTarget:
            _onAttackTargetTriggers.Remove(trigger);
            break;
        case TriggerType.OnDamaged:
            _onDamagedTriggers.Remove(trigger);
            break;
        case TriggerType.OnDamageTarget:
            _onDamageTargetTriggers.Remove(trigger);
            break;
        case TriggerType.OnHpChanged:
            _onHpChangedTriggers.Remove(trigger);
            break;
        default:
            break;
        }
    }

    public bool TriggerOnAttackTarget(Unit target, AttackData attackData) {
        for (var it = _onAttackTargetTriggers.GetEnumerator(); it.MoveNext();) {
            if (it.Current.OnAttackTarget(target, attackData) == false) {
                return false;
            }
        }
        return true;
    }

    public bool TriggerOnDamaged(Unit source, AttackData ad) {
        for (var it = _onDamagedTriggers.GetEnumerator(); it.MoveNext();) {
            if (it.Current.OnDamaged(source, ad) == false) {
                return false;
            }
        }
        return true;
    }

    public bool TriggerOnDamageTarget(Unit target, AttackData ad) {
        for (var it = _onDamageTargetTriggers.GetEnumerator(); it.MoveNext();) {
            if (it.Current.OnDamageTarget(target, ad) == false) {
                return false;
            }
        }
        return true;
    }

    public void TriggerOnHpChanged(float changed) {
        for (var it = _onHpChangedTriggers.GetEnumerator(); it.MoveNext();) {
            it.Current.OnHpChanged(changed);
        }
    }
}
