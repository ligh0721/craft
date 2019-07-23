using UnityEngine.UI;

public class UnitItem : PropertyItem {
    public PropertyType[] _order;

    protected Unit _unit;

    void Start() {
    }

    public void SetPropertyOrder(params PropertyType[] order) => _order = order;

    public void SetUnit(Unit unit) {
        _unit = unit;
        UpdateUnit();
    }

    public void UpdateUnit() {
        Clear();
        SetTitle(_unit.Name, $"Lv.{_unit.GetProperty(PropertyType.Level).ToString()}");
        foreach (var type in _order) {
            var prop = _unit.GetProperty(type);
            if (prop == null) {
                continue;
            }
            var item = NewDropListItem();
            var txtName = item.transform.Find("Name").GetComponent<Text>();
            var txtValue = item.transform.Find("Value").GetComponent<Text>();
            txtName.text = prop.Name;
            txtValue.text = prop.ToString();
            AddDropListItem(item);
        }
        UpdateLayout();
    }
}
