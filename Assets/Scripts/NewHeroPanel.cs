using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHeroPanel : MonoBehaviour {
    public RectTransform _itemTemplate;

    Dictionary<PropertyType, Text> _propValueTextMap;
    Dictionary<PropertyType, int> _propValueMap;

    NewHeroPanel() {
        _propValueTextMap = new Dictionary<PropertyType, Text>();
        _propValueMap = new Dictionary<PropertyType, int>();
    }

    void Awake() {
        _itemTemplate.gameObject.SetActive(false);
    }

    void Start() {
        AddItem(PropertyType.Vitality, "体力");
        AddItem(PropertyType.Strength, "力量");
        AddItem(PropertyType.Intelligence, "智力");
        AddItem(PropertyType.Agility, "敏捷");
        LayoutRebuilder.MarkLayoutForRebuild(_itemTemplate.parent as RectTransform);
    }

    RectTransform AddItem(PropertyType type, string name) {
        var item = Instantiate(_itemTemplate);
        item.SetParent(_itemTemplate.parent);
        item.gameObject.SetActive(true);
        var propName = item.Find("Name").GetComponent<Text>();
        propName.text = name;
        var propValue = item.Find("Value").GetComponent<Text>();
        _propValueTextMap.Add(type, propValue);
        _propValueMap.Add(type, 0);
        return item;
    }

    public void GenProperties() {
        int v = Utils.RandomInt(5, 11);
        _propValueTextMap[PropertyType.Vitality].text = $"{v}";
        _propValueMap[PropertyType.Vitality] = v;

        v = Utils.RandomInt(5, 11);
        _propValueTextMap[PropertyType.Strength].text = $"{v}";
        _propValueMap[PropertyType.Strength] = v;

        v = Utils.RandomInt(5, 11);
        _propValueTextMap[PropertyType.Intelligence].text = $"{v}";
        _propValueMap[PropertyType.Intelligence] = v;

        v = Utils.RandomInt(5, 11);
        _propValueTextMap[PropertyType.Agility].text = $"{v}";
        _propValueMap[PropertyType.Agility] = v;
    }

    public void OnStartClick() {
        var state = PlayerStateManager.currentState;

        state.exp = 0;
        state.hero.vitality = _propValueMap[PropertyType.Vitality];
        state.hero.strength = _propValueMap[PropertyType.Strength];
        state.hero.intelligence = _propValueMap[PropertyType.Intelligence];
        state.hero.agility = _propValueMap[PropertyType.Agility];

        gameObject.SetActive(false);
        transform.root.GetComponent<StartScene>().ContinueGame();
    }

    public void OnRegenClick() => GenProperties();
}
