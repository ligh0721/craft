using System;
using System.Collections.Generic;

[Flags]
public enum RelationFlags {
    None = 0,
    Self = 1 << 0,
    Ally = 1 << 1,
    Enemy = 1 << 2,
}

public static class UnitFilter {
    public delegate bool Filter(Unit unit, params object[] args);

    public static bool Alive(Unit unit, params object[] _) => unit.Alive;

    public static bool CanBeTargetedAt(Unit unit, params object[] args) => Utils.CanBeTargetedAt((Unit)args[0], unit, (RelationFlags)args[1]);

    public static bool AliveCanBeTargetedAt(Unit unit, params object[] args) => unit.Alive && Utils.CanBeTargetedAt((Unit)args[0], unit, (RelationFlags)args[1]);
}

public interface ITargetUnits {
    int UnitCount { get; }

    IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args);
}

public class NoTarget : ITargetUnits {
    public int UnitCount => 0;

    public IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args) {
        yield return null;
    }
}

public class OneTarget : ITargetUnits {
    protected Unit _one;

    public Unit One => _one;

    public int UnitCount => 1;

    public OneTarget(Unit one) {
        _one = one;
    }

    public IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args) {
        if (filter != null && filter(_one, args)) {
            yield return _one;
        }
    }
}

public class GroupTarget : ITargetUnits {
    protected int _group;

    public int Group => _group;

    public int UnitCount => _units.Count;

    protected SortedSet<Unit> _units;

    public GroupTarget(int group) {
        _group = group;
        _units = new SortedSet<Unit>();
    }

    public bool AddUnit(Unit unit) => _units.Add(unit);

    public IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args) {
        for (var iter = _units.GetEnumerator(); iter.MoveNext();) {
            if (filter != null && filter(iter.Current, args)) {
                yield return iter.Current;
            }
        }
    }
}

public class ForceTarget : ITargetUnits {
    protected int _force;

    public int Force => _force;

    protected SortedDictionary<int, GroupTarget> _groups;

    protected int _unitCount;

    public int UnitCount => _unitCount;

    public ForceTarget(int force) {
        _force = force;
        _groups = new SortedDictionary<int, GroupTarget>();
        _unitCount = 0;
    }

    public bool AddUnit(int group, Unit unit) {
        if (_groups.TryGetValue(group, out var battleGroup) == false) {
            battleGroup = new GroupTarget(group);
            _groups.Add(group, battleGroup);
        }
        var ret = battleGroup.AddUnit(unit);
        if (ret) {
            ++_unitCount;
        }
        return ret;
    }

    public GroupTarget GetGroup(int group) => _groups.TryGetValue(group, out var groupTarget) == false ? null : groupTarget;

    public IEnumerator<GroupTarget> GetGroupEnumerator() {
        for (var groupIter = _groups.GetEnumerator(); groupIter.MoveNext();) {
            yield return groupIter.Current.Value;
        }
    }

    public IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args) {
        for (var groupIter = _groups.GetEnumerator(); groupIter.MoveNext();) {
            for (var iter = groupIter.Current.Value.GetUnitEnumerator(filter, args); iter.MoveNext();) {
                yield return iter.Current;
            }
        }
    }
}

public class AllTarget : ITargetUnits {
    protected SortedDictionary<int, ForceTarget> _forces;

    protected int _unitCount;

    public int UnitCount => _unitCount;

    public AllTarget() {
        _forces = new SortedDictionary<int, ForceTarget>();
        _unitCount = 0;
    }

    public bool AddUnit(Unit unit, int force, int group) {
        if (_forces.TryGetValue(force, out var forceObj) == false) {
            forceObj = new ForceTarget(force);
            _forces.Add(force, forceObj);
        }
        var ret = forceObj.AddUnit(group, unit);
        if (ret) {
            ++_unitCount;
        }
        return ret;
    }

    public ForceTarget GetForce(int force) => _forces.TryGetValue(force, out var forceTarget) == false ? null : forceTarget;

    public IEnumerator<ForceTarget> GetForceEnumerator() {
        for (var forceIter = _forces.GetEnumerator(); forceIter.MoveNext();) {
            yield return forceIter.Current.Value;
        }
    }

    public IEnumerator<GroupTarget> GetGroupEnumerator() {
        for (var forceIter = _forces.GetEnumerator(); forceIter.MoveNext();) {
            for (var groupIter = forceIter.Current.Value.GetGroupEnumerator(); groupIter.MoveNext();) {
                yield return groupIter.Current;
            }
        }
    }

    public IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args) {
        for (var forceIter = _forces.GetEnumerator(); forceIter.MoveNext();) {
            for (var groupIter = forceIter.Current.Value.GetGroupEnumerator(); groupIter.MoveNext();) {
                for (var iter = groupIter.Current.GetUnitEnumerator(filter, args); iter.MoveNext();) {
                    yield return iter.Current;
                }
            }
        }
    }
}
