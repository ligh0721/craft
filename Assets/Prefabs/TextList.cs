using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextList : MonoBehaviour {
    public GameObject _content;
    public GameObject _itemTemplate;
    public int _maxLines = 100;

    protected LinkedList<GameObject> _items;

    void Awake() {
        _items = new LinkedList<GameObject>();
        _itemTemplate.SetActive(false);
    }

    void Start() {
    }

    public void AddItem(string text, bool updateLayout = true) {
        var item = Instantiate(_itemTemplate);
        item.transform.SetParent(_content.transform, false);
        item.GetComponent<Text>().text = text;
        item.SetActive(true);
        if (updateLayout == true) {
            UpdateLayout();
        }
        _items.AddLast(item);
        while (_items.Count > _maxLines) {
            Destroy(_items.First.Value);
            _items.RemoveFirst();
        }
    }

    public void UpdateLayout() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content.GetComponent<RectTransform>());
    }
}
