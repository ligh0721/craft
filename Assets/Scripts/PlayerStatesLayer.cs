using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerStatesLayer : MonoBehaviour {
    public Text[] _slotTitles;
    public EventHandler t;

    PlayerState[] _stateCaches;
    float _h;
    float _s;
    float _v;

    void Awake() {
        _stateCaches = new PlayerState[_slotTitles.Length];
        Color.RGBToHSV(GetComponent<Image>().color, out _h, out _s, out _v);
        UpdateColor();
    }

    void Update() {
        UpdateColor();
    }

    void UpdateColor() {
        const float factor = 20.0f;
        float h = (_h + (Time.time % factor) / factor) % 1.0f;
        GetComponent<Image>().color = Color.HSVToRGB(h, _s, _v);
    }

    public void LoadStates() {
        for (int i = 0; i < _slotTitles.Length; ++i) {
            var state = PlayerStateManager.LoadState(i);
            _stateCaches[i] = state;
            var slotTitle = _slotTitles[i];
            if (state == null) {
                slotTitle.fontSize = 35;
                slotTitle.text = "新的故事";
            } else {
                slotTitle.fontSize = 25;
                var dt = state.gameDate - DateTime.MinValue;
                var dt2 = state.playTime - DateTime.MinValue;
                var level = Utils.CalcLevel(state.exp, GlobalData.expTable, out _);
                slotTitle.text = $"Lv.{level} 第{(int)dt.TotalDays+1}天\n{(int)dt2.TotalHours:D2}:{dt2.Minutes:D2}";
            }
        }
    }

    public void OnSlotClick(int slot) {
        var newGame = false;
        var state = _stateCaches[slot];
        if (state == null) {
            newGame = true;
            state = PlayerStateManager.NewState();
            _stateCaches[slot] = state;
        }
        PlayerStateManager.ChooseState(state, slot);
        transform.root.GetComponent<StartScene>().StartGame(newGame);
    }
}
