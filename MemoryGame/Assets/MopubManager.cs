using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MopubNS;

public class MopubManager : MonoBehaviour
{
    [Inject(InjectFrom.Anywhere)]
    public enabler enable;

    void Start()
    {

        /*        MopubSdk.getInstance().showLoginUI(
                      delegate (LoginSuccessResult s) {
                          Debug.Log("login success");
        *//*                    logs.Add("login success");
                          logs.Add(JsonUtility.ToJson(s));*//*
                      },
                       delegate (MopubSDKError error) {
                           Debug.Log("showLoginUI failed");
        *//*                   logs.Add("showLoginUI failed");
                           logs.Add(JsonUtility.ToJson(error));*//*
                       },
                       null
                   );*/


    }


    void Update()
    {
        
    }

    public void initMopub()
    {

        Debug.Log("mopub init called; in game");

                MopubSdk.getInstance().init(delegate (InitSuccessResult result) {
                //init success
                Debug.Log("init success");

                MopubSdk.getInstance().callRealNameUI(
                    false,//true为联网游戏，false为单机游戏
                    delegate (string s){
                                //认证成功后，cp在这做认证成功的回调逻辑
                                Debug.Log("callRealNameUI success");
                        enable.enableStartCanvasInteraction(); //entry 
                            },
                    delegate (string failed)
                            {
                                        //sdk内部做好错误提示，cp不需要在这里实现回调逻辑
                                        Debug.Log("callRealNameUI failed");

                            },
                    delegate (MopubSDKError error)
                            {
                                        //未成年人防成谜到时间，在此回调方法退出游戏
                                        Debug.Log(JsonUtility.ToJson(error));
                                Application.Quit();
                            }
                   );
            },
            delegate (MopubSDKError result) {
                //init fail
                Debug.Log("init fail");
            }
        );
    }
}
