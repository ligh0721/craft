using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerManager {
    static GameObject _currentLayer;

    static LayerManager() {
    }

    public static void LoadLayer(GameObject layer) {
        layer.SetActive(true);
        if (layer != _currentLayer) {
            _currentLayer?.SetActive(false);
            _currentLayer = layer;
        }
    }

    public static void LoadLayer(MonoBehaviour layer) => LoadLayer(layer.gameObject);
}
