using System;

[Flags]
public enum Relation {
    None = 0,
    Self = 1 << 0,
    Ally = 1 << 1,
    Enemy = 1 << 2,
}

public enum RangeType {
    None,
    One,
    Group,
    Force,
    All,
}
