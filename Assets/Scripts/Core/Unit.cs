using System.Collections.Generic;


public class TriggerGroup
{
    protected HashSet<ITriggerable> _triggers;
    pro
}

public class Unit
{
    protected string _name;

    public string Name
    {
        get
        {
            return _name;
        }
    }

    protected Dictionary<string, Property> _properties;
    protected Dictionary<TriggerType, HashSet<ITriggerable>> _allTriggers;
    protected Dictionary<string, Skill> _skills;

    public Unit(string name)
    {
        _name = name;
        _properties = new Dictionary<string, Property>();
        _allTriggers = new Dictionary<TriggerType, HashSet<ITriggerable>>();
    }

    public Property AddProperty(Property prop)
    {
        _properties.Add(prop.Name, prop);
        return prop;
    }

    public void DelProperty(string name)
    {
        _properties.Remove(name);
    }

    public Property GetProperty(string name)
    {
        return _properties.TryGetValue(name, out Property ret) ? ret : null;
    }

    public Skill AddSkill(Skill skill)
    {
        skill.AddToUnit(this);
        foreach (var triggerType in skill.TriggerTypes)
        {

            if (_allTriggers.TryGetValue(triggerType, out HashSet<ITriggerable> triggers) == false)
            {
                triggers = new HashSet<ITriggerable>();
                _allTriggers.Add(triggerType, triggers);
            }
            triggers.Add(skill);
        }
        _skills.Add(skill.Name, skill);
        return skill;
    }

    public void DelSkill(string name)
    {
        if (_skills.TryGetValue(name, out Skill skill) == false)
        {
            return;
        }
        _skills.Remove(name);
        foreach (var triggerType in skill.TriggerTypes)
        {
            _allTriggers[triggerType].Remove(skill);
        }
        skill.DelFromUnit();
    }
}
