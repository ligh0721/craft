using UnityEngine;

public class HealthBarColorHelper {
    protected float _s;
    protected float _v;

    public HealthBarColorHelper(float s, float v) => SetBaseColor(s, v);

    public HealthBarColorHelper(byte s, byte v) => SetBaseColor(s, v);

    public HealthBarColorHelper(Color @base) => SetBaseColor(@base);

    public Color GetColor(float value) => Color.HSVToRGB(value / 3.0f, _s, _v);

    public Color GetColor(float value, float a) {
        var color = Color.HSVToRGB(value / 3.0f, _s, _v);
        color.a = a;
        return color;
    }

    public void SetBaseColor(float s, float v) {
        _s = s;
        _v = v;
    }

    public void SetBaseColor(byte s, byte v) {
        _s = s / 255.0f;
        _v = v / 255.0f;
    }

    public void SetBaseColor(Color @base) => Color.RGBToHSV(@base, out _, out _s, out _v);
}
