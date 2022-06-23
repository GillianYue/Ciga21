using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class SEToastTool : MonoBehaviour
{
    private Text text;
    private RectTransform rectTransform;

    private Coroutine timer;

    private void Awake()
    {
        this.text = GetComponentInChildren<Text>();
        this.rectTransform = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(string msg, float time, Action callback)
    {
        this.gameObject.SetActive(true);

        if (this.timer != null)
        {
            StopCoroutine(this.timer);
        }

        text.text = msg;
        int StrLenth = msg.Length;
        int ToastWidth = 1;
        int ToastHeight = 1;
        if (StrLenth > 10)
        {
            ToastHeight = (StrLenth / 10) + 1;
            ToastWidth = 10;

        }
        else
        {
            ToastWidth = StrLenth;
        }

        this.rectTransform.sizeDelta = new Vector2(50 * ToastWidth + 100, 50 * ToastHeight + 100);

        

         this.timer = StartCoroutine(SESDKTools.ICoroutine(time, delegate
        {
            this.close();
            if(callback != null)
            {
                callback();
            }

        }));
    }

    public void close()
    {
        if(this.timer != null)
        {
            StopCoroutine(this.timer);
        }
        
        this.gameObject.SetActive(false);
    }
}
