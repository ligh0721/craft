using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionPanel : MonoBehaviour {
    public void OnQuitClick() {
        StateManager.SaveState();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnDebugClick() {
        SceneManager.LoadScene("Test");
    }
}
