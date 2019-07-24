using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnUnitClickCallback(Unit unit);

public class UnitGroup : MonoBehaviour {
    public Transform _itemTemplate;

    protected GroupTarget _group;

    protected OnUnitClickCallback _onUnitClickCallback;

    protected Dictionary<Transform, Unit> _itemUnitMap;

    UnitGroup() {
        _itemUnitMap = new Dictionary<Transform, Unit>();
    }

    void Awake() {
        _itemTemplate.gameObject.SetActive(false);
    }

    void Start() {
    }

    public void OnUnitClick() {
        var unit = _itemUnitMap[EventSystem.current.currentSelectedGameObject.transform];
        _onUnitClickCallback?.Invoke(unit);
    }

    public void SetGroup(GroupTarget group, OnUnitClickCallback onUnitClickCallback = null) {
        _group = group;
        _onUnitClickCallback = onUnitClickCallback;
        for (var it = _group.GetUnitEnumerator(); it.MoveNext();) {
            var unit = it.Current;
            var item = Instantiate(_itemTemplate);
            var itemOne = item.GetComponent<UnitOne>();
            itemOne.SetOne(unit);
            item.SetParent(_itemTemplate.parent, false);
            item.gameObject.SetActive(true);
            _itemUnitMap.Add(item, unit);
        }
    }
}
