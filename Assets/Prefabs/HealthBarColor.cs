using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarColor : MonoBehaviour {
    public Color _baseColor = Color.green;
    public Graphic _graphic;

    HealthBarColorHelper _healthBarColorHelper;
    float _lastValue;

    HealthBarColorHelper helper {
        get {
            if (_healthBarColorHelper == null) {
                _healthBarColorHelper = new HealthBarColorHelper(_baseColor);
            }
            return _healthBarColorHelper;
        }
    }

    void Awake() {
    }

    private void OnValidate() {
        helper.SetBaseColor(_baseColor);
        UpdateColor(true);
    }

    void UpdateColor(bool updateAlpha = false) {
        if (_graphic != null) {
            if (updateAlpha) {
                _graphic.color = helper.GetColor(_lastValue, _baseColor.a);
            } else {
                var a = _graphic.color.a;
                var color = helper.GetColor(_lastValue);
                color.a = a;
                _graphic.color = color;
            }
        }
    }

    public void OnValueChanged(float value) {
        _lastValue = value;
        UpdateColor();
    }
}
