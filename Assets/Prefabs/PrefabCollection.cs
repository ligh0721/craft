using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct PrefabItem {
    public string key;
    public GameObject prefab;
}

public class PrefabCollection : MonoBehaviour {
    public PrefabItem[] _prefabs;

    Dictionary<string, GameObject> _prefabMap;

    void Awake() {
        _prefabMap = new Dictionary<string, GameObject>();
        for (int i = 0; i < _prefabs.Length; ++i) {
            _prefabMap.Add(_prefabs[i].key, _prefabs[i].prefab);
        }
    }

    public GameObject Instantiate(string key) {
        if (_prefabMap.TryGetValue(key, out var prefab) == false) {
            return null;
        }
        return Instantiate(prefab);
    }

    public GameObject Instantiate(string key, RectTransform parent) {
        if (_prefabMap.TryGetValue(key, out var prefab) == false) {
            return null;
        }
        return Instantiate(prefab, parent, false);
    }

    public T Instantiate<T>(string key) where T : Component {
        var ret = Instantiate(key);
        if (ret == null) {
            return null;
        }
        return ret.GetComponent<T>();
    }

    public T Instantiate<T>(string key, RectTransform parent) where T : Component {
        var ret = Instantiate(key, parent);
        if (ret == null) {
            return null;
        }
        return ret.GetComponent<T>();
    }
}
