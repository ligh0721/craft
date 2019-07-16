using System;

public static class Utils {
    static Random _rnd;

    static Utils() {
        _rnd = new Random(Guid.NewGuid().GetHashCode());
    }

    public static int RandomInt() => _rnd.Next();

    public static int RandomInt(int maxValue) => _rnd.Next(maxValue);

    public static bool Chance(float chance) => chance > 0.00f && _rnd.NextDouble() < chance;

    public static int AllyMask(int force1, int force2, params int[] forces) {
        int mask = (1 << force1) | (1 << force2);
        foreach (var force in forces) {
            mask |= 1 << force;
        }
        return mask;
    }

    public static Relation ForceRelation(int force, int targetForce, int allyMask) {
        if (force == targetForce) {
            return Relation.Self;
        }
        return (targetForce & allyMask) != 0 ? Relation.Ally : Relation.Enemy;
    }

    public static bool CanEffect(Unit unit, Unit target, Relation effective) {
        int force = unit.GetIntProperty(PropertyType.BattleForce);
        int allyMask = unit.GetIntProperty(PropertyType.BattleForceMask);
        int targetForce = unit.GetIntProperty(PropertyType.BattleForce);
        return (ForceRelation(force, targetForce, allyMask) & effective) != 0;
    }
}
