using UnityEngine;
using System.Collections;
using MopubNS;
using System.Collections.Generic;
using System;
using MopubNS.ThirdParty.MiniJSON;

#if UNITY_EDITOR
public class SESDKEditorBackendMock : MonoBehaviour   
{
    public int failureChance = 15;

    private static string keySucceedOrder = "keySucceedOrder";
    private static string keyConsumedOrder = "keyConsumedOrder";

    private Dictionary<string, string> succeedOrder;
    private Dictionary<string, string> consumedOrder;

    private DemoADMock demoADMock;

    private Action<List<MopubSDKPurchasedItem>> unconsumedItemUpdatedListener;

    private class BackendData
    {
        List<MopubSDKPaymentItemDetails> productCatelog = new List<MopubSDKPaymentItemDetails>();
    }

    private static SESDKEditorBackendMock _instance;
    [HideInInspector]
    public static SESDKEditorBackendMock instance {
        get
        {
            if(_instance == null)
            {
                InitBackend();
            }
            return _instance;
        }
    }

    public static void InitBackend()
    {
        var type = typeof(SESDKEditorBackendMock);
        _instance = new GameObject("SESDKEditorBackendMock", type).GetComponent<SESDKEditorBackendMock>(); // Its Awake() method sets Instance.
    }

    List<MopubSDKPaymentItemDetails> itemDetails = new List<MopubSDKPaymentItemDetails>() {
            new MopubSDKPaymentItemDetails (
                "1001", "100 coins", 99, "USD",
                "$0.99",
                PaymentItemType.PaymentItemTypeConsumable
            ),
            new MopubSDKPaymentItemDetails (
                "1002", "600 coins", 599, "USD",
                "$5.99",
                PaymentItemType.PaymentItemTypeConsumable
            ),
            new MopubSDKPaymentItemDetails (
                "1003", "NO ADs", 199, "USD",
                "$1.99",
                PaymentItemType.PaymentItemTypeConsumable
            )
        };

  public void showRealNameView(Action<string> success,Action<string> failed)
    {
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success("success");

            }));
            
        }
    }
    
    public void callRealNameUI(bool netWorkable,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
    {
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success("success");

            }));
            
        }
    }
  
       public void realNameRecharge(int amount,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
    {
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success("success");

            }));
            
        }
    }
    #region login
    public void login(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {
        var result = new LoginSuccessResult(
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.DEVICE)
                }
                         )
                         )
                     );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));
            
        }
    }

    public void autoLoginTouristWithUI(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {
        var result = new LoginSuccessResult(
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.VISITOR)
                }
                         )
                         )
                     );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));

        }
    }

    public void switchAccount(Action<MopubSdkAccessToken> success)
    {
        var token = 
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.DEVICE)
                }
                         )
                         );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(token);

            }));

        }
    }


    public void logout(Action<string> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success("success");

            }));
            
        }
    }

    public void loginWithDevice(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {
        var result = new LoginSuccessResult(
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.DEVICE)
                }
                         )
                         )
                     );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));

        }
    }

    public void loginWithWeChat(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {
        var result = new LoginSuccessResult(
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.WE_CHAT)
                }
                         )
                         )
                     );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));

        }
    }

    public void fetchMobileAuthCode(string phoneNumber, Action<FetchSuccessResult> success, Action<MopubSDKError> failed)
    {
        if(success != null)
        {
            success(new FetchSuccessResult());
        }
    }

    public void loginWithMobile(string phoneNumber, string code, string activeCode,bool isOneKeyLogin, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {
        var result = new LoginSuccessResult(
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.MOBILE)
                }
                         )
                         )
                     );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));

        }
    }

    public void fetchEmailAuthCode(string email, Action<FetchSuccessResult> success, Action<MopubSDKError> failed)
    {
        if(success != null)
        {
            success(new FetchSuccessResult());
        }
    }

    public void loginWithEmail(string email, string password, string code, string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {
        var result = new LoginSuccessResult(
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.EMAIL)
                }
                         )
                         )
                     );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));

        }
    }

    public void loginWithVisitor(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {
        var result = new LoginSuccessResult(
                         new MopubSdkAccessToken(
                             "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
                             new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.VISITOR)
                }
                         )
                         )
                     );
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));

        }
    }


    public void resetPasswordWithEmail(string email, string password, string code, Action<string> success, Action<MopubSDKError> failed){
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success("success");

            }));

        }
    }

    public MopubSdkAccessToken currentAccessToken()
    {
        return new MopubSdkAccessToken(
            "accountID", "sessionToken", "lastLoginTimestamp", "currentNickName", new MopubSdkLinkedAccount(
            new List<MopubSdkThirdPartyAccount>() {
                        new MopubSdkThirdPartyAccount (AccountType.DEVICE)
            }
        ));
    }

    public void verifySessionToken(string token, Action<VerifySuccessResult> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            success(new VerifySuccessResult());
        }
	}

	public void createInviteCode(Action<string> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            success("mock code");
        }
	}

	public void fetchInviteeList(Action<List<object>> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {   
            string a = "[\"111\",\"222\"]";
            List<object> list = Json.Deserialize(a) as List<object>;
            success(list);
        }
	}

	public void uploadInviteCode(string code, Action<string> success, Action<MopubSDKError> failed)
    {
		if (success != null)
        {
            success("A");
        }
	}

    // public void openPrevention(Action<string> preventionDidTrigger){
    //     if (preventionDidTrigger != null)
    //     {
    //         StartCoroutine(SESDKTools.ICoroutine(1f, delegate
    //         {
    //             preventionDidTrigger("success");

    //         }));

    //     }
    // }

    public void fetchIdCardInfo(Action<MopubSDKIdCardInfo> success, Action<MopubSDKError> failed){
        var result = new MopubSDKIdCardInfo("1998-01-01");
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success(result);

            }));

        }
    }

    public void verifyIdCard(string realName, string cardNumber, Action<string> success, Action<MopubSDKError> failed){
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success("success");

            }));

        }
    }

    public void fetchPaidAmount(Action<string> success, Action<MopubSDKError> failed){
        if (success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(1f, delegate
            {
                success("0");

            }));

        }
    }

    public void linkWithGameCenter(Action<MopubSDKLinkWithGameCenterResult> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            success(new MopubSDKLinkWithGameCenterResult());
        }
    }

    public void linkWithFacebook(Action<MopubSDKLinkWithFacebookResult> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            success(new MopubSDKLinkWithFacebookResult());
        }
    }

    public void linkWithWeChat(Action<MopubSDKLinkWithWeChatResult> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            success(new MopubSDKLinkWithWeChatResult());
        }
    }

    public void linkWithEmail(Action<MopubSDKLinkWithEmailResult> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            success(new MopubSDKLinkWithEmailResult());
        }
    }

    public void linkWithMobile(Action<MopubSDKLinkWithMobileResult> success, Action<MopubSDKError> failed)
    {
        if (success != null)
        {
            success(new MopubSDKLinkWithMobileResult());
        }
    }
    #endregion

    #region payment
    public void fetchPaymentItemDetails(Action<List<MopubSDKPaymentItemDetails>> success, Action<MopubSDKError> failed)
    {
        if (this.happenChance(failureChance))
        {
            if(failed != null)
            {
                StartCoroutine(SESDKTools.ICoroutine(0.5f, delegate
                {
                    MopubSDKError error = new MopubSDKError("[mock]",999,"[mock]",999,"[mock]fetchPaymentItemDetailsFailed","[mock]");
                    failed(error);

                }));
            }
            return;
        }

        if(success != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(0.5f, delegate
            {
                success(itemDetails);

            }));
            
        }
    }

    public void setUnconsumedItemUpdatedListener(Action<List<MopubSDKPurchasedItem>> listener)
    {
        this.unconsumedItemUpdatedListener = listener;
        this.checkUnconsumedOrder();
    }


    public void buyItem(MopubSDKStartPaymentInfo MopubSDKStartPaymentInfo,
                                           Action<MopubSDKPaymentInfo> paymentProcessingWithPaymentInfo,
                                           Action<MopubSDKPaymentInfo, MopubSDKError> paymentFailedWithPaymentInfo)
    {
        float processTime = UnityEngine.Random.Range(5, 41) / 10.0f;
        Debug.Log("backendMock_processTime: " + processTime.ToString());

        MopubSDKPaymentItemDetails item = null;
        foreach(MopubSDKPaymentItemDetails it in itemDetails)
        {
            if(it.itemID == MopubSDKStartPaymentInfo.itemID)
            {
                item = it;
            }
        }

        if(item == null)
        {
            StartCoroutine(SESDKTools.ICoroutine(0, delegate
            {
                MopubSDKError error = new MopubSDKError("[mock]",999,"[mock]",999,"[mock]itemIDError","[mock]");
                paymentFailedWithPaymentInfo(null, error);

            }));

            return;
        }


        MopubSDKPaymentInfo info = new MopubSDKPaymentInfo(
            MopubSDKStartPaymentInfo.itemID,
            MopubSDKStartPaymentInfo.cpOrderID,
            UnityEngine.Random.Range(10000, 20000).ToString(),
            item.price,
            item.currency,
            MopubSDKStartPaymentInfo.characterName,
            MopubSDKStartPaymentInfo.characterID,
            MopubSDKStartPaymentInfo.serverName,
            MopubSDKStartPaymentInfo.serverID);

        bool failed = this.happenChance(failureChance);

        if (failed)
        {
            StartCoroutine(SESDKTools.ICoroutine(processTime, delegate
            {
                MopubSDKError error = new MopubSDKError("[mock]",999,"[mock]",999,"[mock]paymentFailed","[mock]");
                paymentFailedWithPaymentInfo(info, error);

            }));
        }
        else
        {
            
            StartCoroutine(SESDKTools.ICoroutine(processTime, delegate
            {
                this.saveOrder(info.sdkOrderID, info.itemID, keySucceedOrder, this.succeedOrder);
                paymentProcessingWithPaymentInfo(info);

            }));
        }
    }

    public void fetchUnconsumedPurchasedItems(Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed)
    {
        List<MopubSDKPurchasedItem> lsit = new List<MopubSDKPurchasedItem>();
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
        long purchasedTime = Convert.ToInt64(ts.TotalMilliseconds);
        purchasedTime -= 1000 * 10;
        foreach (var e in this.succeedOrder)
        {
            MopubSDKPurchasedItem item = new MopubSDKPurchasedItem(
                e.Value,
                e.Key,
                purchasedTime,
                0,
                "cpOrderID",
                MopubSDKItemConsumeState.MopubSDKItemConsumeStateUnconsumed);
            lsit.Add(item);
        }
        foreach (var e in this.consumedOrder)
        {
            MopubSDKPurchasedItem item = new MopubSDKPurchasedItem(
                e.Value,
                e.Key,
                purchasedTime,
                0,
                "cpOrderID",
                MopubSDKItemConsumeState.MopubSDKItemConsumeStateConsumed);
            lsit.Add(item);
        }
        StartCoroutine(SESDKTools.ICoroutine(0.5f, delegate
        {
            if (success != null)
            {
                success(lsit);
            }

        }));
    }

    public void consumePurchase(string sdkOrderID)
    {
        string itemID = this.succeedOrder[sdkOrderID];
        if(itemID == null)
        {
            Debug.Log("backendMockconsumePurchaseItemIDNull");
            return;
        }
        this.saveOrder(sdkOrderID, null, keySucceedOrder, this.succeedOrder);
        this.saveOrder(sdkOrderID, itemID, keyConsumedOrder, this.consumedOrder);
    }

    private void saveOrder(string orderID, string itemID, string saveKey, Dictionary<string, string> map)
    {
        if(itemID == null)
        {
            if (map.ContainsKey(orderID))
            {
                map.Remove(orderID);
            }
        }
        else{
            map[orderID] = itemID;
        }
        string json = Json.Serialize(map);
        if (string.IsNullOrEmpty(json))
        {
            return;
        }
        Debug.Log("backendMock_saveOrder: "+json);
        PlayerPrefs.SetString(saveKey, json);
    }

    private Dictionary<string, string> readOrder(string readKey)
    {
        string json = PlayerPrefs.GetString(readKey);
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }
        Dictionary<string, object> map = Json.Deserialize(json) as Dictionary<string, object>;
        if(map != null)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (var item in map)
            {
                res[item.Key] = item.Value.ToString();
            }
            return res;
        }
        return null;
    }

    private void checkUnconsumedOrder()
    {
        InvokeRepeating("_checkUnconsumedOrder", 0, 6);
    }
    private void _checkUnconsumedOrder()
    {
        StartCoroutine(SESDKTools.ICoroutine(0, delegate
        {
            if(this.unconsumedItemUpdatedListener != null)
            {
                List<MopubSDKPurchasedItem> lsit = new List<MopubSDKPurchasedItem>();
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
                long purchasedTime = Convert.ToInt64(ts.TotalMilliseconds);
                purchasedTime -= 1000 * 10;
                foreach (var e in this.succeedOrder)
                {
                    MopubSDKPurchasedItem item = new MopubSDKPurchasedItem(
                        e.Value,
                        e.Key,
                        purchasedTime,
                        0,
                        "cpOrderID",
                        MopubSDKItemConsumeState.MopubSDKItemConsumeStateUnconsumed);
                    lsit.Add(item);
                }
                if(lsit.Count > 0)
                {
                    this.unconsumedItemUpdatedListener.Invoke(lsit);
                }
                
            }

        }));
    }
    #endregion

    #region AD
    //smartAd

    public bool hasSmartAd(string gameEntry)
    {
        if (happenChance(failureChance))
        {
            return false;
        }
        return true;
    }

    public void showSmartAd(string gameEntry)
    {
        // if(demoADMock != null)
        // {
        //     this.demoADMock.ShowSmart(gameEntry,
        //                delegate (string entry, MopubAdInfo adInfo)
        //                {   

        //                }, delegate (string entry, MopubAdInfo adInfo)
        //                {

        //                });
            
        // }
        // else
        // {
        //     Debug.Log("demoADmock is not run and skip");
        // }
    
    }
    //Reward
    private MopubRewardedVideoListener rewardedADListener;
    public void setRewardedVideoListener(MopubRewardedVideoListener listener)
    {
        this.rewardedADListener = listener;
    }

    public bool hasRewardedVideo(string gameEntry)
    {
        if (happenChance(failureChance))
        {
            return false;
        }
        return true;
    }

    public void showRewardVideoAd(string gameEntry)
    {
        if(demoADMock != null)
        {
            this.demoADMock.ShowReward(gameEntry,
                       delegate (string entry, MopubAdInfo adInfo)
                       {
                           if (this.rewardedADListener != null)
                           {
                               this.rewardedADListener.onRewardedVideoCompleted(entry, adInfo);
                               this.rewardedADListener.onRewardedVideoClosed(entry, adInfo);
                           }

                       }, delegate (string entry, MopubAdInfo adInfo)
                       {
                           if (this.rewardedADListener != null)
                           {
                               this.rewardedADListener.onRewardedVideoClicked(entry, adInfo);
                           }

                       });
            
        }
        else
        {
            Debug.Log("demoADmock is not run and skip");
        }
        if (this.rewardedADListener != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(0, delegate
            {
                MopubAdInfo adInfo = new MopubAdInfo("test", "test", "test");
                this.rewardedADListener.onRewardedVideoStarted(gameEntry, adInfo);
            }));
        }
    }

    //Interstitial
    private MopubInterstitialAdListener interADListener;

    public void setInterstitialAdListener(MopubInterstitialAdListener listener)
    {
        interADListener = listener;
    }

    public bool hasInterstitial(string gameEntry)
    {
        if (happenChance(failureChance))
        {
            return false;
        }
        return true;
    }

    public void showInterstitialAd(string gameEntry)
    {
        if (demoADMock != null)
        {
            this.demoADMock.ShowInterstitial(gameEntry,
            delegate (string entry, MopubAdInfo adInfo)
            {
                if (this.interADListener != null)
                {
                    this.interADListener.onInterstitialDismissed(entry, adInfo);
                }

            }, delegate (string entry, MopubAdInfo adInfo)
            {
                if (this.interADListener != null)
                {
                    this.interADListener.onInterstitialClicked(entry, adInfo);
                }

            });
        }
        else
        {
            Debug.Log("demoADmock is not run and skip");
        }
        if (this.interADListener != null)
        {
            StartCoroutine(SESDKTools.ICoroutine(0, delegate
            {
                MopubAdInfo adInfo = new MopubAdInfo("test", "test", "test");
                this.interADListener.onInterstitialShown(gameEntry, adInfo);
            }));
        }
    }

    //banner
    public void showBanner(BannerADPosition position)
    {
        if(demoADMock != null)
        {
            this.demoADMock.showBanner();
        }
        else
        {
            Debug.Log("demoADmock is not run and skip");
        }
    }
    public void dismissBanner()
    {
        if (demoADMock != null)
        {
            this.demoADMock.closeBanner();
        }
        else
        {
            Debug.Log("demoADmock is not run and skip");
        }
    }

    //native ad
    private MopubNativeAdListener nativeAdListener;
    public void setNativeAdListener(MopubNativeAdListener listener)
    {
        this.nativeAdListener = listener;
    }
    public bool hasNativeAd(string gameEntry)
    {
        if (happenChance(failureChance))
        {
            return false;
        }
        return true;
    }
    public void showNativeAdFixed(string gameEntry, SDKNavtiveAdPosition positon, float spacing)
    {
        bool bottom = positon == SDKNavtiveAdPosition.bottomCenter;
        if (demoADMock != null)
        {
            this.demoADMock.ShowNative(gameEntry, bottom, spacing,
            delegate (string entry)
            {
                if (this.nativeAdListener != null)
                {
                    this.nativeAdListener.onNativeAdDismissed(entry);
                }
            }, delegate (string entry)
            {

                if (this.nativeAdListener != null)
                {
                    this.nativeAdListener.onNativeAdClicked(entry);
                }
            });

            if (this.nativeAdListener != null)
            {
                this.nativeAdListener.onNativeAdDidShown(gameEntry);
            }
        }
        else
        {
            Debug.Log("demoADmock is not run and skip");
            if (this.nativeAdListener != null)
            {
                this.nativeAdListener.onNativeAdDidShown(gameEntry);
                this.nativeAdListener.onNativeAdDismissed(gameEntry);
            }
        }
    }

    public void closeNativeAd(string gameEntry)
    {
        if(this.demoADMock != null)
        {
            this.demoADMock.closeNative();
        }
        if (this.nativeAdListener != null)
        {
            this.nativeAdListener.onNativeAdDismissed(gameEntry);
        }
    }
    #endregion

    #region online params
    Dictionary<String, String> ingameParams = new Dictionary<string, string> {
            {"OpenWheel","true"},{"ShowShop","true"},{"GM","false"}
        };
    public void setIngameParamsUpdatedListener(Action<Dictionary<String, String>> ingameParamsUpdatedListener)
    {
        StartCoroutine(SESDKTools.ICoroutine(0.5f, delegate
        {
            if(ingameParamsUpdatedListener != null)
            {
                ingameParamsUpdatedListener(ingameParams);
            }

        }));
    }

    public void setAdvertisingIngameParamsUpdatedListener(Action<Dictionary<String, String>> ADIngameParamsUpdatedListener)
    {
        StartCoroutine(SESDKTools.ICoroutine(0.5f, delegate
        {
            if (ADIngameParamsUpdatedListener != null)
            {
                ADIngameParamsUpdatedListener(ingameParams);
            }

        }));
    }
    #endregion
	
	public void setAFDataUpdatedListener(Action<Dictionary<String, String>> afDataUpdatedListener)
	{
		StartCoroutine(SESDKTools.ICoroutine(0.5f, delegate
        {
            if (afDataUpdatedListener != null)
            {
                Dictionary<String, String> afParams = new Dictionary<string, string> {
                    {"af_status","Organic"}
                };
                afDataUpdatedListener(afParams);
            }

        }));
	}

    public void setPackageUpdateDelegate(Action success)
    {
        StartCoroutine(SESDKTools.ICoroutine(1.0f, delegate
        {
            if (success != null)
            {
                success();
            }

        }));
    }
	
    private void Awake()
    {
        this.succeedOrder = this.readOrder(keySucceedOrder);
        if(this.succeedOrder == null)
        {
            this.succeedOrder = new Dictionary<string, string>();
        }

        this.consumedOrder = this.readOrder(keyConsumedOrder);
        if (this.consumedOrder == null)
        {
            this.consumedOrder = new Dictionary<string, string>();
        }

        var canvas = GameObject.Find("UICanvas");
        if (canvas != null)
        {
            this.demoADMock = canvas.transform.Find("DemoAD").GetComponent<DemoADMock>();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool happenChance(int chance)
    {
        int value = UnityEngine.Random.Range(0, 101);
        //Debug.Log("happenChance: " + value);
        if(value <= chance)
        {
            return true;
        }
        return false;
    }
}
#endif
