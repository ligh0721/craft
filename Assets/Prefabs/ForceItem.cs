public class ForceItem : PropertyItem {
    protected ForceTarget _force;

    void Start() {
    }

    public void SetForce(ForceTarget force) {
        _force = force;
        UpdateForce();
    }

    public void UpdateForce() {
        Clear();
        SetTitle($"战斗势力{_force.Force}", _force.UnitCount.ToString());
        for (var it = _force.GetGroupEnumerator(); it.MoveNext();) {
            var item = NewDropListItem();
            GroupItem groupItem = item.GetComponent<GroupItem>();
            groupItem.SetUpdateLayoutCallback(UpdateLayout);
            groupItem.SetGroup(it.Current);
            AddDropListItem(item);
        }
        UpdateLayout();
    }
}
