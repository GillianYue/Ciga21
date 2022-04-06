#if FALSE //delete


#if !UNITY_STANDALONE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MopubNS;

public class MopubManager : MonoBehaviour
{
    [Inject(InjectFrom.Anywhere)]
    public enabler enable;

    public Text t1, t2;


    void Start()
    {

        StartCoroutine(initMopub());
    }


    void Update()
    {
        
    }

    public void startInitMopub() { StartCoroutine(initMopub()); }

    public IEnumerator initMopub()
    {
        yield return new WaitForSecondsRealtime(2);

        Debug.Log("mopub init called; in game");

        MopubSdk.getInstance().init(delegate (InitSuccessResult result) {
                //init success
                Debug.Log("init success!!");
                if(t1.enabled) t1.text = "init success!!";
        },
            delegate (MopubSDKError result) {
                //init fail
                Debug.Log("init fail!!");
                if (t1.enabled) t1.text = "init fail!!";
            }
        );

    }

    public void realnameAuth()
    {
        StartCoroutine(realnameAuthCoroutine());
    }

    public IEnumerator realnameAuthCoroutine()
    {
        yield return new WaitForSecondsRealtime(0);

        MopubSdk.getInstance().callRealNameUI(
                false,
                delegate (string s)
                {
                    Debug.Log("realName success!!" + s);
                    StartCoroutine(enable.checkLoadLevel());
                    if (t2.enabled) t2.text = "realName success!!" + s;
                },
                delegate (string failed)
                {
                    Debug.Log("realName failed!!" + failed);
                    if (t2.enabled) t2.text = "realName failed!!" + failed;
                },
                delegate (MopubSDKError error)
                {
                    Debug.Log("realName failed error!!" + error);
                    if (t2.enabled) t2.text = "realName failed error!!" + error;
                }
            );
    }



}


#endif



#endif