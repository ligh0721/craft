using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {
    public PlayerStatesLayer _playerStatesLayer;
    public NewHeroLayer _newHeroLayer;
    
    void Start() {
        _newHeroLayer.gameObject.SetActive(false);

        _playerStatesLayer.gameObject.SetActive(true);
        _playerStatesLayer.LoadStates();
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
        _newHeroLayer.gameObject.SetActive(true);
        _newHeroLayer.GenProperties();
    }

    public void ContinueGame() {

    }
}
