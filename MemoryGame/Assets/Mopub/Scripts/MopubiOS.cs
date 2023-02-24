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

		public override void addEventGlobalParams(Dictionary<string, string> data)
		{
			_addEventGlobalParams(Json.Serialize(data));
		}

		//引导页
		public override void showGuidPageView(Action<string> success){
			if (success != null)
            {
				success("success");
			}
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

		// 展示绑定账号界面
		public override void showLinkAccountView(Action success)
        {
			MopubCallbackManager.linkAccountViewSuccessDelegate = success;

			_showLinkAccountView();
		}

		// 展示切换账号界面
		public override void showSwitchAccountView(Action<MopubSdkAccessToken> success)
        {
			MopubCallbackManager.switchAccountSuccessDelegate = success;

			_showSwitchAccountView();
		}

		// 展示删除账号界面
		public override void showDeleteAccountView(Action success)
        {
			MopubCallbackManager.deleteAccountSuccessDelegate = success;

			_showDeleteAccountView();
        }
		
		public override bool isAccountOnlyTourist(){
			return _isAccountOnlyTourist();
		}
		
		public override void showAccountCenter(Action linkSuccess,Action deleteSuccess,Action<MopubSdkAccessToken> success,Action<string> userVerify){
			MopubCallbackManager.linkAccountViewSuccessDelegate = linkSuccess;
            MopubCallbackManager.switchAccountSuccessDelegate = success;
            MopubCallbackManager.deleteAccountSuccessDelegate = deleteSuccess;
			MopubCallbackManager.userVerifyDelegate = userVerify;
			_showAccountCenter();
		}
		public override void showArchive(Dictionary<string,object> data)
		{
			_showArchive(Json.Serialize(data));
		}
		
		public override void showLinkedAfterPurchaseView(){
			_showLinkedAfterPurchaseView();
		}

		public override void setLanguage(string language)
		{
			_setLanguage(language);
		}

		public override void openNoticeDialog()
		{
			_openNoticeDialog();
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

		public override void autoLoginTouristWithUI(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
        {
			MopubCallbackManager.autoLoginTouristWithUISuccessDelegate = success;
			MopubCallbackManager.autoLoginTouristWithUIFailedDelegate = failed;

			_autoLoginTouristWithUI();

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
		public override void reLoginFlow(Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
			MopubCallbackManager.reLoginFlowSuccessDelegate = success;
			MopubCallbackManager.reLoginFlowFailedDelegate = failed;
			_reLoginFlow();
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
				paymentInfo.serverID,
				paymentInfo.level);
		}

		public override void startLinkagePayment(Dictionary<string,string> extraData){}

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

		public override void logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID, Dictionary<string, string> extraData)
		{
			_logPlayerInfoWithData(characterName, characterID, characterLevel, serverName, serverID, Json.Serialize(extraData));
		}

		public override void logCustomEvent (string eventName,  Dictionary<string,object> data) {
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

		public override void setATTDataUpdatedListener(Action<Dictionary<String, object>> attDataUpdatedListener)
		{
			MopubCallbackManager.setATTDataUpdatedListenerDelegate(attDataUpdatedListener);
			
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
		public override bool openRate(string shop){
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

		public override string getPackageVersionCode()
		{
			return _getPackageVersionCode();
		}

		public override string getCgi()
		{
			return _getCgi();
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

		public override void saveCloudCache(string uid, long version, string data, Action success, Action<MopubSDKError> failed)
		{
			MopubCallbackManager.saveCloudCacheSuccessDelegate = success;
			MopubCallbackManager.saveCloudCacheFailedDelegate = failed;

			_saveCloudCache(uid, version, data);
		}

		public override void getCloudCache(string uid, Action<MopubSDKCloudCache> success, Action<MopubSDKError> failed)
		{
			MopubCallbackManager.getCloudCacheSuccessDelegate = success;
			MopubCallbackManager.getCloudCacheFailedDelegate = failed;

			_getCloudCache(uid);
		}
		public override void getRedeem(string code,Action<string> success,Action<MopubSDKError> failed)
        {
            MopubCallbackManager.getRedeemSuccessDelegate = success;
            MopubCallbackManager.getRedeemFailDelegate = failed;

			_getRedeem(code);
        }
		public override string getActivateCode(){
			return "";
		}

		// 初始化推送sdk
		public override void initPushSDK(string cgi, string appid, string appSecret, bool passThroughEnable, string logHost, string logPath, Action<string> clickCallback, Action<string> receiveMessageCallback)
        {
			MopubCallbackManager.pushDidClickDelegate = clickCallback;
			MopubCallbackManager.pushReceiveMessageDelegate = receiveMessageCallback;

			_initPushSDK(cgi, appid, appSecret, passThroughEnable, logHost, logPath);
        }

		// 设置推送别名
		public override void setPushAlias(string alias)
        {
			_setPushAlias(alias);
        }

		// 监听包更新回调
		public override void setPackageUpdatedListener(Action success)
        {
			MopubCallbackManager.packageUpdatedSuccessDelegate = success;
        }
		public override string getAppSignMD5(){
			return "";
		}
		public override string getAppSignSHA1(){
			return "";
		}
		public override string getAppSignSHA256(){
			return "";
		}

		public override void requestPermission(MopubPermissionsInfo[] permissionsInfo, Action finish, Action cancel){
        }

		public override Dictionary<string, object> getCpParams()
        {
			string paramsJson = _getCpParams();
			Dictionary<string, object> dic = null;
			if (paramsJson != null)
            {
				dic = (Dictionary<string, object>)Json.Deserialize(paramsJson);
			}
			else
            {
				dic = new Dictionary<string, object>();
			}

			return dic;
        }

		// 分享到微信
		public override void openShareToWechat(string entrance, MopubWechatSharedData sharedData, Action success, Action<MopubSDKError> failure)
		{
			MopubCallbackManager.appshareSuccessDelegate = success;
			MopubCallbackManager.appshareFailureDelegate = failure;

			string dataJson = JsonUtility.ToJson(sharedData);

			_openShareToWechat(entrance, dataJson);
		}

		public override void openShareToQQ(string entrance, MopubQQSharedData sharedData, Action success, Action<MopubSDKError> failure)
		{
			MopubCallbackManager.appshareSuccessDelegate = success;
			MopubCallbackManager.appshareFailureDelegate = failure;

			string dataJson = JsonUtility.ToJson(sharedData);

			_openShareToQQ(entrance, dataJson);
		}

		public override MopubAppSharedCampaignInfo getSharedCampaignInfo(){

			string res;

			res = _getCurrentSharedCampaign();

			MopubAppSharedCampaignInfo info;
			if (res != null)
            {
				info = JsonUtility.FromJson<MopubAppSharedCampaignInfo>(res);
			}
			else
			{
				info = new MopubAppSharedCampaignInfo(MopubAppSharedCampaignType.share, "unkonwn", 0, 0);
			}


			return info;

        }

        public override MopubAppSharedCampaignInfo getInvitedCampaignInfo(){
			string res;

			res = _getCurrentInvitedCampaign();

			MopubAppSharedCampaignInfo info;
			if (res != null)
			{
				info = JsonUtility.FromJson<MopubAppSharedCampaignInfo>(res);
			}
			else
			{
				info = new MopubAppSharedCampaignInfo(MopubAppSharedCampaignType.share, "unkonwn", 0, 0);
			}


			return info;
		}

		public override Dictionary<string, object> getSDKInfo()
        {
			string paramsJson = _getSDKInfo();
			Dictionary<string, object> dic = null;
			if (paramsJson != null)
			{
				dic = (Dictionary<string, object>)Json.Deserialize(paramsJson);
			}
			else
			{
				dic = new Dictionary<string, object>();
			}

			return dic;
		}

		public override void fetchInviteBonusList(Action<List<MopubInviteBonusCategory>> success, Action<MopubSDKError> failure)
        {
			MopubCallbackManager.fetchInviteBonusListSuccessDelegate = success;
			MopubCallbackManager.fetchInviteBonusListFailureDelegate = failure;

			_fetchInviteBonusList();

            //_handleMessageFromUnity("fetchInviteBonusList", null);
        }

		public override void acceptInviteBonus(string inviteId, string pkey, Action success, Action<MopubSDKError> failure)
		{
			MopubCallbackManager.acceptInviteBonusSuccessDelegate = success;
			MopubCallbackManager.acceptInviteBonusFailureDelegate = failure;

			_acceptBonus(inviteId, pkey);
		}

		public override void fetchIPv4Info(Action<MopubIPv4Info> success, Action<MopubSDKError> failure){
			MopubCallbackManager.fetchIPv4InfoSuccessDelegate = success;
			MopubCallbackManager.fetchIPv4InfoFailureDelegate = failure;

			_fetchIPv4Info();
        }

    #region dllimport

		[DllImport("__Internal")]
		internal extern static void _init (string gameContentVersion);

		[DllImport("__Internal")]
		internal extern static void _addEventGlobalParams(string globalParams);

		[DllImport("__Internal")]
		internal extern static void _login ();

        [DllImport("__Internal")]
        internal extern static void _showLinkAccountView();

		[DllImport("__Internal")]
		internal extern static void _showSwitchAccountView();

		[DllImport("__Internal")]
		internal extern static void _showDeleteAccountView();

		[DllImport("__Internal")]
		internal extern static void _showAccountCenter();

        [DllImport("__Internal")]
        internal extern static void _showArchive(string jsonString);

        [DllImport("__Internal")]
		internal extern static bool _isAccountOnlyTourist();

		[DllImport("__Internal")]
		internal extern static void _showLinkedAfterPurchaseView();

		[DllImport("__Internal")]
		internal extern static void _setLanguage(string language);

		[DllImport("__Internal")]
		internal extern static void _openNoticeDialog();

		[DllImport("__Internal")]
		internal extern static void _startRealnameAuthenticationWithUI();

		[DllImport("__Internal")]
		internal extern static void _loginWithDevice(string activeCode);

		[DllImport("__Internal")]
		internal extern static void _loginWithVisitor(string activeCode);

		[DllImport("__Internal")]
		internal extern static void _reLoginFlow();

		[DllImport("__Internal")]
		internal extern static void _autoLoginTouristWithUI();

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
		internal extern static void _startPayment(string itemID, string cpOrderID, string characterName, string characterID, string serverName, string serverID, int level);

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
		internal extern static void _logPlayerInfoWithData(string characterName, string characterID, int characterLevel, string serverName, string serverID, string extraData);

		[DllImport("__Internal")]
        internal extern static bool _openRatingView();

		[DllImport("__Internal")]
		internal extern static bool _openRate();
		
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
		internal extern static string _getPackageVersionCode();

		[DllImport("__Internal")]
		internal extern static string _getCgi();

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

		[DllImport("__Internal")]
		internal extern static void _saveCloudCache(string uid, long version, string data);

		[DllImport("__Internal")]
		internal extern static void _getCloudCache(string uid);

		[DllImport("__Internal")]
		internal extern static void _getRedeem(string code);


		[DllImport("__Internal")]
		internal extern static void _initPushSDK(string cgi, string appid, string appSecret, bool passThroughEnable, string logHost, string logPath);

		[DllImport("__Internal")]
		internal extern static void _setPushAlias(string alias);

		[DllImport("__Internal")]
		internal extern static string _getCpParams();

		[DllImport("__Internal")]
		internal extern static void _openShareToWechat(string entrance, string shareData);

		[DllImport("__Internal")]
		internal extern static void _openShareToQQ(string entrance, string shareData);

		[DllImport("__Internal")]
		internal extern static string _getSDKInfo();

		[DllImport("__Internal")]
		internal extern static string _getCurrentSharedCampaign();

		[DllImport("__Internal")]
		internal extern static string _getCurrentInvitedCampaign();

		[DllImport("__Internal")]
		internal extern static void _fetchInviteBonusList();

		[DllImport("__Internal")]
		internal extern static void _acceptBonus(string inviteId, string pkey);

		[DllImport("__Internal")]
		internal extern static void _fetchIPv4Info();

		[DllImport("__Internal")]
		internal extern static string _handleMessageFromUnity(string messageName, string jsonString);

    #endregion
	}
#endif

}