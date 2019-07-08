using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnTest(Text txt)
    {
        txt.text = "OK";
        TargetEffective te = TargetEffective.Ally | TargetEffective.Self;
        Debug.LogFormat("{0}", te);
    }
}
