using UnityEngine;
using UnityEngine.UI;

public class TextList : MonoBehaviour {
    public GameObject _content;
    public GameObject _itemTemplate;

    void Awake() {
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
    }

    public void UpdateLayout() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content.GetComponent<RectTransform>());
    }
}
