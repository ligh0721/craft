using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitOne : MonoBehaviour {
    public Text _titleName;
    public Text _titleValue;
    public Slider _slider;

    protected Unit _one;

    void Start() {

    }

    public void SetOne(Unit one) {
        _one = one;
        _titleName.text = _one.Name;
        _titleValue.text = $"Lv.{_one.GetIntProperty(PropertyType.Level)}";

        var hp = _one.GetProperty<MaxValueProperty>(PropertyType.Health);
        var per = Math.Min(1, Math.Max(0, hp.Current / hp.Value));
        _slider.value = per;
    }
}
