using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour {
    public ScrollText _text;
    Unit _unit;
    Unit _unit2;
    ActiveSkill _act;

    // Start is called before the first frame update
    void Start() {
        RelationFlags te = RelationFlags.Ally | RelationFlags.Self;
        Debug.LogFormat("{0}", te);

        Color c = new Color(1.0f, 1.0f, 1.0f);
        Debug.Log(c.ToString());
        Color32 c2 = new Color32(255, 128, 125, 255);
        Debug.Log(ColorUtility.ToHtmlStringRGB(c2));

        _text.B().C("61fa68").T("t5word").C("5df3f5").T("@").C("61fa68").T("tmac").C("6771fe").T(" ~/proj/craft").PrintLn();
        _text.B().C("e5b0ff").T("胖子").E().T("进行了").C("6771fe").T("魔法").E().T("攻击").PrintLn();

        _unit = new Unit("Player");
        _unit.UpdateBattleProperties();
        var hp = _unit.GetProperty<MaxValueProperty>(PropertyType.Health);
        var cri = _unit.GetProperty<ValueProperty>(PropertyType.CriticalRate);
        var cridmg = _unit.GetProperty<ValueProperty>(PropertyType.CriticalDamage);
        var force = _unit.GetProperty<IntProperty>(PropertyType.BattleForce);
        var forceMask = _unit.GetProperty<IntProperty>(PropertyType.BattleForceAllyMask);
        hp.Base = 100;
        cri.Base = 0.50f;
        cridmg.Base = 2.00f;
        force.Value = 1;
        forceMask.Value = Utils.AllyMask(force.Value);
        _act = _unit.AddSkill(new AttackAct("attack", 0, TargetType.One, new AttackFactors(magicFactorA: 0)));

        _unit2 = new Unit("Enemy");
        _unit2.UpdateBattleProperties();
        var hp2 = _unit2.GetProperty<MaxValueProperty>(PropertyType.Health);
        var cri2 = _unit2.GetProperty<ValueProperty>(PropertyType.CriticalRate);
        var cridmg2 = _unit2.GetProperty<ValueProperty>(PropertyType.CriticalDamage);
        var force2 = _unit2.GetProperty<IntProperty>(PropertyType.BattleForce);
        var forceMask2 = _unit2.GetProperty<IntProperty>(PropertyType.BattleForceAllyMask);
        hp2.Base = 1000;
        cri2.Base = 0.50f;
        cridmg2.Base = 2.00f;
        force2.Value = 2;
        forceMask2.Value = Utils.AllyMask(force2.Value);

        ShowUnit(_unit);
        ShowUnit(_unit2);

        Utils.CanBeTargetedAt(_unit, _unit2, RelationFlags.Enemy);

    }

    // Update is called once per frame
    void Update() {

    }

    void ShowUnit(Unit unit) {
        var hp = unit.GetProperty<MaxValueProperty>(PropertyType.Health);
        _text.T(
            "NAME: {0}, HP: {1:N0}/{2:N0}, ATK: {3:N0}, MAG: {4:N0}, CRIT: {5:P0}, CDMG: {6:P0}, DEF: {7:N0}, MDEF: {8:N0}",
            unit.Name,
            hp.Current,
            hp.Value,
            unit.GetFloatProperty(PropertyType.PhysicAttack),
            unit.GetFloatProperty(PropertyType.MagicAttack),
            unit.GetFloatProperty(PropertyType.CriticalRate),
            unit.GetFloatProperty(PropertyType.CriticalDamage),
            unit.GetFloatProperty(PropertyType.PhysicDefense),
            unit.GetFloatProperty(PropertyType.MagicDefense)).PrintLn();
    }

    public void OnBtnTest(Text txt) {
        txt.text = "OK";
        _act.Cast(new OneTarget(_unit2));
        ShowUnit(_unit2);
    }
}
