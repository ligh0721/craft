public class Property
{
    protected string _name;

    public string Name
    {
        get
        {
            return _name;
        }
    }

    protected float _x;

    public float X
    {
        get
        {
            return _x;
        }

        set
        {
            _x = value;
            _y = _a * _x + _b;
        }
    }

    protected float _a;

    public float A
    {
        get
        {
            return _a;
        }

        set
        {
            _a = value;
            _y = _a * _x + _b;
        }
    }

    protected float _b;

    public float B
    {
        get
        {
            return _b;
        }

        set
        {
            _b = value;
            _y = _a * _x + _b;
        }
    }

    protected float _y;

    public float Y
    {
        get
        {
            return _y;
        }
    }

    public Property(string name, float x, float a = 1.0f, float b = 0.0f)
    {
        _name = name;
        _x = x;
        _a = a;
        _b = b;
        _y = a * x + b;
    }
}
