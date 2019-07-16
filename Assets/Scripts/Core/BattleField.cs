using System.Collections.Generic;


public static class UnitFilter {
    public delegate bool Filter(Unit unit, params object[] args);

    public static bool Alive(Unit unit, params object[] _) => unit.Alive;
}

public class BattleGroup {
    protected int _group;

    public int Group => _group;

    protected SortedSet<Unit> _units;

    public BattleGroup(int group) {

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

public class BattleForce {
    protected int _force;
    protected SortedDictionary<int, BattleGroup> _groups;

    public BattleForce(int force) {
        _force = force;
        _groups = new SortedDictionary<int, BattleGroup>();
    }

    public void AddUnit(int group, Unit unit) {
        if (_groups.TryGetValue(group, out var battleGroup) == false) {
            _groups.Add(group, new BattleGroup(group));
        }
        battleGroup.AddUnit(unit);
    }

    public IEnumerator<BattleGroup> GetGroupEnumerator() {
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

public class BattleField {
    protected SortedDictionary<int, BattleForce> _forces;

    public BattleField() {
        _forces = new SortedDictionary<int, BattleForce>();
    }

    public void AddUnit(Unit unit, int force, int group) {
        if (_forces.TryGetValue(force, out var forceObj) == false) {
            _forces.Add(force, new BattleForce(force));
        }
        forceObj.AddUnit(group, unit);
    }

    public IEnumerator<BattleForce> GetForceEnumerator() {
        for (var forceIter = _forces.GetEnumerator(); forceIter.MoveNext();) {
            yield return forceIter.Current.Value;
        }
    }

    public IEnumerator<BattleGroup> GetGroupEnumerator() {
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
