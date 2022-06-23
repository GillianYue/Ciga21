using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MopubNS
{
	//base class
	[Serializable]
	public class MopubSDKError
	{
		public string errorDomain;
		public int domainCode;
		public string domainMessage;
		public int clientCode;
		public string clientMessage;
		public string exception;

		public MopubSDKError (string errorDomain, int domainCode, string domainMessage, int clientCode, string clientMessage, string exception)
		{
			this.errorDomain = errorDomain;
			this.domainCode = domainCode;
			this.domainMessage = domainMessage;
			this.clientCode = clientCode;
			this.clientMessage = clientMessage;
			this.exception = exception;
		}

        public override string ToString()
        {
            var log = new System.Text.StringBuilder("{");
            log.Append("errorDomain=");
            log.Append(errorDomain);

            log.Append(",domainCode=");
            log.Append(domainCode);

            if (domainMessage != null)
            {
                log.Append(", domainMessage=");
                log.Append(domainMessage);
            }
            if (clientMessage != null)
            {
                log.Append(", clientCode=");
                log.Append(clientCode);
                log.Append(", clientMessage=");
                log.Append(clientMessage);  
            }
			if (exception != null)
			{
				log.Append(", exception=");
				log.Append(exception);
			}
            log.Append("}");
            return log.ToString();
        }
    }
	//--- init ---
	[Serializable]
	public class InitSuccessResult
	{
		
	}

	//--- login ---
	public enum AccountType 
	{
		UNKNOWN = 0,
		DEVICE = 1,
		IOS_GAME_CENTER = 2,
		GOOGLE_PLAY_GAME = 3,
		FACEBOOK = 4,
		WE_CHAT = 5,
		MOBILE = 6,
		EMAIL = 7,
        VISITOR = 8
	}

	[Serializable]
	public class MopubSdkThirdPartyAccount
	{
		//public string nickname;
		public AccountType accountType;

		public MopubSdkThirdPartyAccount (AccountType accountType)
		{
			this.accountType = accountType;
		}
	}


	[Serializable]
	public class MopubSdkLinkedAccount
	{
		public List<MopubSdkThirdPartyAccount> accounts;

		public MopubSdkLinkedAccount (List<MopubSdkThirdPartyAccount> accounts)
		{
			this.accounts = accounts;
		}
	}

	[Serializable]
	public class MopubSdkAccessToken
	{
		public string accountID;
		public string sessionToken;
		public MopubSdkLinkedAccount linkedAccount;
		public string lastLoginTimestamp;
		public string currentNickName;

		public MopubSdkAccessToken (string accountID, string sessionToken, string lastLoginTimestamp, string currentNickName, MopubSdkLinkedAccount linkedAccount)
		{
			this.accountID = accountID;
			this.sessionToken = sessionToken;
			this.lastLoginTimestamp = lastLoginTimestamp;
			this.currentNickName = currentNickName;
			this.linkedAccount = linkedAccount;
		}
	}

	[Serializable]
	public class LoginSuccessResult
	{
		public MopubSdkAccessToken accessToken;

		public LoginSuccessResult (MopubSdkAccessToken accessToken)
		{
			this.accessToken = accessToken;
		}
	}

	[Serializable]
	public class FetchSuccessResult
	{
	}

	[Serializable]
	public class VerifySuccessResult
	{

	}

	[Serializable]
	public class MopubSDKLinkWithGameCenterResult
	{
		//TODO
	}

	[Serializable]
	public class MopubSDKLinkWithFacebookResult
	{
		//TODO
	}

	[Serializable]
	public class MopubSDKLinkWithWeChatResult
	{
		//TODO
	}

	[Serializable]
	public class MopubSDKLinkWithEmailResult
	{
		//TODO
	}

	[Serializable]
	public class MopubSDKLinkWithMobileResult
	{
		//TODO
	}

	[Serializable]
	public class MopubSDKIdCardInfo
	{
		public string birthday;

		public MopubSDKIdCardInfo(string birthday)
		{
			this.birthday = birthday;
		}
	}

    [Serializable]
    public enum MopubSDKRealnameStatus
    {
		MopubSDKRealnameStatusNotAuthentication,
		MopubSDKRealnameStatusVerifying,
		MopubSDKRealnameStatusVerified,
		MopubSDKRealnameStatusVerifyFailed,
        MopubSDKRealnameStatusVerifyFailedThreeTimes
	}

    [Serializable]
    public class MopubSDKRealnameInfo
    {
		public MopubSDKRealnameStatus status;
		public string birthday;

        public MopubSDKRealnameInfo(MopubSDKRealnameStatus status)
        {
			this.status = status;
			this.birthday = "0000-00-00";
        }

        public MopubSDKRealnameInfo(MopubSDKRealnameStatus status, string birthday)
        {
			this.status = status;
			this.birthday = birthday;
        }
    }

	[Serializable]
	public enum MopubSDKRealnameHeartBeatStatus
	{
		normal,                // normal
		addictionPrevent       // addiction prevention
	}

	[Serializable]
	public class MopubSDKRealnameHeartBeat {
		public long totalTime_ms;            // this game, today total time
		public long totalTimeIdCard_ms;      // this realname user, all game, today total time
		public MopubSDKRealnameHeartBeatStatus status; 
	}

	[Serializable]
	public enum MopubSDKRealnamePaymentStatus {
		normal,                            // Normal
		forbiddenUserUnderEight,           // Forbidden because user under eight
		forbiddenAmountExceedLimit,        // Forbidden because exceed limit
		forbiddenOnceExceedLimit           // Forbidden because once exceed limit
	}

	[Serializable]
	public class MopubSDKRealnamePaidAmountInfo {

		public long queryAmount;
		public long rechargeAmount;
		public MopubSDKRealnamePaymentStatus status;

	}

	//--- pay ---
	[Serializable]
	public enum MopubSDKItemConsumeState
	{
		MopubSDKItemConsumeStateUnconsumed,
		MopubSDKItemConsumeStateConsumed
	}

    //Consumable item purchased by player
	[Serializable]
	public class MopubSDKPurchasedItem
	{
		public string itemID;
		public string sdkOrderID;
		public long purchaseTimeMS;
		public long consumeTimeMS;
		public string cpOrderID;
		public MopubSDKItemConsumeState consumeState;

		public MopubSDKPurchasedItem (string itemID, string sdkOrderID, long purchaseTimeMS,
		                                 long consumeTimeMS, string cpOrderID, MopubSDKItemConsumeState consumeState)
		{
			this.itemID = itemID;
			this.sdkOrderID = sdkOrderID;
			this.purchaseTimeMS = purchaseTimeMS;
			this.consumeTimeMS = consumeTimeMS;
			this.consumeState = consumeState;
			this.cpOrderID = cpOrderID;
		}
	}

    public enum MopubSDKItemSubscriptionState
    {
        REFUND = -2,
        TERMINATED = -1,
        INIT = 0,
        ACTIVE = 1,
        GRACE = 2,
        REVERSED = 3,
        CANCEL = 4,
    }

    //Subscription item purchased by player
    [Serializable]
    public class MopubSDKSubscriptionItem
    {
        public string itemID;
        public string sdkOrderID;
        public long validTimeMS;
        public long invalidTimeMS;
        public MopubSDKItemSubscriptionState state;

        public MopubSDKSubscriptionItem(string itemID, string sdkOrderID, long validTimeMS,
                                         long invalidTimeMS, MopubSDKItemSubscriptionState state)
        {
            this.itemID = itemID;
            this.sdkOrderID = sdkOrderID;
            this.validTimeMS = validTimeMS;
            this.invalidTimeMS = invalidTimeMS;
            this.state = state;
        }
    }
        

    [Serializable]
	public enum PaymentItemType
	{
		PaymentItemTypeConsumable = 1,
        PaymentItemTypeSubscription = 2

    }
	[Serializable]
	public class MopubSDKPaymentItemDetails
	{
		public string itemID;
		public string displayName;
		public long price;
		public string currency;
		public string formattedPrice;
		public PaymentItemType type;

		public MopubSDKPaymentItemDetails (string itemID, string displayName, long price, string currency,
		                                      string formattedPrice,
		                                      PaymentItemType type)
		{
			this.itemID = itemID;
			this.displayName = displayName;
			this.price = price;
			this.currency = currency;
			this.formattedPrice = formattedPrice;
			this.type = type;
		}

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
	[Serializable]
	public class MopubSDKStartPaymentInfo
	{
		public string itemID;
		public string cpOrderID;
		public string characterName;
		public string characterID;
		public string serverName;
		public string serverID;

		public MopubSDKStartPaymentInfo (string itemID, string cpOrderID,
			string characterName,
			string characterID,
			string serverName,
			string serverID)
		{
			this.itemID = itemID;
			this.cpOrderID = cpOrderID;
			this.characterName = characterName;
			this.characterID = characterID;
			this.serverName = serverName;
			this.serverID = serverID;
		}

	}
	[Serializable]
	public class MopubSDKPaymentInfo
	{
		public string itemID;
		public string cpOrderID;
		public string sdkOrderID;
		public long price;
		public string currency;
		public string characterName;
		public string characterID;
		public string serverName;
		public string serverID;

		public MopubSDKPaymentInfo (string itemID, string cpOrderID,
		                               string sdkOrderID,
		                               long price,
		                               string currency,
		                               string characterName,
		                               string characterID,
		                               string serverName,
		                               string serverID)
		{
			this.itemID = itemID;
			this.cpOrderID = cpOrderID;
			this.sdkOrderID = sdkOrderID;
			this.price = price;
			this.currency = currency;
			this.characterName = characterName;
			this.characterID = characterID;
			this.serverName = serverName;
			this.serverID = serverID;
		}
		
	}

	[Serializable]
	public class MopubSDKRanking
    {
		public MopubSDKRankingInfo userInfo;
		public List<MopubSDKRankingInfo> rankList;

		public override string ToString()
		{
			return JsonUtility.ToJson(this);
		}
	}

	[Serializable]
	public class MopubSDKRankingInfo
    {
		public string uid;
		public int rank;
		public int level;
		public string nickname;

        public override string ToString()
        {
			return JsonUtility.ToJson(this);
		}
    }


	//--- ad ---
	public enum BannerADPosition
    {
        bottomCenter, topCenter
    }

    public enum SDKNavtiveAdPosition
    {
        bottomCenter, topCenter
    }
	
	[Serializable]
	public class MopubSDKLocalMsg
	{
		public string title;
		public string content;
		public string date;
		public string hour;
		public string min;
		
		public MopubSDKLocalMsg(string title,string content,
							       string date,
								   string hour,
								   string min)
		{
			this.title = title;
			this.content = content;
			this.date = date;
			this.hour = hour;
			this.min = min;
		}
	}
	

    public class MopubSdk :
	#if UNITY_EDITOR
	MopubEditor
	#elif UNITY_ANDROID
	MopubAndroid
	#else
	MopubiOS
	#endif
	{
		static private MopubSdk instance;

		static public MopubSdk getInstance ()
		{
			if (instance == null) {
				instance = new MopubSdk ();
			}
			return instance;
		}

		public static long GetTimeStamp ()
		{ 
			TimeSpan ts = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0, 0); 
			return Convert.ToInt64 (ts.TotalSeconds); 
		}


	}

	[Serializable]
	public class MopubAdInfo
    {
		public string networkType;
		public string adUnitId;
		public string placementId;

        public MopubAdInfo (string networkType, string adUnitId, string placementId) {
			this.networkType = networkType;
			this.adUnitId = adUnitId;
			this.placementId = placementId;
        }

    }


}