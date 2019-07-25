using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderEx : MonoBehaviour {
    public Text _text;
    public string _format;

    void OnValidate() {
        OnValueChanged(GetComponent<Slider>().value);
    }

    public void OnValueChanged(float value) {
        _text.text = value.ToString(_format);
    }
}
