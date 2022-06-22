using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPoint : MonoBehaviour
{
    private string tips="引擎初始化";
    private int pointNumber = 1;
    private float timer = 0.0f;
    private float lastUpdateTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTips();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log("timer " + timer + " lastUpdateTime " + lastUpdateTime);
        if(timer - lastUpdateTime > 0.3)
        {
            UpdateTips();
        }
    }

    void UpdateTips()
    {
        pointNumber++;
        if(pointNumber > 5)
        {
            pointNumber = 1;
        }
        string s = "";
        for (int i = 0; i < pointNumber; i++)
        {
            s += ".";
        }
        Text text = this.gameObject.GetComponent<Text>();
        text.text = tips + s;
        lastUpdateTime = timer;
    }

    public void SetText(string msg)
    {
        tips = msg;
    }
}
