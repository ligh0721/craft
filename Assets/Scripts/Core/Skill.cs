using System.Collections.Generic;
using System.Collections;

public class Skill : ITriggerable
{
    protected string _name;

    public string Name
    {
        get
        {
            return _name;
        }
    }

    protected Unit _owner;

    public Unit Owner
    {
        get
        {
            return _owner;
        }
    }

    protected TriggerType[] _triggerTypes;

    public TriggerType[] TriggerTypes
    {
        get
        {
            return _triggerTypes;
        }
    }

    public Skill(string name, params TriggerType[] triggerTypes)
    {
        _name = name;
        _triggerTypes = triggerTypes;
    }

    public void AddToUnit(Unit unit)
    {
        _owner = unit;
        OnAdd();
    }

    public void DelFromUnit()
    {
        OnDel();
        _owner = null;
    }

    public bool OnAttackTarget(Unit target, AttackData attackData)
    {
        throw new System.NotImplementedException();
    }

    protected void OnAdd()
    {

    }

    protected void OnDel()
    {

    }
}
