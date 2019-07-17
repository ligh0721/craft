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
    IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args);
}

public class NoTarget : ITargetUnits {
    public IEnumerator<Unit> GetUnitEnumerator(UnitFilter.Filter filter = null, params object[] args) {
        yield return null;
    }
}

public class OneTarget : ITargetUnits {
    protected Unit _one;

    public Unit One => _one;

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

    protected SortedSet<Unit> _units;

    public GroupTarget(int group) {
        _group = group;
        _units = new SortedSet<Unit>();
    }

    public void AddUnit(Unit unit) {
        _units.Add(unit);
    }

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
    protected SortedDictionary<int, GroupTarget> _groups;

    public ForceTarget(int force) {
        _force = force;
        _groups = new SortedDictionary<int, GroupTarget>();
    }

    public void AddUnit(int group, Unit unit) {
        if (_groups.TryGetValue(group, out var battleGroup) == false) {
            _groups.Add(group, new GroupTarget(group));
        }
        battleGroup.AddUnit(unit);
    }

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

    public AllTarget() {
        _forces = new SortedDictionary<int, ForceTarget>();
    }

    public void AddUnit(Unit unit, int force, int group) {
        if (_forces.TryGetValue(force, out var forceObj) == false) {
            _forces.Add(force, new ForceTarget(force));
        }
        forceObj.AddUnit(group, unit);
    }

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
