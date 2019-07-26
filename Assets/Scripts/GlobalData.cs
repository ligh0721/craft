using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


// [(((l-1)**3+60)/5.0*((l-1)*2+60))/10 for l in range(1, 101)]
public class ExpTable {
    public string type;
    public int[] data;
}

public class CityTable {
    [Serializable]
    public class CityData : IComparable {
        public string name;
        public int level;
        public string[] unlock;

        public int CompareTo(object obj) {
            var target = obj as CityData;
            var dtLevel = (level - target.level);
            if (dtLevel != 0) {
                return dtLevel;
            }
            return string.Compare(name, target.name);
        }
    }

    public string type;
    public CityData[] data;
}


public class GlobalData : MonoBehaviour {
    public TextAsset _expTable;
    public TextAsset _cityTable;

    public const int MaxLevel = 100;
    public static int[] ExpTable { get; private set; }

    public static SortedDictionary<string, CityTable.CityData> CityData { get; private set; }

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        Load();
        SceneManager.LoadScene("Start");
    }

    void Load() {
        var expTable = JsonUtility.FromJson<ExpTable>(_expTable.text);
        ExpTable = expTable.data;

        var cityTable = JsonUtility.FromJson<CityTable>(_cityTable.text);
        CityData = new SortedDictionary<string, CityTable.CityData>();
        foreach (var city in cityTable.data) {
            CityData.Add(city.name, city);
        }
    }
}
