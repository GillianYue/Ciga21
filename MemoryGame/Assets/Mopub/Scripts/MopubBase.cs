﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MopubNS
{
	public abstract class MopubBase
	{
		public static string PLUGIN_SDK_VERSION = "1.0.0";
		// Allocate the MoPubManager singleton, which receives all callback events from the native SDKs.
		protected static void InitCallbackManager()
		{
			Debug.Log ("InitCallbackManager");
			Debug.Log ("PluginSDK-version-"+PLUGIN_SDK_VERSION);
			var type = typeof(MopubCallbackManager);
			var mgr = new GameObject("MopubManager", type).GetComponent<MopubCallbackManager>(); // Its Awake() method sets Instance.
			if (MopubCallbackManager.Instance != mgr)
				Debug.LogWarning(
					"It looks like you have the " + type.Name
					+ " on a GameObject in your scene. Please remove the script from your scene.");
		}

		public abstract void init (Action<InitSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void init (string gameContentVersion, Action<InitSuccessResult> success, Action<MopubSDKError> failed);
		//实名认证--新版
		public abstract void showRealNameView(Action<string> success,Action<string> failed);
		
		//实名认证页面sdk--旧版
		public abstract void callRealNameUI(bool netWorkable,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror);
		
		//反成谜支付页面sdk
		public abstract void realNameRecharge(int amount,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror);
		public abstract void login (Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void logout(Action<string> success, Action<MopubSDKError> failed);
		public abstract void loginWithDevice(Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithDevice(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithWeChat(Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithWeChat(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithVisitor(Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithVisitor(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed);

		public abstract void fetchMobileAuthCode(string phoneNumber, Action<FetchSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithMobile(string phoneNumber, string code, bool isOneKeyLogin,Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithMobile(string phoneNumber, string code, string activeCode, bool isOneKeyLogin,Action<LoginSuccessResult> success, Action<MopubSDKError> failed);

		public abstract void fetchEmailAuthCode(string email, Action<FetchSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithEmail(string email, string password, string code, Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void loginWithEmail(string email, string password, string code, string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed);
		public abstract void resetPasswordWithEmail(string email, string password, string code, Action<string> success, Action<MopubSDKError> failed);
		public abstract void showLoginUI(Action<LoginSuccessResult> success, Action<MopubSDKError> failed,string activeCode);

		public abstract MopubSdkAccessToken currentAccessToken ();

		public abstract void verifySessionToken(string token, Action<VerifySuccessResult> success, Action<MopubSDKError> failed);

		public abstract void createInviteCode(Action<string> success, Action<MopubSDKError> failed);

		public abstract void fetchInviteeList(Action<List<object>> success, Action<MopubSDKError> failed);

		public abstract void uploadInviteCode(string code, Action<string> success, Action<MopubSDKError> failed);

        // old addiction prevention
		public abstract void openPrevention();
		public abstract long getTotalOnlineTime();
		public abstract void fetchIdCardInfo(Action<MopubSDKIdCardInfo> success, Action<MopubSDKError> failed);
		public abstract void verifyIdCard(string realName, string cardNumber, Action<string> success, Action<MopubSDKError> failed);
		public abstract void fetchPaidAmount(Action<string> success, Action<MopubSDKError> failed);

		// new addiction prevention
		public abstract void openRealnamePrevention();
		public abstract long getPreventionTotalOnlineTime();
		public abstract MopubSDKRealnameHeartBeat getRealnameHeartBeat();
		public abstract void queryRealnameInfo(Action<MopubSDKRealnameInfo> success, Action<MopubSDKError> failed);
		public abstract void realnameAuthentication(string realname, string cardNumber, Action<MopubSDKRealnameInfo> success, Action<MopubSDKError> failed);
		public abstract void fetchPaidAmountMonthly(int amount, Action<MopubSDKRealnamePaidAmountInfo> success, Action<MopubSDKError> failed);

		public abstract void linkWithGameCenter (Action<MopubSDKLinkWithGameCenterResult> success, Action<MopubSDKError> failed);

		public abstract void linkWithFacebook (Action<MopubSDKLinkWithFacebookResult> success, Action<MopubSDKError> failed);

		public abstract void linkWithWeChat (Action<MopubSDKLinkWithWeChatResult> success, Action<MopubSDKError> failed);

		public abstract void linkWithEmail(string email, string password, string code, Action<MopubSDKLinkWithEmailResult> success, Action<MopubSDKError> failed);

		public abstract void linkWithMobile(string phoneNumber, string code, Action<MopubSDKLinkWithMobileResult> success, Action<MopubSDKError> failed);

		public abstract void setUnconsumedItemUpdatedListener (Action<List<MopubSDKPurchasedItem>> unconsumedItemUpdatedListener);

        public abstract void fetchPaymentItemDetails (Action<List<MopubSDKPaymentItemDetails>> success, Action<MopubSDKError> failed);
		public abstract void fetchUnconsumedPurchasedItems (Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed);
		public abstract void fetchAllPurchasedItems (Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed);
		public abstract void consumePurchase (string sdkOrderID);
		public abstract void startPayment (MopubSDKStartPaymentInfo paymentInfo, 
			Action<MopubSDKPaymentInfo> paymentProcessingWithPaymentInfo,
			Action<MopubSDKPaymentInfo, MopubSDKError> paymentFailedWithPaymentInfo);

        //subscription
        //Will be called after logins if player had any subscription item.
        public abstract void setSubscriptionItemUpdatedListener(Action<List<MopubSDKSubscriptionItem>> subscriptionItemUpdatedListener);

        public abstract void restoreSubscription(Action success, Action<MopubSDKError> failed);

        public abstract void fetchAllPurchasedSubscriptionItems(Action<List<MopubSDKSubscriptionItem>> success);


        public abstract bool hasSmartAd(string gameEntry);
		public abstract void showSmartAd(string gameEntry);
		    
        public abstract void setRewardedVideoListener (MopubRewardedVideoListener listener);
		public abstract bool hasRewardedVideo(string gameEntry);
		public abstract void showRewardVideoAd(string gameEntry);

		public abstract void setInterstitialAdListener (MopubInterstitialAdListener listener);
		public abstract bool hasInterstitial(string gameEntry);
		public abstract void showInterstitialAd(string gameEntry);

        public abstract void showBanner(BannerADPosition position);
        public abstract void dismissBanner();


        //native ad
        public abstract void setNativeAdListener(MopubNativeAdListener listener);
        public abstract bool hasNativeAd(string gameEntry);
        //If positon=bottomCenter, 'spacing' indicates the distance from the bottom. If  positon=topCenter, 'spacing' indicates the distance from the top
        public abstract void showNativeAdFixed(string gameEntry, SDKNavtiveAdPosition positon, float spacing);

        public abstract void closeNativeAd(string gameEntry);


        //log and data
        public abstract void logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID);


        public abstract void logCustomEvent (string eventName,  Dictionary<string,string> data);


		public abstract void logCustomAFEvent (string eventName, Dictionary<string,object> data);
		
		public abstract void logStartLevel(string levelName);
		
		public abstract void logFinishLevel(string levelName);
		
		public abstract void logUnlockLevel(string levelName);
		
		public abstract void logSkipLevel(string levelName);
		
		public abstract void logSkinUsed(string skinName);
		
		public abstract void logAdEntrancePresent(string name);

		public abstract void testLog (string log);

        ///Ingame Params

        public abstract void setIngameParamsUpdatedListener (Action<Dictionary<String, String>> ingameParamsUpdatedListener);

        public abstract void setAdvertisingIngameParamsUpdatedListener(Action<Dictionary<String, String>> ADIngameParamsUpdatedListener);

		public abstract void setAFDataUpdatedListener(Action<Dictionary<String, String>> afDataUpdatedListener);
        ///social

        public abstract bool openJoinChatGroup();

		public abstract bool openJoinWhatsappChatting();
		
		public abstract bool openRatingView();

		public abstract bool openRate();
		
		public abstract void setOnPushInitCallback(Action<string> success, Action<MopubSDKError> failed);

		public abstract void bindAccounts(Dictionary<int, string> accountsType, Action<string> success, Action<MopubSDKError> failed);

		public abstract bool addLocalNotification(MopubSDKLocalMsg localMsg);

		public abstract string getAppDistributor();

		public abstract string getPuid();

		public abstract string getRegion();


		public abstract string getGameVersion();
		public abstract string getTimeStamp();

		public abstract void launchCustomer();

		public abstract void launchCustomerWithTransitPage(string pageType, string email);

		public abstract void setCustomerUnreadMessageListener(Action<Dictionary<String, int>> unreadMessageUpdatedListener);

		public abstract bool isUserNotificationEnable();

		public abstract void openSystemNotificationSetting();

		public abstract void fetchRanking(string uid, int page, int size, Action<MopubSDKRanking> success, Action<MopubSDKError> failure);

	}
}