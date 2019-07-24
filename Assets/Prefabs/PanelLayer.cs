using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLayer : MonoBehaviour {
    public GameObject _panelRect;

    protected GameObject _panel;

    protected CanvasGroup _canvasGroup;

    protected Animation _maskFadeIn;

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
        _maskFadeIn = GetComponent<Animation>();
    }

    void Start() {

    }

    public void OnClickLayer() {
        ClosePanel();
    }

    public void OpenPanel(GameObject panel) {
        _panel = panel;
        _panel.transform.SetParent(_panelRect.transform, false);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1;
        _maskFadeIn.Play();
    }

    public void ClosePanel() {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
        _maskFadeIn.Stop();
        Destroy(_panel);
    }
}
