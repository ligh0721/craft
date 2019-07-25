using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LayerManager {
    static GameObject _currentLayer;

    static LayerManager() {
        SceneManager.sceneLoaded += OnLoadScene;
    }

    static void OnLoadScene(Scene scene, LoadSceneMode mode) => _currentLayer = null;

    public static void LoadLayer(GameObject layer) {
        layer.SetActive(true);
        if (layer != _currentLayer) {
            _currentLayer?.SetActive(false);
            _currentLayer = layer;
        }
    }

    public static void LoadLayer(MonoBehaviour layer) => LoadLayer(layer.gameObject);
}
