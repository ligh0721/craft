using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour {
    public ScrollText _text;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnBtnTest(Text txt) {
        txt.text = "OK";
        TargetEffective te = TargetEffective.Ally | TargetEffective.Self;
        Debug.LogFormat("{0}", te);

        Color c = new Color(1.0f, 1.0f, 1.0f);
        Debug.Log(c.ToString());
        Color32 c2 = new Color32(255, 128, 125, 255);
        Debug.Log(ColorUtility.ToHtmlStringRGB(c2));

        _text.B().C("61fa68").T("t5word").C("5df3f5").T("@").C("61fa68").T("tmac").C("6771fe").T(" ~/proj/craft\n").Print();
        _text.B().C("e5b0ff").T("胖子").E().T("进行了").C("6771fe").T("魔法").E().T("攻击").Print();
    }
}
