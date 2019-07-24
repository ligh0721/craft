using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;


[Serializable]
public class SubState {
    [SerializeField]
    string name;

    [SerializeField]
    List<string> testList;

    [SerializeField]
    Dictionary<int, int> testDict;

    [SerializeField]
    HashSet<int> testSet;

    [SerializeField]
    HashSet<int> testSet2;

    public SubState() {
        testList = new List<string>();
        testDict = new Dictionary<int, int>();
        testSet = new HashSet<int>();
    }

    public void GenTestData() {
        name = "test";
        testList.Add("AAA");
        testList.Add("BBB");
        testDict.Add(1, 500);
        testDict.Add(2, 100);
        testSet.Add(11);
        testSet.Add(22);
    }

    public int CheckLoad() {
        Debug.Log(testDict.ToString());
        return testDict[1];
    }
}

[Serializable]
public class PlayerState {
    public DateTime playTime;
    protected DateTime _saveTime;

    public int level;
    public DateTime gameDate;

    protected PlayerState() {
        playTime = new DateTime();
        level = 1;
        gameDate = new DateTime();
    }

    public static PlayerState NewState() {
        var state = new PlayerState();
        return state;
    }

    public void GenTestData() {
        playTime += TimeSpan.FromMinutes(564654);
        level = 16;
        gameDate += TimeSpan.FromHours(534345.454);
        UpdatePlayTime(true);
    }

    public void UpdatePlayTime(bool init = false) {
        if (init) {
            _saveTime = DateTime.Now;
        } else {
            var now = DateTime.Now;
            playTime += now - _saveTime;
            _saveTime = now;
        }
    }
}

public static class PlayerSaveManager {
    public const int MAX_SLOTS = 3;

    public static PlayerState currentState { get; private set; }

    static PlayerSaveManager() {
    }

    public static string GetFilePath(int slot) => $"{Application.persistentDataPath}/State{slot:D2}.sav";

    public static PlayerState Load(int slot) {
        var path = GetFilePath(slot);
        Debug.Log($"Load {path}");
        if (File.Exists(path) == false) {
            return null;
        }

        var st = new FileStream(path, FileMode.Open);
        var fmt = new BinaryFormatter();
        var state = fmt.Deserialize(st) as PlayerState;
        st.Close();
        currentState = state;
        return state;
    }

    public static void Save(PlayerState state, int slot) {
        var path = GetFilePath(slot);
        Debug.Log($"Save {path}");

        var st = new FileStream(path, FileMode.OpenOrCreate);
        var fmt = new BinaryFormatter();
        state.UpdatePlayTime();
        fmt.Serialize(st, state);
        st.Close();
    }

    public static void GenTestSaves() {
        var state = PlayerState.NewState();
        state.GenTestData();
        Save(state, 0);
        Save(state, 1);
        Save(state, 3);
    }
}
