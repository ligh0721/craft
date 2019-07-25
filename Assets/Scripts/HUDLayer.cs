using UnityEngine;
using UnityEngine.UI;

public class HUDLayer : MonoBehaviour {
    public Text _title;
    public RectTransform _toolbarItemTemplate;
    public Text _level;
    public Slider _expBar;

    void Awake() {
        _toolbarItemTemplate.gameObject.SetActive(false);
    }

    void Start() {
        UpdateHUD();
        AddToolbarItem<ToolbarItem.Status>();
        AddToolbarItem<ToolbarItem.Travel>();
        AddToolbarItem<ToolbarItem.Option>();
    }

    public void SetTitle(string title) {
        _title.text = title;
    }

    public void UpdateHUD() {
        var level = Utils.CalcLevel(PlayerStateManager.currentState.exp, GlobalData.expTable, out var per);
        _level.text = $"Lv.{level.ToString()}";
        _expBar.value = per;
    }

    public void AddToolbarItem<T>() where T : ToolbarItem.ToolbarItemBase {
        var item = Instantiate(_toolbarItemTemplate, _toolbarItemTemplate.parent, false);
        item.gameObject.SetActive(true);

        var script = item.gameObject.AddComponent<T>();
        script._title = item.GetComponentInChildren<Text>();
        item.GetComponent<Button>().onClick.AddListener(script.OnClick);
    }
}
