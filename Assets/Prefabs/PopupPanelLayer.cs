using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPanelLayer : MonoBehaviour {
    public GameObject _panelRect;

    GameObject _panel;

    CanvasGroup _canvasGroup;

    Animation _maskFadeIn;

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
        _maskFadeIn = GetComponent<Animation>();
    }

    void Start() {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
    }

    public void OnClickLayer() {
        ClosePanel();
    }

    public void PopupPanel(GameObject panel) {
        _panel = panel;
        _panel.transform.SetParent(_panelRect.transform, false);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1;
        _panel.SetActive(true);
        _maskFadeIn.Play();
    }

    public void PopupPanel(MonoBehaviour panel) => PopupPanel(panel.gameObject);

    public void ClosePanel() {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
        _maskFadeIn.Stop();
        _panel.SetActive(false);
    }
}
