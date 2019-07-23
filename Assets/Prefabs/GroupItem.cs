public class GroupItem : PropertyItem {
    protected GroupTarget _group;

    void Start() {
    }

    public void SetGroup(GroupTarget group) {
        _group = group;
        UpdateGroup();
    }

    public void UpdateGroup() {
        Clear();
        SetTitle($"战斗组{_group.Group}", _group.UnitCount.ToString());
        for (var it = _group.GetUnitEnumerator(UnitFilter.Alive); it.MoveNext();) {
            var item = NewDropListItem();
            UnitItem unitItem = item.GetComponent<UnitItem>();
            unitItem.SetUpdateLayoutCallback(UpdateLayout);
            unitItem.SetUnit(it.Current);
            AddDropListItem(item);
        }
        UpdateLayout();
    }
}
