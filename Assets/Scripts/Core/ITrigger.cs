using System.Collections.Generic;

public enum TriggerType {
    None,
    OnAttackTarget,
    OnDamageTarget,
}

public interface ITrigger {
    bool OnAttackTarget(Unit target, AttackData attackData);
    bool OnDamageTarget(Unit target, float value);
}

public class TriggerCollection {
    protected HashSet<ITrigger> _onAttackTargetTriggers;
    protected HashSet<ITrigger> _onDamageTargetTriggers;

    public TriggerCollection() {
        _onAttackTargetTriggers = new HashSet<ITrigger>();
        _onDamageTargetTriggers = new HashSet<ITrigger>();
    }

    public void AddTrigger(TriggerType type, ITrigger trigger) {
        switch (type) {
        case TriggerType.OnAttackTarget:
            _onAttackTargetTriggers.Add(trigger);
            break;
        case TriggerType.OnDamageTarget:
            _onDamageTargetTriggers.Add(trigger);
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
        case TriggerType.OnDamageTarget:
            _onDamageTargetTriggers.Remove(trigger);
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

    public bool TriggerOnDamageTarget(Unit target, float value) {
        for (var it = _onDamageTargetTriggers.GetEnumerator(); it.MoveNext();) {
            if (it.Current.OnDamageTarget(target, value) == false) {
                return false;
            }
        }
        return true;
    }
}
