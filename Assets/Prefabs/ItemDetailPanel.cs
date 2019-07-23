using UnityEngine;
using UnityEngine.UI;

public class ItemDetailPanel : MonoBehaviour {
    public Text _title;
    public RectTransform _propertyItemTemplate;

    public PropertyType[] _propsOrder;

    void Awake() {
        _propertyItemTemplate.gameObject.SetActive(false);
    }

    public void SetTitle(string title) => _title.text = title;

    public void SetPropertiesOrder(params PropertyType[] order) => _propsOrder = order;

    public void SetPropertyCollection(PropertyCollection props) {
        foreach (var type in _propsOrder) {
            var prop = props.GetProperty(type);
            if (prop == null) {
                continue;
            }
            var item = Instantiate(_propertyItemTemplate);
            var txtName = item.transform.Find("Name").GetComponent<Text>();
            var txtValue = item.transform.Find("Value").GetComponent<Text>();
            txtName.text = prop.Name;
            txtValue.text = prop.ToString();
            item.SetParent(_propertyItemTemplate.parent, false);
            item.gameObject.SetActive(true);
        }
    }
}
