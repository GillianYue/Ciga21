//
//  MopubSDKManager.h
//  Mopub-ios-sdk-unity
//
//  Created by 王城 on 2019/6/5.
//  Copyright © 2019 Mopub. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

@interface MopubSDKManager : NSObject

+ (instancetype)sharedInstance;

+ (void)SDKInit:(nullable NSString *)gameContentVersion;

+ (void)addEventGlobalParams:(NSString *)jsonParams;

///auth

+ (void)showLinkAccountView;

+ (void)showSwitchAccountView;

+ (void)showDeleteAccountView;

+ (void)showAccountCenter;

+ (void)showLinkedAfterPurchaseView;

+ (BOOL)isAccountOnlyTourist;

+ (void)showArchive:(NSString *)jsonParams;

+ (void)setLanguage:(NSString *)language;

+ (void)openNoticeDialog;

+ (void)login;

+ (void)loginWithDevice;
+ (void)loginDeviceWithActiveCode:(NSString *)activeCode;

+ (void)loginWithWeChat;
+ (void)loginWeChatWithActiveCode:(NSString *)activeCode;

+ (void)fetchMobileAuthCodeWithPhoneNumber:(NSString *)phoneNumber;

+ (void)loginWithMobile:(NSString *)phoneNumber authCode:(NSString *)authCode activeCode:(NSString *)activeCode isOneKeyLogin:(BOOL)isOneKeyLogin;

+ (void)createInviteCode;

+ (void)fetchInviteeList;

+ (void)uploadInviteCode:(NSString *)code;

+ (void)verifySessionToken: (NSString *)token;

+ (void)linkWithGameCenter;

+ (void)linkWithFacebook;

+ (void)linkWithWeChat;

+ (void)linkWithMobile: (NSString *)phoneNumber authCode:(NSString *)authCode;

+ (void)fetchEmailAuthCodeWithEmail:(NSString *)email;

+ (void)loginWithEmail:(NSString *)email
              password:(NSString *)password
              authCode:(NSString *__nullable)authCode
            activeCode:(NSString *__nullable)activeCode;

+ (void)resetPasswordWithEmail:(NSString *)email newPassword:(NSString *)newPassword authCode:(NSString *)authCode;

+ (void)linkWithEmail:(NSString *)email password:(NSString *)password authCode:(NSString *)authCode;

+ (void)loginVisitorWithActiveCode: (NSString *)activeCode ;

+ (void)reLoginFlow;
/**
 返回当前accessToken的json

 @return json形式的accessToken信息
 */
+ (nullable NSString *)currentAccessTokenJSON;

/**
 获取当前设备的puid
 */
+ (NSString *)getPuid;

/**
 获取当前国家地区的代码
 */
+ (NSString *)getRegion;

/**
 获取包版本号
 */
+ (NSString *)getPackageVersionCode;

/**
 获取cgi
 */
+ (NSString *)getCgi;

/**
 获取当前游戏版号
 */
+ (NSString *)getGameVersion;

/**
 打开客服系统
 */
+ (void)launchCustomer;

/**
 Open customer service and redict to transit page
 */
+ (void)launchCustomerWithTransitPage:(NSString *)pageType;

/**
 Open customer service with email
 */
+ (void)launchCustomerWithTransitPage:(NSString *)pageType email:(NSString *_Nullable)email;

/**
 set unread messages listener
 */
+ (void)setUnreadMessageUpdatedListener;

///analytics

+ (void)logCustomEventWithEventName:(NSString *)eventName jsonParams:(NSString *)jsonParams;

+ (void)logCustomAFEventWithEventName:(NSString *)eventName jsonParams:(NSString *)jsonParams;


///ad
+ (BOOL)hasRewardedVideoWithGameEntry:(NSString *)gameEntry;
+ (void)showRewardVideoAdWithGameEntry:(NSString *)gameEntry;

+ (BOOL)hasInterstitialWithGameEntry:(NSString *)gameEntry;
+ (void)showInterstitialAdWithGameEntry:(NSString *)gameEntry;


///payment
+ (void)fetchPaymentItemDetails;
+ (void)fetchUnconsumedPurchasedItems;
+ (void)fetchAllPurchasedItems;
+ (void)setUnconsumedItemUpdatedListener;

+ (void)consumePurchaseWithSDKOrderID:(NSString *)sdkOrderID;
+ (void)startPaymentWithItemID:(NSString *)itemID cpOrderID:(nullable NSString *)cpOrderID characterName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID level:(int)level;

///subscription
+ (void)fetchAllPurchasedSubscriptionItem;

+ (void)restoreSubscription;

+ (void)setSubscriptionItemUpdatedListener;

///ingame
+ (void)setIngameParamsListener;

///banner
+ (void)showBanner:(int)position;
+ (void)dismissBanner;

+ (void)log:(NSString *)log;


///native ad
+ (BOOL)hasNativeAd:(NSString *)gameEntry;

+ (void)showNativeAdFixed:(NSString *)gameEntry position:(int)position spacing:(float) spacing;

+ (void)closeNativeAd:(NSString *)gameEntry;

///social
+ (BOOL)openJoinChatGroup;

+ (BOOL)openJoinWhatsappChatting;

+ (BOOL)openRatingView;

+ (BOOL)openRating;

+ (BOOL) addLocalNotifaciton:(NSString *)title content:(NSString *)content date:(NSString *)date hour:(NSString *)hour min:(NSString *)min;

#pragma -mark game log
+ (void)logPlayerInfoWithCahrName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID characterLevel:(int)characterLevel serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID;

+ (void)logPlayerInfoWithCahrName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID characterLevel:(int)characterLevel serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID extraData:(NSString *)extraDataJSONString;

+ (void)logStartLevel:(nullable NSString *)levelName;

+ (void)logFinishLevel:(nullable NSString *)levelName;

+ (void)logUnlockLevel:(nullable NSString *)levelName;

+ (void)logSkipLevel:(nullable NSString *)levelName;

+ (void)logSkinUsed:(nullable NSString *)skinName;

+ (void)logAdEntrancePresent:(nullable NSString *)name;

/**
 appsflyer 数据回调
 */
+ (void)setAFDataDelegate;

// 旧防沉迷
+ (void)openAddctionPrevention;

+ (long)getTotalOnlineTime;

+ (void)verifyIdCardWithName:(NSString *)name cardNumber:(NSString *)cardNumber;

+ (void)fetchIdCardInfo;

+ (void)fetchPaidAmount:(int)amount;

// 新防沉迷
+ (void)openRealnamePrevention;
+ (long)getPreventionTotalOnlineTime;
+ (NSString *)getRealnameHeartBeat;
+ (void)realnameAuthentication:(NSString *)name cardNumber:(NSString *)cardNumber;
+ (void)queryRealnameInfo;
+ (void)fetchPaidAmountMonthly:(int)amount;

+ (void)logout;

+ (BOOL)isUserNotificationEnable;

+ (void)openSystemNotificationSetting;

+ (void)startRealnameAuthenticationWithUI;

+ (void)fetchRankingWithUid:(NSString *)uid page:(int)page size:(int)size;

+ (NSString *)getTimeStamp;

+ (void)saveCloudCacheWithUid:(NSString *)uid version:(NSInteger)version data:(NSString *)data;

+ (void)getCloudCacheWithUid:(NSString *)uid;

+ (void)getRedeemWithCode:(NSString *)code;

+ (void)pushSDKInitWithCgi:(NSString *)cgi appid:(NSString *)appid appSecret:(NSString *)appSecret passThroughEnable:(BOOL)passThroughEnable logHost:(NSString *)logHost logPath:(NSString *)logPath;

+ (void)setPushAlias:(NSString *)alias;


+ (void)autoLoginTouristWithUI;

+ (NSDictionary *)getCpParams;

+ (NSDictionary *)getSDKInfo;

+ (NSDictionary *)getCurrentSharedCampaign;

+ (NSDictionary *)getCurrentInvitedCampaign;

+ (void)openShareToWechatForEntrance:(NSString *)entrance shareData:(NSString *)shareData;

+ (void)openShareToQQForEntrance:(NSString *)entrance shareData:(NSString *)shareData;

+ (void)fetchInviteBonusList;

+ (void)acceptInviteBonusWithInviteId:(NSString *)inviteId rewardedId:(NSString *)rewardedId;

+ (void)fetchIPv4Info;

@end

NS_ASSUME_NONNULL_END
