using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {
    public PlayerStatesPanel _playerStatesPanel;
    public NewHeroPanel _newHeroPanel;
    

    void Start() {
        _newHeroPanel.gameObject.SetActive(false);

        _playerStatesPanel.gameObject.SetActive(true);
        _playerStatesPanel.LoadStates();
    }

    public void StartGame(bool newGame) {
        var state = PlayerStateManager.currentState;
        state.UpdatePlayTime(true);
        if (newGame) {
            NewGame();
        } else {
            ContinueGame();
        }
    }

    void NewGame() {
        _newHeroPanel.gameObject.SetActive(true);
        _newHeroPanel.GenProperties();
    }

    public void ContinueGame() {

    }
}
