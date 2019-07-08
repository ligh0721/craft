public enum TriggerType
{
    OnAttackTarget,
    OnDamageTarget,
}

public interface ITriggerable
{
    bool OnAttackTarget(Unit target, AttackData attackData);
}
