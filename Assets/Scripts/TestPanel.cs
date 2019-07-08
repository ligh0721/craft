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
        Dictionary<int, int> d = new Dictionary<int, int>();
        d[5] = 1;
        Debug.LogFormat("{0}", d[5]);
    }
}
