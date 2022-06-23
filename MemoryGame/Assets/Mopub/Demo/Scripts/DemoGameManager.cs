using UnityEngine;
using System.Collections;

using MopubNS;
using System.Collections.Generic;
using MopubNS.ThirdParty.MiniJSON;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class DemoGameManager : MonoBehaviour
{
    private DemoPlayerData currentPlayerData;

    public GameObject productCatelog;
    public GameObject btnShop;

    public SEToastTool toast;

    [Serializable]
    public class PlayerCoinsChangedEvent : UnityEvent<int> { };
    public PlayerCoinsChangedEvent onPlayerCoinsChangedEvent;

    private void Awake()
    {
        currentPlayerData = DemoPlayerData.ReadData();
        if (currentPlayerData == null)
        {
            currentPlayerData = DemoPlayerData.CreateNewData();
        }
        this.ChangePlayerCoin(currentPlayerData.coins);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region payment

    public void shipUnconsumedItems(List<MopubSDKPurchasedItem> items)
    {
        if(this.currentPlayerData == null)
        {
            return ;
        }

        SESDKTools.SELogInfo("findUncomsuesItems");
        foreach (var item in items)
        {
            //important: make sure you have not sent this sdkOrderID's item yet
            if (!this.currentPlayerData.WhetherContainsConsumableOrder(item.sdkOrderID))
            {
                //send this item to the player
                SESDKTools.SELogInfo("willGiveItemSdkOrderID_" + item.sdkOrderID + "_" + item.itemID);
                if (this.SendItem(item.itemID, item.sdkOrderID))
                {
                    //After successfully sending the item, please consume the order
                    MopubSdk.getInstance().consumePurchase(item.sdkOrderID);
                }
            }
            else
            {
                SESDKTools.SELogInfo("alreadySendItem_" + item.sdkOrderID);
                //If you had already send this order, consume the order
                MopubSdk.getInstance().consumePurchase(item.sdkOrderID);
            }
        }
    }

    private bool SendItem(string itemID, string orderID)
    {
        bool canSend = false;
        switch (itemID)
        {
            case "1001":
                this.ChangePlayerCoin(this.currentPlayerData.coins + 100);
                canSend = true;
                break;
            case "1002":
                this.ChangePlayerCoin(this.currentPlayerData.coins + 600);
                canSend = true;
                break;
            case "1003":
                this.currentPlayerData.showAD = false;
                canSend = true;
                break;
            default:
                break;
        }
        if (canSend)
        {
            this.currentPlayerData.AddNewConsumableOrder(orderID, true);
        }
        
        return canSend;
    }

    Dictionary<string, GameObject> showingShopItems = new Dictionary<string, GameObject>();

    public void showShop()
    {
        Text text = this.btnShop.transform.GetComponentInChildren<Text>();

        //if the catalog is active, means catalog need to be closed
        if (this.productCatelog.activeInHierarchy)
        {
            foreach(var obj in showingShopItems)
            {
                Destroy(obj.Value);
            }
            showingShopItems.Clear();
            this.productCatelog.SetActive(false);
            text.text = "Shop";
            this.toast.close();
            return;
        }

        this.productCatelog.SetActive(true);
        text.text = "Close";
        this.toast.Show("Loading...", 5.0f, delegate
        {
            this.toast.Show("Loading timeout, please try again", 6.0f, null);

        });

        MopubSDKUnity.instance.FetchItemCatelog(delegate(List<MopubSDKPaymentItemDetails> items) {
            this.toast.close();
            Debug.Log("showShop");
            Debug.Log(Json.Serialize(items));

            Transform grid = this.productCatelog.transform.Find("Grid");
            
            Transform itemTemplate = grid.Find("Item");
            for(int i = 0; i < items.Count; i++)
            {
                DemoPaymentItem item;
                if(i == 0)
                {
                    itemTemplate.gameObject.SetActive(true);
                    item = itemTemplate.GetComponent<DemoPaymentItem>();
                }
                else
                {
                    string itemID = items[i].itemID;
                    if (showingShopItems.ContainsKey(itemID))
                    {
                        break;
                    }
                    item = Instantiate(itemTemplate, grid).GetComponent<DemoPaymentItem>();
                    showingShopItems.Add(itemID, item.gameObject);
                }

                item.itemDetail = items[i];
            }

	    },
        delegate (MopubSDKError error) {
            this.toast.Show("Loading failed, please try again", 4.0f, null);
            SESDKTools.SELogError(error, "fetchProductFailed");
        });
    }

    public void BuyItem(MopubSDKPaymentItemDetails item)
    {
        this.toast.Show("Process...", 12.0f, delegate
        {
            this.toast.Show("Processing", 3.0f, null);

        });

        MopubSDKUnity.instance.BuyItem(
            item,
            (MopubSDKPaymentInfo info, MopubSDKError error) =>
            {
                if(error != null)
                {
                    this.toast.Show("Payment failed, please try again", 4.0f, null);
                }
                else
                {
                    this.toast.Show("Payment successful, will be shipped later", 4.0f, null);
                }
            });
    }

    #endregion

    #region Ads
    //RewardedAd
    public void showRewardedAd(string entry)
    {
        bool canShow = MopubSDKUnity.instance.showRewardVideoAdAuto(entry);
        if (!canShow)
        {
            this.toast.Show("Ads are still loading", 3.0f, null);
        }
    }

    //InterstitialAd
    public void showInterstitialAd(string entry)
    {
        if (!this.currentPlayerData.showAD)
        {
            this.toast.Show("You have purchased remove ads", 3.0f, null);
            return;
        }

        bool canShow = MopubSDKUnity.instance.showInterstitialAdAuto(entry);
        if (!canShow)
        {
            Debug.Log("Interstitial Ad are still loading");
        }
    }

    //banner
    bool bannerSown = false;
    public void ShowBannerAuto()
    {
        if (!this.currentPlayerData.showAD)
        {
            this.toast.Show("You have purchased remove ads", 3.0f, null);
            return;
        }

        if (bannerSown)
        {
            this.DissmissBanner();
        }
        else
        {
            this.ShowBanner();
        }
        bannerSown = !bannerSown;
    }

    private void ShowBanner()
    {
        MopubSDKUnity.instance.showBanner(BannerADPosition.bottomCenter);
    }

    private void DissmissBanner()
    {
        MopubSDKUnity.instance.dismissBanner();
    }

    //nativeAd
    private bool nativeAdShown = false;
    public void showNativeAd(string entry)
    {
        if (!this.currentPlayerData.showAD)
        {
            this.toast.Show("You have purchased remove ads", 3.0f, null);
            return;
        }

        if (nativeAdShown)
        {
            MopubSDKUnity.instance.closeNativeAd(entry);
            this.nativeAdShown = false;
            return;
        }

        bool canShow = MopubSDKUnity.instance.showNativeAdFixedAuto(entry, SDKNavtiveAdPosition.topCenter, 5.0f);
        if (!canShow)
        {
            Debug.Log("Native Ad are still loading");
        }
        else
        {
            this.nativeAdShown = true;
        }
    }



    ///Rewarded Video callback
    public void OnRewardedVideoStartedEvent(string entry) {
        Debug.Log("DemoGaneManager-OnRewardedVideoStartedEvent");

        //We need to turn off game audio in this callback
        //turnOffMusic();

        //If the banner is shown, we should dismiss it
        if (this.bannerSown)
        {
            this.DissmissBanner();
        }
    }

    public void OnRewardedVideoClosedEvent(string entry) {
        Debug.Log("DemoGaneManager-OnRewardedVideoClosedEvent");

        //We need to turn on game audio in this callback
        //TurnOnMusic();

        //Redisplay banner
        if (this.bannerSown)
        {
            this.ShowBanner();
        }
    }

    public void OnRewardedVideoCompletedEvent(string entry) {
        Debug.Log("DemoGaneManager-OnRewardedVideoCompletedEvent");

        //We need to give the reward in this callback
        switch (entry)
        {
            case "coins50":
                this.ChangePlayerCoin(this.currentPlayerData.coins + 50);
                this.currentPlayerData.SaveData();
                break;
            default:
                break;
        }
        
    }

    //Interstitial callback
    public void OnInterstitialShownEvent(string entry) {
        Debug.Log("DemoGaneManager-OnInterstitialShownEvent");

        //We need to turn off game audio in this callback
        //turnOffMusic();

        //If the banner is shown, we should dismiss it
        if (this.bannerSown)
        {
            this.DissmissBanner();
        }
    }

    public void OnInterstitialDismissedEvent(string entry) {
        Debug.Log("DemoGaneManager-OnInterstitialDismissedEvent");

        //We need to turn on game audio in this callback
        //TurnOnMusic();

        //Redisplay banner
        if (this.bannerSown)
        {
            this.ShowBanner();
        }
    }

    //Native Ad callback
    public void OnNativeAdShownEvent(string entry) {
        Debug.Log("DemoGaneManager-OnNativeAdShownEvent");
    }

    public void OnNativeAdDismissedEvent(string entry) {
        Debug.Log("DemoGaneManager-OnNativeAdDismissedEvent");
        this.nativeAdShown = false;
    }

    #endregion

    #region helper

    private void ChangePlayerCoin(int coins)
    {
        this.currentPlayerData.coins = coins;
        if (onPlayerCoinsChangedEvent != null)
        {
            onPlayerCoinsChangedEvent.Invoke(coins);
        }
    }

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        this.currentPlayerData = DemoPlayerData.CreateNewData();
        this.ChangePlayerCoin(this.currentPlayerData.coins);
    }

    #endregion
}
