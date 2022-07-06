using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MopubNS;

public class EntryManager : MonoBehaviour
{
    //public Animator headphoneScreen;

    //public GameObject realdNameObject;
    //public GameObject loadingObject;
    public AddPoint addPointText;
    private AsyncOperation asyncLoad;

    //public GameObject[] activateUponSceneLoad;
    //public GameObject prevEventSystem;

    private void Awake()
    {

    }

    void Start()
    {
        //StartCoroutine(headphoneScreenEntry());
        StartSDK();

    }

    

    void StartSDK()
    {
        //realdNameObject.SetActive(false);
        //loadingObject.SetActive(true);
        //tipsObject.SetActive(false);
        PopMsg("SDK登录");
        MopubSdk.getInstance().init(
            delegate (InitSuccessResult result) {
           //     Debug.Log("mopub sdk init success");
                //init success
                //
                MopubSdk.getInstance().loginWithDevice(
                    delegate (LoginSuccessResult result) {
                        //login success
                    //    PopMsg("登录成功");
                    //    Debug.Log("login success");
                        //debug
                        //PopRealName();
                        //打开实名
                        //MopubSdk.getInstance().openRealnamePrevention();

                        PopRealName();
                        //MopubSdk.getInstance().queryRealnameInfo(
                        //      delegate (MopubSDKRealnameInfo res) {
                        //          // 获取成功
                        //          MopubSDKRealnameStatus status = res.status;   // 0: 还没申请实名认证
                        //                                                        // 1: 正在审核中
                        //                                                        // 2: 认证成功/认证过
                        //                                                        // 3: 认证失败
                        //          string birthday = res.birthday;     // yyyy-MM-dd
                        //                                              // 除了认证成功外，其他状态为 0000-00-00

                        //          Debug.Log("查询实名状态 " + status);
                        //          dealRealNameStatus(status);
                        //      },
                        //      delegate (MopubSDKError error) {
                        //          // 获取失败
                        //          //弹出实名窗口
                        //          PopMsg("实名信息查询失败，请退出应用重试", 0);
                        //      }
                        //    );
                    },
                    delegate (MopubSDKError result) {
                        //login fail
                    //    Debug.Log("login fail");
                        PopMsg("登录失败，网络异常，请退出应用重试");
                    }
                );
            },
            delegate (MopubSDKError result) {
                //init fail
                // Debug.Log("mopub sdk init fail");
                PopMsg("初始化失败，网络异常，请退出应用重试");
            }
        );
    }
    void PopRealName()
    {
        PopMsg("实名认证中");
        //loadingObject.SetActive(false);
        //realdNameObject.SetActive(true);
        MopubSdk.getInstance().showRealNameView(
               delegate (string s) {
                   //认证成功，且在游戏时间内
           //        Debug.Log("认证成功");
                   StartGame();
               },
                delegate (string failed) {
                    //认证成功，不在游戏时间内
            //        Debug.Log("认证失败");
                }
           );
    }
    //public void SendRealName()
    //{
    //    loadingObject.SetActive(true);
    //    //InputField nameInput = realdNameObject.transform.Find("NameInput").GetComponent<InputField>();
    //    //InputField idInput = realdNameObject.transform.Find("IdInput").GetComponent<InputField>();
    //    string nameText = nameInput.text;
    //    string idText = idInput.text;
    //    if(nameText!= null && nameText.Length > 0 && idText != null && idText.Length > 0)
    //    {
    //        Debug.Log("准备验证 "+nameText + " " + idText);
    //        MopubSdk.getInstance().realnameAuthentication(nameText, idText,
    //            delegate (MopubSDKRealnameInfo res)
    //        {
    //            // 成功回调只能说明向后端发起实名认证成功
    //            // 实名认证的结果可能为认证成功，正在审核，认证失败
    //            // 具体请判断 res.status 的值
    //            // 在实名认证时返回的status可能会为 4
    //            // 表示当天已经认证失败3次，请不要再进行验证
    //            MopubSDKRealnameStatus status = res.status;
    //            Debug.Log("验证实名状态 " + status);
    //            dealRealNameStatus(status);
    //        },
    //          delegate (MopubSDKError error)
    //          {
    //              // 发起认证失败
    //              PopMsg("实名认证失败，请稍后再重试", 2);
    //          });
    //    }
    //    else
    //    {
    //        PopMsg("输入不能为空", 2);
    //    }
    //}
    //void dealRealNameStatus(MopubSDKRealnameStatus status)
    //{
    //    if (status == MopubSDKRealnameStatus.MopubSDKRealnameStatusNotAuthentication ||
    //                                status == MopubSDKRealnameStatus.MopubSDKRealnameStatusVerifying ||
    //                                status == MopubSDKRealnameStatus.MopubSDKRealnameStatusVerifyFailed)
    //    {
    //        PopRealName();
    //    }
    //    else if (status == MopubSDKRealnameStatus.MopubSDKRealnameStatusVerified)
    //    {
    //        StartGame();
    //    }
    //    else if (status == MopubSDKRealnameStatus.MopubSDKRealnameStatusVerifyFailedThreeTimes)
    //    {
    //        PopMsg("实名验证失败次数过多，请明天再尝试", 0);
    //    }
    //}
    void PopMsg(string msg)
    {
       //  addPointText.SetText(msg);
        
    }
    //IEnumerator MissTipsAction()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    tipsObject.SetActive(false);
    //}

    //public void MissTips()
    //{
    //    if (missExit)
    //    {
    //        Application.Quit();
    //    }
    //    else
    //    {
    //        tipsObject.SetActive(false);
    //    }
    //}


    void StartGame()
    {
        PopMsg("开始加载游戏");
        //loadingObject.SetActive(true);
        StartCoroutine(headphoneScreenEntry());
    }

    IEnumerator headphoneScreenEntry()
    {
        yield return new WaitForSeconds(0);

        

        /*        headphoneScreen.gameObject.SetActive(true);
                headphoneScreen.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                headphoneScreen.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                headphoneScreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);*/

        //prevEventSystem.SetActive(false);

        asyncLoad = SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);

        //yield return new WaitForSeconds(2);

        //headphoneScreen.SetTrigger("fadeIn");

        //yield return new WaitForSeconds(1);
        PopMsg("加载中");
        yield return new WaitUntil(()=> asyncLoad.isDone );
        PopMsg("加载成功，进入游戏");
        /*        if(activateUponSceneLoad.Length > 0)
                foreach (GameObject g in activateUponSceneLoad)
                {
                    g.SetActive(true);
                }*/

        //headphoneScreen.SetTrigger("fadeOut");

        /*        GameObject mainCamHolder = GameObject.FindGameObjectWithTag("MainCameraHolder");

                mainCamHolder.SetActive(true);
                gameObject.tag = "Untagged";

                Camera mainCam = mainCamHolder.transform.GetChild(0).GetComponent<Camera>();
                mainCam.enabled = true;

                GetComponent<Camera>().enabled = false;
                this.gameObject.SetActive(false);*/

        //Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        
    }
}
