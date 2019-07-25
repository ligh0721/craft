using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public enum Where {
    None,
    Home,
    City,
    Road,
    Mine,
}

[Serializable]
public class HeroState {
    public int vitality;
    public int strength;
    public int intelligence;
    public int agility;

    public HeroState() {
        vitality = 0;
        strength = 0;
        intelligence = 0;
        agility = 0;
    }
}

[Serializable]
public class PlayerState {
    public DateTime playTime;
    [NonSerialized]
    DateTime _saveTime;

    public int exp;
    public DateTime gameDate;

    public HeroState hero;
    [NonSerialized]
    Unit _hero;

    public Where where;

    protected PlayerState() {
        playTime = new DateTime();
        exp = 0;
        gameDate = new DateTime();
        hero = new HeroState();
        where = Where.Home;
    }

    public static PlayerState New() {
        var state = new PlayerState();
        return state;
    }

    public PropertyCollection HeroProperties {
        get {
            if (_hero == null) {
                _hero = new Unit("约瑟夫");
            }
            var level = Utils.CalcLevel(exp, GlobalData.expTable, out _);
            _hero.GetProperty<IntProperty>(PropertyType.Level).Value = level;
            _hero.GetProperty<ValueProperty>(PropertyType.Vitality).Base = hero.vitality;
            _hero.GetProperty<ValueProperty>(PropertyType.Strength).Base = hero.strength;
            _hero.GetProperty<ValueProperty>(PropertyType.Intelligence).Base = hero.intelligence;
            _hero.GetProperty<ValueProperty>(PropertyType.Agility).Base = hero.agility;
            _hero.UpdateBattleProperties();
            return _hero.Properties;
        }
    }

    public void GenTestData() {
        playTime += TimeSpan.FromMinutes(565);
        exp = 13336;
        gameDate += TimeSpan.FromHours(24.454);
        UpdatePlayTime(true);
    }

    /// <summary>
    /// 更新玩家的游戏时间。游戏开始后或游戏中存档前调用
    /// </summary>
    /// <param name="init">游戏开始时init应设为true</param>
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

public static class PlayerStateManager {
    public const int MAX_SLOTS = 3;

    public static PlayerState currentState { get; private set; }
    static int _currentSlot;

    static PlayerStateManager() {
    }

    public static string GetFilePath(int slot) => $"{Application.persistentDataPath}/State{slot:D2}.sav";

    public static PlayerState LoadState(int slot) {
        var path = GetFilePath(slot);
        if (File.Exists(path) == false) {
            return currentState = null;
        }

        var st = new FileStream(path, FileMode.Open);
        var fmt = new BinaryFormatter();
        var state = fmt.Deserialize(st) as PlayerState;
        st.Close();
        return state;
    }

    public static void SaveState(PlayerState state, int slot) {
        var path = GetFilePath(slot);
        Debug.Log($"Save {path}");

        var st = new FileStream(path, FileMode.OpenOrCreate);
        var fmt = new BinaryFormatter();
        state.UpdatePlayTime();
        Debug.Log(state.playTime);
        fmt.Serialize(st, state);
        st.Close();
    }

    public static void SaveState() => SaveState(currentState, _currentSlot);

    public static PlayerState NewState() => PlayerState.New();

    public static void ChooseState(PlayerState state, int slot) {
        currentState = state;
        _currentSlot = slot;
        state.UpdatePlayTime(true);
    }

    public static void GenTestSaves() {
        var state = PlayerState.New();
        state.GenTestData();
        SaveState(state, 0);
        SaveState(state, 1);
    }
}
