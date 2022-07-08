using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Runtime.InteropServices;
using MopubNS.ThirdParty.MiniJSON;
namespace MopubNS
{
    public class MopubAndroid : MopubBase
    {

        static MopubAndroid()
        {
            Debug.Log("static MopubAndroid");
            #if UNITY_ANDROID
            InitCallbackManager ();
            Debug.Log("static MopubAndroid success");
            #endif
        }




        public override void init(Action<InitSuccessResult> success, Action<MopubSDKError> failed)
        {
            Debug.Log("static MopubAndroid init");
            MopubCallbackManager.initSuccessDelegate = success;
            MopubCallbackManager.initFailedDelegate = failed;
            _init(null);
        }

        public override void init(string gameContentVersion, Action<InitSuccessResult> success, Action<MopubSDKError> failed)
        {
            Debug.Log("static MopubAndroid init with gameContentVersion");
            MopubCallbackManager.initSuccessDelegate = success;
            MopubCallbackManager.initFailedDelegate = failed;
            _init(gameContentVersion);
        }

        //实名认证ui新版
        public override void showRealNameView(Action<string> success,Action<string> failed){
           Debug.Log("static MopubAndroid showRealNameView");
           MopubCallbackManager.showRealNameViewSuccessDelegate = success;
           MopubCallbackManager.showRealNameViewFailedDelegate = failed;
            _showRealNameView();
       }
       //实名认证ui--old
       public override void callRealNameUI(bool netWorkable,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
       {
       Debug.Log("static MopubAndroid callRealNameUI");
       MopubCallbackManager.realNameUISuccessDelegate = success;
       MopubCallbackManager.realNameUIFailedDelegate = failed;
       MopubCallbackManager.realNameUIFailedErrorDelegate = failederror;
       _callRealNameUI(netWorkable);
       }
       
        //反成谜支付页面sdk
       public override void realNameRecharge(int amount,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
       {
       Debug.Log("static MopubAndroid realNameRecharge");
       MopubCallbackManager.realNameUISuccessDelegate = success;
       MopubCallbackManager.realNameUIFailedDelegate = failed;
       MopubCallbackManager.realNameUIFailedErrorDelegate = failederror;
       _realNameRecharge(amount);
       }
        public override void login(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.loginSuccessDelegate = success;
            MopubCallbackManager.loginFailedDelegate = failed;
            _login();
        }

        public override void logout(Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.logoutSuccessDelegate = success;
            MopubCallbackManager.logoutFailedDelegate = failed;
            _logout();
        }

        public override void loginWithDevice(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.loginDeviceSuccessDelegate = success;
            MopubCallbackManager.loginDeviceFailedDelegate = failed;
            _loginWithDevice(null);
        }

        public override void loginWithDevice(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.loginDeviceSuccessDelegate = success;
            MopubCallbackManager.loginDeviceFailedDelegate = failed;
            _loginWithDevice(activeCode);
        }

        public override void loginWithWeChat(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.loginWeChatSuccessDelegate = success;
            MopubCallbackManager.loginWeChatFailedDelegate = failed;
            _loginWithWeChat(null);
        }

        public override void loginWithWeChat(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.loginWeChatSuccessDelegate = success;
            MopubCallbackManager.loginWeChatFailedDelegate = failed;
            _loginWithWeChat(activeCode);
        }

        public override void loginWithVisitor(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.loginVisitorSuccessDelegate = success;
            MopubCallbackManager.loginVisitorFailedDelegate = failed;

            _loginWithVisitor(null);
        }

        public override void loginWithVisitor(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.loginVisitorSuccessDelegate = success;
            MopubCallbackManager.loginVisitorFailedDelegate = failed;

            _loginWithVisitor(activeCode);
        }

        public override void fetchMobileAuthCode(string phoneNumber, Action<FetchSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.fetchMobileAuthCodeSuccessDelegate = success;
            MopubCallbackManager.fetchMobileAuthCodeFailedDelegate = failed;
            _fetchMobileAuthCode(phoneNumber);
        }

		public override void loginWithMobile(string phoneNumber, string code, bool isOneKeyLogin,Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.loginMobileSuccessDelegate = success;
            MopubCallbackManager.loginMobileFailedDelegate = failed;
            _loginWithMobile(phoneNumber, code, null,isOneKeyLogin);
        }

		public override void loginWithMobile(string phoneNumber, string code, string activeCode,bool isOneKeyLogin, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.loginMobileSuccessDelegate = success;
            MopubCallbackManager.loginMobileFailedDelegate = failed;
            _loginWithMobile(phoneNumber, code, activeCode,isOneKeyLogin);
        }

        public override void fetchEmailAuthCode(string email, Action<FetchSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.fetchEmailAuthCodeSuccessDelegate = success;
            MopubCallbackManager.fetchEmailAuthCodeFailedDelegate = failed;
            _fetchEmailAuthCode(email);
        }
		public override void loginWithEmail(string email, string password, string code, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.loginEmailSuccessDelegate = success;
            MopubCallbackManager.loginEmailFailedDelegate = failed;
            _loginWithEmail(email, password, code, null);
        }
		public override void loginWithEmail(string email, string password, string code, string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.loginEmailSuccessDelegate = success;
            MopubCallbackManager.loginEmailFailedDelegate = failed;
            _loginWithEmail(email, password, code, activeCode);
        }

        public override void resetPasswordWithEmail(string email, string password, string code, Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.resetPasswordWithEmailSuccessDelegate = success;
            MopubCallbackManager.resetPasswordWithEmailFailedDelegate = failed;
            _resetPasswordWithEmail(email, password, code);
        }

        public override void showLoginUI(Action<LoginSuccessResult> success, Action<MopubSDKError> failed,string activeCode){
            MopubCallbackManager.loginUISuccessDelegate = success;
            MopubCallbackManager.loginUIFailedDelegate = failed;
           _showLoginUI(activeCode);
        }

        public override MopubSdkAccessToken currentAccessToken(){
            String tokenJSON = _currentAccessToken();
            Debug.Log(tokenJSON);
            MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken>(tokenJSON);
            return token;
        }

        public override void verifySessionToken(string token, Action<VerifySuccessResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.verifySessionTokenSuccessDelegate = success;
            MopubCallbackManager.verifySessionTokenFailedDelegate = failed;
            _verifySessionToken(token);
        }

        public override void createInviteCode(Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.createInviteCodeSuccessDelegate = success;
            MopubCallbackManager.createInviteCodeFailedDelegate = failed;
            _createInviteCode();
        }

		public override void fetchInviteeList(Action<List<object>> success, Action<MopubSDKError> failed){
            MopubCallbackManager.fetchInviteeListSuccessDelegate = success;
		    MopubCallbackManager.fetchInviteeListFailedDelegate = failed;
            _fetchInviteeList();
        }

		public override void uploadInviteCode(string code, Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.uploadInviteCodeSuccessDelegate = success;
		    MopubCallbackManager.uploadInviteCodeFailedDelegate = failed;
            _uploadInviteCode(code);
        }

        // old addiction prevention
        public override void openPrevention(){
            // MopubCallbackManager.preventionDidTriggerDelegate = preventionDidTrigger;
            _openPrevention();
        }

        public override long getTotalOnlineTime(){
            return _getTotalOnlineTime();
        }

		public override void fetchIdCardInfo(Action<MopubSDKIdCardInfo> success, Action<MopubSDKError> failed){
            MopubCallbackManager.fetchIdCardInfoSuccessDelegate = success;
            MopubCallbackManager.fetchIdCardInfoFailedDelegate = failed;
            _fetchIdCardInfo();
        }

		public override void verifyIdCard(string realName, string cardNumber, Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.verifyIdCardSuccessDelegate = success;
            MopubCallbackManager.verifyIdCardFailedDelegate = failed;
            _verifyIdCard(realName, cardNumber);
        }

		public override void fetchPaidAmount(Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.fetchPaidAmountSuccessDelegate = success;
            MopubCallbackManager.fetchPaidAmountFailedDelegate = failed;
            _fetchPaidAmount();
        }

        // new addiction prevention
        public override void openRealnamePrevention()
        {
            _openRealnamePrevention();
        }

        public override long getPreventionTotalOnlineTime()
        {
            return _getPreventionTotalOnlineTime();
        }

        public override MopubSDKRealnameHeartBeat getRealnameHeartBeat() {
            
            string heartBeatString = _getRealnameHeartBeat();

            MopubSDKRealnameHeartBeat heartBeat = JsonUtility.FromJson<MopubSDKRealnameHeartBeat>(heartBeatString);
			return heartBeat;
        }

        public override void queryRealnameInfo(Action<MopubSDKRealnameInfo> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.queryRealnameInfoSuccessDelegate = success;
            MopubCallbackManager.queryRealnameInfoFailedDelegate = failed;

            _queryRealnameInfo();
        }

        public override void realnameAuthentication(string realname, string cardNumber, Action<MopubSDKRealnameInfo> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.realnameAuthenticationSuccessDelegate = success;
            MopubCallbackManager.realnameAuthenticationFailedDelegate = failed;

            _realnameAuthentication(realname, cardNumber);
        }

        public override void fetchPaidAmountMonthly(int amount, Action<MopubSDKRealnamePaidAmountInfo> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.fetchPaidAmountMonthlySuccessDelegate = success;
            MopubCallbackManager.fetchPaidAmountMonthlyFailedDelegate = failed;

            _fetchPaidAmountMonthly(amount);
        }

        public override void linkWithGameCenter(Action<MopubSDKLinkWithGameCenterResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.linkWithGameCenterSuccessDelegate = success;
            MopubCallbackManager.linkWithGameCenterFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("linkGoogle");
            }
        }

        public override void linkWithFacebook(Action<MopubSDKLinkWithFacebookResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.linkWithFacebookSuccessDelegate = success;
            MopubCallbackManager.linkWithFacebookFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("linkFacebook");
            }
        }

        public override void linkWithWeChat (Action<MopubSDKLinkWithWeChatResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.linkWithWeChatSuccessDelegate = success;
            MopubCallbackManager.linkWithWeChatFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("linkWeChat");
            }
        }

        public override void linkWithEmail (string email, string password, string code, Action<MopubSDKLinkWithEmailResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.linkWithEmailSuccessDelegate = success;
            MopubCallbackManager.linkWithEmailFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("linkEmail", email, password, code);
            }
        }

        public override void linkWithMobile(string phoneNumber, string code, Action<MopubSDKLinkWithMobileResult> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.linkWithMobileSuccessDelegate = success;
            MopubCallbackManager.linkWithMobileFailedDelegate = failed;

            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("linkMobile", phoneNumber, code);
            }
        }

        public override void setUnconsumedItemUpdatedListener(Action<List<MopubSDKPurchasedItem>> unconsumedItemUpdatedListener){
            MopubCallbackManager.unconsumedItemUpdatedListenerDelegate = unconsumedItemUpdatedListener;
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("setUnconsumedItemUpdatedListener");
			}
        }
        public override void fetchPaymentItemDetails(Action<List<MopubSDKPaymentItemDetails>> success, Action<MopubSDKError> failed){
			MopubCallbackManager.fetchPaymentItemDetailsFailedDelegate = failed;
			MopubCallbackManager.fetchPaymentItemDetailsSuccessDelegate = success;
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("fetchPaymentItemDetails");
			}

        }
		
        public override void fetchUnconsumedPurchasedItems(Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed){
			MopubCallbackManager.fetchUnconsumedPurchasedItemSuccessDelegate = success;
			MopubCallbackManager.fetchUnconsumedPurchasedItemFailedDelegate = failed;
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("fetchUnconsumedPurchasedItems");
			}
        }
		
        public override void fetchAllPurchasedItems(Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed){
			MopubCallbackManager.fetchAllPurchasedItemSuccessDelegate = success;
			MopubCallbackManager.fetchAllPurchasedItemFailedDelegate = failed;
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("fetchAllPurchasedItems");
			}
        }
		
        public override void consumePurchase(string sdkOrderID){
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("consumePurchase",sdkOrderID);
			}
        }
        public override void startPayment(MopubSDKStartPaymentInfo paymentInfo,
            Action<MopubSDKPaymentInfo> paymentProcessingWithPaymentInfo,
                                          Action<MopubSDKPaymentInfo, MopubSDKError> paymentFailedWithPaymentInfo){
			MopubCallbackManager.paymentProcessingWithPaymentInfoDelegate = paymentProcessingWithPaymentInfo;
			MopubCallbackManager.paymentFailedWithPaymentInfoDelegate = paymentFailedWithPaymentInfo;
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("startPayment",paymentInfo.itemID, 
				paymentInfo.cpOrderID, 
				paymentInfo.characterName, 
				paymentInfo.characterID, 
				paymentInfo.serverName, 
				paymentInfo.serverID);
			}
		}

        //subscripton
        public override void setSubscriptionItemUpdatedListener(Action<List<MopubSDKSubscriptionItem>> subscriptionItemUpdatedListener)
        {
            MopubCallbackManager.subscriptionItemUpdatedDelegate = subscriptionItemUpdatedListener;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("setSubscriptionIntemUpdatedlistener");
			}
        }

        public override void restoreSubscription(Action success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.restoreSubscriptionSuccessDelegate = success;
            MopubCallbackManager.restoreSubscriptionFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("restoreSubscription");
			}
        }

        public override void fetchAllPurchasedSubscriptionItems(Action<List<MopubSDKSubscriptionItem>> success)
        {
            MopubCallbackManager.fetchAllPurchasedSubscriptionItemsSuccessDelegate = success;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("fetchAllPurchasedSubscriptionItems");
			}
        }

        //AD
        public override bool hasSmartAd(string gameEntry){
            bool isReady = false;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                isReady = sdkClass.CallStatic<bool>("hasSmartAd", gameEntry);
            }
            return isReady;
        }

        public override void showSmartAd(string gameEntry){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("showSmartAd", gameEntry);
            }
        }

        public override void setRewardedVideoListener(MopubRewardedVideoListener listener){
            MopubCallbackManager.rewardedVideoListener = listener;
        }

        public override bool hasRewardedVideo(string gameEntry){
            bool isReady = false;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                isReady = sdkClass.CallStatic<bool>("hasRewardedVideo", gameEntry);
            }
            return isReady;
        }

        public override void showRewardVideoAd(string gameEntry){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("showVideoAd", gameEntry);
            }
        }

        public override void setInterstitialAdListener(MopubInterstitialAdListener listener){
            MopubCallbackManager.interstitialAdListener = listener;
        }

        public override bool hasInterstitial(string gameEntry)
        {
            bool isReady = false;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                isReady = sdkClass.CallStatic<bool>("hasInterstitial", gameEntry);
            }
            return isReady;
        }

        public override void showInterstitialAd(string gameEntry){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("showInterstitialAd", gameEntry);
            }
        }

        public override void showBanner(BannerADPosition position)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("showBanner", (int)position);
            }
        }
        public override void dismissBanner()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("dismissBanner");
            }
        }

        //native ad
        public override void setNativeAdListener(MopubNativeAdListener listener)
        {
            MopubCallbackManager.nativeAdListener = listener;
        }

        public override bool hasNativeAd(string gameEntry)
        {
            bool isReady = false;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                isReady = sdkClass.CallStatic<bool>("hasNativeAd", gameEntry);
            }
            return isReady;
        }

        //If positon=bottomCenter, 'spacing' indicates the distance from the bottom. If  positon=topCenter, 'spacing' indicates the distance from the top
        public override void showNativeAdFixed(string gameEntry, SDKNavtiveAdPosition position, float spacing)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("showNativeAdFixed", gameEntry, (int)position, spacing);
            }
        }

        public override void closeNativeAd(string gameEntry)
        {
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("closeNativeAd", gameEntry);
            }
        }

        //log

        public override void logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID)
        {
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
				sdkClass.CallStatic("logPlayerInfo", characterName, characterID, characterLevel, serverName, serverID);
            }
        }

        public override void logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID, Dictionary<string, string> extraData)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                AndroidJavaObject map = null;
                if (extraData!=null){
                    Debug.Log("static logPlayerInfo data not null");
                    map = CreateJavaMapFromDictainary(extraData);
                }
                else{
                    map = new AndroidJavaObject("java.util.HashMap");
                    Debug.Log("static logPlayerInfo data is null");
                }
                sdkClass.CallStatic("logPlayerInfo", characterName, characterID, characterLevel, serverName, serverID, map);
            }
        }

        public override void logCustomEvent(string eventName, Dictionary<string, string> data){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                AndroidJavaObject map = null;
                if (data!=null){
                    Debug.Log("static logCustomEvent data not null");
                    map = CreateJavaMapFromDictainary(data);
                }
                else{
                    map = new AndroidJavaObject("java.util.HashMap");
                    Debug.Log("static logCustomEvent data is null");
                }

                sdkClass.CallStatic("logCustomEvent", eventName, map);
            }
        }

        public override void logCustomAFEvent (string eventName, Dictionary<string,object> data){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                AndroidJavaObject map = null;
                if (data!=null){
                    Debug.Log("static logCustomAFEvent data not null");
                    map = CreateJavaMapFromDictainary(data);
                }
                else{
                    map = new AndroidJavaObject("java.util.HashMap");
                    Debug.Log("static logCustomAFEvent data is null");
                }

                sdkClass.CallStatic("logCustomAFEvent", eventName, map);
            }
        }

		public override void logStartLevel(string levelName){
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
				sdkClass.CallStatic("logStartLevel", levelName);
            }
		}
		
		public override void logFinishLevel(string levelName){
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
				sdkClass.CallStatic("logFinishLevel", levelName);
            }
		}
		
		public override void logUnlockLevel(string levelName){
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
				sdkClass.CallStatic("logUnlockLevel", levelName);
            }
		}
		
		public override void logSkipLevel(string levelName){
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
				sdkClass.CallStatic("logSkipLevel", levelName);
            }
		}
		
		public override void logSkinUsed(string skinName){
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
				sdkClass.CallStatic("logSkinUsed", skinName);
            }
		}
		
		public override void logAdEntrancePresent(string name){
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
				sdkClass.CallStatic("logAdEntrancePresent", name);
            }
		}

        public static AndroidJavaObject CreateJavaMapFromDictainary(IDictionary<string, string> parameters)
        {
            AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr putMethod = AndroidJNIHelper.GetMethodID(
                javaMap.GetRawClass(), "put",
                    "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (KeyValuePair<string, string> kvp in parameters)
            {

                using (AndroidJavaObject k = new AndroidJavaObject(
                    "java.lang.String", kvp.Key))
                {
                    using (AndroidJavaObject v = new AndroidJavaObject(
                        "java.lang.String", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                                putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }

        public static AndroidJavaObject CreateJavaMapFromDictainary(IDictionary<string, object> parameters)
        {
            AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr putMethod = AndroidJNIHelper.GetMethodID(
                javaMap.GetRawClass(), "put",
                    "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (KeyValuePair<string, object> kvp in parameters)
            {

                using (AndroidJavaObject k = new AndroidJavaObject(
                    "java.lang.String", kvp.Key))
                {
                    using (AndroidJavaObject v = new AndroidJavaObject(
                        "java.lang.Object", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                                putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }

        public override void testLog(string log){
            logCustomEvent(log,null);
        }

        public override bool isUserNotificationEnable()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                return sdkClass.CallStatic<bool>("isUserNotificationEnable");
            }
        }

        public override void openSystemNotificationSetting()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("openSystemNotificationSetting");
            }
        }

        static void _init(string gameContentVersion)
        {
            Debug.Log("static MopubAndroid _init -1");
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                Debug.Log("static MopubAndroid _init -2");
                sdkClass.CallStatic("init",gameContentVersion);
                Debug.Log("static MopubAndroid _init -3");
            }
        }

        static void _callRealNameUI(bool netWorkable)
        {
            Debug.Log("static MopubAndroid _callRealNameUI");
            using(AndroidJavaClass sdkClass = new AndroidJavaClass("com.sdk.plugin.RealNameUnityPlugin"))
            {
                sdkClass.CallStatic("realNameStart",netWorkable);
            }
        }
        static void _showRealNameView()
        {
            Debug.Log("static MopubAndroid _showRealNameView");
            using(AndroidJavaClass sdkClass = new AndroidJavaClass("com.ui.core.plugin.UICoreUnityPlugin"))
            {
                sdkClass.CallStatic("showRealNameView");
            }
        }
        static void _realNameRecharge(int amount)
        {
            Debug.Log("static MopubAndroid _realNameRecharge");
            using(AndroidJavaClass sdkClass = new AndroidJavaClass("com.sdk.plugin.RealNameUnityPlugin"))
            {
                sdkClass.CallStatic("realNameRecharge",amount);
            }
        }
        static void _login()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("login");
            }
        }

        static void _logout()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("logout");
            }
        }

        static void _loginWithDevice(string activeCode)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("loginWithDevice", activeCode);
            }
        }

        static void _loginWithWeChat(string activeCode)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("loginWithWeChat", activeCode);
            }
        }

        static void _loginWithMobile(string phoneNumber, string code, string activeCode,bool isOneKeyLogin)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("loginWithMobile", phoneNumber, code, activeCode,isOneKeyLogin);
            }
        }

        static void _loginWithVisitor(string activeCode)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("loginWithVisitor", activeCode);
            }
        }

        static void _fetchMobileAuthCode(string phoneNumber)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("fetchMobileAuthCode", phoneNumber);
            }
        }

        static void _loginWithEmail(string email, string password, string code, string activeCode)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("loginWithEmail", email, password, code, activeCode);
            }
        }

        static void _fetchEmailAuthCode(string email)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("fetchEmailAuthCode", email);
            }
        }

        static void _resetPasswordWithEmail(string email, string password, string code)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("resetPasswordWithEmail", email, password, code);
            }
        }

        static void _showLoginUI(string activeCode){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.sdk.engine.LoginUnityPlugin"))
            {
                sdkClass.CallStatic("showLoginUI", activeCode);
            }
        }


        static string _currentAccessToken(){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                return sdkClass.CallStatic<string>("currentAccessToken");
            }
        }

        static void _verifySessionToken(string token){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("verifySessionToken", token);
            }
        }

        static void _createInviteCode(){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("createInviteCode");
            }
        }

        static void _fetchInviteeList(){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("fetchInviteeList");
            }
        }

        static void _uploadInviteCode(string code){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("uploadInviteCode", code);
            }
        }

        // old addiction prevention
        static void _openPrevention(){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("openPrevention");
            }
        }

        static long _getTotalOnlineTime(){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                return sdkClass.CallStatic<long>("getTotalOnlineTime");
            }
        }

        static void _fetchIdCardInfo(){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("fetchIdCardInfo");
            }
        }

        static void _verifyIdCard(string realName, string cardNumber){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("verifyIdCard", realName, cardNumber);
            }
        }

        static void _fetchPaidAmount(){
            // using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            // {
            //     sdkClass.CallStatic("fetchPaidAmount");
            // }
        }

        // new addiction prevention
        static void _openRealnamePrevention()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("openRealnamePrevention");
            }
        }

        static long _getPreventionTotalOnlineTime()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                return sdkClass.CallStatic<long>("getPreventionTotalOnlineTime");
            }
        }

        static string _getRealnameHeartBeat()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                return sdkClass.CallStatic<string>("getRealnameHeartBeat");
            }
        }

        static void _queryRealnameInfo()
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("queryRealnameInfo");
            }
        }

        static void _realnameAuthentication(string realname, string cardNumber)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("realnameAuthentication", realname, cardNumber);
            }
        }

        static void _fetchPaidAmountMonthly(int amount)
        {
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("fetchPaidAmountMonthly", amount);
            }
        }

        public override void setIngameParamsUpdatedListener (Action<Dictionary<String, String >> ingameParamsUpdatedListener){
			MopubCallbackManager.setIngameParamsUpdatedListenerDelegate (ingameParamsUpdatedListener);
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("getInGameOnlineParams");
			}
		}

        public override void setAdvertisingIngameParamsUpdatedListener(Action<Dictionary<String, String>> ADIngameParamsUpdatedListener)
        {
            MopubCallbackManager.setAdvertisingIngameParamsUpdatedListenerDelegate(ADIngameParamsUpdatedListener);
        }
		
		public override void setAFDataUpdatedListener(Action<Dictionary<String, String>> afDataUpdatedListener){
			MopubCallbackManager.setAFDataUpdatedListenerDelegate(afDataUpdatedListener);
			using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				sdkClass.CallStatic("getAFInstallConversionData");
			}
		}
		
        public override bool openJoinChatGroup(){
            bool res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<bool>("openJoinChatGroup");
			}
            return res;
        }

        public override bool openJoinWhatsappChatting()
        {
            bool res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<bool>("openJoinWhatsappChatting");
			}
            return res;
        }

        public override bool openRatingView(){
			bool res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<bool>("openRatingView");
			}
            return res;
		}

        public override bool openRate()
        {
            bool res;
            using(AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                res = sdkClass.CallStatic<bool>("openRating");
            }
            return res;
        }

        public override void setOnPushInitCallback(Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.pushInitSuccessDelegate = success;
            MopubCallbackManager.pushInitFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("setOnPushInitCallback");
            }
        }

		public override void bindAccounts(Dictionary<int, string> accountsType, Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.bindAccountsSuccessDelegate = success;
            MopubCallbackManager.bindAccountsFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                AndroidJavaObject map = null;
                if (accountsType!=null){
                    Debug.Log("static bindAccounts data not null");
                    map = CreateJavaMapFromDictainary2(accountsType);
                }
                else{
                    map = new AndroidJavaObject("java.util.HashMap");
                    Debug.Log("static bindAccounts data is null");
                }
                sdkClass.CallStatic("bindAccounts", map);
            }
        }

        public static AndroidJavaObject CreateJavaMapFromDictainary2(IDictionary<int, string> parameters)
        {
            AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr putMethod = AndroidJNIHelper.GetMethodID(
                javaMap.GetRawClass(), "put",
                    "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (KeyValuePair<int, string> kvp in parameters)
            {

                using (AndroidJavaObject k = new AndroidJavaObject(
                    "java.lang.Integer", kvp.Key))
                {
                    using (AndroidJavaObject v = new AndroidJavaObject(
                        "java.lang.String", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                                putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }
		
		public override bool addLocalNotification(MopubSDKLocalMsg localMsg){
			bool res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<bool>("addLocalNotification",localMsg.title,
						localMsg.content,
						localMsg.date,
						localMsg.hour,
						localMsg.min);
			}
            return res;
		}

        public override string getAppDistributor(){
            string res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<string>("getAppDistributor");
			}
            return res;
        }

        public override string getPuid() {
            string res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<string>("getPuid");
			}
            return res;
        }

        public override string getRegion() {
            string res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<string>("getRegion");
			}
            return res;
		}
        public override string getTimeStamp() {
            string res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<string>("getTimeStamp");
			}
            return res;
		}

        public override string getGameVersion()
        {
           string res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
			{
				res = sdkClass.CallStatic<string>("getVersion");
			}
            return res;
        }

        public override string getPackageVersionCode()
        {
            string res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                res = sdkClass.CallStatic<string>("getVersionCode");
            }
            return res;
        }

        public override string getCgi()
        {
            string res;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                res = sdkClass.CallStatic<string>("getCgi");
            }
            return res;
        }

        public override void launchCustomer(){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("launchCustomer");
            }
        }

        public override void launchCustomerWithTransitPage(string pageType, string email){
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("launchCustomerWithTransitPage", pageType, email);
            }
        }

        public override void setCustomerUnreadMessageListener(Action<Dictionary<String, int>> unreadMessageUpdatedListener){
            MopubCallbackManager.setUnreadMessageUpdatedListenerDelegate(unreadMessageUpdatedListener);
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("setCustomerUnreadMessageListener");
            }
        }

        public override void fetchRanking(string uid, int page, int size, Action<MopubSDKRanking> success, Action<MopubSDKError> failure)
        {
            MopubCallbackManager.fetchRankingSuccessDelegate = success;
            MopubCallbackManager.fetchRankingFailedDelegate = failure;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("fetchRanking", uid, page, size);
            }
        }

        public override void saveCloudCache(string uid, long version, string data, Action success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.saveCloudCacheSuccessDelegate = success;
            MopubCallbackManager.saveCloudCacheFailedDelegate = failed;
            using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("saveCloudFile", uid, version, data);
            }

        }

        public override void getCloudCache(string uid, Action<MopubSDKCloudCache> success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.getCloudCacheSuccessDelegate = success;
            MopubCallbackManager.getCloudCacheFailedDelegate = failed;
             using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("getCloudFile", uid);
            }

        }

        public override void getRedeem(string code,Action<string> success,Action<MopubSDKError> failed)
        {
            MopubCallbackManager.getRedeemSuccessDelegate = success;
            MopubCallbackManager.getRedeemFailDelegate = failed;
             using (AndroidJavaClass sdkClass = new AndroidJavaClass("com.mopub.MopubUnityPlugin"))
            {
                sdkClass.CallStatic("getRedeemData", code);
            }
        }
        public override void showGuidPageView(Action<string> success){
            Debug.Log("static MopubAndroid showGuidPageView");
            MopubCallbackManager.showGuidPageViewSuccessDelegate = success;
            using(AndroidJavaClass sdkClass = new AndroidJavaClass("com.ui.core.plugin.UICoreUnityPlugin"))
            {
                sdkClass.CallStatic("showGuidPageView");
            }
        }

    }

}