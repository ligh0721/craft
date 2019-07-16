using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour {
    public ScrollText _text;
    Unit u;

    // Start is called before the first frame update
    void Start() {
        Relation te = Relation.Ally | Relation.Self;
        Debug.LogFormat("{0}", te);

        Color c = new Color(1.0f, 1.0f, 1.0f);
        Debug.Log(c.ToString());
        Color32 c2 = new Color32(255, 128, 125, 255);
        Debug.Log(ColorUtility.ToHtmlStringRGB(c2));

        _text.B().C("61fa68").T("t5word").C("5df3f5").T("@").C("61fa68").T("tmac").C("6771fe").T(" ~/proj/craft").PrintLn();
        _text.B().C("e5b0ff").T("胖子").E().T("进行了").C("6771fe").T("魔法").E().T("攻击").PrintLn();

        u = new Unit("test");
        u.UpdateBattleProperties();
        var hp = u.GetPropertyObject<MaxValueProperty>(PropertyType.Health);
        var cri = u.GetPropertyObject<ValueProperty>(PropertyType.CriticalRate);
        var cridmg = u.GetPropertyObject<ValueProperty>(PropertyType.CriticalDamage);
        cri.Base = 0.50f;
        cridmg.Base = 2333.333333f;

        _text.T("HP: {0}/{1}", hp.Current, hp.Value).PrintLn();
        hp.Current = 50;
        _text.T("HP: {0}/{1}", hp.Current, hp.Value).PrintLn();
        hp.Overflow = true;
        hp.Current = 150;
        _text.T("HP: {0}/{1}", hp.Current, hp.Value).PrintLn();
        hp.Overflow = false;
        _text.T("HP: {0}/{1}", hp.Current, hp.Value).PrintLn();
        hp.Base = 1500;
        hp.Current = 250;
        _text.T("HP: {0:N0}/{1}", hp.Current, hp.Value).PrintLn();
        hp.Base = 100;
        _text.T("HP: {0:N0}/{1}", hp.Current, hp.Value).PrintLn();

        _text.T("ATK: {0}, MAG: {1}, CRI: {2}", u.GetFloatProperty(PropertyType.PhysicAttack), u.GetFloatProperty(PropertyType.MagicAttack), u.GetFloatProperty(PropertyType.CriticalRate) * 100).PrintLn();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnBtnTest(Text txt) {
        txt.text = "OK";
        var ad = u.Attack(null, magicFactorA: 0, criticalRateFactor: 0.25f);
        _text.T("{0:N0}+{1:N0}", ad.Physical, ad.Magic).PrintLn();
    }
}
