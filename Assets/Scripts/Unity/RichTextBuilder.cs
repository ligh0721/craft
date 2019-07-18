using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RichTextBuilder {
    protected Stack<string> _endLabels;
    protected StringBuilder _strBuilder;

    public RichTextBuilder() {
        _endLabels = new Stack<string>();
        _strBuilder = new StringBuilder();
    }

    public RichTextBuilder T(string format, params object[] args) {
        _strBuilder.AppendFormat(format, args);
        return this;
    }

    public RichTextBuilder C(Color color) {
        _strBuilder.AppendFormat("<color=#{0}>", ColorUtility.ToHtmlStringRGB(color));
        _endLabels.Push("</color>");
        return this;
    }

    public RichTextBuilder C(string htmlRGB) {
        _strBuilder.AppendFormat("<color=#{0}>", htmlRGB);
        _endLabels.Push("</color>");
        return this;
    }

    public RichTextBuilder CE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</color>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public RichTextBuilder B() {
        _strBuilder.Append("<b>");
        _endLabels.Push("</b>");
        return this;
    }

    public RichTextBuilder BE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</b>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public RichTextBuilder I() {
        _strBuilder.Append("<i>");
        _endLabels.Push("</i>");
        return this;
    }

    public RichTextBuilder IE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</i>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public RichTextBuilder S(int size) {
        _strBuilder.AppendFormat("<size={0}>", size);
        _endLabels.Push("</size>");
        return this;
    }

    public RichTextBuilder SE() {
        string endLabel = _endLabels.Pop();
        Debug.Assert(endLabel == "</size>");
        _strBuilder.Append(endLabel);
        return this;
    }

    public RichTextBuilder E() {
        while (_endLabels.Count > 0) {
            _strBuilder.Append(_endLabels.Pop());
        }
        return this;
    }

    public string Print() {
        E();
        string ret = _strBuilder.ToString();
        _strBuilder.Clear();
        return ret;
    }
}
