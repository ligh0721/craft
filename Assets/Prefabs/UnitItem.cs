using UnityEngine.UI;

public class UnitItem : PropertyItem {
    void Start() {
        SetTitle("测试单位", "Lv.1");
        AddUnitItem("HP", "100/100");
        AddUnitItem("ATK", "120");
        ////AddProperty("DEF", "20");
        ////AddProperty("ATK", "120");
        ////AddProperty("DEF", "20");
        ////AddProperty("ATK", "120");
        ////AddProperty("DEF", "20");
        //UpdateLayout();
    }

    public void AddUnitItem(string name, string value) {
        var item = NewDropListItem();
		var txtName = item.transform.Find("Name").GetComponent<Text>();
        var txtValue = item.transform.Find("Value").GetComponent<Text>();
        txtName.text = name;
        txtValue.text = value;

        AddDropListItem(item);
    }
}
