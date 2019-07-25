using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPanelLayer : MonoBehaviour {
    GameObject _panel;

    public void ShowPanel(GameObject panel) {
        panel.transform.SetParent(transform, false);
        panel.SetActive(true);
        if (panel != _panel) {
            _panel?.SetActive(false);
            _panel = panel;
        }
    }

    public void ShowPanel(MonoBehaviour panel) => ShowPanel(panel.gameObject);
}
