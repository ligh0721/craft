using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour {
    public Text _text;
    protected Stack<string> _endLabels;
    protected StringBuilder _strBuilder;

    protected ScrollText() {
        _endLabels = new Stack<string>();
        _strBuilder = new StringBuilder();
    }

    void Start() {
    }

    public ScrollText T(string format, params object[] args) {
        _strBuilder.AppendFormat(format, args);
        return this;
    }

    public ScrollText C(Color color) {
        _strBuilder.AppendFormat("<color=#{0}>", ColorUtility.ToHtmlStringRGB(color));
        _endLabels.Push("</color>");
        return this;
    }

    public ScrollText C(string htmlRGB) {
        _strBuilder.AppendFormat("<color=#{0}>", htmlRGB);
        _endLabels.Push("</color>");
        return this;
    }

    public ScrollText CE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</color>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public ScrollText B() {
        _strBuilder.Append("<b>");
        _endLabels.Push("</b>");
        return this;
    }

    public ScrollText BE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</b>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public ScrollText I() {
        _strBuilder.Append("<i>");
        _endLabels.Push("</i>");
        return this;
    }

    public ScrollText IE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</i>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public ScrollText S(int size) {
        _strBuilder.AppendFormat("<size={0}>", size);
        _endLabels.Push("</size>");
        return this;
    }

    public ScrollText SE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</size>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public ScrollText E() {
        while (_endLabels.Count > 0) {
            _strBuilder.Append(_endLabels.Pop());
        }
        return this;
    }

    public void Print() {
        E();
        _text.text += _strBuilder.ToString();
        _strBuilder.Clear();
    }

    public void PrintLn() {
        _strBuilder.Append("\n");
        Print();
    }
}
