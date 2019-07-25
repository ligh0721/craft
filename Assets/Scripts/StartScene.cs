using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {
    public PlayerStatesLayer _playerStatesLayer;
    public NewHeroLayer _newHeroLayer;
    
    void Start() {
        _newHeroLayer.gameObject.SetActive(false);

        LayerManager.LoadLayer(_playerStatesLayer);
        _playerStatesLayer.LoadStates();
    }

    public void StartGame(bool newGame) {
        if (newGame) {
            NewGame();
        } else {
            ContinueGame();
        }
    }

    void NewGame() {
        LayerManager.LoadLayer(_newHeroLayer);
        _newHeroLayer.GenProperties();
    }

    public void ContinueGame() {
        SceneManager.LoadScene("Main");
    }
}
