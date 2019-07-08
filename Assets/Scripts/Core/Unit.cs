using System.Collections.Generic;


public class Unit {
    protected string _name;

    public string Name => _name;

    protected PropertyCollection _properties;
    protected TriggerCollection _triggers;
    protected Dictionary<string, Skill> _skills;

    public Unit(string name) {
        _name = name;
        _properties = new PropertyCollection();
        _triggers = new TriggerCollection();

        _properties.Add(new Property(PropertyType.Vitality, 10.0f));
        _properties.Add(new Property(PropertyType.Strength, 10.0f));
        _properties.Add(new Property(PropertyType.Intelligence, 10.0f));
        _properties.Add(new Property(PropertyType.Agility, 10.0f)); 
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
}
