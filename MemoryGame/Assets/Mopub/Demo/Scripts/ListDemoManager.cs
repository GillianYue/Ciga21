using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using MopubNS;

public class ListDemoManager : MonoBehaviour
{

	private int count;
	private List<string> logs = new List<string> (){ };
	private Dictionary<String,int> playerPackages = new Dictionary<String,int> ();
	int maxLogsCount = 30;
	private List<MopubSDKPaymentItemDetails> MopubSDKPaymentItemDetails;

	//int startPaymentIndex = 0;
	int buttonWith = 300;
	int buttonHeight = 80;
	string myGameContentVersion = "1.0";

	static public bool onFront = true;

	int screenWidth = 0;
	int screenHeight = 0;
	private Vector2 btnScrollPosition;
	private Vector2 logScrollPosition;
	private int totalScrollHeight = 1000;

	public string activeCode = "";
	public string phoneNum = "";
	public string authCode = "";
	public string inviteCode = "";
	public string email = "";
	public string password = "";

	public string name = "";
	public string idCard = "";
	public string queryAmount = "";

	public string pageType = "";
	public string csEmail = "";
	private bool showAmazonGift = false;

	public string accountType = "";
	public string accountName = "";

	public string rankPageString = "1";
	public string rankSizeString = "1";

	public class DemoRewardListener: MopubRewardedVideoListener
	{
		private ListDemoManager demo;
		public DemoRewardListener(ListDemoManager demo){
			this.demo = demo;
		}
		public void onRewardedVideoStarted (string gameEntry, MopubAdInfo adInfo)
		{
			Debug.Log("show reward start " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
			demo.logs.Add ("show reward start " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
		}

		public void onRewardedVideoPlaybackError (string gameEntry, int errorCode, string msg){
			demo.logs.Add ("show reward error " + gameEntry);
		}

		public void onRewardedVideoClicked (string gameEntry, MopubAdInfo adInfo)
		{
			demo.logs.Add ("show reward click " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
		}

		public void onRewardedVideoClosed (string gameEntry, MopubAdInfo adInfo)
		{
			demo.logs.Add ("show reward close " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
		}

		public void onRewardedVideoCompleted (string gameEntry, MopubAdInfo adInfo)
		{
			//give player reward item
			demo.logs.Add ("show reward complete " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
			demo.giveItem("reward_item");
		}
	}

	public class DemoInterListener: MopubInterstitialAdListener
	{
		private ListDemoManager demo;
		public DemoInterListener(ListDemoManager demo){
			this.demo = demo;
		}
		public void onInterstitialShown(string gameEntry, MopubAdInfo adInfo)
		{
			demo.logs.Add ("show Interstitial with " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
		}
		public void onInterstitialClicked(string gameEntry, MopubAdInfo adInfo)
		{
			demo.logs.Add ("Interstitial click " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
		}
		public void onInterstitialDismissed(string gameEntry, MopubAdInfo adInfo)
		{
			demo.logs.Add ("Interstitial dismiss " + gameEntry + ",network: " + adInfo.networkType + ", adUnitId: " + adInfo.adUnitId + ", placementId: " + adInfo.placementId);
		}
	}

    public class DemoNativeAdListener: MopubNativeAdListener
    {
        private ListDemoManager demo;
        public DemoNativeAdListener(ListDemoManager demo)
        {
            this.demo = demo;
        }

        public void onNativeAdDidShown(string gameEntry)
        {
            demo.logs.Add("NativeAd Did Shown: " + gameEntry);
        }
        public void onNativeAdDismissed(string gameEntry)
        {
            demo.logs.Add("NativeAd Did Dismissed: " + gameEntry);
        }
        public void onNativeAdClicked(string gameEntry)
        {
            demo.logs.Add("NativeAd Did Clicked: " + gameEntry);
        }
    }


	// public void Awake(){
	// 	Debug.Log("Awake");
	// 	AndroidJavaObject marketSdk = new AndroidJavaClass("com.superera.market.SupereraMarketSDK").CallStatic<AndroidJavaObject>("getInstance");
	// 	AndroidJavaObject context = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
	// 	marketSdk.Call("init", context, "1111920278", "209745", "68698", "applog", "a36d3b684e2cb2087716d1bc65755eb7", "superera_log_demo", "superera_log_demo", true, true);
	// }

	// void OnApplicationPause(bool pause){
    // 	if(pause){
    //     	//切换到后台时执行
	// 		AndroidJavaObject marketSdk = new AndroidJavaClass("com.superera.market.SupereraMarketSDK").CallStatic<AndroidJavaObject>("getInstance");
	// 		AndroidJavaObject context = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
	// 		Debug.Log("onPause");
	// 		marketSdk.Call("onPause", context);
    // 	}
    // 	else {
    //     	//切换到前台时执行，游戏启动时执行一次
	// 		AndroidJavaObject marketSdk = new AndroidJavaClass("com.superera.market.SupereraMarketSDK").CallStatic<AndroidJavaObject>("getInstance");
	// 		AndroidJavaObject context = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
	// 		Debug.Log("onResume");
    //     	marketSdk.Call("onResume", context);
    // 	}
	// }

	public void Start ()
	{
		Debug.Log("Start");
        this.screenWidth = UnityEngine.Screen.width;
        this.screenHeight = UnityEngine.Screen.height;
		#if !UNITY_EDITOR
		buttonWith = screenWidth / 2 ;
		buttonHeight = screenHeight / 13;
		#endif
		btnScrollPosition = Vector2.zero;
		logScrollPosition = Vector2.zero;

		//the sdkOrderIds should save in the local or cloud
		HashSet<String> hasSendedSdkOrders = new HashSet<string> ();

		MopubSdk.getInstance().setOnPushInitCallback(delegate(string token){
			logs.Add("push init success:" + token);
		}, delegate(MopubSDKError error){
			logs.Add("push init failed");
			logs.Add(JsonUtility.ToJson(error));
		});

        //build all needed listener
        // Listen to consumable item was purchased
        MopubSdk.getInstance ().setUnconsumedItemUpdatedListener (delegate(List<MopubSDKPurchasedItem> uncomsuedItems) {
			//for any unconsumed item,we need to send item to the player and then mark this item has been consumed 
			logs.Add("find uncomsuesItems");
			foreach (var item in uncomsuedItems) {
				//important: make sure you have not sent this sdkOrderID's item yet
				if(!hasSendedSdkOrders.Contains(item.sdkOrderID)){
					//send this item to the player
					logs.Add("will add item to player's package, sdkOrderID: "+item.sdkOrderID);
					giveItem(item.itemID);
					hasSendedSdkOrders.Add(item.sdkOrderID);
				}
                //After sending the item, please consume the order
                MopubSdk.getInstance ().consumePurchase (item.sdkOrderID);
			}
		});

        //Listen to subscription item was purchased or state changed
        MopubSdk.getInstance().setSubscriptionItemUpdatedListener(delegate (List<MopubSDKSubscriptionItem> subscriptionItems) {
            //for any subscription item, we need to make the subscription active
            logs.Add("find subscription items");
            foreach (var item in subscriptionItems)
            {
                long currentTimestamp = Convert.ToInt64(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);
                
                //make sure the subscription is valid
                if (currentTimestamp > item.validTimeMS
                && currentTimestamp < item.invalidTimeMS
                && (item.state == MopubSDKItemSubscriptionState.ACTIVE || item.state == MopubSDKItemSubscriptionState.GRACE))
                {
                    //important: make sure you have not activated this subscription type yet
                    if (!hasSendedSdkOrders.Contains(item.itemID))
                    {
                        //active this item to the player
                        logs.Add("will active subscription item to player's package, sdkOrderID: " + item.sdkOrderID);
                        giveItem(item.itemID);
                        hasSendedSdkOrders.Add(item.sdkOrderID);
                    }
                }
                else
                {
                    //if the subscription is invalid, remove the subscription
                    removeItem(item.itemID);
                }

            }
        });

        //ad listener
        MopubSdk.getInstance().setRewardedVideoListener(new DemoRewardListener(this));
		MopubSdk.getInstance().setInterstitialAdListener(new DemoInterListener(this));
        MopubSdk.getInstance().setNativeAdListener(new DemoNativeAdListener(this));

        //set ingame params listener
        MopubSdk.getInstance ().setIngameParamsUpdatedListener (delegate(Dictionary<String, String> ingameParams) {
			logs.Add("ingame params update");
			if (ingameParams.ContainsKey("SHOW_AMAZON_GIFT")){
                String s = ingameParams["SHOW_AMAZON_GIFT"];
                if (int.Parse(s) != 1) {
                    showAmazonGift = false;
                }else {
					showAmazonGift = true;
				}
            }else {
            	showAmazonGift = false;
            }
			foreach(var pair in ingameParams){
				logs.Add("find ingame params, key=" + pair.Key+ ", value=" + pair.Value);
			}
		});

        //set adingame params listener
        MopubSdk.getInstance().setAdvertisingIngameParamsUpdatedListener(delegate (Dictionary<String, String> adIngameParams) {
            logs.Add("ad ingame params update");
            foreach (var pair in adIngameParams)
            {
                logs.Add("find ad ingame params, key=" + pair.Key + ", value=" + pair.Value);
            }
        });
		
		MopubSdk.getInstance().setAFDataUpdatedListener(delegate (Dictionary<String, String> afDataParams) {
            logs.Add("af data update");
            foreach (var pair in afDataParams)
            {
                logs.Add("find af data, key=" + pair.Key + ", value=" + pair.Value);
            }
        });

		MopubSdk.getInstance().setCustomerUnreadMessageListener(delegate (Dictionary<String, int> unreadMessageDataParams) {
			 logs.Add("unread message data update");
			 foreach (var pair in unreadMessageDataParams)
            {
                logs.Add("find unread message data, key=" + pair.Key + ", value=" + pair.Value);
            }
		});
		
		string res = MopubSdk.getInstance().getAppDistributor();
		logs.Add("App distributor:" + res);
    }

	void giveItem(string itemID){
		int nowNumber = 0;
		if (playerPackages.ContainsKey (itemID)) {
			nowNumber = playerPackages [itemID];
		}
		playerPackages [itemID] = nowNumber + 1;
		logs.Add("add item to player's package "+itemID);
	}

    void removeItem(string itemID)
    {
        
        if (playerPackages.ContainsKey(itemID))
        {
            int nowNumber = playerPackages[itemID] - 1;
            if(nowNumber < 1)
            {
                playerPackages.Remove(itemID);
            }
            else
            {
                playerPackages[itemID] = nowNumber;
            }
        }
        logs.Add("remove item from player's package " + itemID);
    }


    void OnGUI ()
	{
                Debug.Log("on gui");
		if (!onFront) {
			//if it is not on front stop draw the gui
			Debug.Log ("no on front return");
			return;
		}
		while (logs.Count > maxLogsCount) {
			logs.RemoveAt (0);
		}

		GUIStyle fontStyle = new GUIStyle();
		fontStyle.normal.background = null; 
		fontStyle.normal.textColor= new Color(1,1,1); 
		fontStyle.fontSize = 30;  
		fontStyle.wordWrap = true;

		GUIStyle inputStyle = new GUIStyle(GUI.skin.textField);
		inputStyle.fontSize = 30;
		inputStyle.normal.textColor= new Color(1,1,1); 
		inputStyle.wordWrap = true;

		GUIStyle btnStyle = new GUIStyle(GUI.skin.button); 
		btnStyle.alignment=TextAnchor.MiddleCenter;
		btnStyle.fontSize=28;
		btnStyle.normal.textColor=Color.white;
		btnStyle.wordWrap = true;


		string l = "";
		for (int i = logs.Count -1 ; i >=0; i--) {
			l += logs [i] + "\n*************\n";
		}

		logScrollPosition = GUI.BeginScrollView (new Rect (5, 0, Screen.width - 5, Screen.height/4), logScrollPosition, new Rect (5, 0, Screen.width - 10, 2000),false,false);
		GUI.Label (new Rect (5, 0, Screen.width - 15, Screen.height/4), l, fontStyle);
		GUI.EndScrollView();

		string playerItemStr = "player packages:\n";
		foreach (var pair in playerPackages) {
			playerItemStr += pair.Key + " : " + pair.Value + " \n";
		}

		int y = Screen.height/4 + 5;

		GUI.Label (new Rect (buttonWith + 10, y + 10,  Screen.width - buttonWith - 20, 150), playerItemStr,fontStyle);

		btnScrollPosition = GUI.BeginScrollView (new Rect (0, y, Screen.width, Screen.height-Screen.height/4), btnScrollPosition, new Rect (0, y, Screen.width, totalScrollHeight),false,false);
	
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "init", btnStyle)) {
			logs.Add ("call init");
			MopubSdk.getInstance ().init (
				myGameContentVersion,
				delegate(InitSuccessResult result) { 
					logs.Add ("init success");
					logs.Add (JsonUtility.ToJson (result)); 
					logs.Add ("region: " + MopubSdk.getInstance().getRegion());
				},
				delegate(MopubSDKError error) {
					logs.Add("init failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}
		y += buttonHeight + 10;
		 if(GUI.Button(new Rect(0,y,buttonWith,buttonHeight),"showRealNameView",btnStyle)){
           logs.Add("call showRealNameView");
		   MopubSdk.getInstance().showRealNameView(
			   delegate(string s) { 
					logs.Add ("callRealNameUI success");
				},
				delegate (string failed){
					logs.Add("callRealNameUI failed");
				}
		   );
		 }
		 y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"充值金额", fontStyle);
		queryAmount = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), queryAmount, inputStyle);
		y += buttonHeight + 10;
		 if(GUI.Button(new Rect(0,y,buttonWith,buttonHeight),"realNameRecharge",btnStyle)){
           logs.Add("call realNameRecharge");
		   MopubSdk.getInstance().realNameRecharge(
			   int.Parse(queryAmount),
			   delegate(string s) { 
					logs.Add ("realNameRecharge success");
				},
				delegate (string failed){
					logs.Add("realNameRecharge failed");
				},
			   delegate (MopubSDKError error){
				   logs.Add("realNameRecharge failederror");
				   logs.Add(JsonUtility.ToJson(error));
			   }
		   );
		 }
		 y += buttonHeight + 10;
		 if(GUI.Button(new Rect(0,y,buttonWith,buttonHeight),"showLoginUI",btnStyle)){
           logs.Add("call showLoginUI");
		   MopubSdk.getInstance().showLoginUI(
			   delegate(LoginSuccessResult s) { 
					logs.Add ("login success");
					logs.Add (JsonUtility.ToJson (s)); 
				},
			   delegate (MopubSDKError error){
				   logs.Add("showLoginUI failed");
				   logs.Add(JsonUtility.ToJson(error));
			   },
			   "A2ENT2"
		   );
		 }
		 
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "login", btnStyle)) {
			logs.Add ("call login");
			MopubSdk.getInstance ().login (
				delegate(LoginSuccessResult s) { 
					logs.Add ("login success");
					logs.Add (JsonUtility.ToJson (s)); 
				},
				delegate(MopubSDKError error) {
					logs.Add("login failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logout", btnStyle)) {
			logs.Add ("call logout");
			MopubSdk.getInstance ().logout (
				delegate(string s) { 
					logs.Add ("logout success");
				},
				delegate(MopubSDKError error) {
					logs.Add("logout failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"激活码：", fontStyle);
		activeCode = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), activeCode, inputStyle);
		

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "loginWithDevice", btnStyle))
		{
			logs.Add("call loginWithDevice activeCode:"+activeCode);
			MopubSdk.getInstance().loginWithDevice(
				activeCode,
				delegate (LoginSuccessResult s) {
					logs.Add("loginWithDevice success");
					logs.Add(JsonUtility.ToJson(s));
				},
				delegate (MopubSDKError error) {
					logs.Add("loginWithDevice failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
			activeCode = "";
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "loginWithWeChat", btnStyle))
		{
			logs.Add("call loginWithWeChat activeCode:"+activeCode);
			MopubSdk.getInstance().loginWithWeChat(
				activeCode,
				delegate (LoginSuccessResult s) {
					logs.Add("loginWithWeChat success");
					logs.Add(JsonUtility.ToJson(s));
				},
				delegate (MopubSDKError error) {
					logs.Add("loginWithWeChat failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
			activeCode = "";
		}

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"手机号：", fontStyle);
		phoneNum = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), phoneNum, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchMobileAuthCode", btnStyle)) {
			logs.Add ("call fetchMobileAuthCode");
			MopubSdk.getInstance ().fetchMobileAuthCode(
				phoneNum,
				delegate (FetchSuccessResult f){
					logs.Add("fetchMobileAuthCode success");
				},
				delegate (MopubSDKError error) {
					logs.Add("fetchMobileAuthCode failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"验证码：", fontStyle);
		authCode = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), authCode, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "loginWithMobile", btnStyle)){
			logs.Add("call loginWithMobile activeCode:"+activeCode);
			MopubSdk.getInstance().loginWithMobile(
				phoneNum,
				authCode,
				activeCode,
				false,
				delegate (LoginSuccessResult s) {
					logs.Add("loginWithMobile success");
					logs.Add(JsonUtility.ToJson(s));
				},
				delegate (MopubSDKError error) {
					logs.Add("loginWithMobile failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
			activeCode = "";
			phoneNum = "";
			authCode = "";
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "loginWithMobileOneKey", btnStyle)){
			logs.Add("call loginWithMobileOneKey activeCode:"+activeCode);
			MopubSdk.getInstance().loginWithMobile(
				phoneNum,
				authCode,
				activeCode,
				true,
				delegate (LoginSuccessResult s) {
					logs.Add("loginWithMobileOneKey success");
					logs.Add(JsonUtility.ToJson(s));
				},
				delegate (MopubSDKError error) {
					logs.Add("loginWithMobileOneKey failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
			activeCode = "";
			phoneNum = "";
			authCode = "";
		}
		
		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"邮箱：", fontStyle);
		email = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), email, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchEmailAuthCode", btnStyle)) {
			logs.Add ("call fetchEmailAuthCode");
			MopubSdk.getInstance ().fetchEmailAuthCode(
				email,
				delegate (FetchSuccessResult f){
					logs.Add("fetchEmailAuthCode success");
				},
				delegate (MopubSDKError error) {
					logs.Add("fetchEmailAuthCode failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"验证码：", fontStyle);
		authCode = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), authCode, inputStyle);

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"密码：", fontStyle);
		password = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), password, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "loginWithEmail", btnStyle)){
			logs.Add("call loginWithEmail activeCode:"+activeCode);
			MopubSdk.getInstance().loginWithEmail(
				email,
				password,
				authCode,
				activeCode,
				delegate (LoginSuccessResult s) {
					logs.Add("loginWithEmail success");
					logs.Add(JsonUtility.ToJson(s));
				},
				delegate (MopubSDKError error) {
					logs.Add("loginWithEmail failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
			activeCode = "";
			password = "";
			authCode = "";
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "loginWithVisitor", btnStyle))
		{
			logs.Add("call loginWithVisitor activeCode:" + activeCode);
			MopubSdk.getInstance().loginWithVisitor(
				activeCode,
				delegate (LoginSuccessResult s)
				{
					logs.Add("loginWithVisitor success");
					logs.Add(JsonUtility.ToJson(s));
				},
				delegate (MopubSDKError error)
				{
					logs.Add("loginWithVisitor failed");
					logs.Add(JsonUtility.ToJson(error));
				});

			activeCode = "";
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "linkEmail", btnStyle)) {
			logs.Add ("call linkEmail");
			MopubSdk.getInstance ().linkWithEmail (
				email,
				password,
				authCode,
				delegate(MopubSDKLinkWithEmailResult r) { 
					logs.Add ("linkEmail success");
					logs.Add (JsonUtility.ToJson (r)); 
				},
				delegate(MopubSDKError error) {
					logs.Add("linkEmail failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "linkMobile", btnStyle))
		{
			logs.Add("call linkMobile");

			MopubSdk.getInstance().linkWithMobile(phoneNum,
				authCode,
				delegate (MopubSDKLinkWithMobileResult r)
				{
					logs.Add("linkEmail success");
					logs.Add(JsonUtility.ToJson(r));
				},
				delegate (MopubSDKError error)
				{
					logs.Add("linkMobile failed");
					logs.Add(JsonUtility.ToJson(error));
				});
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "resetPasswordWithEmail", btnStyle)) {
			logs.Add ("call resetPasswordWithEmail");
			MopubSdk.getInstance ().resetPasswordWithEmail(
				email,
				password,
				authCode,
				delegate (string f){
					logs.Add("resetPasswordWithEmail success");
				},
				delegate (MopubSDKError error) {
					logs.Add("resetPasswordWithEmail failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "verifySessionToken", btnStyle)) {
			logs.Add ("call verifySessionToken");
			MopubSdkAccessToken accessToken = MopubSdk.getInstance ().currentAccessToken ();
			string token = accessToken.sessionToken;
			MopubSdk.getInstance ().verifySessionToken(
				token,
				delegate (VerifySuccessResult f){
					logs.Add("verifySessionToken success");
				},
				delegate (MopubSDKError error) {
					logs.Add("verifySessionToken failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "createInviteCode", btnStyle)) {
			logs.Add ("call createInviteCode");
			MopubSdk.getInstance ().createInviteCode(
				delegate (string code){
					logs.Add("createInviteCode success inviteCode:"+code);
				},
				delegate (MopubSDKError error) {
					logs.Add("createInviteCode failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchInviteeList", btnStyle)) {
			logs.Add ("call fetchInviteeList");
			MopubSdk.getInstance ().fetchInviteeList(
				delegate (List<object> res){
					var str = "受邀者[";
					foreach(var one in res){
						str += one + ",";
					}
					str = str.TrimEnd(',') + "]";
					logs.Add("fetchInviteeList success:" + str);
				},
				delegate (MopubSDKError error) {
					logs.Add("fetchInviteeList failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"邀请码：", fontStyle);
		inviteCode = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), inviteCode, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "uploadInviteCode", btnStyle)) {
			logs.Add ("call uploadInviteCode");
			MopubSdk.getInstance ().uploadInviteCode(
				inviteCode,
				delegate (string res){
					logs.Add("uploadInviteCode success gameAccountId:" + res);
				},
				delegate (MopubSDKError error) {
					logs.Add("uploadInviteCode failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
			inviteCode = "";
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "openPrevention", btnStyle)) {
			logs.Add ("call openPrevention");

			//MopubSdk.getInstance ().openPrevention();
			MopubSdk.getInstance().openRealnamePrevention();
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "getTotalOnlineTime", btnStyle)) {
			logs.Add ("call getTotalOnlineTime");
			//long res = MopubSdk.getInstance ().getTotalOnlineTime();
			long res = MopubSdk.getInstance().getPreventionTotalOnlineTime();
			logs.Add("total online time:" + res);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "getRealnameHeatBeat", btnStyle)) {
			logs.Add ("call getRealnameHeatBeat");
			//long res = MopubSdk.getInstance ().getTotalOnlineTime();
			MopubSDKRealnameHeartBeat res = MopubSdk.getInstance().getRealnameHeartBeat();

			string status = "";
			if (res.status == MopubSDKRealnameHeartBeatStatus.normal) {
				status = "正常";
			}
			else if (res.status == MopubSDKRealnameHeartBeatStatus.addictionPrevent) {
				status = "防沉迷控制";
			}

			logs.Add("今天累计在线时长：" + res.totalTime_ms +"，该实名用户所有游戏总时长：" + res.totalTimeIdCard_ms + "，状态：" + status);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchIdCardInfo", btnStyle)) {
			logs.Add ("call fetchIdCardInfo");

			MopubSdk.getInstance().queryRealnameInfo(
				delegate (MopubSDKRealnameInfo res) {
					logs.Add("fetchIdCardInfo success");
					logs.Add(JsonUtility.ToJson(res));
				},
				delegate (MopubSDKError error) {
					logs.Add("fetchIdCardInfo failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);


			//MopubSdk.getInstance ().fetchIdCardInfo(
			//	delegate (MopubSDKIdCardInfo res){
			//		logs.Add("fetchIdCardInfo success");
			//		logs.Add(JsonUtility.ToJson(res));
			//	},
			//	delegate (MopubSDKError error) {
			//		logs.Add("fetchIdCardInfo failed");
			//		logs.Add(JsonUtility.ToJson(error));
			//	}
			//);
		}

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"姓名：", fontStyle);
		name = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), name, inputStyle);

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"身份证：", fontStyle);
		idCard = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), idCard, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "verifyIdCard", btnStyle)) {
			logs.Add ("call verifyIdCard");

			MopubSdk.getInstance().realnameAuthentication(name,
				idCard,
				delegate (MopubSDKRealnameInfo res)
				{
					logs.Add("verifyIdCard success");
					logs.Add(JsonUtility.ToJson(res));
				},
				delegate (MopubSDKError error)
				{
					logs.Add("verifyIdCard failed");
					logs.Add(JsonUtility.ToJson(error));
				});

                    //MopubSdk.getInstance ().verifyIdCard(
                    //	name,
                    //	idCard,
                    //	delegate (string res){
                    //		logs.Add("verifyIdCard success");
                    //	},
                    //	delegate (MopubSDKError error) {
                    //		logs.Add("verifyIdCard failed");
                    //		logs.Add(JsonUtility.ToJson(error));
                    //	}
                    //);
        }

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"充值金额", fontStyle);
		queryAmount = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), queryAmount, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchPaidAmount", btnStyle)) {
			logs.Add ("call fetchPaidAmount");
			MopubSdk.getInstance().fetchPaidAmountMonthly(
				int.Parse(queryAmount),
				delegate (MopubSDKRealnamePaidAmountInfo res) {
					logs.Add("fetchPaidAmount success");

					string status = "";
					if (res.status == MopubSDKRealnamePaymentStatus.normal) {
						status = "允许充值";
					}
					else if (res.status == MopubSDKRealnamePaymentStatus.forbiddenUserUnderEight) {
						status = "小于8岁，不允许充值";
					}
					
					else if (res.status == MopubSDKRealnamePaymentStatus.forbiddenAmountExceedLimit) {
						status = "超过总充值上限，不允许充值";
					}
					else if (res.status == MopubSDKRealnamePaymentStatus.forbiddenOnceExceedLimit) {
						status = "超过单次充值上限，不允许充值";
					}

					logs.Add("查询金额: " + res.queryAmount + ", 本月充值金额：" + res.rechargeAmount + ", 状态: " + status);
				},
				delegate (MopubSDKError error) {
					logs.Add("fetchPaidAmount failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
			//MopubSdk.getInstance ().fetchPaidAmount(
			//	delegate (string res){
			//		logs.Add("fetchPaidAmount success");
			//		logs.Add("本月充值金额：" + res);
			//	},
			//	delegate (MopubSDKError error) {
			//		logs.Add("fetchPaidAmount failed");
			//		logs.Add(JsonUtility.ToJson(error));
			//	}
			//);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "currentToken", btnStyle)) {
			logs.Add ("call currentAccessToken");
			var result = MopubSdk.getInstance ().currentAccessToken ();
			logs.Add (JsonUtility.ToJson (result) + "");
		}
		
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "puid", btnStyle)) {
			logs.Add ("call puid");
			var result = MopubSdk.getInstance ().getPuid ();
			logs.Add (result);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "showRegion", btnStyle)) {
			logs.Add ("call showRegion");
			var result = MopubSdk.getInstance ().getRegion();
			logs.Add ("region: " + result);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "linkGameCenter", btnStyle)) {
			logs.Add ("call linkgamecenter");
			MopubSdk.getInstance ().linkWithGameCenter (
				delegate(MopubSDKLinkWithGameCenterResult r) { 
					logs.Add ("linkGameCenter success");
					logs.Add (JsonUtility.ToJson (r)); 
				},
				delegate(MopubSDKError error) {
					logs.Add("linkGameCenter failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "linkFacebook", btnStyle)) {
			logs.Add ("call linkfacebook");
			MopubSdk.getInstance ().linkWithFacebook (
				delegate(MopubSDKLinkWithFacebookResult r) { 
					logs.Add ("linkFacebook success");
					logs.Add (JsonUtility.ToJson (r)); 
				},
				delegate(MopubSDKError error) {
					logs.Add("linkFacebook failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "linkWeChat", btnStyle)) {
			logs.Add ("call linkwechat");
			MopubSdk.getInstance ().linkWithWeChat (
				delegate(MopubSDKLinkWithWeChatResult r) { 
					logs.Add ("linkWeChat success");
					logs.Add (JsonUtility.ToJson (r)); 
				},
				delegate(MopubSDKError error) {
					logs.Add("linkWeChat failed");
					logs.Add(JsonUtility.ToJson(error));
				}
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchItemDetails", btnStyle)) {
			logs.Add ("call fetchItemDetails");
			MopubSdk.getInstance ().fetchPaymentItemDetails (
				delegate(List<MopubSDKPaymentItemDetails> itemDetails){
					logs.Add("call fetchItemDetails success");
					var str = "";
					foreach(var one in itemDetails){
						str += JsonUtility.ToJson(one) + "\n";
					}
					logs.Add(str);
					//save the pay items
					this.MopubSDKPaymentItemDetails = itemDetails;
				}, null
			);
		}

		if (MopubSDKPaymentItemDetails != null && MopubSDKPaymentItemDetails.Count > 0) {
			foreach (var itemDetails in MopubSDKPaymentItemDetails) {
				y += buttonHeight + 10;
				String itemId = itemDetails.itemID;
				if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "buy itemId:"+ itemId, btnStyle)) {
					logs.Add ("call startPayment with "+itemDetails.itemID+" ");
					long startPaymentIndex=DateTime.Now.Ticks;
					string cporderId = startPaymentIndex.ToString();//CP could a uniqe string to mark this payment
					MopubSdk.getInstance ().startPayment (
						new MopubSDKStartPaymentInfo(
							itemId,
							cporderId,
							"char_people_1",
							"1",
							"world",
							"1"
						),
						delegate(MopubSDKPaymentInfo info){
							//the payment submit success and wait for the unconsumed item callback
							logs.Add("payment have submit success "+itemId+" "+cporderId);
						}, 
						delegate(MopubSDKPaymentInfo paymentInfo, MopubSDKError error) {
							logs.Add("payment failed");
							logs.Add(JsonUtility.ToJson(error));
						}
					);
				}

			}
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchUnconsumeItems", btnStyle)) {
			logs.Add ("call fetchUnconsumeItems");
			MopubSdk.getInstance ().fetchUnconsumedPurchasedItems (
				delegate(List<MopubSDKPurchasedItem> r){
					if(r == null || r.Count == 0){
						logs.Add("none Unconsumed Purchased Items");
						return;
					}
					var str = "";
					foreach(var one in r){
						str += JsonUtility.ToJson(one) + "\n";
					}
					logs.Add(str);

				},
				null
			);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "fetchAllPurchasedItems", btnStyle)) {
			logs.Add ("call fetchAllPurchasedItems");
			MopubSdk.getInstance ().fetchAllPurchasedItems (
				delegate(List<MopubSDKPurchasedItem> r){
					if(r == null || r.Count == 0){
						logs.Add("none Purchased Items");
						return;
					}
					var str = "";
					foreach(var one in r){
						str += JsonUtility.ToJson(one) + "\n";
					}
					logs.Add(str);
				},
				null
			);

		}

        y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "fetch all purchased subscription items", btnStyle))
        {
            logs.Add("call fetchAllPurchasedSubscriptionItems");
            MopubSdk.getInstance().fetchAllPurchasedSubscriptionItems(
                delegate (List<MopubSDKSubscriptionItem> r) {
                    if (r == null || r.Count == 0)
                    {
                        logs.Add("nones Purchased subscription ");
                        return;
                    }
                    var str = "";
                    foreach (var one in r)
                    {
                        str += JsonUtility.ToJson(one) + "\n";
                    }
                    logs.Add(str);
                }
            );

        }

        y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "restore subscription", btnStyle))
        {
            logs.Add("call restoreSubscription");
            MopubSdk.getInstance().restoreSubscription(
                delegate ()
                {
                    logs.Add("restore success");
                },
                delegate (MopubSDKError error)
                {
                    logs.Add("restore failed, please retry");
                });

        }
		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0,y,buttonWith,buttonHeight),"hasSmartAd",btnStyle)){
			var result = MopubSdk.getInstance ().hasSmartAd ("wheel");
			logs.Add("call hasSmartAd "+ result);
		}

		y += buttonHeight +10;
		if(GUI.Button(new Rect(0,y,buttonWith,buttonHeight),"showSmartAd",btnStyle)){
            logs.Add ("call showSmartAd ");
			MopubSdk.getInstance ().showSmartAd ("wheel");
		}

        y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "hasReward", btnStyle)) {
			var result = MopubSdk.getInstance ().hasRewardedVideo ("wheel");
			logs.Add ("call hasRewardedVideo " + result);
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "showRewardVideoAd", btnStyle)) {
			logs.Add ("call showRewardVideoAd ");
			MopubSdk.getInstance ().showRewardVideoAd ("wheel");
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "hasInterstitial", btnStyle)) {
			var result = MopubSdk.getInstance ().hasInterstitial ("wheel");
			logs.Add ("call hasInterstitial " + result + " ");
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "showInterstitialAd", btnStyle)) {
			logs.Add ("call showInterstitialAd ");
			MopubSdk.getInstance ().showInterstitialAd ("wheel");
		}

        y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "show native when loaded", btnStyle))
        {
            logs.Add("call hasNativeAd ");
            bool hasNative = MopubSdk.getInstance().hasNativeAd("default");
            if (hasNative)
            {
                logs.Add("call showNativeAd ");
                MopubSdk.getInstance().showNativeAdFixed("default", SDKNavtiveAdPosition.bottomCenter, 0);
            }
            else
            {
                logs.Add("don't have native ad");
            }
        }

        y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "close native ad", btnStyle))
        {
            logs.Add("call closeNativeAd ");
            MopubSdk.getInstance().closeNativeAd("default");
        }

        y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "showBannerAd", btnStyle))
        {
            logs.Add("call showBannerAd ");
            MopubSdk.getInstance().showBanner(BannerADPosition.bottomCenter);
        }

        y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "dissmisBannerAd", btnStyle))
        {
            logs.Add("call dismissBannerAd ");
            MopubSdk.getInstance().dismissBanner();
        }


        y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logCustomAFEvent", btnStyle)) {
			logs.Add ("call logCustomAFEvent");
			Dictionary<string, object> params1 = new Dictionary<string, object>();
			params1.Add("af_content_type","consume");
			params1.Add("af_content_id","1001");
			params1.Add("af_quantity",1);
			MopubSdk.getInstance ().logCustomAFEvent ("testLogCustomAFEvent", params1);
		}

        y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "testLog", btnStyle)) {
			logs.Add ("call testLog ");
			MopubSdk.getInstance ().testLog ("testLog");
		}
		
		
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logStartLevel", btnStyle)) {
			logs.Add ("call logStartLevel ");
			MopubSdk.getInstance ().logStartLevel ("testUnityLevelName");
		}
		
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logFinishLevel", btnStyle)) {
			logs.Add ("call logFinishLevel ");
			MopubSdk.getInstance ().logFinishLevel ("testUnityLevelName");
		}
		
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logUnlockLevel", btnStyle)) {
			logs.Add ("call logUnlockLevel ");
			MopubSdk.getInstance ().logUnlockLevel ("testLogUnlockLevel");
		}
		
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logSkipLevel", btnStyle)) {
			logs.Add ("call logSkipLevel ");
			MopubSdk.getInstance ().logSkipLevel ("testLogSkipLevel");
		}
		
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logSkinUsed", btnStyle)) {
			logs.Add ("call logSkinUsed ");
			MopubSdk.getInstance ().logSkinUsed ("testLogSkinUsed");
		}
		
		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "logAdEntrancePresent", btnStyle)) {
			logs.Add ("call logAdEntrancePresent ");
			MopubSdk.getInstance ().logAdEntrancePresent ("testLogAdEntrancePresent");
		}

        y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "jump to chat app", btnStyle))
        {
            logs.Add("call openJoinChatGroup ");
            bool res = MopubSdk.getInstance().openJoinChatGroup();
            if (res)
            {
                logs.Add("Jump success");
            }
            else
            {
                logs.Add("Jump failed");
            }
        }

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "jump to whatsapp", btnStyle))
		{
			logs.Add("call openJoinWhatsappChatting ");
			bool res = MopubSdk.getInstance().openJoinWhatsappChatting();
			if (res)
			{
				logs.Add("Jump success");
			}
			else
			{
				logs.Add("Jump failed");
			}
		}

		y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "open rating view", btnStyle))
        {
            logs.Add("call openRatingView ");
            bool res = MopubSdk.getInstance().openRatingView();
            if (res)
            {
                logs.Add("open success");
            }
            else
            {
                logs.Add("open failed");
            }
        }

		y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "open rating ", btnStyle))
        {
            logs.Add("call openRating ");
            bool res = MopubSdk.getInstance().openRate();
            if (res)
            {
                logs.Add("open success");
            }
            else
            {
                logs.Add("open failed");
            }
        }
		
		y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "add local notification", btnStyle))
        {
            logs.Add("call addLocalNotification");
			string date = DateTime.Now.ToString("yyyyMMdd");
			string hour = DateTime.Now.ToString("HH");
			string min = DateTime.Now.ToString("mm");
            bool res = MopubSdk.getInstance().addLocalNotification(
							new MopubSDKLocalMsg("this is title","this is content",date,hour,min)
							);
            if (res)
            {
                logs.Add("add success");
            }
            else
            {
                logs.Add("add failed");
            }
        }

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"账号类型：", fontStyle);
		accountType = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), accountType, inputStyle);

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"账号名称：", fontStyle);
		accountName = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), accountName, inputStyle);

		y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "bind accounts", btnStyle))
        {
            logs.Add("call bind accounts");
			Dictionary<int, string> params1 = new Dictionary<int, string>();
			params1.Add(Int32.Parse(accountType), accountName);
            MopubSdk.getInstance().bindAccounts(params1, delegate(string arg){
				logs.Add("bind accounts success");
			}, delegate(MopubSDKError error){
				logs.Add("bind accounts failed");
				logs.Add(JsonUtility.ToJson(error));
			});
        }
		
		y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "launch customer system", btnStyle))
        {
            logs.Add("call launchCustomer");
            MopubSdk.getInstance().launchCustomer();
        }

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"中间页类型：", fontStyle);
		pageType = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), pageType, inputStyle);

		y += buttonHeight + 10;
		GUI.Label(new Rect(0,y,130,buttonHeight),"邮箱：", fontStyle);
		csEmail = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), csEmail, inputStyle);

		y += buttonHeight + 10;
        if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "launch customer system with page", btnStyle))
        {
            logs.Add("call launchCustomerWithTransitPage");
			if (showAmazonGift == true){
            	MopubSdk.getInstance().launchCustomerWithTransitPage(pageType, csEmail);
			}else{
				logs.Add("entrance closed");
			}
        }

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "launch customer system with page", btnStyle))
		{
			logs.Add("call launchCustomerWithTransitPage");
			if (showAmazonGift == true)
			{
				MopubSdk.getInstance().launchCustomerWithTransitPage(pageType, csEmail);
			}
			else
			{
				logs.Add("entrance closed");
			}
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "get game version", btnStyle))
		{
			logs.Add("call getGameVersion");
			logs.Add("gameVersion: " + MopubSdk.getInstance().getGameVersion());
		}

		y += buttonHeight + 10;
		if (GUI.Button (new Rect (0, y, buttonWith, buttonHeight), "showTimeStamp", btnStyle)) {
			logs.Add ("call showTimeStamp");
			var result = MopubSdk.getInstance ().getTimeStamp();
			logs.Add ("showTimeStamp: " + result);
		}


		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "log market custom event", btnStyle))
		{
			logs.Add("call logMarketCustomEvent");
			Dictionary<string, string> params1 = new Dictionary<string, string>();
			params1.Add("key","value");
			AndroidJavaObject map = CreateJavaMapFromDictionary(params1);
			AndroidJavaObject marketSdk = new AndroidJavaClass("com.superera.market.SupereraMarketSDK").CallStatic<AndroidJavaObject>("getInstance");
			marketSdk.Call("logCustomEvent", "eventName", map);
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "log market register", btnStyle))
		{
			logs.Add("logMarketRegister");
			AndroidJavaObject marketSdk = new AndroidJavaClass("com.superera.market.SupereraMarketSDK").CallStatic<AndroidJavaObject>("getInstance");
			marketSdk.Call("onRegister", "wechat", true);
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "log market login", btnStyle))
		{
			logs.Add("logMarketLogin");
			AndroidJavaObject marketSdk = new AndroidJavaClass("com.superera.market.SupereraMarketSDK").CallStatic<AndroidJavaObject>("getInstance");
			marketSdk.Call("onLogin", "wechat", true);
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "log market purchase", btnStyle))
		{
			logs.Add("logMarketPurchase");
			AndroidJavaObject marketSdk = new AndroidJavaClass("com.superera.market.SupereraMarketSDK").CallStatic<AndroidJavaObject>("getInstance");
			marketSdk.Call("onPurchase", "gift","flower", "008",1,"wechat","¥", true, 1);
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "is user notification enable", btnStyle))
		{
			logs.Add("is user notification enable");
			bool enable = MopubSdk.getInstance().isUserNotificationEnable();
            if (enable)
            {
				logs.Add("notification enable");
			}
            else
            {
				logs.Add("notification disable");
			}
			
		}

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "open system notification setting", btnStyle))
		{
			logs.Add("open system notification setting");
			MopubSdk.getInstance().openSystemNotificationSetting();
		}

		y += buttonHeight + 10;
		GUI.Label(new Rect(0, y, 130, buttonHeight), "页码：", fontStyle);
		rankPageString = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), rankPageString, inputStyle);

		y += buttonHeight + 10;
		GUI.Label(new Rect(0, y, 130, buttonHeight), "数量：", fontStyle);
		rankSizeString = GUI.TextField(new Rect(130, y, buttonWith - 130, buttonHeight), rankSizeString, inputStyle);

		y += buttonHeight + 10;
		if (GUI.Button(new Rect(0, y, buttonWith, buttonHeight), "fetch ranking list", btnStyle))
		{
			logs.Add("fetch ranking list");

			MopubSdk.getInstance().fetchRanking("demoCharID", int.Parse(rankPageString), int.Parse(rankSizeString), delegate(MopubSDKRanking ranking) {

				logs.Add("fetch ranking list success");
				logs.Add(ranking.ToString());

			}, delegate(MopubSDKError error) {

				logs.Add("fetch ranking list failed");
				logs.Add(JsonUtility.ToJson(error));
			});
		}

		GUI.EndScrollView ();

		totalScrollHeight = y + 100;

	}

	public static AndroidJavaObject CreateJavaMapFromDictionary(IDictionary<string, string> parameters){
    	AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
    	IntPtr putMethod = AndroidJNIHelper.GetMethodID(javaMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
    	object[] args = new object[2];
    	foreach (KeyValuePair<string, string> kvp in parameters){
        	using (AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key)){
            	using (AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value)){
                	args[0] = k;
                	args[1] = v;
                	AndroidJNI.CallObjectMethod(javaMap.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
            	}
        	}
    	}

    	return javaMap;
	}

	void Update(){
		if (Input.touchCount > 0) {
			
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Moved) {
				if (touch.position.y < Screen.height/4 * 3) {
					btnScrollPosition.y += touch.deltaPosition.y;
				} else {
					logScrollPosition.x -= touch.deltaPosition.x;
					logScrollPosition.y += touch.deltaPosition.y;
				}
			}
		}
	}
}