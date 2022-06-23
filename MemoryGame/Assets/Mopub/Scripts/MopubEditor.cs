using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

namespace MopubNS
{
#if UNITY_EDITOR
    public class MopubEditor:MopubBase
	{

		static MopubEditor(){
			#if UNITY_EDITOR
			InitCallbackManager ();
            SESDKEditorBackendMock.InitBackend();
			#endif
		}

		public MopubEditor ()
		{
			//startThread ();
		}


		private Boolean hasInit = false;
		override public void init (Action<InitSuccessResult> success, Action<MopubSDKError> failed)
		{
			this.hasInit = true;
            if (success != null) {
				success (new InitSuccessResult ());
			}
		}

		override public void init (string gameContentVersion, Action<InitSuccessResult> success, Action<MopubSDKError> failed)
		{
            Debug.Log("MopubEditor init with gameContentVersion");
			this.hasInit = true;
            if (success != null) {
				success (new InitSuccessResult ());
			}
		}
		override public void callRealNameUI(bool netWorkable,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
       {
       Debug.Log("static MopubAndroid callRealNameUI");
	   SESDKEditorBackendMock.instance.callRealNameUI(netWorkable,success,failed,failederror);
    
       }

	   override public void showRealNameView(Action<string> success,Action<string> failed)
       {
       Debug.Log("static MopubAndroid callRealNameUI");
		SESDKEditorBackendMock.instance.showRealNameView(success,failed);
       }

	   	override public void realNameRecharge(int amount,Action<string> success,Action<string> failed,Action<MopubSDKError> failederror)
       {
       Debug.Log("static MopubAndroid realNameRecharge");
	   SESDKEditorBackendMock.instance.realNameRecharge(amount,success,failed,failederror);
    
       }

		override public void login (Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
            SESDKEditorBackendMock.instance.login(success, failed);
		}

		public override void logout(Action<string> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.logout(success, failed);
        }

		public override void loginWithDevice(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithDevice(null, success, failed);
		}

		public override void loginWithDevice(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithDevice(activeCode, success, failed);
		}

		public override void loginWithWeChat(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithWeChat(null, success, failed);
		}

		public override void loginWithWeChat(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithWeChat(activeCode, success, failed);
		}

		public override void fetchMobileAuthCode(string phoneNumber, Action<FetchSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.fetchMobileAuthCode(phoneNumber, success, failed);
		}

		public override void loginWithMobile(string phoneNumber, string code, bool isOneKeyLogin,Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithMobile(phoneNumber, code, null, isOneKeyLogin,success, failed);
		}

		public override void loginWithMobile(string phoneNumber, string code, string activeCode,bool isOneKeyLogin, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithMobile(phoneNumber, code, activeCode, isOneKeyLogin,success, failed);
		}

		public override void fetchEmailAuthCode(string email, Action<FetchSuccessResult> success, Action<MopubSDKError> failed){
			SESDKEditorBackendMock.instance.fetchEmailAuthCode(email, success, failed);
        }
		public override void loginWithEmail(string email, string password, string code, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
           
			SESDKEditorBackendMock.instance.loginWithEmail(email, password, code, null, success, failed);
        }
		public override void loginWithEmail(string email, string password, string code, string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.loginWithEmail(email, password, code, activeCode, success, failed);
        }
		public override void resetPasswordWithEmail(string email, string password, string code, Action<string> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.resetPasswordWithEmail(email, password, code, success, failed);
        }

		public override void loginWithVisitor(Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithVisitor(null, success, failed);
		}

		public override void loginWithVisitor(string activeCode, Action<LoginSuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.loginWithVisitor(activeCode, success, failed);
		}
		public override void showLoginUI(Action<LoginSuccessResult> success, Action<MopubSDKError> failed,string activeCode){
            MopubCallbackManager.loginUISuccessDelegate = success;
            MopubCallbackManager.loginUIFailedDelegate = failed;
        }

		override public MopubSdkAccessToken currentAccessToken ()
		{
            return SESDKEditorBackendMock.instance.currentAccessToken();

        }

		public override void verifySessionToken(string token, Action<VerifySuccessResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.verifySessionToken(token, success, failed);
		}

		public override void createInviteCode(Action<string> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.createInviteCode(success, failed);
		}

		public override void fetchInviteeList(Action<List<object>> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.fetchInviteeList(success, failed);
		}

		public override void uploadInviteCode(string code, Action<string> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.uploadInviteCode(code, success, failed);	
		}

        // old addiction prevention
		public override void openPrevention(){
            // SESDKEditorBackendMock.instance.openPrevention(preventionDidTrigger);
        }

		public override long getTotalOnlineTime(){
			return 0;
		}

		public override void fetchIdCardInfo(Action<MopubSDKIdCardInfo> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.fetchIdCardInfo(success, failed);
        }

		public override void verifyIdCard(string realName, string cardNumber, Action<string> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.verifyIdCard(realName, cardNumber, success, failed);
        }

		public override void fetchPaidAmount(Action<string> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.fetchPaidAmount(success, failed);
        }

		// new addiction prevention

		// new addiction prevention
		public override void openRealnamePrevention()
		{
			//_openRealnamePrevention();
		}

		public override long getPreventionTotalOnlineTime()
		{
			return 0;
			//return _getPreventionTotalOnlineTime();
		}

		public override MopubSDKRealnameHeartBeat getRealnameHeartBeat() {
			return null;
		}

		public override void queryRealnameInfo(Action<MopubSDKRealnameInfo> success, Action<MopubSDKError> failed)
		{
			MopubCallbackManager.queryRealnameInfoSuccessDelegate = success;
			MopubCallbackManager.queryRealnameInfoFailedDelegate = failed;

			//_queryRealnameInfo();
		}

		public override void realnameAuthentication(string realname, string cardNumber, Action<MopubSDKRealnameInfo> success, Action<MopubSDKError> failed)
		{
			MopubCallbackManager.realnameAuthenticationSuccessDelegate = success;
			MopubCallbackManager.realnameAuthenticationFailedDelegate = failed;

			//_realnameAuthentication(realname, cardNumber);
		}

		public override void fetchPaidAmountMonthly(int amount, Action<MopubSDKRealnamePaidAmountInfo> success, Action<MopubSDKError> failed)
		{
			MopubCallbackManager.fetchPaidAmountMonthlySuccessDelegate = success;
			MopubCallbackManager.fetchPaidAmountMonthlyFailedDelegate = failed;

			//_fetchPaidAmountMonthly();
		}

		override public void linkWithGameCenter (Action<MopubSDKLinkWithGameCenterResult> success, Action<MopubSDKError> failed)
		{
            SESDKEditorBackendMock.instance.linkWithGameCenter(success, failed);

        }

		override public void linkWithFacebook(Action<MopubSDKLinkWithFacebookResult> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.linkWithFacebook(success, failed);
        }

		override public void linkWithWeChat(Action<MopubSDKLinkWithWeChatResult> success, Action<MopubSDKError> failed){
            SESDKEditorBackendMock.instance.linkWithWeChat(success, failed);
        }

		public override void linkWithEmail (string email, string password, string code, Action<MopubSDKLinkWithEmailResult> success, Action<MopubSDKError> failed){
			SESDKEditorBackendMock.instance.linkWithEmail(success, failed);
        }

		public override void linkWithMobile(string phoneNumber, string code, Action<MopubSDKLinkWithMobileResult> success, Action<MopubSDKError> failed)
		{
			SESDKEditorBackendMock.instance.linkWithMobile(success, failed);
		}


		//pay
		private int sdkOrderIdIndex = 1000;
		private List<MopubSDKPurchasedItem> purchasedItems = new List<MopubSDKPurchasedItem> ();

        private Action<List<MopubSDKSubscriptionItem>> subscriptionItemUpdatedListener;
        private List<MopubSDKSubscriptionItem> purchasedSubscriptionItems = new List<MopubSDKSubscriptionItem>();


		override public void setUnconsumedItemUpdatedListener (Action<List<MopubSDKPurchasedItem>> listener)
		{
            SESDKEditorBackendMock.instance.setUnconsumedItemUpdatedListener(listener);
		}

		override public void fetchPaymentItemDetails (Action<List<MopubSDKPaymentItemDetails>> success, Action<MopubSDKError> failed)
		{
            SESDKEditorBackendMock.instance.fetchPaymentItemDetails(success, failed);
		}


		override public void fetchUnconsumedPurchasedItems (Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed)
		{
            SESDKEditorBackendMock.instance.fetchUnconsumedPurchasedItems(success, failed);

        }

		override public void fetchAllPurchasedItems (Action<List<MopubSDKPurchasedItem>> success, Action<MopubSDKError> failed)
		{
			success (purchasedItems);
		}

		override public void consumePurchase (string sdkOrderID)
		{
            SESDKEditorBackendMock.instance.consumePurchase(sdkOrderID);
		}

        public override void setSubscriptionItemUpdatedListener(Action<List<MopubSDKSubscriptionItem>> subscriptionItemUpdatedListener)
        {
            this.subscriptionItemUpdatedListener = subscriptionItemUpdatedListener;
        }

        public override void restoreSubscription(Action success, Action<MopubSDKError> failed)
        {
            success();
        }

        public override void fetchAllPurchasedSubscriptionItems(Action<List<MopubSDKSubscriptionItem>> success)
        {
            success(purchasedSubscriptionItems);
        }

        override public void startPayment (MopubSDKStartPaymentInfo paymentInfo, 
		                                   Action<MopubSDKPaymentInfo> paymentProcessingWithPaymentInfo,
		                                   Action<MopubSDKPaymentInfo, MopubSDKError> paymentFailedWithPaymentInfo)
		{
            SESDKEditorBackendMock.instance.buyItem(
                paymentInfo,
                paymentProcessingWithPaymentInfo,
                paymentFailedWithPaymentInfo);

        }

		
		override public bool hasSmartAd (string gameEntry)
		{
			return SESDKEditorBackendMock.instance.hasSmartAd(gameEntry);
		}

		override public void showSmartAd (string gameEntry)
		{
            SESDKEditorBackendMock.instance.showSmartAd(gameEntry);

        }


		override public void setRewardedVideoListener (MopubRewardedVideoListener listener)
		{
            SESDKEditorBackendMock.instance.setRewardedVideoListener(listener);

        }

		override public bool hasRewardedVideo (string gameEntry)
		{
			return SESDKEditorBackendMock.instance.hasRewardedVideo(gameEntry);
		}

		override public void showRewardVideoAd (string gameEntry)
		{
            SESDKEditorBackendMock.instance.showRewardVideoAd(gameEntry);

        }


		override public void setInterstitialAdListener (MopubInterstitialAdListener listener)
		{
            SESDKEditorBackendMock.instance.setInterstitialAdListener(listener);

        }

		override public bool hasInterstitial (string gameEntry)
		{
			return SESDKEditorBackendMock.instance.hasInterstitial(gameEntry);
		}

		override public void showInterstitialAd (string gameEntry)
		{
            SESDKEditorBackendMock.instance.showInterstitialAd(gameEntry);
        }

        public override void showBanner(BannerADPosition position)
        {
            SESDKEditorBackendMock.instance.showBanner(position);

        }
        public override void dismissBanner()
        {
            SESDKEditorBackendMock.instance.dismissBanner();
        }

        //native ad
        public override void setNativeAdListener(MopubNativeAdListener listener)
        {
            SESDKEditorBackendMock.instance.setNativeAdListener(listener);
        }
        public override bool hasNativeAd(string gameEntry)
        {
            return SESDKEditorBackendMock.instance.hasNativeAd(gameEntry);
        }
        //If positon=bottomCenter, 'spacing' indicates the distance from the bottom. If  positon=topCenter, 'spacing' indicates the distance from the top
        public override void showNativeAdFixed(string gameEntry, SDKNavtiveAdPosition positon, float spacing)
        {
            SESDKEditorBackendMock.instance.showNativeAdFixed(gameEntry, positon, spacing);
        }

        public override void closeNativeAd(string gameEntry)
        {
            SESDKEditorBackendMock.instance.closeNativeAd(gameEntry);
        }



        override public void logCustomEvent (string eventName, Dictionary<string,string> data)
		{
			var mapStr = "";
			foreach (var pair in data) {
				mapStr += pair.Key + ":" + pair.Value + " , ";
			}
			Debug.Log ("log with data: " + eventName + " " + mapStr);
		}

		public override void logCustomAFEvent (string eventName,  Dictionary<string,object> data)
		{
			var mapStr = "";
			foreach (var pair in data) {
				mapStr += pair.Key + ":" + pair.Value + " , ";
			}
			Debug.Log ("af log with data: " + eventName + " " + mapStr);
		}
		
		public override void logStartLevel(string levelName){
		}
		
		public override void logFinishLevel(string levelName){
		}
		
		public override void logUnlockLevel(string levelName){
		}
		
		public override void logSkipLevel(string levelName){
		}
		
		public override void logSkinUsed(string skinName){
		}
		
		public override void logAdEntrancePresent(string name){
		}

        public override bool openJoinChatGroup()
        {
            return false;
        }

        public override bool openJoinWhatsappChatting()
        {
			return false;
        }

        public override bool openRatingView()
		{
			return false;
		}

		public override bool openRate()
		{
			return false;	
		}
		
		public override bool addLocalNotification(MopubSDKLocalMsg localMsg)
		{
			return false;
		}

		public override void setOnPushInitCallback(Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.pushInitSuccessDelegate = success;
            MopubCallbackManager.pushInitFailedDelegate = failed;
            if (success != null) {
				success("1111111");
			}
        }

		public override void bindAccounts(Dictionary<int, string> accountsType, Action<string> success, Action<MopubSDKError> failed){
            MopubCallbackManager.bindAccountsSuccessDelegate = success;
            MopubCallbackManager.bindAccountsFailedDelegate = failed;
            if (success != null) {
				success("success");
			}
        }
		
        public enum EditorEventName
		{
			reward_start,
			reward_playbackerror,
			reward_click,
			reward_complete,
			reward_close,

			inter_show,
			inter_click,
			inter_dismiss,
		}



		public override void testLog (string log)
		{
			Debug.Log (log);
		}

		public override void setIngameParamsUpdatedListener (Action<Dictionary<String, String >> ingameParamsUpdatedListener){
            SESDKEditorBackendMock.instance.setIngameParamsUpdatedListener(ingameParamsUpdatedListener);

        }

        public override void setAdvertisingIngameParamsUpdatedListener(Action<Dictionary<String, String>> ADIngameParamsUpdatedListener)
        {
            SESDKEditorBackendMock.instance.setAdvertisingIngameParamsUpdatedListener(ADIngameParamsUpdatedListener);
        }
		
		public override void setAFDataUpdatedListener(Action<Dictionary<String, String>> afDataUpdatedListener)
		{
			SESDKEditorBackendMock.instance.setAFDataUpdatedListener(afDataUpdatedListener);
		}
		
        public override void logPlayerInfo(string characterName, string characterID, int characterLevel, string serverName, string serverID)
        {
            Debug.Log("logPlayerInfoSuc");
        }

		public override string getAppDistributor(){
            return null;
        }

		public override string getPuid() {
			return "aaaaa";
		}

		public override string getRegion() {
			return "aaaaa";
		}

		public override string getGameVersion()
        {
			return "1.0.0";
        }
		public override string getTimeStamp() {
			return "timeStamp";
		}


		public override void launchCustomer(){
            
        }

		public override void launchCustomerWithTransitPage(string pageType, string email){
        }

		public override void setCustomerUnreadMessageListener(Action<Dictionary<String, int>> unreadMessageUpdatedListener){
            
        }

		public override bool isUserNotificationEnable()
		{
			return true;
		}

		public override void openSystemNotificationSetting()
		{

		}

		public override void fetchRanking(string uid, int page, int size, Action<MopubSDKRanking> success, Action<MopubSDKError> failure)
		{
			MopubCallbackManager.fetchRankingSuccessDelegate = success;
			MopubCallbackManager.fetchRankingFailedDelegate = failure;
		}
	}
#endif
}