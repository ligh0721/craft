using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


[Serializable]
public class SubState {
    [SerializeField]
    string name;
    [SerializeField]
    List<string> testList;
    [SerializeField]
    SerialDictBase<int, int, Dictionary<int, int>> testDict;
    [SerializeField]
    SerialHashSet<int> testSet;

    public SubState() {
        testList = new List<string>();
        testDict = new SerialDictBase<int, int, Dictionary<int, int>>(new Dictionary<int, int>());
        testSet = new HashSet<int>();
    }

    public void GenTestData() {
        name = "test";
        testList.Add("AAA");
        testList.Add("BBB");
        testDict.ToDictionary().Add(1, 500);
        testDict.ToDictionary().Add(2, 100);
        testSet.ToSet().Add(11);
        testSet.ToSet().Add(22);
    }
}

[Serializable]
public class PlayerState {

    //protected static PlayerState _current;

    //public static PlayerState Current {
    //    get {
    //        if (_current == null) {
    //            _current = new PlayerState();
    //        }
    //        return _current;
    //    }
    //}

    [SerializeField]
    SubState subState;

    public PlayerState() {
        subState = new SubState();
    }

    public void GenTestData() {
        subState.GenTestData();
    }
}

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
    public SerialDict(Dictionary<KEY, VALUE> target)
        : base(target) {
    }

    public static implicit operator SerialDict<KEY, VALUE>(Dictionary<KEY, VALUE> target) => new SerialDict<KEY, VALUE>(target);
}

[Serializable]
public class SerialSortedDict<KEY, VALUE> : SerialDictBase<KEY, VALUE, SortedDictionary<KEY, VALUE>> {
    public SerialSortedDict(SortedDictionary<KEY, VALUE> target)
        : base(target) {
    }

    public static implicit operator SerialSortedDict<KEY, VALUE>(SortedDictionary<KEY, VALUE> target) => new SerialSortedDict<KEY, VALUE>(target);
}

[Serializable]
public abstract class SerialSetBase<VALUE, SET> : ISerializationCallbackReceiver where SET : ISet<VALUE>, new() {
    protected SET _target;

    [SerializeField]
    protected List<VALUE> _values;

    public SerialSetBase(SET target) {
        _target = target;
    }

    public void OnAfterDeserialize() {
        var count = _values.Count;
        _target = new SET();
        for (int i = 0; i < count; ++i) {
            _target.Add(_values[i]);
        }
    }

    public void OnBeforeSerialize() {
        _values = new List<VALUE>(_target);
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

public static class PlayerSaveManager {
    public const int MAX_SLOTS = 3;

    static PlayerSaveManager() {
    }

    public static string GetFilePath(int slot) => $"{Application.persistentDataPath}/State{slot:D2}.sav";

    public static PlayerState Load(int slot) {
        var saveFile = GetFilePath(slot);
        Debug.Log($"Load {saveFile}");
        if (File.Exists(saveFile) == false) {
            return null;
        }
        var data = File.ReadAllText(saveFile, Encoding.UTF8);
        var state = JsonUtility.FromJson<PlayerState>(data);
        return state;
    }

    public static void Save(PlayerState state, int slot) {
        var saveFile = GetFilePath(slot);
        Debug.Log($"Save {saveFile}");
        var data = JsonUtility.ToJson(state);
        File.WriteAllText(saveFile, data);
    }
}
