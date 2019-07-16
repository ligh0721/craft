public static class Utils {
    static int AllyMask(int force1, int force2, params int[] forces) {
        int mask = (1 << force1) | (1 << force2);
        foreach (var force in forces) {
            mask |= 1 << force;
        }
        return mask;
    }

    static Relation ForceRelation(int force, int targetForce, int allyMask) {
        if (force == targetForce) {
            return Relation.Self;
        }
        return (targetForce & allyMask) != 0 ? Relation.Ally : Relation.Enemy;
    }

    static bool CanEffect(Unit unit, Unit target, Relation effective) {
        int force = unit.GetIntProperty(PropertyType.BattleForce);
        int allyMask = unit.GetIntProperty(PropertyType.BattleForceMask);
        int targetForce = unit.GetIntProperty(PropertyType.BattleForce);
        return (ForceRelation(force, targetForce, allyMask) & effective) != 0;
    }
}
