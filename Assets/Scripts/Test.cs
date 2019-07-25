using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class Test : MonoBehaviour {
    public PopupPanelLayer _panelLayler;
    public TextList _out;
    public UnitForce _player;
    public UnitForce _enemies;
    public ItemDetailPanel _detailPanel;


    Unit _unit;
    Unit _unit2;
    Unit _unit3;
    ActiveSkill _act;
    BattleField _battle;
    RichTextBuilder _rich;

    // Start is called before the first frame update
    void Start() {
        _rich = new RichTextBuilder();

        _out.AddItem(_rich.B().C("61fa68").T("t5word").C("5df3f5").T("@").C("61fa68").T("tmac").C("6771fe").T(" ~/proj/craft").Print());
        _out.AddItem(_rich.B().C("e5b0ff").T("胖子").E().T("进行了").C("6771fe").T("魔法").E().T("攻击").Print());

        _battle = new BattleField();
        _battle.ForceAlly(1);
        _battle.ForceAlly(2, 3);
        _battle.ForceAlly(3, 2);

        _unit = new Unit("Player");
        _unit.UpdateBattleProperties();
        var hp = _unit.GetProperty<MaxValueProperty>(PropertyType.Health);
        var cri = _unit.GetProperty<ValueProperty>(PropertyType.CriticalRate);
        var cridmg = _unit.GetProperty<ValueProperty>(PropertyType.CriticalDamage);
        hp.Base = 100;
        hp.Current = 65;
        cri.Base = 0.50f;
        cridmg.Base = 2.00f;
        _act = _unit.AddSkill(new AttackAct("attack", 0, TargetType.One, new AttackFactors(magicFactorA: 0)));

        _unit2 = CreateTestUnit("史莱姆皇帝");
        _unit3 = CreateTestUnit("白金之星");

        _battle.AddUnit(_unit, 1, 1);

        _battle.AddUnit(_unit2, 2, 1);
        _battle.AddUnit(CreateTestUnit("金属史莱姆"), 2, 1);
        _battle.AddUnit(CreateTestUnit("岩石史莱姆"), 2, 1);
        _battle.AddUnit(CreateTestUnit("火焰史莱姆"), 2, 1);

        _battle.AddUnit(CreateTestUnit("野狼首领"), 2, 2);
        _battle.AddUnit(CreateTestUnit("野狼"), 2, 2);
        _battle.AddUnit(CreateTestUnit("野狼"), 2, 2);

        _battle.AddUnit(_unit3, 3, 2);

        ShowUnit(_unit);
        ShowUnit(_unit2);
        ShowUnit(_unit3);

        var playerForce = _battle.AllTarget.GetForce(1);
        var enemiesForce = _battle.AllTarget.GetForce(2);
        _player.SetForce(playerForce, OnUnitClick);
        _enemies.SetForce(enemiesForce, OnUnitClick);
    }

    void OnUnitClick(Unit unit) {
        var detailPanel = Instantiate(_detailPanel);
        detailPanel.SetTitle(unit.Name);
        detailPanel.SetPropertyCollection(unit.Properties);
        _panelLayler.OpenPanel(detailPanel.gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    Unit CreateTestUnit(string unitName) {
        Unit unit2 = new Unit(unitName);
        unit2.UpdateBattleProperties();
        var hp2 = unit2.GetProperty<MaxValueProperty>(PropertyType.Health);
        var cri2 = unit2.GetProperty<ValueProperty>(PropertyType.CriticalRate);
        var cridmg2 = unit2.GetProperty<ValueProperty>(PropertyType.CriticalDamage);
        var def2 = unit2.GetProperty<ValueProperty>(PropertyType.PhysicDefense);
        hp2.Base = 1000;
        cri2.Base = 0.50f;
        cridmg2.Base = 2.00f;
        def2.Base = 100;
        return unit2;
    }

    void ShowUnit(Unit unit) {
        var hp = unit.GetProperty<MaxValueProperty>(PropertyType.Health);
        _out.AddItem(_rich.T(
            "NAME: {0}, HP: {1:N0}/{2:N0}, ATK: {3:N0}, MAG: {4:N0}, CRIT: {5:P0}, CDMG: {6:P0}, DEF: {7:N0}, MDEF: {8:N0}",
            unit.Name,
            hp.Current,
            hp.Value,
            unit.GetFloatProperty(PropertyType.PhysicAttack),
            unit.GetFloatProperty(PropertyType.MagicAttack),
            unit.GetFloatProperty(PropertyType.CriticalRate),
            unit.GetFloatProperty(PropertyType.CriticalDamage),
            unit.GetFloatProperty(PropertyType.PhysicDefense),
            unit.GetFloatProperty(PropertyType.MagicDefense)).Print());
    }

    public void OnBtnTest() {
        _act.Cast(new OneTarget(_unit2));
        _act.Cast(new OneTarget(_unit3));
        ShowUnit(_unit2);
        ShowUnit(_unit3);

        PlayerStateManager.GenTestSaves();
    }

    public void OnBtnTest1() {
        var s = PlayerStateManager.LoadState(0);
        var dt = s.playTime - DateTime.MinValue;
        _out.AddItem($"{(int)dt.TotalHours:D2}:{dt.Minutes:D2}");

        dt = s.gameDate - DateTime.MinValue;
        _out.AddItem($"{(int)dt.TotalDays + 1} Days");
    }
}
