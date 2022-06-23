using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Runtime.InteropServices;
using MopubNS.ThirdParty.MiniJSON;
using MopubNS;



namespace MopubNS
{

#if UNITY_IOS
	public class MopubiOS:MopubBase
	{

		static MopubiOS(){
			Debug.Log ("static MopubiOS");
#if UNITY_IOS
			InitCallbackManager ();
#endif

		}

		public override void init (Action<InitSuccessResult> success, Action<MopubSDKError> failed) {
			MopubCallbackManager.initSuccessDelegate = success;
			MopubCallbackManager.initFailedDelegate = failed;
			_init (null);
		}

		public override void init (string gameContentVersion, Action<InitSuccessResult> success, Action<MopubSDKError> failed){
			MopubCallbackManager.initSuccessDelegate = success;
			MopubCallbackManager.initFailedDelegate = failed;
			_init (gameContentVersion);
		}
	//实名认证页面sdk--new
        public override void showRealNameView(Action<string> success,Action<string> failed)
                {
                        Debug.Log("static MopubAndroid showRealNameView");
						MopubCallbackManager.showRealNameViewSuccessDelegate = success;
						MopubCallbackManager.showRealNameViewFailedDelegate = failed;
						 _startRealnameAuthenticationWithUI();
                 }
    //实名认证页面sdk
    	public override void callRealNameUI(bool netWorkable,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
                {
                        Debug.Log("static MopubAndroid callRealNameUI");
                        MopubCallbackManager.realNameUISuccessDelegate = success;
                        MopubCallbackManager.realNameUIFailedDelegate = failed;
                        MopubCallbackManager.realNameUIFailedErrorDelegate = failederror;
                        _startRealnameAuthenticationWithUI();
                 }
	

             //反成谜支付页面sdk
       public override void realNameRecharge(int amount,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
       {
       Debug.Log("static MopubAndroid realNameRecharge");
       MopubCallbackManager.realNameUISuccessDelegate = success;
       MopubCallbackManager.realNameUIFailedDelegate = failed;
       MopubCallbackManager.realNameUIFailedErrorDelegate = failederror;
       }

                public override void showLoginUI(Action<LoginSuccessResult> success, Action<MopubSDKError> failed,string activeCode){
            MopubCallbackManager.loginUISuccessDelegate = success;
            MopubCallbackManager.loginUIFailedDelegate = failed;
        }

		public override void login (Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
			MopubCallbackManager.loginSuccessDelegate = success;
			MopubCallbackManager.loginFailedDelegate = failed;
			_login ();
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

		public override void fetchMobileAuthCode(string phoneNumber, Action<FetchSuccessResult> success, Action<MopubSDKError> failed){
			MopubCallbackManager.fetchMobileAuthCodeSuccessDelegate = success;
			MopubCallbackManager.fetchMobileAuthCodeFailedDelegate = failed;

			_fetchMobileAuthCode(phoneNumber);
		}

		public override void loginWithMobile(string phoneNumber, string code, bool isOneKeyLogin, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
			MopubCallbackManager.loginMobileSuccessDelegate = success;
			MopubCallbackManager.loginMobileFailedDelegate = failed;

			_loginWithMobile(isOneKeyLogin, phoneNumber, code, null);
		}

		public override void loginWithMobile(string phoneNumber, string code, string activeCode, bool isOneKeyLogin, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
			MopubCallbackManager.loginMobileSuccessDelegate = success;
			MopubCallbackManager.loginMobileFailedDelegate = failed;

			_loginWithMobile(isOneKeyLogin, phoneNumber, code, activeCode);
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

		public override void resetPasswordWithEmail(string email, string password, string code, Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.resetPasswordWithEmailSuccessDelegate = success;
            MopubCallbackManager.resetPasswordWithEmailFailedDelegate = failed;

			_resetPasswordWithEmail(email, password, code);
        }

        public override MopubSdkAccessToken currentAccessToken () {
			String tokenJSON = _currentAccessToken ();
			Debug.Log (tokenJSON);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken> (tokenJSON);
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

		public override void linkWithGameCenter (Action<MopubSDKLinkWithGameCenterResult> success, Action<MopubSDKError> failed) {
			MopubCallbackManager.linkWithGameCenterSuccessDelegate = success;
			MopubCallbackManager.linkWithGameCenterFailedDelegate = failed;
			_linkWithGameCenter ();
		}

		public override void linkWithFacebook(Action<MopubSDKLinkWithFacebookResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.linkWithFacebookSuccessDelegate = success;
            MopubCallbackManager.linkWithFacebookFailedDelegate = failed;
            
			_linkWithFacebook ();
        }

		public override void linkWithWeChat(Action<MopubSDKLinkWithWeChatResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.linkWithWeChatSuccessDelegate = success;
            MopubCallbackManager.linkWithWeChatFailedDelegate = failed;
            
			_linkWithWeChat ();
        }

		public override void linkWithEmail (string email, string password, string code, Action<MopubSDKLinkWithEmailResult> success, Action<MopubSDKError> failed){
            MopubCallbackManager.linkWithEmailSuccessDelegate = success;
            MopubCallbackManager.linkWithEmailFailedDelegate = failed;
			_linkWithEmail(email, password, code);
        }

		public override void linkWithMobile(string phoneNumber, string code, Action<MopubSDKLinkWithMobileResult> success, Action<MopubSDKError> failed)
        {
			MopubCallbackManager.linkWithMobileSuccessDelegate = success;
			MopubCallbackManager.linkWithMobileFailedDelegate = failed;

			_linkWithMobile(phoneNumber, code);
        }

		public override void setUnconsumedItemUpdatedListener (Action<List<MopubSDKPurchasedItem>> unconsumedItemUpdatedListener) {
			MopubCallbackManager.unconsumedItemUpdatedListenerDelegate = unconsumedItemUpdatedListener;
			_setUnconsumedItemUpdatedListener ();
		}
		public override void fetchPaymentItemDetails (Action<List<MopubSDKPaymentItemDetails>> success, Action<MopubSDKError> failed) {
			MopubCallbackManager.fetchPaymentItemDetailsFailedDelegate = failed;
			MopubCallbackManager.fetchPaymentItemDetailsSuccessDelegate = success;
			_fetchPaymentItemDetails ();
		}
		public override void fetchUnconsumedPurchasedItems (Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed) {
			MopubCallbackManager.fetchUnconsumedPurchasedItemSuccessDelegate = success;
			MopubCallbackManager.fetchUnconsumedPurchasedItemFailedDelegate = failed;
			_fetchUnconsumedPurchasedItems ();
		}
		public override void fetchAllPurchasedItems (Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed) {
			MopubCallbackManager.fetchAllPurchasedItemSuccessDelegate = success;
			MopubCallbackManager.fetchAllPurchasedItemFailedDelegate = failed;
			_fetchAllPurchasedItems ();
		}

		public override void consumePurchase (string sdkOrderID) {
			_consumePurchase (sdkOrderID);
		}
		public override void startPayment (MopubSDKStartPaymentInfo paymentInfo, 
			Action<MopubSDKPaymentInfo> paymentProcessingWithPaymentInfo,
			Action<MopubSDKPaymentInfo, MopubSDKError> paymentFailedWithPaymentInfo) {
			MopubCallbackManager.paymentProcessingWithPaymentInfoDelegate = paymentProcessingWithPaymentInfo;
			MopubCallbackManager.paymentFailedWithPaymentInfoDelegate = paymentFailedWithPaymentInfo;
			_startPayment (
				paymentInfo.itemID, 
				paymentInfo.cpOrderID, 
				paymentInfo.characterName, 
				paymentInfo.characterID, 
				paymentInfo.serverName, 
				paymentInfo.serverID);
		}

        //subscripton
        public override void setSubscriptionItemUpdatedListener(Action<List<MopubSDKSubscriptionItem>> subscriptionItemUpdatedListener)
        {
            MopubCallbackManager.subscriptionItemUpdatedDelegate = subscriptionItemUpdatedListener;
            _setSubscriptionItemUpdatedListener();
        }

        public override void restoreSubscription(Action success, Action<MopubSDKError> failed)
        {
            MopubCallbackManager.restoreSubscriptionSuccessDelegate = success;
            MopubCallbackManager.restoreSubscriptionFailedDelegate = failed;
            _restoreSubscription();
        }

        public override void fetchAllPurchasedSubscriptionItems(Action<List<MopubSDKSubscriptionItem>> success)
        {
            MopubCallbackManager.fetchAllPurchasedSubscriptionItemsSuccessDelegate = success;
            _fetchAllPurchasedSubscriptionItems();
        }



        //ad
        public override void setRewardedVideoListener (MopubRewardedVideoListener listener) {
			MopubCallbackManager.rewardedVideoListener = listener;
		}

		public override bool hasRewardedVideo(string gameEntry) {
			return _hasRewardedVideo(gameEntry);
		}

		public override void showRewardVideoAd(string gameEntry) {
			_showRewardVideoAd (gameEntry);
		}

		public override void setInterstitialAdListener (MopubInterstitialAdListener listener) {
			MopubCallbackManager.interstitialAdListener = listener;
		}

		public override bool hasInterstitial(string gameEntry) {
			return _hasInterstitial(gameEntry);
		}

		public override void showInterstitialAd(string gameEntry) {
			_showInterstitialAd (gameEntry);
		}

        public override void showBanner(BannerADPosition position)
        {
            _showBanner((int)position);
        }
        public override void dismissBanner()
        {
            _dismissBanner();
        }

        //native ad

        public override void setNativeAdListener(MopubNativeAdListener listener)
        {
            MopubCallbackManager.nativeAdListener = listener;
        }
        public override bool hasNativeAd(string gameEntry)
        {
            return _hasNativeAd(gameEntry);
        }

		public override bool hasSmartAd(string gameEntry){
            bool isReady = false;
            
            return isReady;
        }

        public override void showSmartAd(string gameEntry){
            
        }
        //If positon=bottomCenter, 'spacing' indicates the distance from the bottom. If  positon=topCenter, 'spacing' indicates the distance from the top
        public override void showNativeAdFixed(string gameEntry, SDKNavtiveAdPosition positon, float spacing)
        {
            _showNativeAdFixed(gameEntry, (int)positon, spacing);
        }

        public override void closeNativeAd(string gameEntry)
        {
            _closeNativeAd(gameEntry);
        }

        //log

        public override void logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID)
        {
            _logPlayerInfo(characterName, characterID, characterLevel, serverName, serverID);
        }


        public override void logCustomEvent (string eventName,  Dictionary<string,string> data) {
			_logCustomEvent (eventName, Json.Serialize (data));
		}

		public override void logCustomAFEvent (string eventName,  Dictionary<string,object> data) {
			_logCustomAFEvent(eventName, Json.Serialize(data));
		}
		
		public override void logStartLevel(string levelName){
            _logStartLevel(levelName);
		}
		
		public override void logFinishLevel(string levelName){
            _logFinishLevel(levelName);
		}
		
		public override void logUnlockLevel(string levelName){
            _logUnlockLevel(levelName);
		}
		
		public override void logSkipLevel(string levelName){
            _logSkipLevel(levelName);
		}
		
		public override void logSkinUsed(string skinName){
            _logSkinUsed(skinName);
		}
		
		public override void logAdEntrancePresent(string name){
            _logAdEntrancePresent(name);
		}

		public override void testLog (string log) {
			_logNative (log);
		}

		public override void setIngameParamsUpdatedListener (Action<Dictionary<String, String>> ingameParamsUpdatedListener){
			MopubCallbackManager.setIngameParamsUpdatedListenerDelegate(ingameParamsUpdatedListener);
			_setIngameParamsListener ();
		}

        public override void setAdvertisingIngameParamsUpdatedListener(Action<Dictionary<String, String>> ADIngameParamsUpdatedListener)
        {
            MopubCallbackManager.setAdvertisingIngameParamsUpdatedListenerDelegate(ADIngameParamsUpdatedListener);
            //_setADIngameParamsListener();
        }

		public override void setAFDataUpdatedListener(Action<Dictionary<String, String>> afDataUpdatedListener)
		{
			MopubCallbackManager.setAFDataUpdatedListenerDelegate(afDataUpdatedListener);
			_setAFDataDelegate();
		}

        public override bool openJoinChatGroup()
        {
            return _openJoinChatGroup();
        }

        public override bool openJoinWhatsappChatting()
        {
			return _openJoinWhatsappChatting();
        }

        public override bool openRatingView(){
			return _openRatingView();
		}

		public override bool openRate(){
			return _openRate();
		}
		
		public override bool addLocalNotification(MopubSDKLocalMsg localMsg)
		{
			return _addLocalNotifaciton(localMsg.title, localMsg.content, localMsg.date, localMsg.hour, localMsg.min);
		}

		public override void setOnPushInitCallback(Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.pushInitSuccessDelegate = success;
            MopubCallbackManager.pushInitFailedDelegate = failed;
            
        }

		public override void bindAccounts(Dictionary<int, string> accountsType, Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.bindAccountsSuccessDelegate = success;
            MopubCallbackManager.bindAccountsFailedDelegate = failed;
            
        }

		public override string getAppDistributor(){
            return null;
        }

		public override string getPuid() {
			return _getPuid();
		}

		public override string getRegion() {
			return _getRegion();
		}


		public override string getGameVersion()
		{
			return _getGameVersion();
		}
		public override string getTimeStamp() {
			return _getTimeStamp();
		}


		public override void launchCustomer(){
			_launchCustomer();

		}

		public override void launchCustomerWithTransitPage(string pageType, string email){
			_launchCustomerWithTransitPageEmail(pageType, email);
        }

		public override void setCustomerUnreadMessageListener(Action<Dictionary<String, int>> unreadMessageUpdatedListener){
            MopubCallbackManager.setUnreadMessageUpdatedListenerDelegate(unreadMessageUpdatedListener);

			_setUnreadMessageUpdatedListener();

		}

		public override bool isUserNotificationEnable()
        {
			return _isUserNotificationEnable();
        }

		public override void openSystemNotificationSetting()
        {
			_openSystemNotificationSetting();
        }

		public override void fetchRanking(string uid, int page, int size, Action<MopubSDKRanking> success, Action<MopubSDKError> failure)
        {
			MopubCallbackManager.fetchRankingSuccessDelegate = success;
			MopubCallbackManager.fetchRankingFailedDelegate = failure;

			_fetchRanking(uid, page, size);
        }

		#region dllimport

		[DllImport("__Internal")]
		internal extern static void _init (string gameContentVersion);

		[DllImport("__Internal")]
		internal extern static void _login ();

        [DllImport("__Internal")]
        internal extern static void _startRealnameAuthenticationWithUI();

		[DllImport("__Internal")]
		internal extern static void _loginWithDevice(string activeCode);

		[DllImport("__Internal")]
		internal extern static void _loginWithVisitor(string activeCode);

		[DllImport("__Internal")]
		internal extern static void _loginWithWeChat(string activeCode);

		[DllImport("__Internal")]
		internal extern static void _fetchMobileAuthCode(string phoneNumber);

		[DllImport("__Internal")]
		internal extern static void _loginWithMobile(bool isOneKeyLogin, string phoneNumber, string authCode, string activeCode);

		[DllImport("__Internal")]
		internal extern static void _linkWithMobile(string phoneNumber, string authCode);

		[DllImport("__Internal")]
		internal extern static void _fetchEmailAuthCode(string email);

		[DllImport("__Internal")]
		internal extern static void _loginWithEmail(string email, string password, string authCode, string code);

		[DllImport("__Internal")]
		internal extern static void _linkWithEmail(string email, string password, string authCode);

		[DllImport("__Internal")]
		internal extern static void _resetPasswordWithEmail(string email, string password, string authCode);

		[DllImport("__Internal")]
		internal extern static void _setGameUserID (string gameUserID);

		[DllImport("__Internal")]
		internal extern static string _currentAccessToken ();

		[DllImport("__Internal")]
		internal extern static void _verifySessionToken(string sessionToken);

		[DllImport("__Internal")]
		internal extern static void _createInviteCode();

		[DllImport("__Internal")]
		internal extern static void _fetchInviteeList();

		[DllImport("__Internal")]
		internal extern static void _uploadInviteCode(string inviteCode);

        // old addiction prevention
		[DllImport("__Internal")]
		internal extern static void _openPrevention();

		[DllImport("__Internal")]
		internal extern static void _fetchIdCardInfo();

		[DllImport("__Internal")]
		internal extern static long _getTotalOnlineTime();

		[DllImport("__Internal")]
		internal extern static void _verifyIdCard(string name, string cardNumber);

		[DllImport("__Internal")]
		internal extern static void _fetchPaidAmount();

		// new addiction prevention
		[DllImport("__Internal")]
		internal extern static void _openRealnamePrevention();

		[DllImport("__Internal")]
		internal extern static void _fetchPaidAmountMonthly(int amount);

		[DllImport("__Internal")]
		internal extern static long _getPreventionTotalOnlineTime();

		[DllImport("__Internal")]
		internal extern static string _getRealnameHeartBeat();

		[DllImport("__Internal")]
		internal extern static void _realnameAuthentication(string name, string cardNumber);

		[DllImport("__Internal")]
		internal extern static void _queryRealnameInfo();

		[DllImport("__Internal")]
        internal extern static string _getPuid();

		[DllImport("__Internal")]
		internal extern static void _linkWithGameCenter ();

		[DllImport("__Internal")]
		internal extern static void _linkWithFacebook ();

		[DllImport("__Internal")]
		internal extern static void _linkWithWeChat ();

		[DllImport("__Internal")]
		internal extern static void _logCustomEvent (string eventName, string dataJSON);

		[DllImport("__Internal")]
		internal extern static void _logCustomAFEvent(string eventName, string dataJSON);

		///ad
		[DllImport("__Internal")]
		internal extern static bool _hasRewardedVideo(string gameEntry);

		[DllImport("__Internal")]
		internal extern static void _showRewardVideoAd(string gameEntry);

		[DllImport("__Internal")]
		internal extern static bool _hasInterstitial(string gameEntry);

		[DllImport("__Internal")]
		internal extern static void _showInterstitialAd(string gameEntry);

		///payment
		[DllImport("__Internal")]
		internal extern static void _fetchPaymentItemDetails();

		[DllImport("__Internal")]
		internal extern static void _fetchUnconsumedPurchasedItems();

		[DllImport("__Internal")]
		internal extern static void _fetchAllPurchasedItems();

		[DllImport("__Internal")]
		internal extern static void _consumePurchase(string sdkOrderID);

		[DllImport("__Internal")]
		internal extern static void _setUnconsumedItemUpdatedListener();

		[DllImport("__Internal")]
		internal extern static void _startPayment(string itemID, string cpOrderID, string characterName, string characterID, string serverName, string serverID);

		[DllImport("__Internal")]
		internal extern static void _logNative(string log);

		///ingame 
		[DllImport("__Internal")]
		internal extern static void _setIngameParamsListener();

        [DllImport("__Internal")]
        internal extern static void _setADIngameParamsListener();

        ///banner
        [DllImport("__Internal")]
        internal extern static void _showBanner(int position);

        [DllImport("__Internal")]
        internal extern static void _dismissBanner();

        [DllImport("__Internal")]
        internal extern static void _setSubscriptionItemUpdatedListener();

        [DllImport("__Internal")]
        internal extern static void _restoreSubscription();

        [DllImport("__Internal")]
        internal extern static void _fetchAllPurchasedSubscriptionItems();

        [DllImport("__Internal")]
        internal extern static bool _openJoinChatGroup();

		[DllImport("__Internal")]
		internal extern static bool _openJoinWhatsappChatting();

		[DllImport("__Internal")]
        internal extern static bool _hasNativeAd(string gameEntry);

        [DllImport("__Internal")]
        internal extern static void _showNativeAdFixed(string gameEntry, int position, float spacing);

        [DllImport("__Internal")]
        internal extern static void _closeNativeAd(string gameEntry);

        [DllImport("__Internal")]
        internal extern static void _logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID);

        [DllImport("__Internal")]
        internal extern static bool _openRatingView();

		[DllImport("__Internal")]
		internal extern static void _openReate();
		
        [DllImport("__Internal")]
        internal extern static bool _addLocalNotifaciton(string title, string content, string date, string hour, string min);


        [DllImport("__Internal")]
        internal extern static void _logStartLevel(string levelName);

        [DllImport("__Internal")]
        internal extern static void _logFinishLevel(string levelName);

        [DllImport("__Internal")]
        internal extern static void _logUnlockLevel(string levelName);

        [DllImport("__Internal")]
        internal extern static void _logSkipLevel(string levelName);

        [DllImport("__Internal")]
        internal extern static void _logSkinUsed(string skinName);

        [DllImport("__Internal")]
        internal extern static void _logAdEntrancePresent(string name);

        [DllImport("__Internal")]
        internal extern static void _setAFDataDelegate();

        [DllImport("__Internal")]
        internal extern static string _getRegion();


		[DllImport("__Internal")]
		internal extern static string _getGameVersion();

		[DllImport("__Internal")]
		internal extern static string _getTimeStamp();

		[DllImport("__Internal")]
		internal extern static void _launchCustomer();

		[DllImport("__Internal")]
		internal extern static void _launchCustomerWithTransitPageEmail(string pageType, string emial);

		[DllImport("__Internal")]
		internal extern static void _logout();

		[DllImport("__Internal")]
		internal extern static void _setUnreadMessageUpdatedListener();

		[DllImport("__Internal")]
		internal extern static bool _isUserNotificationEnable();

		[DllImport("__Internal")]
		internal extern static void _openSystemNotificationSetting();

		[DllImport("__Internal")]
		internal extern static void _fetchRanking(string uid, int page, int size);
		#endregion
	}
#endif

}