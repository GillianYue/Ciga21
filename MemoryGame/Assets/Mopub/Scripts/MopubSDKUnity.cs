using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MopubNS;
using System;
using UnityEngine.Events;

public class MopubSDKUnity : MonoBehaviour, MopubRewardedVideoListener, MopubInterstitialAdListener, MopubNativeAdListener
{
    //Whether to log in automatically when the game starts
    public bool loginAtGameStarted = true;

    //When the player purchases the item, the callback will notify the need to ship
    [Serializable]
    public class UnconsumedItemsShippingEvent : UnityEvent<List<MopubSDKPurchasedItem>> { };
    public UnconsumedItemsShippingEvent onUnconsumedItemsShippingEvent;

    [HideInInspector]
    public static MopubSDKUnity instance;

    [HideInInspector]
    public bool sdkInitialized = false;

    [HideInInspector]
    public LoginSuccessResult loginResult = null;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            this.loginResult = null;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        //Initialize the SDK
        if (!this.sdkInitialized)
        {
            MopubSdk.getInstance().init(
                delegate (InitSuccessResult result)
                {
                    SESDKTools.SELogInfo("initSuccess");
                    this.sdkInitialized = true;
                    this.login();
                },
                delegate (MopubSDKError error)
                {
                    SESDKTools.SELogError(error, "initFailed");
                    this.login();
                });
        }
        //set up listener for purchased unconsumed items
        this.SetUnconsumedItemUpdatedListener();

        //set up ad listener
        this.setRewardedVideoListener();
        this.setInterstitialAdListener();
        this.setNativeAdListener();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    #region login
    void login(Action<LoginSuccessResult> success = null, Action<MopubSDKError> failed = null)
    {
        StartCoroutine(this.loginNextFrame(success, failed));
    }

    private bool loginProcessing = false;
    IEnumerator loginNextFrame(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
    {

        if (loginProcessing)
        {
            SESDKTools.SELogInfo("loginProcessing");
            yield return new WaitForSeconds(6.0f);
        }

        if (this.loginResult != null)
        {
            SESDKTools.SELogInfo("alreadyLoggedIn");
            if(success != null)
            {
                success(loginResult);
            }
            yield break;
        }
        
        yield return null;

        SESDKTools.SELogInfo("callLogin");
        loginProcessing = true;
        MopubSdk.getInstance().login(
            delegate (LoginSuccessResult s)
            {
                loginProcessing = false;
                SESDKTools.SELogInfo("loginSuccess");
                this.loginResult = s;
                this.FetchItemCatelog(null, null);
                if(success != null)
                {
                    success(s);
                }
            },
            delegate (MopubSDKError error)
            {
                loginProcessing = false;
                SESDKTools.SELogError(error, "loginFailed");
                if (failed != null)
                {
                    failed(error);
                }
            });
    }
    #endregion

    #region payment
    private void SetUnconsumedItemUpdatedListener()
    {
        //set up listener for purchased unconsumed items
        SESDKTools.SELogInfo("callSetUnconsumedItemUpdatedListener");
        MopubSdk.getInstance().setUnconsumedItemUpdatedListener(delegate (List<MopubSDKPurchasedItem> items)
        {
            if (this.onUnconsumedItemsShippingEvent != null)
            {
                onUnconsumedItemsShippingEvent.Invoke(items);
            }

        });
    }

    private List<MopubSDKPaymentItemDetails> productCatelogCache;
    //fetch product catalog from SDK's backend
    public void FetchItemCatelog(Action<List<MopubSDKPaymentItemDetails>> success, Action<MopubSDKError> failed)
    {
        if(this.productCatelogCache != null)
        {
            if(success != null)
            {
                success(productCatelogCache);
            }
            return;
        }

        if (this.loginResult != null)
        {
            this.FetchItemCatelogDirectly(success, failed);
        }else
        {
            this.login(
                delegate (LoginSuccessResult res) {
                    this.FetchItemCatelogDirectly(success, failed);

                }, delegate (MopubSDKError error) {
                    if (failed != null)
                    {
                        failed(error);
                    }
                
        
            });
        }
    }

    public void FetchItemCatelogDirectly(Action<List<MopubSDKPaymentItemDetails>> success, Action<MopubSDKError> failed)
    {
        SESDKTools.SELogInfo("callFetchItemDetails");
        MopubSdk.getInstance().fetchPaymentItemDetails(
            delegate (List<MopubSDKPaymentItemDetails> itemDetails) {
                SESDKTools.SELogInfo("callFetchItemDetailsSuccess");
                this.productCatelogCache = itemDetails;
                if (success != null)
                {
                    success(productCatelogCache);
                }
            }, failed
        );
    }

    /// <summary>
    /// buy a item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="processingEndcallback">When the payment process is normal (does not mean that the payment was successful), MopubSDKError is null</param>
    public void BuyItem(MopubSDKPaymentItemDetails item, Action<MopubSDKPaymentInfo, MopubSDKError> processingEndcallback)
    {
        MopubSDKStartPaymentInfo info = new MopubSDKStartPaymentInfo(
            item.itemID,
            null,
            null,
            null,
            null,
            null);
        SESDKTools.SELogInfo("callStartPayment_"+item.itemID);
        MopubSdk.getInstance().startPayment(
            info,
            (MopubSDKPaymentInfo paymentInfo) => {
                SESDKTools.SELogInfo("paymentProcessing_" + item.itemID);
                if(processingEndcallback != null)
                {
                    processingEndcallback(paymentInfo, null);
                }

            },
            (MopubSDKPaymentInfo paymentInfo, MopubSDKError error) => {
                SESDKTools.SELogError(error, "paymentFailed_" + item.itemID);
                if (processingEndcallback != null)
                {
                    processingEndcallback(paymentInfo, error);
                }
            }
        );
    }

    #endregion

    #region Ads
    //Event
    [Serializable]
    public class RewardedVideoStartedEvent : UnityEvent<string> { }
    public RewardedVideoStartedEvent onRewardedVideoStartedEvent;

    [Serializable]
    public class RewardedVideoClosedEvent : UnityEvent<string> { }
    public RewardedVideoClosedEvent onRewardedVideoClosedEvent;

    [Serializable]
    public class RewardedVideoCompletedEvent : UnityEvent<string> { }
    public RewardedVideoCompletedEvent onRewardedVideoCompletedEvent;

    [Serializable]
    public class InterstitialShownEvent : UnityEvent<string> { }
    public InterstitialShownEvent onInterstitialShownEvent;

    [Serializable]
    public class InterstitialDismissedEvent : UnityEvent<string> { }
    public InterstitialDismissedEvent onInterstitialDismissedEvent;

    [Serializable]
    public class NativeAdDidShownEvent : UnityEvent<string> { }
    public NativeAdDidShownEvent onNativeAdShownEvent;

    [Serializable]
    public class NativeAdDismissedEvent : UnityEvent<string> { }
    public NativeAdDismissedEvent onNativeAdDismissedEvent;

    

    public bool hasSmartAd(string gameEntry)
    {
        return MopubSdk.getInstance().hasSmartAd(gameEntry);
    }

    public void showSmartAd(string gameEntry)
    {
        MopubSdk.getInstance().showSmartAd(gameEntry);
    }

     //reward
    private void setRewardedVideoListener()
    {
        MopubSdk.getInstance().setRewardedVideoListener(this);

    }

    public bool hasRewardedVideo(string gameEntry)
    {
        return MopubSdk.getInstance().hasRewardedVideo(gameEntry);
    }

    public void showRewardVideoAd(string gameEntry)
    {
        MopubSdk.getInstance().showRewardVideoAd(gameEntry);
    }

    public bool showRewardVideoAdAuto(string gameEntry)
    {
        bool canShow = this.hasRewardedVideo(gameEntry);
        if (canShow)
        {
            MopubSdk.getInstance().showRewardVideoAd(gameEntry);
        }
        return canShow;
    }

    //reward callbacks

    public void onRewardedVideoStarted(string gameEntry, MopubAdInfo adInfo)
    {
        SESDKTools.SELogInfo("onRewardedVideoStarted:" + gameEntry);
        if (this.onRewardedVideoStartedEvent != null)
        {
            this.onRewardedVideoStartedEvent.Invoke(gameEntry);
        }
    }

    public void onRewardedVideoPlaybackError(string gameEntry, int errorCode, string msg)
    {
        SESDKTools.SELogInfo("onRewardedVideoPlaybackError:" + msg);
    }

    public void onRewardedVideoClicked(string gameEntry, MopubAdInfo adInfo)
    {
        SESDKTools.SELogInfo("onRewardedVideoClicked:" + gameEntry);
    }

    public void onRewardedVideoClosed(string gameEntry, MopubAdInfo adInfo)
    {
        SESDKTools.SELogInfo("onRewardedVideoClosed:" + gameEntry);
        if (this.onRewardedVideoClosedEvent != null)
        {
            this.onRewardedVideoClosedEvent.Invoke(gameEntry);
        }
    }

    //We need to give the reward in this callback
    public void onRewardedVideoCompleted(string gameEntry, MopubAdInfo adInfo)
    {
        SESDKTools.SELogInfo("onRewardedVideoCompleted:" + gameEntry);
        if (this.onRewardedVideoCompletedEvent != null)
        {
            this.onRewardedVideoCompletedEvent.Invoke(gameEntry);
        }
    }


    //interstitial

    public void setInterstitialAdListener()
    {
        MopubSdk.getInstance().setInterstitialAdListener(this);

    }

    public bool hasInterstitial(string gameEntry)
    {
        return MopubSdk.getInstance().hasInterstitial(gameEntry);
    }

    public void showInterstitialAd(string gameEntry)
    {
        MopubSdk.getInstance().showInterstitialAd(gameEntry);
    }

    public bool showInterstitialAdAuto(string gameEntry)
    {
        bool canShow = this.hasInterstitial(gameEntry);
        if (canShow)
        {
            MopubSdk.getInstance().showInterstitialAd(gameEntry);
        }
        return canShow;
    }

    //interstitial callbacks

    public void onInterstitialShown(string gameEntry, MopubAdInfo adInfo)
    {
        if (this.onInterstitialShownEvent != null)
        {
            this.onInterstitialShownEvent.Invoke(gameEntry);
        }
    }

    public void onInterstitialClicked(string gameEntry, MopubAdInfo adInfo)
    {
        SESDKTools.SELogInfo("onInterstitialClicked:" + gameEntry);
    }

    public void onInterstitialDismissed(string gameEntry, MopubAdInfo adInfo)
    {
        if (this.onInterstitialDismissedEvent != null)
        {
            this.onInterstitialDismissedEvent.Invoke(gameEntry);
        }
    }

    //banner 
    public void showBanner(BannerADPosition position)
    {
        MopubSdk.getInstance().showBanner(position);

    }
    public void dismissBanner()
    {
        MopubSdk.getInstance().dismissBanner();
    }

    //native ad
    public void setNativeAdListener()
    {
        MopubSdk.getInstance().setNativeAdListener(this);
    }

    public bool hasNativeAd(string gameEntry)
    {
        return MopubSdk.getInstance().hasNativeAd(gameEntry);
    }

    //If positon=bottomCenter, 'spacing' indicates the distance from the bottom. If  positon=topCenter, 'spacing' indicates the distance from the top
    public void showNativeAdFixed(string gameEntry, SDKNavtiveAdPosition positon, float spacing)
    {
        MopubSdk.getInstance().showNativeAdFixed(gameEntry, positon, spacing);
    }

    public bool showNativeAdFixedAuto(string gameEntry, SDKNavtiveAdPosition positon, float spacing)
    {
        bool canShow = this.hasNativeAd(gameEntry);
        if (canShow)
        {
            MopubSdk.getInstance().showNativeAdFixed(gameEntry, positon, spacing);
        }
        return canShow;
    }

    public void closeNativeAd(string gameEntry)
    {
        MopubSdk.getInstance().closeNativeAd(gameEntry);
    }

    public void onNativeAdDidShown(string gameEntry)
    {
        if (this.onNativeAdShownEvent != null)
        {
            this.onNativeAdShownEvent.Invoke(gameEntry);
        }
    }

    public void onNativeAdDismissed(string gameEntry)
    {
        if (this.onNativeAdDismissedEvent != null)
        {
            this.onNativeAdDismissedEvent.Invoke(gameEntry);
        }
    }

    public void onNativeAdClicked(string gameEntry)
    {
        SESDKTools.SELogInfo("onInterstitialClicked:" + gameEntry);
    }

    #endregion
}
