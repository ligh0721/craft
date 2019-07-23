using UnityEngine;
using UnityEngine.UI;

public class UnitGroup : MonoBehaviour {
    public Transform _itemTemplate;

    protected GroupTarget _group;

    void Awake() {
        _itemTemplate.gameObject.SetActive(false);
    }

    void Start() {
    }

    public void SetGroup(GroupTarget group) {
        _group = group;
        for (var it = _group.GetUnitEnumerator(); it.MoveNext();) {
            var unit = it.Current;
            var item = Instantiate(_itemTemplate);
            var prop = item.Find("PropertyItem");
            prop.Find("Name").GetComponent<Text>().text = unit.Name;
            prop.Find("Value").GetComponent<Text>().text = $"Lv.{unit.GetIntProperty(PropertyType.Level)}";
            item.SetParent(_itemTemplate.parent, false);
            item.gameObject.SetActive(true);
        }
    }
}
