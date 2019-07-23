using System.Collections.Generic;

public class BattleField {
    protected AllTarget _targetUnits;

    public AllTarget AllTarget => _targetUnits;

    protected Dictionary<int, int> _forceAllyMasks;

    public BattleField() {
        _targetUnits = new AllTarget();
        _forceAllyMasks = new Dictionary<int, int>();
    }

    public void ForceAlly(int force1, params int[] otherForces) => _forceAllyMasks.Add(force1, Utils.AllyMask(force1, otherForces));

    public void AddUnit(Unit unit, int force, int group) {
        if (_forceAllyMasks.TryGetValue(force, out var allyMask) == false) {
            allyMask = Utils.AllyMask(force);
            _forceAllyMasks.Add(force, allyMask);
        }

        var forceProp = unit.GetProperty<IntProperty>(PropertyType.BattleForce);
        var forceAllyMaskProp = unit.GetProperty<IntProperty>(PropertyType.BattleForceAllyMask);
        var groupProp = unit.GetProperty<IntProperty>(PropertyType.BattleGroup);
        forceProp.Value = force;
        forceAllyMaskProp.Value = allyMask;
        groupProp.Value = group;
        _targetUnits.AddUnit(unit, force, group);
    }
}
