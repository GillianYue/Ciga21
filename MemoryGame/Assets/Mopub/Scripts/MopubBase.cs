using System.Collections;
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

		public abstract void autoLoginTouristWithUI(Action<LoginSuccessResult> success, Action<MopubSDKError> failed);

		public abstract MopubSdkAccessToken currentAccessToken ();

		public abstract void verifySessionToken(string token, Action<VerifySuccessResult> success, Action<MopubSDKError> failed);

		public abstract void reLoginFlow(Action<LoginSuccessResult> success, Action<MopubSDKError> failed);

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

		public abstract void startLinkagePayment(Dictionary<string,string> extraData);

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
		public abstract void logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID, Dictionary<string, string> extraData);

		public abstract void addEventGlobalParams(Dictionary<string, string> data);

		public abstract void logCustomEvent (string eventName,  Dictionary<string,object> data);


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

		public abstract void setATTDataUpdatedListener(Action<Dictionary<String, object>> attDataUpdatedListener);

        ///social

        public abstract bool openJoinChatGroup();

		public abstract bool openJoinWhatsappChatting();
		
		public abstract bool openRatingView();

		public abstract bool openRate();

		public abstract bool openRate(string shop);
		
		public abstract void setOnPushInitCallback(Action<string> success, Action<MopubSDKError> failed);

		public abstract void bindAccounts(Dictionary<int, string> accountsType, Action<string> success, Action<MopubSDKError> failed);

		public abstract bool addLocalNotification(MopubSDKLocalMsg localMsg);

		public abstract string getAppDistributor();

		public abstract string getPuid();

		public abstract string getRegion();

		public abstract string getPackageVersionCode();

		public abstract string getCgi();

		public abstract string getGameVersion();
		public abstract string getTimeStamp();

		public abstract void launchCustomer();

		public abstract void launchCustomerWithTransitPage(string pageType, string email);

		public abstract void setCustomerUnreadMessageListener(Action<Dictionary<String, int>> unreadMessageUpdatedListener);

		public abstract bool isUserNotificationEnable();

		public abstract void openSystemNotificationSetting();

		public abstract void fetchRanking(string uid, int page, int size, Action<MopubSDKRanking> success, Action<MopubSDKError> failure);

		public abstract void saveCloudCache(string uid, long version, string data, Action success, Action<MopubSDKError> failed);

		public abstract void getCloudCache(string uid, Action<MopubSDKCloudCache> success, Action<MopubSDKError> failed);

		public abstract void getRedeem(string code,Action<string> success,Action<MopubSDKError> failed);

		public abstract void showGuidPageView(Action<string> success);

		// 展示绑定账号界面
		public abstract void showLinkAccountView(Action success);

		// 展示切换账号界面
		public abstract void showSwitchAccountView(Action<MopubSdkAccessToken> success);

		// 展示删除账号界面
		public abstract void showDeleteAccountView(Action success);

		public abstract string getActivateCode();

		// 初始化推送sdk
		public abstract void initPushSDK(string cgi, string appid, string appSecret, bool passThroughEnable, string logHost, string logPath, Action<string> clickCallback, Action<string> receiveMessageCallback);

		// 设置推送别名
		public abstract void setPushAlias(string alias);
		
		public abstract string getAppSignMD5();
		public abstract string getAppSignSHA1();
		public abstract string getAppSignSHA256();


		public abstract void setPackageUpdatedListener(Action success);
		//九重港澳台账号中心
		public abstract void showAccountCenter(Action linkSuccess,Action deleteSuccess,Action<MopubSdkAccessToken> success,Action<string> userVerify);
		//接受档案
		public abstract void showArchive(Dictionary<string,object> data);
		//是否游客
		public abstract bool isAccountOnlyTourist();
		//支付弹窗绑定
		public abstract void showLinkedAfterPurchaseView();
		// 设置多语言，zh-Hant是繁体 zh-Hans是简体 en是英语   
		public abstract void setLanguage(string language);

		// 展示公告
		public abstract void openNoticeDialog();

		public abstract void requestPermission(MopubPermissionsInfo[] permissionsInfo, Action finish, Action cancel);

		// 获取cp在线参数
		public abstract Dictionary<string, object> getCpParams();

		// 分享到微信
		public abstract void openShareToWechat(string entrance, MopubWechatSharedData sharedData, Action success, Action<MopubSDKError> failure);

		// 分享到qq
		public abstract void openShareToQQ(string entrance, MopubQQSharedData sharedData, Action success, Action<MopubSDKError> failure);

		//当前时间对应的分享活动
		public abstract MopubAppSharedCampaignInfo getSharedCampaignInfo();

		//当前时间对应的邀请活动
		public abstract MopubAppSharedCampaignInfo getInvitedCampaignInfo();

		public abstract Dictionary<string, object> getSDKInfo();

		public abstract void fetchInviteBonusList(Action<List<MopubInviteBonusCategory>> success, Action<MopubSDKError> failure);

		public abstract void acceptInviteBonus(string inviteId, string pkey, Action success, Action<MopubSDKError> failure);

		public abstract void fetchIPv4Info(Action<MopubIPv4Info> success, Action<MopubSDKError> failure);
	}
}
