﻿using System;

public static class Utils {
    static Random _rnd;

    static Utils() {
        _rnd = new Random(Guid.NewGuid().GetHashCode());
    }

    public static int RandomInt() => _rnd.Next();

    public static int RandomInt(int maxValue) => _rnd.Next(maxValue);

    public static int RandomInt(int minValue, int maxValue) => _rnd.Next(minValue, maxValue);

    public static bool Chance(float chance) => chance > 0.00f && _rnd.NextDouble() < chance;

    public static int AllyMask(int force1, params int[] otherForces) {
        int mask = 1 << force1;
        foreach (var force in otherForces) {
            mask |= 1 << force;
        }
        return mask;
    }

    public static RelationFlags ForceRelation(int force, int targetForce, int allyMask) {
        if (force == targetForce) {
            return RelationFlags.Self;
        }
        return ((1 << targetForce) & allyMask) != 0 ? RelationFlags.Ally : RelationFlags.Enemy;
    }

    public static bool CanBeTargetedAt(Unit unit, Unit target, RelationFlags targetRelation) {
        int force = unit.GetIntProperty(PropertyType.BattleForce);
        int allyMask = unit.GetIntProperty(PropertyType.BattleForceAllyMask);
        int targetForce = target.GetIntProperty(PropertyType.BattleForce);
        return (ForceRelation(force, targetForce, allyMask) & targetRelation) != 0;
    }

    public static int CalcLevel(int exp, int[] expTable, out float per) {
        int maxLevel = expTable.Length;
        int level = 1;
        float baseExp = 0;
        per = 1.00f;
        foreach (var levelExp in expTable) {
            if (exp < levelExp) {
                per = (exp - baseExp) / (levelExp - baseExp);
                break;
            }
            ++level;
            baseExp = levelExp;
        }
        if (level > maxLevel) {
            level = maxLevel;
        }
        return level;
    }

    public static Action Bind<T>(T func, params object[] args) => delegate () {
        (func as Delegate)?.DynamicInvoke(args);
    };
}
