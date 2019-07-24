using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerialDictBase<KEY, VALUE, DICT> : ISerializationCallbackReceiver where DICT : IDictionary<KEY, VALUE>, new() {
    protected DICT _target;

    [SerializeField]
    protected List<KEY> _keys;
    [SerializeField]
    protected List<VALUE> _values;

    public SerialDictBase(DICT target) {
        _target = target;
    }

    public void OnAfterDeserialize() {
        var count = Math.Min(_keys.Count, _values.Count);
        _target = new DICT();
        for (int i = 0; i < count; ++i) {
            _target.Add(_keys[i], _values[i]);
        }
    }

    public void OnBeforeSerialize() {
        _keys = new List<KEY>(_target.Keys);
        _values = new List<VALUE>(_target.Values);
    }

    public DICT ToDictionary() => _target;
}

[Serializable]
public class SerialDict<KEY, VALUE> : SerialDictBase<KEY, VALUE, Dictionary<KEY, VALUE>> {
    public SerialDict(Dictionary<KEY, VALUE> target) : base(target) {
    }

    public static implicit operator SerialDict<KEY, VALUE>(Dictionary<KEY, VALUE> target) => new SerialDict<KEY, VALUE>(target);
}

[Serializable]
public class SerialSortedDict<KEY, VALUE> : SerialDictBase<KEY, VALUE, SortedDictionary<KEY, VALUE>> {
    public SerialSortedDict(SortedDictionary<KEY, VALUE> target) : base(target) {
    }

    public static implicit operator SerialSortedDict<KEY, VALUE>(SortedDictionary<KEY, VALUE> target) => new SerialSortedDict<KEY, VALUE>(target);
}

[Serializable]
public class SerialSetBase<VALUE, SET> : ISerializationCallbackReceiver where SET : ISet<VALUE>, new() {
    protected SET _target;

    [SerializeField]
    protected List<VALUE> values;

    public SerialSetBase(SET target) {
        _target = target;
    }

    public void OnAfterDeserialize() {
        var count = values.Count;
        _target = new SET();
        for (int i = 0; i < count; ++i) {
            _target.Add(values[i]);
        }
    }

    public void OnBeforeSerialize() {
        values = new List<VALUE>(_target);
    }

    public SET ToSet() => _target;
}


[Serializable]
public class SerialHashSet<VALUE> : SerialSetBase<VALUE, HashSet<VALUE>> {
    public SerialHashSet(HashSet<VALUE> target)
        : base(target) {
    }

    public static implicit operator SerialHashSet<VALUE>(HashSet<VALUE> target) => new SerialHashSet<VALUE>(target);
}

[Serializable]
public class SerialSortedSet<VALUE> : SerialSetBase<VALUE, SortedSet<VALUE>> {
    public SerialSortedSet(SortedSet<VALUE> target)
        : base(target) {
    }

    public static implicit operator SerialSortedSet<VALUE>(SortedSet<VALUE> target) => new SerialSortedSet<VALUE>(target);
}
