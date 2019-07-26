using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelPanel : MonoBehaviour {
    public RectTransform _itemTemplate;

    public static readonly Color LowLevel = new Color32(0x00, 0x7F, 0x00, 0xff);
    public static readonly Color MidLevel = new Color32(0xde, 0xa8, 0x00, 0xff);
    public static readonly Color HighLevel = new Color32(0xFF, 0x00, 0x00, 0xff);

    RichTextBuilder _rt;

    TravelPanel() {
        _rt = new RichTextBuilder();
    }

    void Awake() {
        _itemTemplate.gameObject.SetActive(false);
    }

    void Start() {
    }

    public void AddCity(CityTable.CityData city, Color color, bool locked) {
        var item = Instantiate(_itemTemplate, _itemTemplate.parent, false);

        item.Find("Button/Text").GetComponent<Text>().text = _rt.T($"{city.name}  ").C(color).T($"Lv.{city.level}").Print();
        item.Find("Button").GetComponent<Button>().interactable = !locked;

        item.gameObject.SetActive(true);
    }

    public void Clear() {
        foreach (RectTransform city in _itemTemplate.parent) {
            if (city != _itemTemplate) {
                Destroy(city.gameObject);
            }
        }
    }

    public void AddCitiesForPlayer() {
        Clear();
        var level = Utils.CalcLevel(Player.StateManager.currentState.exp, GlobalData.ExpTable, out _);
        for (var it = GlobalData.CityData.GetEnumerator(); it.MoveNext();) {
            var city = it.Current.Value;
            Color color;
            bool locked = false;
            if (level >= city.level) {
                color = LowLevel;
            } else if (level + 2 >= city.level) {
                color = MidLevel;
            } else if (level + 5 >= city.level) {
                color = HighLevel;
            } else {
                color = HighLevel;
                locked = true;
            }
            AddCity(city, color, locked);
        }
    }
}
