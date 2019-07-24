using UnityEngine;
using UnityEngine.UI;

public class UnitForce : MonoBehaviour {
    public Transform _itemTemplate;

    protected ForceTarget _force;

    void Awake() {
        _itemTemplate.gameObject.SetActive(false);
    }

    void Start() {
    }

    public void SetForce(ForceTarget force, OnUnitClickCallback onUnitClickCallback = null) {
        _force = force;
        for (var it = _force.GetGroupEnumerator(); it.MoveNext();) {
            var group = it.Current;
            var item = Instantiate(_itemTemplate);
            var itemGroup = item.GetComponent<UnitGroup>();
            itemGroup.SetGroup(group, onUnitClickCallback);
            item.SetParent(_itemTemplate.parent, false);
            item.gameObject.SetActive(true);
        }
    }
}
