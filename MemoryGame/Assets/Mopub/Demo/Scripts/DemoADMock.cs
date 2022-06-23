using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MopubNS;

public class DemoADMock : MonoBehaviour
{

    public Text fullScreenText;
    public Button btnClose;
    public GameObject fullScreen;
    public GameObject banner;
    public Image native;

    private string entry;

    private Action<string, MopubAdInfo> click;

    public void ShowReward(string entry, Action<string, MopubAdInfo> close, Action<string, MopubAdInfo> click)
    {
        this.fullScreenText.text = "This is a mock reward AD";
        this.entry = entry;
        this.click = click;

        this.Show(close);
    }

    public void ShowInterstitial(string entry,  Action<string, MopubAdInfo> close, Action<string, MopubAdInfo> click)
    {
        this.fullScreenText.text = "This is a mock interstitial AD";
        this.entry = entry;
        this.click = click;

        this.Show(close);
    }

    private void Show(Action<string, MopubAdInfo> close)
    {
        //this.gameObject.SetActive(true);
        this.fullScreen.SetActive(true);

        this.btnClose.onClick.AddListener(() =>
        {
            //this.gameObject.SetActive(false);
            this.fullScreen.SetActive(false);
            if (close != null)
            {
                MopubAdInfo adInfo = new MopubAdInfo("test", "test", "test");
                close(this.entry, adInfo);
            }
            this.entry = null;
            this.click = null;
            this.btnClose.onClick.RemoveAllListeners();
        });
    }

   public void BackgroundCkick()
    {
        if(this.click != null)
        {
            MopubAdInfo adInfo = new MopubAdInfo("test", "test", "test");
            this.click(this.entry, adInfo);
        }
    }

    public void showBanner()
    {
        this.banner.SetActive(true);
    }

    public void closeBanner()
    {
        this.banner.SetActive(false);
    }


    private string nativeEntry;
    private Action<string> onNativeClickEvent;

    public void ShowNative(string entry, bool bottom, float offset, Action<string> close, Action<string> onNativeClick)
    {
        this.nativeEntry = entry;
        this.native.gameObject.SetActive(true);
        this.onNativeClickEvent = onNativeClick;

        Vector3 pos = this.native.rectTransform.position;
        if (bottom)
        {
            pos.y = 600 + offset;
        }
        else
        {
            pos.y = 1334 - offset;
        }
        this.native.rectTransform.position = pos;

        Button btnCloseNative = this.native.GetComponentInChildren<Button>();
        btnCloseNative.onClick.AddListener(() =>
        {
            
            this.native.gameObject.SetActive(false);
            if(close != null)
            {
                close(this.nativeEntry);
            }
            this.nativeEntry = null;
            btnCloseNative.onClick.RemoveAllListeners();
            this.onNativeClickEvent = null;
        });
    }

    public void closeNative()
    {
        Button btnCloseNative = this.native.GetComponentInChildren<Button>();
        this.nativeEntry = null;
        btnCloseNative.onClick.RemoveAllListeners();
        this.native.gameObject.SetActive(false);
        this.onNativeClickEvent = null;
    }

    public void onNativeClick()
    {
        if(this.onNativeClickEvent != null)
        {
            this.onNativeClickEvent(this.nativeEntry);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
