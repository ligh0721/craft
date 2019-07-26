using Player;
using UnityEngine;
using UnityEngine.UI;

namespace ToolbarItem {
    public abstract class ToolbarItemBase : MonoBehaviour {
        public Text _title;

        public virtual void OnClick() {
            throw new System.NotImplementedException();
        }
    }

    public class Option : ToolbarItemBase {
        OptionPanel _panel;

        void Start() {
            _title.text = "选项";
        }

        public override void OnClick() {
            if (_panel == null) {
                _panel = transform.root.GetComponent<PrefabCollection>().Instantiate<OptionPanel>("OptionPanel");
            }
            transform.root.Find("PopupPanelLayer").GetComponent<PopupPanelLayer>().PopupPanel(_panel);
        }
    }

    public class Status : ToolbarItemBase {
        ItemDetailPanel _panel;

        void Start() {
            _title.text = "状态";
        }

        public override void OnClick() {
            if (_panel == null) {
                _panel = transform.root.GetComponent<PrefabCollection>().Instantiate<ItemDetailPanel>("ItemDetailPanel");
            }
            _panel.SetTitle("状态信息");
            _panel.SetPropertiesOrder(PropertyType.Level, PropertyType.Vitality, PropertyType.Strength, PropertyType.Intelligence, PropertyType.Agility, PropertyType.Health, PropertyType.PhysicAttack, PropertyType.MagicAttack, PropertyType.PhysicDefense, PropertyType.MagicDefense, PropertyType.CriticalRate, PropertyType.CriticalDamage, PropertyType.Speed);
            _panel.SetPropertyCollection(StateManager.currentState.HeroProperties);
            transform.root.Find("ContentPanelLayer").GetComponent<ContentPanelLayer>().ShowPanel(_panel);
        }
    }

    public class Items : ToolbarItemBase {
        void Start() {
            _title.text = "物品";
        }

        public override void OnClick() {
        }
    }

    public class Shop : ToolbarItemBase {
        void Start() {
            _title.text = "商店";
        }

        public override void OnClick() {
        }
    }

    public class Travel : ToolbarItemBase {
        TravelPanel _panel;

        void Start() {
            _title.text = "旅行";
        }

        public override void OnClick() {
            if (_panel == null) {
                _panel = transform.root.GetComponent<PrefabCollection>().Instantiate<TravelPanel>("TravelPanel");
            }
            _panel.AddCitiesForPlayer();
            transform.root.Find("ContentPanelLayer").GetComponent<ContentPanelLayer>().ShowPanel(_panel);
        }
    }
}
