public class GroupItem : PropertyItem {
    void Start() {
        SetTitle("测试组", "2");
        AddGroupItem("单位1", "Lv.1");
        AddGroupItem("单位2", "Lv.2");
        AddGroupItem("单位3", "Lv.3");
        //AddProperty("DEF", "20");
        //AddProperty("ATK", "120");
        //AddProperty("DEF", "20");
        //AddProperty("ATK", "120");
        //AddProperty("DEF", "20");
        UpdateLayout();
    }

    public void AddGroupItem(string name, string value) {
        var item = NewDropListItem();
        UnitItem unitItem = item.GetComponent<UnitItem>();
        unitItem.SetTitle(name, value);
        unitItem.SetUpdateLayoutCallback(UpdateLayout);
        unitItem.AddUnitItem("HP", "100/100");
        unitItem.AddUnitItem("ATK", "120");
        unitItem.AddUnitItem("HP", "100/100");
        unitItem.AddUnitItem("ATK", "120");
        unitItem.AddUnitItem("HP", "100/100");
        unitItem.AddUnitItem("ATK", "120");

        AddDropListItem(item);
    }
}
