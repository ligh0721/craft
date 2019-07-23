using System;
using UnityEngine;
using UnityEngine.UI;

public delegate void UpdateLayoutCallback();

public delegate void OnButtonCallback();

public class PropertyItem : MonoBehaviour {
    public Text _titleName;
    public Text _titleValue;
    public RectTransform _detailDropList;
    public RectTransform _detailDropListContent;
    public GameObject _dropListItemTemplate;
    public float _detailDropListItemHeight;
    protected float _detailDropListMaxHeight;

    protected UpdateLayoutCallback _updateLayoutCallback;

    protected OnButtonCallback _onButtonCallback;

    void Awake() {
        _detailDropList.gameObject.SetActive(false);
        _dropListItemTemplate.SetActive(false);
        _detailDropListMaxHeight = _detailDropList.rect.height;
    }

    protected GameObject NewDropListItem() => Instantiate(_dropListItemTemplate);

	protected void AddDropListItem(GameObject item) {
		item.transform.SetParent(_detailDropListContent, false);
		item.SetActive(true);
	}

    public void UpdateLayout() {
        //_detailDropList.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Math.Min(_detailDropListMaxHeight, (_detailDropListContent.childCount - 1) * _detailDropListItemHeight));
        float height = 0;
        foreach (RectTransform child in _detailDropListContent.transform) {
            if (child.gameObject != _dropListItemTemplate) {
                height += Math.Max(_detailDropListItemHeight, child.rect.height);
            }
        }
        _detailDropList.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Math.Min(_detailDropListMaxHeight, height));
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(_detailDropListContent);
        _updateLayoutCallback?.Invoke();
    }

    public void SetUpdateLayoutCallback(UpdateLayoutCallback callback) => _updateLayoutCallback = callback;

    public void SetTitle(string name, string value) {
        _titleName.text = name;
        _titleValue.text = value;
    }

    public void OnButton() {
        _onButtonCallback?.Invoke();
    }

    public void SetOnButtonCallback(OnButtonCallback callback) => _onButtonCallback = callback;

    public void OnShowDetail() {
        _detailDropList.gameObject.SetActive(!_detailDropList.gameObject.activeSelf);
        UpdateLayout();
    }

    public void Clear() {
        foreach (RectTransform child in _detailDropListContent.transform) {
            if (child.gameObject != _dropListItemTemplate) {
                Destroy(child.gameObject);
            }
        }
    }
}
