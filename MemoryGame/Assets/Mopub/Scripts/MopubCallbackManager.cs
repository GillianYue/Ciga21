using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MopubNS.ThirdParty.MiniJSON;

namespace MopubNS {
	public class MopubCallbackManager : MonoBehaviour {

		public static MopubCallbackManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad(gameObject);
			} else
				Destroy(this);
		}

		private void OnDestroy()
		{
			if (Instance == this)
				Instance = null;
		}

		#region native callback action
		public static Action<InitSuccessResult> initSuccessDelegate;
		public static Action<MopubSDKError> initFailedDelegate;

		//用户引导页
		public static Action<string> showGuidPageViewSuccessDelegate;
		//实名认证ui新版
		public static Action<string> showRealNameViewSuccessDelegate;
		public static Action<string> showRealNameViewFailedDelegate;
		public static Action<string> realNameUISuccessDelegate;

		public static Action<string> realNameUIFailedDelegate;

		public static Action<MopubSDKError> realNameUIFailedErrorDelegate;
		

		public static Action<LoginSuccessResult> loginSuccessDelegate;
		public static Action<MopubSDKError> loginFailedDelegate;

		public static Action<string> logoutSuccessDelegate;
		public static Action<MopubSDKError> logoutFailedDelegate;

		public static Action<LoginSuccessResult> loginDeviceSuccessDelegate;
		public static Action<MopubSDKError> loginDeviceFailedDelegate;

		public static Action<LoginSuccessResult> loginWeChatSuccessDelegate;
		public static Action<MopubSDKError> loginWeChatFailedDelegate;

		public static Action<LoginSuccessResult> loginMobileSuccessDelegate;
		public static Action<MopubSDKError> loginMobileFailedDelegate;

		public static Action<LoginSuccessResult> loginUISuccessDelegate;
		public static Action<MopubSDKError> loginUIFailedDelegate;

		public static Action<LoginSuccessResult> loginEmailSuccessDelegate;
		public static Action<MopubSDKError> loginEmailFailedDelegate;

		public static Action<LoginSuccessResult> loginVisitorSuccessDelegate;
		public static Action<MopubSDKError> loginVisitorFailedDelegate;

		public static Action<FetchSuccessResult> fetchMobileAuthCodeSuccessDelegate;
		public static Action<MopubSDKError> fetchMobileAuthCodeFailedDelegate;

		public static Action<FetchSuccessResult> fetchEmailAuthCodeSuccessDelegate;
		public static Action<MopubSDKError> fetchEmailAuthCodeFailedDelegate;

		public static Action<string> resetPasswordWithEmailSuccessDelegate;
		public static Action<MopubSDKError> resetPasswordWithEmailFailedDelegate;

		public static Action<VerifySuccessResult> verifySessionTokenSuccessDelegate;
		public static Action<MopubSDKError> verifySessionTokenFailedDelegate;

		public static Action<string> createInviteCodeSuccessDelegate;
		public static Action<MopubSDKError> createInviteCodeFailedDelegate;

		public static Action<List<object>> fetchInviteeListSuccessDelegate;
		public static Action<MopubSDKError> fetchInviteeListFailedDelegate;

		public static Action<string> uploadInviteCodeSuccessDelegate;
		public static Action<MopubSDKError> uploadInviteCodeFailedDelegate;

		public static Action<MopubSDKLinkWithGameCenterResult> linkWithGameCenterSuccessDelegate;
		public static Action<MopubSDKError> linkWithGameCenterFailedDelegate;

		public static Action<MopubSDKLinkWithFacebookResult> linkWithFacebookSuccessDelegate;
		public static Action<MopubSDKError> linkWithFacebookFailedDelegate;

		public static Action<MopubSDKLinkWithWeChatResult> linkWithWeChatSuccessDelegate;
		public static Action<MopubSDKError> linkWithWeChatFailedDelegate;

		public static Action<MopubSDKLinkWithEmailResult> linkWithEmailSuccessDelegate;
		public static Action<MopubSDKError> linkWithEmailFailedDelegate;

		public static Action<MopubSDKLinkWithMobileResult> linkWithMobileSuccessDelegate;
		public static Action<MopubSDKError> linkWithMobileFailedDelegate;

		// public static Action<string> preventionDidTriggerDelegate;

		// old addiction prevention
		public static Action<MopubSDKIdCardInfo> fetchIdCardInfoSuccessDelegate;
		public static Action<MopubSDKError> fetchIdCardInfoFailedDelegate;

		public static Action<string> verifyIdCardSuccessDelegate;
		public static Action<MopubSDKError> verifyIdCardFailedDelegate;

		public static Action<string> fetchPaidAmountSuccessDelegate;
		public static Action<MopubSDKError> fetchPaidAmountFailedDelegate;

		// new addiction prevention
		public static Action<MopubSDKRealnameInfo> queryRealnameInfoSuccessDelegate;
		public static Action<MopubSDKError> queryRealnameInfoFailedDelegate;

		public static Action<MopubSDKRealnameInfo> realnameAuthenticationSuccessDelegate;
		public static Action<MopubSDKError> realnameAuthenticationFailedDelegate;

		public static Action<MopubSDKRealnamePaidAmountInfo> fetchPaidAmountMonthlySuccessDelegate;
		public static Action<MopubSDKError> fetchPaidAmountMonthlyFailedDelegate;

		public static Action<List<MopubSDKPurchasedItem>> unconsumedItemUpdatedListenerDelegate;

		public static Action<List<MopubSDKPaymentItemDetails>> fetchPaymentItemDetailsSuccessDelegate;
		public static Action<MopubSDKError> fetchPaymentItemDetailsFailedDelegate;

		public static Action<List<MopubSDKPurchasedItem>> fetchUnconsumedPurchasedItemSuccessDelegate;
		public static Action<MopubSDKError> fetchUnconsumedPurchasedItemFailedDelegate;

		public static Action<List<MopubSDKPurchasedItem>> fetchAllPurchasedItemSuccessDelegate;
		public static Action<MopubSDKError> fetchAllPurchasedItemFailedDelegate;

		public static Action<MopubSDKPaymentInfo> paymentProcessingWithPaymentInfoDelegate;
		public static Action<MopubSDKPaymentInfo, MopubSDKError> paymentFailedWithPaymentInfoDelegate;

        //订阅回调对象
        public static Action<List<MopubSDKSubscriptionItem>> subscriptionItemUpdatedDelegate;
        public static Action restoreSubscriptionSuccessDelegate;
        public static Action<MopubSDKError> restoreSubscriptionFailedDelegate;
        public static Action<List<MopubSDKSubscriptionItem>> fetchAllPurchasedSubscriptionItemsSuccessDelegate;

        public static MopubRewardedVideoListener rewardedVideoListener;
		public static MopubInterstitialAdListener interstitialAdListener;
        public static MopubNativeAdListener nativeAdListener;

		private static Action<Dictionary<String, String>> ingameParamsUpdatedListenerDelegate;
        private static Dictionary<String, String> ingameParams;
        private static Action<Dictionary<String, String>> advertisingIngameParamsUpdatedListenerDelegate;
        private static Dictionary<String, String> advertisingIngameParams;
		
		private static Action<Dictionary<String, String>> afDataUpdatedListenerDelegate;
		private static Dictionary<String, String> afData;

		private static Action<Dictionary<String, int>> unreadMessageUpdatedListenerDelegate;
		private static Dictionary<String, int> unreadMessageData;

		public static Action<string> pushInitSuccessDelegate;
		public static Action<MopubSDKError> pushInitFailedDelegate;

		public static Action<string> bindAccountsSuccessDelegate;
		public static Action<MopubSDKError> bindAccountsFailedDelegate;

		public static Action<MopubSDKRanking> fetchRankingSuccessDelegate;
		public static Action<MopubSDKError> fetchRankingFailedDelegate;
		// 云存档
		public static Action saveCloudCacheSuccessDelegate;
		public static Action<MopubSDKError> saveCloudCacheFailedDelegate;

		public static Action<MopubSDKCloudCache> getCloudCacheSuccessDelegate;
		public static Action<MopubSDKError> getCloudCacheFailedDelegate;

		//兑换码
		public static Action<string> getRedeemSuccessDelegate;
		public static Action<MopubSDKError> getRedeemFailDelegate;
		
        static string unity_args_key_error = "MopubSDKError";
		//static string unity_args_key_ad_entry = "gameEntry";
		//static string unity_args_key_accesstoken = "accessToken";
		//static string unity_args_key_paymentDetails_list = "paymentDetailsList";
		//static string unity_args_key_purchasedItems_list = "purchasedItemsList";
		static string unity_args_key_paymentInfo = "paymentInfo";

		#endregion

		#region native callback method
		public void sdkLog(string log){
			Debug.Log (log);
		}
		public void sdkInitSuccess(string args) {
			InitSuccessResult result = new InitSuccessResult ();
			if(initSuccessDelegate != null) initSuccessDelegate (result);
		}

		public void sdkInitFailed(string args) {

			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if (error != null) {
				Debug.LogFormat ("sdkInitFailed1: {0}",error.clientMessage);
				Debug.Log (error.clientMessage);
			} else {
				Debug.LogFormat ("sdkInitFailed2: {0}","error null");
			}
			if (initFailedDelegate != null) {
				Debug.LogFormat ("sdkInitFailed3: {0}","will callback");
				initFailedDelegate (error);
			} else {
				Debug.LogFormat ("sdkInitFailed4: {0}","callback null");
			}
		}

		public void sdkLoginSuccess(string args) {
			Debug.Log("sdkLogin:"+args);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken> (args);
			Debug.Log("sdkLogin:"+token.linkedAccount.accounts.Count);
			if (token != null) {
				Debug.Log (token.accountID);
			} else {
				Debug.Log ("token error");
			}
			LoginSuccessResult result = new LoginSuccessResult (token);
			if(loginSuccessDelegate != null)  loginSuccessDelegate (result);
		}

		public void sdkLoginFailed(string args) {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if(loginFailedDelegate != null) loginFailedDelegate (error);
		}
		//用户引导页
		public void showGuidPageViewSuccess(string args){
			if(showGuidPageViewSuccessDelegate != null)showGuidPageViewSuccessDelegate(args);
		}
		//实名认证ui新版
		public void showRealNameViewSuccess(string args){
			if(showRealNameViewSuccessDelegate != null)showRealNameViewSuccessDelegate(args);
		}

		public void showRealNameViewFailed(string args){
			if(showRealNameViewFailedDelegate != null)showRealNameViewFailedDelegate(args);
		}
		public void sdkRealNameStartOnSuccessful(string args){
			if(realNameUISuccessDelegate != null) realNameUISuccessDelegate(args);
		}

		public void sdkRealNameStartOnFailed(string args){
			if(realNameUIFailedDelegate != null) realNameUIFailedDelegate(args);
		}

		public void sdkRealNameStartOnFailedError(string args){
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if(realNameUIFailedErrorDelegate != null) realNameUIFailedErrorDelegate(error);
		}
		public void sdkLogoutSuccess(string args){
			if(logoutSuccessDelegate != null)  logoutSuccessDelegate (args);
		}

		public void sdkLogoutFailed(string args) {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if(logoutFailedDelegate != null) logoutFailedDelegate (error);
		}

		public void sdkLoginDeviceSuccess(string args)
		{
			Debug.Log("sdkLoginDevice:" + args);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken>(args);
			Debug.Log("sdkLogin:" + token.linkedAccount.accounts.Count);
			if (token != null)
			{
				Debug.Log(token.accountID);
			}
			else
			{
				Debug.Log("token error");
			}
			LoginSuccessResult result = new LoginSuccessResult(token);
			if (loginDeviceSuccessDelegate != null) loginDeviceSuccessDelegate(result);
		}

		public void sdkLoginDeviceFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (loginDeviceFailedDelegate != null) loginDeviceFailedDelegate(error);
		}

		public void sdkLoginWeChatSuccess(string args)
		{
			Debug.Log("sdkLoginWeChat:" + args);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken>(args);
			Debug.Log("sdkLogin:" + token.linkedAccount.accounts.Count);
			if (token != null)
			{
				Debug.Log(token.accountID);
			}
			else
			{
				Debug.Log("token error");
			}
			LoginSuccessResult result = new LoginSuccessResult(token);
			if (loginWeChatSuccessDelegate != null) loginWeChatSuccessDelegate(result);
		}

		public void sdkLoginWeChatFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (loginWeChatFailedDelegate != null) loginWeChatFailedDelegate(error);
		}

		public void fetchMobileAuthCodeSuccess(string args)
		{
			FetchSuccessResult result = new FetchSuccessResult();
			if (fetchMobileAuthCodeSuccessDelegate != null) fetchMobileAuthCodeSuccessDelegate(result);
		}

		public void fetchMobileAuthCodeFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (fetchMobileAuthCodeFailedDelegate != null) fetchMobileAuthCodeFailedDelegate(error);
		}

		public void sdkLoginMobileSuccess(string args)
		{
			Debug.Log("sdkLoginMobile:" + args);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken>(args);
			Debug.Log("sdkLogin:" + token.linkedAccount.accounts.Count);
			if (token != null)
			{
				Debug.Log(token.accountID);
			}
			else
			{
				Debug.Log("token error");
			}
			LoginSuccessResult result = new LoginSuccessResult(token);
			if (loginMobileSuccessDelegate != null) loginMobileSuccessDelegate(result);
		}

		public void sdkLoginMobileFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (loginMobileFailedDelegate != null) loginMobileFailedDelegate(error);
		}

		public void sdkLoginUISuccess(string args){
		Debug.Log("sdkLoginUISuccess:" + args);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken>(args);
			Debug.Log("sdkLoginUI:" + token.linkedAccount.accounts.Count);
			if(token !=null){
				Debug.Log(token.accountID);
			}else{
				Debug.Log("token error");
			}
			LoginSuccessResult result =new LoginSuccessResult(token);
			if(loginUISuccessDelegate!=null){
				loginUISuccessDelegate(result);
			}

		}

		public void sdkLoginUIFailed(string args){
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if(loginUIFailedDelegate!=null)loginUIFailedDelegate(error);
		}

		public void fetchEmailAuthCodeSuccess(string args)
		{
			FetchSuccessResult result = new FetchSuccessResult();
			if (fetchEmailAuthCodeSuccessDelegate != null) fetchEmailAuthCodeSuccessDelegate(result);
		}

		public void fetchEmailAuthCodeFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (fetchEmailAuthCodeFailedDelegate != null) fetchEmailAuthCodeFailedDelegate(error);
		}

		public void sdkLoginEmailSuccess(string args)
		{
			Debug.Log("sdkLoginEmail:" + args);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken>(args);
			Debug.Log("sdkLogin:" + token.linkedAccount.accounts.Count);
			if (token != null)
			{
				Debug.Log(token.accountID);
			}
			else
			{
				Debug.Log("token error");
			}
			LoginSuccessResult result = new LoginSuccessResult(token);
			if (loginEmailSuccessDelegate != null) loginEmailSuccessDelegate(result);
		}

		public void sdkLoginEmailFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (loginEmailFailedDelegate != null) loginEmailFailedDelegate(error);
		}

        public void sdkLoginVisitorSuccess(string args)
        {
			Debug.Log("sdkLoginVisitor:" + args);
			MopubSdkAccessToken token = JsonUtility.FromJson<MopubSdkAccessToken>(args);
			Debug.Log("sdkLogin:" + token.linkedAccount.accounts.Count);
			if (token != null)
			{
				Debug.Log(token.accountID);
			}
			else
			{
				Debug.Log("token error");
			}
			LoginSuccessResult result = new LoginSuccessResult(token);
			if (loginVisitorSuccessDelegate != null) loginVisitorSuccessDelegate(result);
		}

        public void sdkLoginVisitorFailed(string args)
        {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (loginVisitorFailedDelegate != null) loginVisitorFailedDelegate(error);
		}

		public void resetPasswordWithEmailSuccess(string args)
		{
			if (resetPasswordWithEmailSuccessDelegate != null) resetPasswordWithEmailSuccessDelegate(args);
		}

		public void resetPasswordWithEmailFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (resetPasswordWithEmailFailedDelegate != null) resetPasswordWithEmailFailedDelegate(error);
		}

		public void verifySessionTokenSuccess(string args)
		{
			VerifySuccessResult result = new VerifySuccessResult();
			if (verifySessionTokenSuccessDelegate != null) verifySessionTokenSuccessDelegate(result);
		}

		public void verifySessionTokenFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (verifySessionTokenFailedDelegate != null) verifySessionTokenFailedDelegate(error);
		}

		public void createInviteCodeSuccess(string args)
		{
			if (createInviteCodeSuccessDelegate != null) createInviteCodeSuccessDelegate(args);
		}

		public void createInviteCodeFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (createInviteCodeFailedDelegate != null) createInviteCodeFailedDelegate(error);
		}

		public void fetchInviteeListSuccess(string args)
		{
			List<object> list = Json.Deserialize(args) as List<object>;
			if (fetchInviteeListSuccessDelegate != null) fetchInviteeListSuccessDelegate(list);
		}

		public void fetchInviteeListFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (fetchInviteeListFailedDelegate != null) fetchInviteeListFailedDelegate(error);
		}

		public void uploadInviteCodeSuccess(string args)
		{
			if (uploadInviteCodeSuccessDelegate != null) uploadInviteCodeSuccessDelegate(args);
		}

		public void uploadInviteCodeFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (uploadInviteCodeFailedDelegate != null) uploadInviteCodeFailedDelegate(error);
		}

		// public void preventionDidTrigger(string args)
		// {
		// 	if (preventionDidTriggerDelegate != null) preventionDidTriggerDelegate(args);
		// }

        // old addiction prevention
		public void fetchIdCardInfoSuccess(string args)
		{	
			MopubSDKIdCardInfo info = JsonUtility.FromJson<MopubSDKIdCardInfo>(args);
			if (fetchIdCardInfoSuccessDelegate != null) fetchIdCardInfoSuccessDelegate(info);
		}

		public void fetchIdCardInfoFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (fetchIdCardInfoFailedDelegate != null) fetchIdCardInfoFailedDelegate(error);
		}

		public void verifyIdCardSuccess(string args)
		{
			if (verifyIdCardSuccessDelegate != null) verifyIdCardSuccessDelegate(args);
		}

		public void verifyIdCardFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (verifyIdCardFailedDelegate != null) verifyIdCardFailedDelegate(error);
		}

		public void fetchPaidAmountSuccess(string args)
		{
			if (fetchPaidAmountSuccessDelegate != null) fetchPaidAmountSuccessDelegate(args);
		}

		public void fetchPaidAmountFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (fetchPaidAmountFailedDelegate != null) fetchPaidAmountFailedDelegate(error);
		}

        // new addiction prevention
        public void queryRealnameInfoSuccess(string args)
        {
			MopubSDKRealnameInfo info = JsonUtility.FromJson<MopubSDKRealnameInfo>(args);
			if (queryRealnameInfoSuccessDelegate != null) queryRealnameInfoSuccessDelegate(info);
        }

        public void queryRealnameInfoFailed(string args)
        {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (queryRealnameInfoFailedDelegate != null) queryRealnameInfoFailedDelegate(error);
        }

        public void realnameAuthenticationSuccess(string args)
        {
			MopubSDKRealnameInfo info = JsonUtility.FromJson<MopubSDKRealnameInfo>(args);
			if (realnameAuthenticationSuccessDelegate != null) realnameAuthenticationSuccessDelegate(info);
		}

        public void realnameAuthenticationFailed(string args)
        {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (realnameAuthenticationFailedDelegate != null) realnameAuthenticationFailedDelegate(error);
		}

		public void fetchPaidAmountMonthlySuccess(string args)
		{
			MopubSDKRealnamePaidAmountInfo info = JsonUtility.FromJson<MopubSDKRealnamePaidAmountInfo>(args);
			if (fetchPaidAmountMonthlySuccessDelegate != null) fetchPaidAmountMonthlySuccessDelegate(info);
		}

		public void fetchPaidAmountMonthlyFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (fetchPaidAmountMonthlyFailedDelegate != null) fetchPaidAmountMonthlyFailedDelegate(error);
		}

		public void sdkLinkGameCenterSuccess(string args) {

			MopubSDKLinkWithGameCenterResult result = new MopubSDKLinkWithGameCenterResult ();
			if(linkWithGameCenterSuccessDelegate != null) linkWithGameCenterSuccessDelegate (result);
		}

		public void sdkLinkGameCenterSuccessFailed(string args) {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if(linkWithGameCenterFailedDelegate != null) linkWithGameCenterFailedDelegate (error);
		}

		public void sdkLinkFacebookSuccess(string args) {

			MopubSDKLinkWithFacebookResult result = new MopubSDKLinkWithFacebookResult ();
			if(linkWithFacebookSuccessDelegate != null) linkWithFacebookSuccessDelegate (result);
		}

		public void sdkLinkFacebookSuccessFailed(string args) {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if(linkWithFacebookFailedDelegate != null) linkWithFacebookFailedDelegate (error);
		}

		public void sdkLinkWeChatSuccess(string args) {

			MopubSDKLinkWithWeChatResult result = new MopubSDKLinkWithWeChatResult ();
			if(linkWithWeChatSuccessDelegate != null) linkWithWeChatSuccessDelegate (result);
		}

		public void sdkLinkWeChatSuccessFailed(string args) {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if(linkWithWeChatFailedDelegate != null) linkWithWeChatFailedDelegate (error);
		}

		public void sdkLinkEmailSuccess(string args) {

			MopubSDKLinkWithEmailResult result = new MopubSDKLinkWithEmailResult ();
			if(linkWithEmailSuccessDelegate != null) linkWithEmailSuccessDelegate (result);
		}

		public void sdkLinkEmailSuccessFailed(string args) {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
			if(linkWithEmailFailedDelegate != null) linkWithEmailFailedDelegate (error);
		}

        public void sdkLinkMobileSuccess(string args)
        {
			MopubSDKLinkWithMobileResult result = new MopubSDKLinkWithMobileResult();
			if (linkWithMobileSuccessDelegate != null) linkWithMobileSuccessDelegate(result);
        }

        public void sdkLinkMobileFailed(string args)
        {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (linkWithMobileFailedDelegate != null) linkWithMobileFailedDelegate(error);
		}

		public void rewardedVideoDidAppear(string args) {

			if(rewardedVideoListener != null)
            {
				Dictionary<string, object> argsDic = Json.Deserialize(args) as Dictionary<string, object>;
				string gameEntry = argsDic["gameEntry"] as string;
				string adInfoString = Json.Serialize(argsDic["adInfo"]);
				MopubAdInfo adInfo = JsonUtility.FromJson<MopubAdInfo>(adInfoString);
				rewardedVideoListener.onRewardedVideoStarted(gameEntry, adInfo);
			}
				
		}
		public void rewardedVideoDidDisappear(string args) {
			if(rewardedVideoListener != null)
            {
				Dictionary<string, object> argsDic = Json.Deserialize(args) as Dictionary<string, object>;
				string gameEntry = argsDic["gameEntry"] as string;
				string adInfoString = Json.Serialize(argsDic["adInfo"]);
				MopubAdInfo adInfo = JsonUtility.FromJson<MopubAdInfo>(adInfoString);
				rewardedVideoListener.onRewardedVideoClosed(gameEntry, adInfo);
			}
			
		}
		public void rewardedVideoDidReceiveTap(string args) {
			if(rewardedVideoListener != null)
            {
				Dictionary<string, object> argsDic = Json.Deserialize(args) as Dictionary<string, object>;
				string gameEntry = argsDic["gameEntry"] as string;
				string adInfoString = Json.Serialize(argsDic["adInfo"]);
				MopubAdInfo adInfo = JsonUtility.FromJson<MopubAdInfo>(adInfoString);
				rewardedVideoListener.onRewardedVideoClicked(gameEntry, adInfo);
			}
			
		}
		public void rewardedVideoDidFinishPlay(string args) {
			if(rewardedVideoListener != null)
            {
				Dictionary<string, object> argsDic = Json.Deserialize(args) as Dictionary<string, object>;
				string gameEntry = argsDic["gameEntry"] as string;
				string adInfoString = Json.Serialize(argsDic["adInfo"]);
				MopubAdInfo adInfo = JsonUtility.FromJson<MopubAdInfo>(adInfoString);
				rewardedVideoListener.onRewardedVideoCompleted(gameEntry, adInfo);
			}
			
		}
		public void rewardedVideoDidFailToPlay(string args) {


            if(rewardedVideoListener != null){
                #if UNITY_IOS
                            rewardedVideoListener.onRewardedVideoPlaybackError (args, 1, "error");
                #elif UNITY_ANDROID
                            rewardedVideoListener.onRewardedVideoPlaybackError (args, 1, "error");
                #else

                #endif
            }

        }

		public void interstitialDidAppear(string args) {
			if(interstitialAdListener != null)
            {
				Dictionary<string, object> argsDic = Json.Deserialize(args) as Dictionary<string, object>;
				string gameEntry = argsDic["gameEntry"] as string;
				string adInfoString = Json.Serialize(argsDic["adInfo"]);
				MopubAdInfo adInfo = JsonUtility.FromJson<MopubAdInfo>(adInfoString);
				interstitialAdListener.onInterstitialShown(gameEntry, adInfo);
			}
				
		}
		public void interstitialDidDisappear(string args) {
			if(interstitialAdListener != null)
            {
				Dictionary<string, object> argsDic = Json.Deserialize(args) as Dictionary<string, object>;
				string gameEntry = argsDic["gameEntry"] as string;
				string adInfoString = Json.Serialize(argsDic["adInfo"]);
				MopubAdInfo adInfo = JsonUtility.FromJson<MopubAdInfo>(adInfoString);
				interstitialAdListener.onInterstitialDismissed(gameEntry, adInfo);
			}
		}
		public void interstitialDidReceiveTap(string args) {
			if(interstitialAdListener != null)
            {
				Dictionary<string, object> argsDic = Json.Deserialize(args) as Dictionary<string, object>;
				string gameEntry = argsDic["gameEntry"] as string;
				string adInfoString = Json.Serialize(argsDic["adInfo"]);
				MopubAdInfo adInfo = JsonUtility.FromJson<MopubAdInfo>(adInfoString);
				interstitialAdListener.onInterstitialClicked(gameEntry, adInfo);
			}
		}

        //native ad
        public void onNativeAdDidShown(string args)
        {
            if(nativeAdListener != null)
            {
                nativeAdListener.onNativeAdDidShown(args);
            }
        }
        public void onNativeAdDismissed(string args)
        {
            if (nativeAdListener != null)
            {
                nativeAdListener.onNativeAdDismissed(args);
            }
        }
        public void onNativeAdClicked(string args)
        {
            if (nativeAdListener != null)
            {
                nativeAdListener.onNativeAdClicked(args);
            }
        }


        public void fetchPaymentItemDetailsSuccess(string args) {
				List<object> list = Json.Deserialize(args) as List<object>;
			List<MopubSDKPaymentItemDetails> itemDetailsList = new List<MopubSDKPaymentItemDetails> ();
				foreach (object itemMap in list) {
				MopubSDKPaymentItemDetails itemDetails = JsonUtility.FromJson<MopubSDKPaymentItemDetails> (itemMap.ToString());
				if (itemDetails != null) {
					itemDetailsList.Add (itemDetails);
				}
			}
				if(fetchPaymentItemDetailsSuccessDelegate!=null) fetchPaymentItemDetailsSuccessDelegate (itemDetailsList);
		}

		public void fetchPaymentItemDetailsFailed(string args) {
				MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (args);
				if(fetchPaymentItemDetailsFailedDelegate != null)
			fetchPaymentItemDetailsFailedDelegate (error);
		}

		public void fetchUnconsumedPurchasedItems(string args) {
				List<object> list = Json.Deserialize(args) as List<object>;
			List<MopubSDKPurchasedItem> purchasedList = new List<MopubSDKPurchasedItem> ();
				foreach (object itemMap in list) {
				MopubSDKPurchasedItem item = JsonUtility.FromJson<MopubSDKPurchasedItem> (itemMap.ToString());
				if (item != null) {
					purchasedList.Add (item);
				}
			}
				if(fetchUnconsumedPurchasedItemSuccessDelegate != null)
			fetchUnconsumedPurchasedItemSuccessDelegate (purchasedList);
		}

		public void fetchAllPurchasedItems(string args) {
				List<object> list = Json.Deserialize(args) as List<object>;
				List<MopubSDKPurchasedItem> purchasedList = new List<MopubSDKPurchasedItem> ();
				foreach (object itemMap in list) {
				MopubSDKPurchasedItem item = JsonUtility.FromJson<MopubSDKPurchasedItem> (itemMap.ToString());
				if (item != null) {
					purchasedList.Add (item);
				}
			}
				if(fetchAllPurchasedItemSuccessDelegate != null)
			fetchAllPurchasedItemSuccessDelegate (purchasedList);
		}

		public void unconsumedItemUpdated(string args) {
				List<object> list = Json.Deserialize(args) as List<object>;
			List<MopubSDKPurchasedItem> purchasedList = new List<MopubSDKPurchasedItem> ();
				foreach (object itemMap in list) {
				MopubSDKPurchasedItem item = JsonUtility.FromJson<MopubSDKPurchasedItem> (itemMap.ToString());
				if (item != null) {
					purchasedList.Add (item);
				}
			}
			if(unconsumedItemUpdatedListenerDelegate != null)
			unconsumedItemUpdatedListenerDelegate (purchasedList);
		}

		public void paymentProcessing(string args) {
				MopubSDKPaymentInfo paymentInfo = JsonUtility.FromJson<MopubSDKPaymentInfo> (args);
			paymentProcessingWithPaymentInfoDelegate (paymentInfo);
		}
		public void paymentFailed(string args) {
				Dictionary<string, object> map = Json.Deserialize (args) as Dictionary<string, object>;
				MopubSDKError error = JsonUtility.FromJson<MopubSDKError> (map [unity_args_key_error].ToString());
				MopubSDKPaymentInfo paymentInfo = JsonUtility.FromJson<MopubSDKPaymentInfo> (map [unity_args_key_paymentInfo].ToString());
			paymentFailedWithPaymentInfoDelegate (paymentInfo, error);
		}

        //订阅回调
        public void subscriptionItemUpdated(string args)
        {
            List<object> list = Json.Deserialize(args) as List<object>;
            List<MopubSDKSubscriptionItem> purchasedList = new List<MopubSDKSubscriptionItem>();
            foreach (object itemMap in list)
            {
                MopubSDKSubscriptionItem item = JsonUtility.FromJson<MopubSDKSubscriptionItem>(itemMap.ToString());
                if (item != null)
                {
                    purchasedList.Add(item);
                }
            }
            if (subscriptionItemUpdatedDelegate != null)
            {
                subscriptionItemUpdatedDelegate(purchasedList);

            }
        }

        public void fetchAllPurchasedSubscriptionItems(string args)
        {
            List<object> list = Json.Deserialize(args) as List<object>;
            List<MopubSDKSubscriptionItem> purchasedList = new List<MopubSDKSubscriptionItem>();
            foreach (object itemMap in list)
            {
                MopubSDKSubscriptionItem item = JsonUtility.FromJson<MopubSDKSubscriptionItem>(itemMap.ToString());
                if (item != null)
                {
                    purchasedList.Add(item);
                }
            }
            if (fetchAllPurchasedSubscriptionItemsSuccessDelegate != null)
                fetchAllPurchasedSubscriptionItemsSuccessDelegate(purchasedList);
        }

        public void restoreSubscriptionSuccess()
        {
            if(restoreSubscriptionSuccessDelegate != null)
            {
                restoreSubscriptionSuccessDelegate();
            }
        }

        public void restoreSubscriptionFailed(string args)
        {
            MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
            if (restoreSubscriptionFailedDelegate != null)
            {
                restoreSubscriptionFailedDelegate(error);
            }
        }

        static public void setIngameParamsUpdatedListenerDelegate( Action<Dictionary<String, String>> listener){
			ingameParamsUpdatedListenerDelegate = listener;
				Debug.Log ("set ingame params listener");
			if (ingameParams != null) {
				Debug.Log ("call as the ingameParams is not null "+ingameParams);
				ingameParamsUpdatedListenerDelegate (ingameParams);
			}
		}

        static public void setAdvertisingIngameParamsUpdatedListenerDelegate(Action<Dictionary<String, String>> listener)
        {
            advertisingIngameParamsUpdatedListenerDelegate = listener;
            Debug.Log("set ad ingame params listener");
            if (advertisingIngameParams != null)
            {
                Debug.Log("call as the ad ingameParams is not null " + advertisingIngameParams);
                advertisingIngameParamsUpdatedListenerDelegate(advertisingIngameParams);
            }
        }

        public void inGameOnlineParams(string args) {
			Dictionary<String,object> input = Json.Deserialize(args) as Dictionary<String, object>;
			Dictionary<String,String> tempParams = new Dictionary<string, string> ();
			foreach (var pair in input) {
				tempParams.Add (pair.Key, (string)pair.Value);
			}
			if (tempParams.Count > 0) {
				ingameParams = tempParams;
				if (ingameParamsUpdatedListenerDelegate != null) {
					ingameParamsUpdatedListenerDelegate (ingameParams);
				}
			}
		}

        public void advertisingInGameOnlineParams(string args)
        {
            Dictionary<String, object> input = Json.Deserialize(args) as Dictionary<String, object>;
            Dictionary<String, String> tempParams = new Dictionary<string, string>();
            foreach (var pair in input)
            {
                tempParams.Add(pair.Key, (string)pair.Value);
            }
            if (tempParams.Count > 0)
            {
                advertisingIngameParams = tempParams;
                if (advertisingIngameParamsUpdatedListenerDelegate != null)
                {
                    advertisingIngameParamsUpdatedListenerDelegate(advertisingIngameParams);
                }
            }
        }
		
		static public void setAFDataUpdatedListenerDelegate(Action<Dictionary<String, String>> listener){
			afDataUpdatedListenerDelegate = listener;
				Debug.Log ("set af data listener");
			if (afData != null) {
				Debug.Log ("call as the afData is not null "+afData);
				afDataUpdatedListenerDelegate (afData);
			}
		}
		
		public void aFInstallConversionData(string args){
			Debug.Log(args);
			Dictionary<String,object> input = Json.Deserialize(args) as Dictionary<String, object>;
			Dictionary<String,String> tempParams = new Dictionary<string, string> ();
			foreach (var pair in input) {
				tempParams.Add (pair.Key, pair.Value.ToString());
			}
			if (tempParams.Count > 0) {
				afData = tempParams;
				if (afDataUpdatedListenerDelegate != null) {
					afDataUpdatedListenerDelegate (afData);
				}
			}
		}


		public static void setUnreadMessageUpdatedListenerDelegate(Action<Dictionary<String, int>> listener){
			unreadMessageUpdatedListenerDelegate = listener;
			Debug.Log("set unread message updated listener");
			if(unreadMessageData != null){
				Debug.Log ("call as the unreadMessageData is not null "+unreadMessageData);
				unreadMessageUpdatedListenerDelegate (unreadMessageData);
			}
		}

		public void unreadMessageUpdated(string args){
			Debug.Log(args);
			Dictionary<String,object> input = Json.Deserialize(args) as Dictionary<String, object>;
			Dictionary<String,int> tempParams = new Dictionary<string, int> ();
			Debug.Log("111" + args);
			foreach (var pair in input) {
				tempParams.Add (pair.Key, Convert.ToInt32(pair.Value));
			}
			if (tempParams.Count > 0) {
				unreadMessageData = tempParams;
				if (unreadMessageUpdatedListenerDelegate != null) {
					unreadMessageUpdatedListenerDelegate (unreadMessageData);
				}
			}
		}

		public void pushInitSuccess(string args)
		{
			if (pushInitSuccessDelegate != null) pushInitSuccessDelegate(args);
		}

		public void pushInitFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (pushInitFailedDelegate != null) pushInitFailedDelegate(error);
		}

		public void bindAccountsSuccess(string args)
		{
			if (bindAccountsSuccessDelegate != null) bindAccountsSuccessDelegate(args);
		}

		public void bindAccountsFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (bindAccountsFailedDelegate != null) bindAccountsFailedDelegate(error);
		}

		public void fetchRankingSuccess(string args)
        {
			MopubSDKRanking ranking = JsonUtility.FromJson<MopubSDKRanking>(args);
			if (fetchRankingSuccessDelegate != null) fetchRankingSuccessDelegate(ranking);
        }

		public void fetchRankingFailed(string args)
		{
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (pushInitFailedDelegate != null) pushInitFailedDelegate(error);
		}

		public void saveCloudCacheSuccess()
        {
			if (saveCloudCacheSuccessDelegate != null) saveCloudCacheSuccessDelegate();
		}

		public void saveCloudCacheFailed(string args)
        {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (saveCloudCacheFailedDelegate != null) saveCloudCacheFailedDelegate(error);
        }

		public void getCloudCacheSuccess(string args)
        {
			MopubSDKCloudCache cache = JsonUtility.FromJson<MopubSDKCloudCache>(args);
			if (getCloudCacheSuccessDelegate != null) getCloudCacheSuccessDelegate(cache);
		}

		public void getCloudCacheFailed(string args)
        {
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if (getCloudCacheFailedDelegate != null) getCloudCacheFailedDelegate(error);
		}

		public void getRedeemSuccess(string args){
			if (getRedeemSuccessDelegate != null) getRedeemSuccessDelegate(args);
		}

		public void getRedeemFailed(string args){
			MopubSDKError error = JsonUtility.FromJson<MopubSDKError>(args);
			if(getRedeemFailDelegate != null) getRedeemFailDelegate(error);
			}		
        #endregion
    }
}

