using System;

[Flags]
public enum TargetEffective {
    None = 0,
    Self = 1 << 0,
    Ally = 1 << 1,
    Enemy = 1 << 2,
}

public enum TargetRange {
    None,
    One,
    Group,
    Force,
    All,
}