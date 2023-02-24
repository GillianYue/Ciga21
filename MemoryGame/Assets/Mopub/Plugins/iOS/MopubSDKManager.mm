//
//  MopubSDKManager.m
//  Superera-ios-sdk-unity
//
//  Created by 王城 on 2019/6/5.
//  Copyright © 2019 Superera. All rights reserved.
//

#import "MopubSDKManager.h"

#include "AppDelegateListener.h"

#if __has_include(<SEAttributeSDK/SEAttributeSDK.h>)
#import <SEAttributeSDK/SEAttributeSDK.h>

#define HAS_SE_ATTRIBUTE 1
#else
#define HAS_SE_ATTRIBUTE 0
#endif

#import <SupereraSDKMobileCore/SupereraSDKMobileCore.h>
#import <SupereraSDKAnalytics/SupereraSDKAnalytics.h>
#import <SupereraSDKAuthCore/SupereraSDKAuthCore.h>
#import <SupereraSDKRealnameUI/SupereraSDKRealnameUI.h>
#import <SupereraSDKUILogin/SupereraSDKUILogin.h>
#import <UserNotifications/UserNotifications.h>


#if __has_include(<JodoPushClient/JodoPushClient.h>)

#include <JodoPushClient/JodoPushClient.h>
#define HAS_JODO_PUSH 1

static BOOL USE_JODO_PUSH = 1;

#else

#define HAS_JODO_PUSH 0

#endif

#if __has_include(<SupereraSDKAd/SupereraSDKAd.h>)
#import <SupereraSDKAd/SupereraSDKAd.h>
#define HAS_AD 1
#define IS_GAT 1
#else
#define HAS_AD 0
#define IS_GAT 0
#endif

#if __has_include(<MAdSDK/MAdSDK.h>)
#import <MAdSDK/MAdSDK.h>
#import <MAdSDK/SupereraAdInfo.h>
#define HAS_MAD 1
#else
#define HAS_MAD 0
#endif

#if __has_include(<SupereraSDKUILogin/SupereraSDKUILogin.h>)
#import <SupereraSDKUILogin/SupereraSDKUILogin.h>
#define HAS_UILogin 1
#else
#define HAS_UILogin 0
#endif


#ifdef __cplusplus
extern "C" {
#endif
    // life cycle management
    void UnityPause(int pause);
    void UnitySendMessage(const char* obj, const char* method, const char* msg);
#ifdef __cplusplus
}
#endif

#pragma - mark method name in Unity to callback
static NSString *unity_method_init_success = @"sdkInitSuccess";
static NSString *unity_method_init_failed = @"sdkInitFailed";

static NSString *unity_method_link_account_view_success = @"linkAccountViewSuccess";
static NSString *unity_method_switch_account_success = @"switchAccountSuccess";
static NSString *unity_method_delete_account_view_success = @"deleteAccountSuccess";

static NSString *unity_method_login_success = @"sdkLoginSuccess";
static NSString *unity_method_login_failed = @"sdkLoginFailed";

static NSString *unity_method_login_device_success = @"sdkLoginDeviceSuccess";
static NSString *unity_method_login_device_failed = @"sdkLoginDeviceFailed";

static NSString *unity_method_login_wechat_success = @"sdkLoginWeChatSuccess";
static NSString *unity_method_login_wechat_failed = @"sdkLoginWeChatFailed";

static NSString *unity_method_fetch_mobile_auth_code_success = @"fetchMobileAuthCodeSuccess";
static NSString *unity_method_fetch_mobile_auth_code_failed = @"fetchMobileAuthCodeFailed";

static NSString *unity_method_login_mobile_success = @"sdkLoginMobileSuccess";
static NSString *unity_method_login_mobile_failed = @"sdkLoginMobileFailed";

static NSString *unity_method_link_mobile_success = @"sdkLinkMobileSuccess";
static NSString *unity_method_link_mobile_failed = @"sdkLinkMobileFailed";

static NSString *unity_method_fetch_email_auth_code_success = @"fetchEmailAuthCodeSuccess";
static NSString *unity_method_fetch_email_auth_code_failed = @"fetchEmailAuthCodeFailed";

static NSString *unity_method_login_email_success = @"sdkLoginEmailSuccess";
static NSString *unity_method_login_email_failed = @"sdkLoginEmailFailed";

static NSString *unity_method_reset_email_password_success = @"resetPasswordWithEmailSuccess";
static NSString *unity_method_reset_email_password_failed = @"resetPasswordWithEmailFailed";

static NSString *unity_method_link_email_success = @"sdkLinkEmailSuccess";
static NSString *unity_method_link_email_failed = @"sdkLinkEmailSuccessFailed";

static NSString *unity_method_login_visitor_success = @"sdkLoginVisitorSuccess";
static NSString *unity_method_login_visitor_failed = @"sdkLoginVisitorFailed";

static NSString *unity_method_verify_session_token_success = @"verifySessionTokenSuccess";
static NSString *unity_method_verify_session_token_failed = @"verifySessionTokenFailed";

static NSString *unity_method_create_invite_code_success = @"createInviteCodeSuccess";
static NSString *unity_method_create_invite_code_failed = @"createInviteCodeFailed";

static NSString *unity_method_fetch_invitee_list_success = @"fetchInviteeListSuccess";
static NSString *unity_method_fetch_invitee_list_failed = @"fetchInviteeListFailed";

static NSString *unity_method_upload_invite_code_success = @"uploadInviteCodeSuccess";
static NSString *unity_method_upload_invite_code_failed = @"uploadInviteCodeFailed";

static NSString *unity_method_link_facebook_success = @"sdkLinkFacebookSuccess";
static NSString *unity_method_link_facebook_failed = @"sdkLinkFacebookSuccessFailed";

static NSString *unity_method_link_gamecenter_success = @"sdkLinkGameCenterSuccess";
static NSString *unity_method_link_gamecenter_failed = @"sdkLinkGameCenterSuccessFailed";

static NSString *unity_method_link_wechat_success = @"sdkLinkWeChatSuccess";
static NSString *unity_method_link_wechat_failed = @"sdkLinkWeChatSuccessFailed";

static NSString *unity_method_rewarded_video_did_appear = @"rewardedVideoDidAppear";
static NSString *unity_method_rewarded_video_did_disappear = @"rewardedVideoDidDisappear";
static NSString *unity_method_rewarded_video_did_receive_tap = @"rewardedVideoDidReceiveTap";
static NSString *unity_method_rewarded_video_did_finish_play = @"rewardedVideoDidFinishPlay";
static NSString *unity_method_rewarded_video_did_fail_to_play = @"rewardedVideoDidFailToPlay";

static NSString *unity_method_interstitial_did_appear = @"interstitialDidAppear";
static NSString *unity_method_interstitial_did_disappear = @"interstitialDidDisappear";
static NSString *unity_method_interstitial_did_receive_tap = @"interstitialDidReceiveTap";

static NSString *unity_method_native_shown = @"onNativeAdDidShown";
static NSString *unity_method_native_dismissed = @"onNativeAdDismissed";
static NSString *unity_method_native_clicked = @"onNativeAdClicked";

static NSString *unity_method_fetchPaymentItemDetails_success = @"fetchPaymentItemDetailsSuccess";
static NSString *unity_method_fetchPaymentItemDetails_failed = @"fetchPaymentItemDetailsFailed";
static NSString *unity_method_fetchUnconsumedPurchasedItems = @"fetchUnconsumedPurchasedItems";
static NSString *unity_method_fetchAllPurchasedItems = @"fetchAllPurchasedItems";
static NSString *unity_method_unconsumedItemUpdated = @"unconsumedItemUpdated";
static NSString *unity_method_paymentProcessing = @"paymentProcessing";
static NSString *unity_method_paymentFailed = @"paymentFailed";
static NSString *unity_method_aFInstallConversionData = @"aFInstallConversionData";

static NSString *unity_method_subscriptionItemUpdated = @"subscriptionItemUpdated";
static NSString *unity_method_fetchAllPruchasedSubscription = @"fetchAllPurchasedSubscriptionItems";
static NSString *unity_method_restoreSubscriptionSuccess = @"restoreSubscriptionSuccess";
static NSString *unity_method_restoreSubscriptionFailed = @"restoreSubscriptionFailed";

static NSString *unity_method_ingameParamsUpdated = @"inGameOnlineParams";
static NSString *unity_method_adIngameParamsUpdated = @"advertisingInGameOnlineParams";

static NSString *unity_method_unreadMessageUpdated = @"unreadMessageUpdated";

// old addiction prevention
//static NSString *unity_method_addiction_prevention_did_trriger = @"preventionDidTriggerDelegate";
static NSString *unity_method_verify_id_card_success = @"verifyIdCardSuccess";
static NSString *unity_method_verify_id_card_failed = @"verifyIdCardFailed";
static NSString *unity_method_fetch_id_card_info_success = @"fetchIdCardInfoSuccess";
static NSString *unity_method_fetch_id_card_info_failed = @"fetchIdCardInfoFailed";
static NSString *unity_method_fetch_paid_amount_success = @"fetchPaidAmountSuccess";
static NSString *unity_method_fetch_paid_amount_failed = @"fetchPaidAmountFailed";

// new addiction prevention
static NSString *unity_method_realname_authentication_success = @"realnameAuthenticationSuccess";
static NSString *unity_method_realname_authentication_failed = @"realnameAuthenticationFailed";
static NSString *unity_method_query_realname_info_success = @"queryRealnameInfoSuccess";
static NSString *unity_method_query_realname_info_failed = @"queryRealnameInfoFailed";
static NSString *unity_method_fetch_paid_amount_monthly_success = @"fetchPaidAmountMonthlySuccess";
static NSString *unity_method_fetch_paid_amount_monthly_failed = @"fetchPaidAmountMonthlyFailed";

static NSString *unity_method_realname_ui_start_success = @"sdkRealNameStartOnSuccessful";
static NSString *unity_method_realname_ui_start_failed = @"sdkRealNameStartOnFailed";
static NSString *unity_method_realname_ui_in_addiction = @"sdkRealNameStartOnFailedError";

static NSString *unity_method_show_realname_ui_success = @"showRealNameViewSuccess";
static NSString *unity_method_show_realname_ui_failed = @"showRealNameViewFailed";

static NSString *unity_method_logout_success = @"sdkLogoutSuccess";
static NSString *unity_method_logout_failed = @"sdkLogoutFailed";

static NSString *unity_method_fetch_ranking_success = @"fetchRankingSuccess";
static NSString *unity_method_fetch_ranking_failed = @"fetchRankingFailed";

static NSString *unity_method_save_cloud_cache_success = @"saveCloudCacheSuccess";
static NSString *unity_method_save_cloud_cache_failed = @"saveCloudCacheFailed";

static NSString *unity_method_get_cloud_cache_success = @"getCloudCacheSuccess";
static NSString *unity_method_get_cloud_cache_failed = @"getCloudCacheFailed";

static NSString *unity_method_get_redeem_success = @"getRedeemSuccess";
static NSString *unity_method_get_redeem_failed = @"getRedeemFailed";

static NSString *unity_method_package_updated_success = @"packageUpdateSuccess";

static NSString *unity_method_app_share_success = @"appsharedSuccess";
static NSString *unity_method_app_share_failed = @"appshareFailed";

static NSString *unity_method_fetch_invite_bonus_list_success = @"fetchInviteBonusSuccess";
static NSString *unity_method_fetch_invite_bonus_list_failed = @"fetchInviteBonusFailed";

static NSString *unity_method_fetch_ipv4_info_success = @"fetchIPv4InfoSuccess";
static NSString *unity_method_fetch_ipv4_info_failed = @"fetchIPv4InfoFailed";

static NSString *unity_method_accept_bonus_success = @"acceptInviteBonusSuccess";
static NSString *unity_method_accept_bonus_failed = @"acceptInviteBonusFailed";

static NSString *unity_args_key_error = @"MopubSDKError";
static NSString *unity_args_key_ad_entry = @"gameEntry";
static NSString *unity_args_key_accesstoken = @"accessToken";
static NSString *unity_args_key_paymentDetails_list = @"paymentDetailsList";
static NSString *unity_args_key_purchasedItems_list = @"purchasedItemsList";
static NSString *unity_args_key_paymentInfo = @"paymentInfo";

static NSString *unity_args_key_autoLoginTouristSuccess = @"autoLoginTouristSuccess";
static NSString *unity_args_key_autoLoginTouristFailed = @"autoLoginTouristFailed";
static NSString *unity_args_key_aTTInstallConversionData = @"aTTInstallConversionData";

static NSString *unity_args_key_userVerify = @"userVerify";

static NSString *unity_args_key_reLoginFlowSuccess = @"reLoginFlowSuccess";
static NSString *unity_args_key_reLoginFlowFailed = @"reLoginFlowFailed";

#if HAS_JODO_PUSH

#if HAS_AD
@interface MopubSDKManager() <SupereraRewaredVideoDelegate, SupereraInterstitialDelegate, SupereraSDKUnconsumedItemUpdatedDelegate, SupereraSDKPaymentDelegate,SupereraSDKSubscriptionItemUpdatedDelegate,SupereraSDKNativeAdDelegate, SupereraSDKCustomerDelegate, SupereraSDKRealnameUIDelegate, JodoPushClientDelegate,SupereraSDKUILoginManagerDelegate, SupereraSDKPackageUpdateManagerDelegate,UNUserNotificationCenterDelegate,SupereraSDKSEAttributeDelegate>
#elif HAS_MAD
@interface MopubSDKManager() <SupereraRewaredVideoDelegate, SupereraSDKUnconsumedItemUpdatedDelegate, SupereraSDKPaymentDelegate,SupereraSDKSubscriptionItemUpdatedDelegate, SupereraSDKCustomerDelegate, SupereraSDKRealnameUIDelegate, JodoPushClientDelegate,SupereraSDKUILoginManagerDelegate, SupereraSDKPackageUpdateManagerDelegate,UNUserNotificationCenterDelegate,SupereraSDKSEAttributeDelegate>
#endif

#else

#if HAS_AD
@interface MopubSDKManager() <SupereraRewaredVideoDelegate, SupereraInterstitialDelegate, SupereraSDKUnconsumedItemUpdatedDelegate, SupereraSDKPaymentDelegate,SupereraSDKSubscriptionItemUpdatedDelegate,SupereraSDKNativeAdDelegate, SupereraSDKCustomerDelegate, SupereraSDKRealnameUIDelegate, AppDelegateListener, SupereraSDKPackageUpdateManagerDelegate,SupereraSDKUILoginManagerDelegate,SupereraSDKCustomerUnreadMessageListener,SupereraSDKSEAttributeDelegate>
#elif HAS_MAD
@interface MopubSDKManager() <SupereraRewaredVideoDelegate, SupereraSDKUnconsumedItemUpdatedDelegate, SupereraSDKPaymentDelegate,SupereraSDKSubscriptionItemUpdatedDelegate, SupereraSDKCustomerDelegate, SupereraSDKRealnameUIDelegate, AppDelegateListener, SupereraSDKPackageUpdateManagerDelegate,SupereraSDKUILoginManagerDelegate,SupereraSDKCustomerUnreadMessageListener,SupereraSDKSEAttributeDelegate>
#endif

#endif
@end

@implementation MopubSDKManager

+ (void)load {
    NSLog(@"执行了 load");
    UnityRegisterAppDelegateListener([self sharedInstance]);
}

#pragma - mark unity help
+ (UIViewController*)unityViewController
{
    return [[[UIApplication sharedApplication] keyWindow] rootViewController];
}


+ (void)callUnityMethod:(NSString*)methodName withArgs:(NSDictionary *)args
{
    if(![NSJSONSerialization isValidJSONObject:args]){
         NSLog(@"callUnityMethod: args error!");
        return;
    }
    NSData* data = [NSJSONSerialization dataWithJSONObject:args options:0 error:nil];
    if(data == nil){
        NSLog(@"callUnityMethod: args error!");
        return;
    }
    NSLog(@"methodName: %@", methodName);
    [self callUnityMethod:methodName withStrArg:[[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding]];
}

+ (void)callUnityMethod:(NSString*)methodName withArrArgs:(NSArray *)args
{
    if(![NSJSONSerialization isValidJSONObject:args]){
        NSLog(@"callUnityMethod: args error!");
        return;
    }
    NSData* data = [NSJSONSerialization dataWithJSONObject:args options:0 error:nil];
    if(data == nil){
        NSLog(@"callUnityMethod: args error!");
        return;
    }
    [self callUnityMethod:methodName withStrArg:[[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding]];
}

+ (void)callUnityMethod:(NSString*)methodName withStrArg:(nullable NSString *)strArg
{
    NSString *str = strArg;
    if(!str){
        str = @"";
    }
    const char *cArg = str.UTF8String;

    UnitySendMessage("MopubManager", methodName.UTF8String, cArg);
    
    
}

#pragma - mark sharedInstance & init



+ (instancetype)sharedInstance
{
    static MopubSDKManager* sharedManager = nil;
    
    if (!sharedManager)
        sharedManager = [[MopubSDKManager alloc] init];
    
    return sharedManager;
}

- (instancetype)init {
    if(self = [super init]){
    
    }
    return self;
}

+ (void)SDKInit:(nullable NSString *)gameContentVersion {
    [[SupereraSDKCore sharedInstance] setSEAttributeDelegate:[MopubSDKManager sharedInstance]];
    NSString *gv = gameContentVersion;
    if(gv == nil){
        gv = @"0";
    }
    //init sdkCore
    SupereraSDKConfiguration *config = [[SupereraSDKConfiguration alloc] init];
    config.gameContentVersion = gv;
    if (!IS_GAT) {
        config.adInitAfterAttribute = YES;
        config.attributeAfterRealname = YES;
    }
//    config.isTestEnvironment = YES;
//    [SupereraSDKLoginManager setTestEnvironment:YES];
    [[SupereraSDKCore sharedInstance] setPackageUpdateDelegate:[MopubSDKManager sharedInstance]];
    
#if HAS_AD
    [SupereraSDKAd sharedInstance].thirdMediation = SupereraSDKAdThirdMediationMax;
#endif
    
    [[SupereraSDKCore sharedInstance] SDKInitWithConfig:config success:^{
        [MopubSDKManager callUnityMethod:unity_method_init_success withStrArg:nil];
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_init_failed withStrArg:[error toJSON]];
        //[MopubSDKManager callUnityMethod:unity_method_init_failed withArgs:@{unity_args_key_error:[error toJSON]}];
    }];
    
    //init ad
//    [SupereraSDKAd setTestEnvironment:YES];
    [[SupereraSDKAd sharedInstance] setRewardedVideoDelegate:[MopubSDKManager sharedInstance]];
    
#if HAS_AD
    [[SupereraSDKAd sharedInstance] setInterstitialDelegate:[MopubSDKManager sharedInstance]];
    [[SupereraSDKAd sharedInstance] setNativeAdDelegate:[MopubSDKManager sharedInstance]];
#endif
    
    [SupereraSDKRealnameUIManager shared].delegate = [MopubSDKManager sharedInstance];
    [SupereraSDKUILoginManager shared].delegate = [MopubSDKManager sharedInstance];
    
//#if HAS_JODO_PUSH
//    if (USE_JODO_PUSH) {
//        JodoPushClientConfig *pushConfig = [[JodoPushClientConfig alloc] init];
//        pushConfig.debug = YES;
//        pushConfig.passThroughEnable = NO;
//        pushConfig.logUploadHost = @"https://logr.push.casdk.cn";
//        pushConfig.logUploadPath = @"clientlog/light_client_log";
//
//        [[JodoPushClient shared] registerJodoPushWithCgi:[MopubSDKManager getCgi] appid:@"80" appSecret:@"87f5280946564197bd37e0378c4ff035" config:pushConfig delegate:[MopubSDKManager sharedInstance]];
//    }
//#endif
}

+ (NSDictionary *)getCpParams {
    return [[SupereraSDKCore sharedInstance] getCpParams];
}

+ (NSDictionary *)getSDKInfo {
    return [[SupereraSDKCore sharedInstance] getSDKInfo];
}

#pragma mark - 账号中心
#if HAS_UILogin

- (void)managerLinkSuccess:(SupereraSDKUILoginManager *)manager {
    [MopubSDKManager callUnityMethod:unity_method_link_account_view_success withStrArg:nil];
}

- (void)managerDeleteAccountSuccess:(SupereraSDKUILoginManager *)manager {
    [MopubSDKManager callUnityMethod:unity_method_delete_account_view_success withStrArg:nil];
}

- (void)managerLoginSuccess:(SupereraSDKUILoginManager *)manager {
    SupereraSDKAccessToken *token = [SupereraSDKAccessToken currentAccessToken];
    if(token){
        [MopubSDKManager callUnityMethod:unity_method_switch_account_success withStrArg:[token toJSON]];
    }
}

- (void)managerAutoLoginSuccess:(SupereraSDKUILoginManager *)manager {
    SupereraSDKAccessToken *token = [SupereraSDKAccessToken currentAccessToken];
    [MopubSDKManager callUnityMethod:unity_args_key_autoLoginTouristSuccess withStrArg:[token toJSON]];
}

- (void)managerAutoLoginFail:(SupereraSDKUILoginManager *)manager error:(SupereraSDKError *)error {
    [MopubSDKManager callUnityMethod:unity_args_key_autoLoginTouristFailed withStrArg:[error toJSON]];
}

- (void)managerUserVerifySuccess:(NSString *)uid {
    [MopubSDKManager callUnityMethod:unity_args_key_userVerify withStrArg:uid];
}

+ (void)showArchive:(NSString *)jsonParams {
    NSLog(@"选择存档");
    NSDictionary *dic = [NSDictionary dictionaryWithJSONString:jsonParams];
    [[SupereraSDKUILoginManager shared] showSelectDigestView:dic];
}

+ (void)showAccountCenter {
    [[SupereraSDKUILoginManager shared] showHomeViewFromViewController:[UIApplication sharedApplication].keyWindow.rootViewController];
}

+ (void)reLoginFlow {
    [[SupereraSDKLoginManager sharedInstance] startReloginSuccess:^{
        [MopubSDKManager callUnityMethod:unity_args_key_reLoginFlowSuccess withStrArg:nil];
    } failure:^(SupereraSDKError * _Nonnull) {
        [MopubSDKManager callUnityMethod:unity_args_key_reLoginFlowFailed withStrArg:nil];
    }];
}

+ (void)showLinkedAfterPurchaseView {
#if IS_GAT
    [[SupereraSDKUILoginManager shared] showQuickLinkViewFromViewController:nil];
#endif
}
+ (void)autoLoginTouristWithUI {
    [[SupereraSDKUILoginManager shared] restartLoginIfNeed];
}

//+ (BOOL)isAccountOnlyTourist {
//    SupereraSDKAccessToken *token = [SupereraSDKAccessToken currentAccessToken];
//    for (SupereraSDKThirdPartyAccount *account in token.linkedAccounts.accounts) {
//        if (account.accountType != SupereraSDKAccountTypeVisitor) {
//            return FALSE;
//        }
//    }
//
//    return true;
//}

+ (void)setLanguage:(NSString *)language {
#if IS_GAT
    [[SupereraSDKUILoginManager shared] setLanguage:language];
#endif
}

+ (void)showDeleteAccountView {
#if !IS_GAT
    [[SupereraSDKUILoginManager shared] showDeleteAccountViewFromController:nil];
#endif
}

#endif
//#endif

+ (BOOL)isAccountOnlyTourist {
    SupereraSDKAccessToken *token = [SupereraSDKAccessToken currentAccessToken];
    for (SupereraSDKThirdPartyAccount *account in token.linkedAccounts.accounts) {
        if (account.accountType != SupereraSDKAccountTypeVisitor) {
            return FALSE;
        }
    }
    return true;
}

#pragma - mark sdk bridge for C method

#pragma - mark auth

+ (void)openNoticeDialog {
    [[SupereraSDKCore sharedInstance] openNoticeDialog];
}


+ (void)login {
    
//    SupereraSDKLoginWithDeviceRequest *req = [[SupereraSDKLoginWithDeviceRequest alloc] init];
//    req.createAccount = YES;
//    [[SupereraSDKLoginManager sharedInstance] loginWithDevice:req success:^(SupereraSDKLoginResult * _Nonnull loginResult) {
//        NSString *tokenJSON = [loginResult.accessToken toJSON];
//        if(tokenJSON != nil){
//            [MopubSDKManager callUnityMethod:unity_method_login_success withStrArg:tokenJSON];
//        }else{
//            [MopubSDKManager callUnityMethod:unity_method_login_success withStrArg:nil];
//        }
//    } failure:^(SupereraSDKError * _Nonnull error) {
//        [MopubSDKManager callUnityMethod:unity_method_login_failed withStrArg:[error toJSON]];
//    }];
    
    [[SupereraSDKLoginManager sharedInstance] autoLoginAndLinkWithType:SupereraSDKAccountTypeUnknown success:^(SupereraSDKLoginResult * _Nonnull loginResult) {

        NSString *tokenJSON = [loginResult.accessToken toJSON];
        if(tokenJSON != nil){
            [MopubSDKManager callUnityMethod:unity_method_login_success withStrArg:tokenJSON];
        }else{
            [MopubSDKManager callUnityMethod:unity_method_login_success withStrArg:nil];
        }


    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_login_failed withStrArg:[error toJSON]];
    }];
}

+ (void)loginWithDevice {
    [self loginDeviceWithActiveCode:nil];
}

+ (void)loginDeviceWithActiveCode:(NSString *)activeCode {
    ///创建登录请求对象
    ///Create login request object
    SupereraSDKLoginWithDeviceRequest *loginRequest = [[SupereraSDKLoginWithDeviceRequest alloc] init];
    ///如果当前设备没有对应的旧帐号，是否创建新的帐号，建议 true 创建
    ///When current device don't have existed account, option whether to  create a new account. Recommend setting it to false.
    loginRequest.createAccount = YES;
    loginRequest.activeCode = activeCode;
    ///发起登录请求
    ///Issue login  request
    [[SupereraSDKLoginManager sharedInstance] loginWithDevice:loginRequest success:^(SupereraSDKLoginResult * _Nonnull loginResult) {
        
        NSString *tokenJSON = [loginResult.accessToken toJSON];
        if(tokenJSON != nil){
            [MopubSDKManager callUnityMethod:unity_method_login_device_success withStrArg:tokenJSON];
        }else{
            [MopubSDKManager callUnityMethod:unity_method_login_device_success withStrArg:nil];
        }
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        
        [MopubSDKManager callUnityMethod:unity_method_login_device_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)loginWithWeChat {
    [self loginWeChatWithActiveCode:nil];
}

+ (void)loginWeChatWithActiveCode:(NSString *)activeCode {
    SupereraSDKLoginWithWeChatRequest *loginRequest = [[SupereraSDKLoginWithWeChatRequest alloc] init];
    loginRequest.createAccount = YES;
    loginRequest.activeCode = activeCode;
    [[SupereraSDKLoginManager sharedInstance] loginWithWeChat:loginRequest success:^(SupereraSDKLoginResult * _Nonnull loginResult) {
        
        NSString *tokenJSON = [loginResult.accessToken toJSON];
        if(tokenJSON != nil){
            [MopubSDKManager callUnityMethod:unity_method_login_wechat_success withStrArg:tokenJSON];
        }else{
            [MopubSDKManager callUnityMethod:unity_method_login_wechat_success withStrArg:nil];
        }
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_login_wechat_failed withStrArg:[error toJSON]];
    }];
}

+ (void)fetchMobileAuthCodeWithPhoneNumber:(NSString *)phoneNumber {
    
    [[SupereraSDKLoginManager sharedInstance] fetchMobileAuthCodeWithPhoneNumber:phoneNumber success:^{
        
        [MopubSDKManager callUnityMethod:unity_method_fetch_mobile_auth_code_success withStrArg:@""];
    } failure:^(SupereraSDKError *error) {
        [MopubSDKManager callUnityMethod:unity_method_fetch_mobile_auth_code_failed withStrArg:[error toJSON]];
    }];
}

+ (void)loginWithMobile:(NSString *)phoneNumber authCode:(NSString *)authCode activeCode:(NSString *)activeCode isOneKeyLogin:(BOOL)isOneKeyLogin {
    SupereraSDKLoginWithMobileRequest *loginRequest = [[SupereraSDKLoginWithMobileRequest alloc] init];
    
    loginRequest.isOneClickLogin = isOneKeyLogin;

    loginRequest.phoneNumber = phoneNumber;
    loginRequest.code = authCode;
    loginRequest.createAccount = YES;
    loginRequest.activeCode = activeCode;
    
    [[SupereraSDKLoginManager sharedInstance] loginWithMobile:loginRequest success:^(SupereraSDKLoginResult * _Nonnull loginResult) {
        
        NSString *tokenJSON = [loginResult.accessToken toJSON];
        if(tokenJSON != nil){
            [MopubSDKManager callUnityMethod:unity_method_login_mobile_success withStrArg:tokenJSON];
        }else{
            [MopubSDKManager callUnityMethod:unity_method_login_mobile_success withStrArg:nil];
        }
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_login_mobile_failed withStrArg:[error toJSON]];
    }];
}

+ (void)linkWithMobile:(NSString *)phoneNumber authCode:(NSString *)authCode {
    [[SupereraSDKLoginManager sharedInstance] linkMobile:phoneNumber code:authCode success:^(SupereraSDKLinkResult * _Nonnull linkResult) {
        
        [MopubSDKManager callUnityMethod:unity_method_link_mobile_success withStrArg:nil];
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        
        [MopubSDKManager callUnityMethod:unity_method_link_mobile_failed withStrArg:[error toJSON]];
    }];
}

+ (void)fetchEmailAuthCodeWithEmail:(NSString *)email {
    
    [[SupereraSDKLoginManager sharedInstance] fetchEmailAuthCodeWithEmail:email success:^{
        [MopubSDKManager callUnityMethod:unity_method_fetch_email_auth_code_success withStrArg:nil];
        
    } failure:^(SupereraSDKError *error) {
        [MopubSDKManager callUnityMethod:unity_method_fetch_email_auth_code_failed withStrArg:[error toJSON]];
    }];
}

+ (void)loginWithEmail:(NSString *)email password:(NSString *)password authCode:(NSString *__nullable)authCode activeCode:(NSString *__nullable)activeCode {
    
    SupereraSDKLoginWithEmailRequest *loginRequest = [[SupereraSDKLoginWithEmailRequest alloc] init];
    loginRequest.email = email;
    loginRequest.password = password;
    
    if (authCode && authCode.length > 0) {
        loginRequest.code = authCode;
        loginRequest.createAccount = YES;
    }
    else {
        loginRequest.code = nil;
    }
    
    if (activeCode && activeCode.length > 0) {
        loginRequest.activeCode = activeCode;
    }
    else {
        loginRequest.activeCode = nil;
    }
    
    [[SupereraSDKLoginManager sharedInstance] loginWithEmail:loginRequest success:^(SupereraSDKLoginResult * _Nonnull loginResult) {
        
        NSString *tokenJSON = [loginResult.accessToken toJSON];
        if(tokenJSON != nil){
            [MopubSDKManager callUnityMethod:unity_method_login_email_success withStrArg:tokenJSON];
        }else{
            [MopubSDKManager callUnityMethod:unity_method_login_email_success withStrArg:nil];
        }
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        
        [MopubSDKManager callUnityMethod:unity_method_login_email_failed withStrArg:[error toJSON]];
    }];
}

+ (void)linkWithEmail:(NSString *)email password:(NSString *)password authCode:(NSString *)authCode {
    [[SupereraSDKLoginManager sharedInstance] linkEmail:email password:password code:authCode success:^(SupereraSDKLinkResult * _Nonnull linkResult) {
        
        [MopubSDKManager callUnityMethod:unity_method_link_email_success withStrArg:nil];
        
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        
        [MopubSDKManager callUnityMethod:unity_method_link_email_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)resetPasswordWithEmail:(NSString *)email newPassword:(NSString *)newPassword authCode:(NSString *)authCode {
    [[SupereraSDKLoginManager sharedInstance] resetPasswordWithEmail:email newPassword:newPassword code:authCode success:^{
        
        [MopubSDKManager callUnityMethod:unity_method_reset_email_password_success withStrArg:nil];
        
    } failure:^(SupereraSDKError *error) {
        
        [MopubSDKManager callUnityMethod:unity_method_reset_email_password_failed withStrArg:[error toJSON]];
    }];
}

+ (void)loginVisitorWithActiveCode: (NSString *)activeCode {
    ///创建登录请求对象
    ///Create login request object
    SupereraSDKLoginWithVisitorRequest *loginRequest = [[SupereraSDKLoginWithVisitorRequest alloc] init];
    ///如果当前设备没有对应的旧帐号，是否创建新的帐号，建议 true 创建
    ///When current device don't have existed account, option whether to  create a new account. Recommend setting it to false.
    loginRequest.createAccount = YES;
    loginRequest.activeCode = activeCode;
    ///发起登录请求
    ///Issue login  request
    [[SupereraSDKLoginManager sharedInstance] loginWithVisitor:loginRequest success:^(SupereraSDKLoginResult * _Nonnull loginResult) {
        
        NSString *tokenJSON = [loginResult.accessToken toJSON];
        if(tokenJSON != nil){
            [MopubSDKManager callUnityMethod:unity_method_login_visitor_success withStrArg:tokenJSON];
        }else{
            [MopubSDKManager callUnityMethod:unity_method_login_visitor_success withStrArg:nil];
        }
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        
        [MopubSDKManager callUnityMethod:unity_method_login_visitor_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)verifySessionToken: (NSString *)token {
    dispatch_async(dispatch_get_main_queue(), ^{
        [[SupereraSDKLoginManager sharedInstance] verifySessionToken:token success: ^{
            [MopubSDKManager callUnityMethod:unity_method_verify_session_token_success withStrArg: @""];
            
        } failure:^(SupereraSDKError *error) {
            [MopubSDKManager callUnityMethod:unity_method_verify_session_token_failed withStrArg:[error toJSON]];
        }];
    });
    
}

+ (void)createInviteCode {
//    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"%p", [SupereraSDKInviteCodeManager sharedInstance]);
        [[SupereraSDKInviteCodeManager sharedInstance] createInviteCode:^(NSString *inviteCode) {
            [MopubSDKManager callUnityMethod:unity_method_create_invite_code_success withStrArg:inviteCode];
            
        } failure:^(SupereraSDKError *error) {
                
            [MopubSDKManager callUnityMethod:unity_method_create_invite_code_failed withStrArg:[error toJSON]];
        }];
//    });
    
}

+ (void)fetchInviteeList {
    [[SupereraSDKInviteCodeManager sharedInstance] fetchInviteeList:^(NSArray<NSString *> *inviteeList) {
        [MopubSDKManager callUnityMethod:unity_method_fetch_invitee_list_success withArrArgs:inviteeList];
    } failure:^(SupereraSDKError *error) {
        [MopubSDKManager callUnityMethod:unity_method_fetch_invitee_list_failed withStrArg:[error toJSON]];
    }];
}

+ (void)uploadInviteCode:(NSString *)code {
    [[SupereraSDKInviteCodeManager sharedInstance] uploadInviteCode:code success:^(NSString *invitorId){
        [MopubSDKManager callUnityMethod:unity_method_upload_invite_code_success withStrArg:invitorId];
        
    } failure:^(SupereraSDKError *error) {
        [MopubSDKManager callUnityMethod:unity_method_upload_invite_code_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)linkWithGameCenter {
    [[SupereraSDKLoginManager sharedInstance] linkGameCenterWithSuccess:^(SupereraSDKLinkResult * _Nonnull linkResult) {
        [self callUnityMethod:unity_method_link_gamecenter_success withArgs:nil];
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_link_gamecenter_failed withStrArg:[error toJSON]];
    }];
}

+ (void)linkWithFacebook {
    
    [[SupereraSDKLoginManager sharedInstance] linkFacebookWithSuccess:^(SupereraSDKLinkResult * _Nonnull linkResult) {
        [self callUnityMethod:unity_method_link_facebook_success withArgs:@{}];
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_link_facebook_failed withStrArg:[error toJSON]];
    }];
}

+ (void)linkWithWeChat {
    [[SupereraSDKLoginManager sharedInstance] linkWeChatWithSuccess:^(SupereraSDKLinkResult * _Nonnull linkResult) {
        [self callUnityMethod:unity_method_link_wechat_success withArgs:@{}];
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_link_wechat_failed withStrArg:[error toJSON]];
    }];
}

/**
 返回当前accessToken的json
 
 @return json形式的accessToken信息
 */
+ (nullable NSString *)currentAccessTokenJSON {
    SupereraSDKAccessToken *token = [SupereraSDKAccessToken currentAccessToken];
    if(token){
        return [token toJSON];
    }
    return nil;
}

/**
 返回当前设备的 puid
 */
+ (NSString *)getPuid {
    return [[SupereraSDKCore sharedInstance] puid];
}

/**
 返回当前国家地区的代码
 */
+ (NSString *)getRegion {
    return [[SupereraSDKCore sharedInstance] getRegion];
}

/**
 获取包版本号
 */
+ (NSString *)getPackageVersionCode {
    return [[SupereraSDKCore sharedInstance] getPackageVersionCode];
}

/**
 获取cgi
 */
+ (NSString *)getCgi {
    return [[SupereraSDKCore sharedInstance] getCGI];
}

/**
 返回当前应用的版本号
 */
+ (NSString *)getGameVersion {
    return [[SupereraSDKCore sharedInstance] getAppVersion];
}

+ (void)launchCustomer {
    [[SupereraSDKCustomerManager sharedInstance] launchWeb];
}

+ (void)launchCustomerWithTransitPage:(NSString *)pageType {
    [[SupereraSDKCustomerManager sharedInstance] launchCustomerWithTransitPage:pageType];
}

+ (void)launchCustomerWithTransitPage:(NSString *)pageType email:(NSString *)email {
    [[SupereraSDKCustomerManager sharedInstance] launchCustomerWithTransitPage:pageType email:email];
}

+ (void)setUnreadMessageUpdatedListener {
//    [SupereraSDKCustomerManager sharedInstance].delegate = [self sharedInstance];
#if IS_GAT
    [[SupereraSDKCustomerManager sharedInstance] addUnreadMessageListener:[self sharedInstance]];
#endif
}


- (void)customerManager:(SupereraSDKCustomerManager *)manager getUnreadMasseges:(NSDictionary *)messages {
    [MopubSDKManager callUnityMethod:unity_method_unreadMessageUpdated withArgs:messages];
}

- (void)customerManager:(nonnull SupereraSDKCustomerManager *)manager dealSuccessWithEventName:(nonnull NSString *)message success:(BOOL)isSuccess {
    
}


+ (void)fetchRankingWithUid:(NSString *)uid page:(int)page size:(int)size {
    
    [SupereraSDKSocialManager getRankingListWithUid:uid page:page size:size completionHandler:^(SupereraSDKRanking * _Nullable ranking, SupereraSDKError * _Nullable error) {
       
        if (error) {
            [MopubSDKManager callUnityMethod:unity_method_fetch_ranking_failed withStrArg:[error toJSON]];
            return;
        }
        
        if (ranking) {
            [MopubSDKManager callUnityMethod:unity_method_fetch_ranking_success withStrArg:[ranking toJSON]];
            return;
        }
        
    }];
}

+ (NSString *)getTimeStamp {
    return [NSString stringWithFormat:@"%ld", [[SupereraSDKCore sharedInstance] getSDKInitServerTime_ms]];
}

+ (void)fetchIPv4Info {
    [[SupereraSDKIPv4Manager sharedInstance] fetchIPv4InfoWithCompletion:^(SupereraSDKIPv4Info * _Nonnull info, SupereraSDKError * _Nonnull error) {
       
        if (error) {
            [MopubSDKManager callUnityMethod:unity_method_fetch_ipv4_info_failed withStrArg:[error toJSON]];
            return;
        }
        
        NSData *data = [NSJSONSerialization dataWithJSONObject:[info toDictionary] options:0 error:nil];
        NSString *jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        
        [MopubSDKManager callUnityMethod:unity_method_fetch_ipv4_info_success withStrArg:jsonString];
        
    }];
}

#pragma - mark analytics

+ (void)addEventGlobalParams:(NSString *)jsonParams {
    if (jsonParams == nil) {
        return;
    }
    
    NSLog(@"add global log params: %@", jsonParams);
    
    NSData *jsonData = [jsonParams dataUsingEncoding:NSUTF8StringEncoding];
    NSError *error;
    
    if (jsonData == nil) {
        return;
    }
    NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData
                                                            options:NSJSONReadingMutableContainers
                                                              error:&error];
    
    [[SupereraSDKCore sharedInstance] addGlobalLogParams:dic];
    
}

+ (void)logCustomEventWithEventName:(NSString *)eventName jsonParams:(NSString *)jsonParams {
    [SupereraSDKEvents logCustomEvent:eventName jsonParams:jsonParams];
}

+ (void)logCustomAFEventWithEventName:(NSString *)eventName jsonParams:(NSString *)jsonParams {
    NSDictionary *params = [NSDictionary dictionaryWithJSONString:jsonParams];
    [SupereraAFLogger logCustomAFEvent:eventName params:params];
}


#pragma - mark ad
+ (BOOL)hasRewardedVideoWithGameEntry:(NSString *)gameEntry {
    return [[SupereraSDKAd sharedInstance] hasRewardedVideoForEntry:gameEntry];
}

+ (void)showRewardVideoAdWithGameEntry:(NSString *)gameEntry {
    [[SupereraSDKAd sharedInstance] showVideoAdWithEntry:gameEntry fromController:[MopubSDKManager unityViewController]];
}

+ (BOOL)hasInterstitialWithGameEntry:(NSString *)gameEntry {
#if HAS_AD
    return [[SupereraSDKAd sharedInstance] hasInterstitialForEntry:gameEntry];
#else
    return false;
#endif
}

+ (void)showInterstitialAdWithGameEntry:(NSString *)gameEntry {
#if HAS_AD
    [[SupereraSDKAd sharedInstance] showInterstitialAdWithEntry:gameEntry fromController:[MopubSDKManager unityViewController]];
#endif
}

///banner
+ (void)showBanner:(int)position {
#if HAS_AD
     BannerPosition pos = (BannerPosition)position;
    [[SupereraSDKAd sharedInstance] showBannerAt:pos];
#endif
}
+ (void)dismissBanner {
#if HAS_AD
    [[SupereraSDKAd sharedInstance] dismissBanner];
#endif
}

///native ad
+ (BOOL)hasNativeAd:(NSString *)gameEntry {
#if HAS_AD
    return [[SupereraSDKAd sharedInstance] hasNaviteAdForEntry:gameEntry];
#else
    return false;
#endif
}

+ (void)showNativeAdFixed:(NSString *)gameEntry position:(int)position spacing:(float) spacing {
#if HAS_AD
    SDKNativeAdPosition pos = (SDKNativeAdPosition)position;
    [[SupereraSDKAd sharedInstance] showNaviteAdFixedForEntry:gameEntry position:pos spacing:spacing];
#endif
}

+ (void)closeNativeAd:(NSString *)gameEntry {
#if HAS_AD
    [[SupereraSDKAd sharedInstance] closeNaviteAdForEntry:gameEntry];
#endif
}

#pragma - mark ad callbak: SupereraRewaredVideoDelegate
- (void)rewardedVideoDidAppear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo {
    NSLog(@"rewardedVideoDidAppear: ");
    NSMutableDictionary *mDic = [NSMutableDictionary dictionary];
    mDic[@"gameEntry"] = entry;
    mDic[@"adInfo"] = [adInfo toDictionary];
    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_appear withArgs:[mDic copy]];
//    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_appear withStrArg:entry];
}

- (void)rewardedVideoDidDisappear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo {
    NSLog(@"rewardedVideoDidDisappear: ");
    NSMutableDictionary *mDic = [NSMutableDictionary dictionary];
    mDic[@"gameEntry"] = entry;
    mDic[@"adInfo"] = [adInfo toDictionary];
    NSLog(@"%@", mDic);
    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_disappear withArgs:[mDic copy]];
//    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_disappear withStrArg:entry];
}

- (void)rewardedVideoDidReceiveTap:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo {
    NSMutableDictionary *mDic = [NSMutableDictionary dictionary];
    mDic[@"gameEntry"] = entry;
    mDic[@"adInfo"] = [adInfo toDictionary];
    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_receive_tap withArgs:[mDic copy]];
//    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_receive_tap withStrArg:entry];
}

- (void)rewardedVideoDidFinishPlay:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo {
    NSLog(@"rewardedVideoDidFinishPlay: ");
    NSMutableDictionary *mDic = [NSMutableDictionary dictionary];
    mDic[@"gameEntry"] = entry;
    mDic[@"adInfo"] = [adInfo toDictionary];
    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_finish_play withArgs:[mDic copy]];
//    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_finish_play withStrArg:entry];
}

- (void)rewardedVideoDidFailToPlay:(NSString *)entry {
    [MopubSDKManager callUnityMethod:unity_method_rewarded_video_did_fail_to_play withStrArg:entry];
}

#pragma - mark ad callback: SupereraInterstitialDelegate
- (void)interstitialDidAppear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo {
    NSMutableDictionary *mDic = [NSMutableDictionary dictionary];
    mDic[@"gameEntry"] = entry;
//    mDic[@"adInfo"] = [adInfo toDictionary];
    [MopubSDKManager callUnityMethod:unity_method_interstitial_did_appear withArgs:[mDic copy]];
//    [MopubSDKManager callUnityMethod:unity_method_interstitial_did_appear withStrArg:entry];
}

- (void)interstitialDidDisappear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo {
    NSMutableDictionary *mDic = [NSMutableDictionary dictionary];
    mDic[@"gameEntry"] = entry;
//    mDic[@"adInfo"] = [adInfo toDictionary];
    [MopubSDKManager callUnityMethod:unity_method_interstitial_did_disappear withArgs:[mDic copy]];
//    [MopubSDKManager callUnityMethod:unity_method_interstitial_did_disappear withStrArg:entry];
}

- (void)interstitialDidReceiveTap:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo {
    NSMutableDictionary *mDic = [NSMutableDictionary dictionary];
    mDic[@"gameEntry"] = entry;
//    mDic[@"adInfo"] = [adInfo toDictionary];
    [MopubSDKManager callUnityMethod:unity_method_interstitial_did_receive_tap withArgs:[mDic copy]];
//    [MopubSDKManager callUnityMethod:unity_method_interstitial_did_receive_tap withStrArg:entry];
}

#pragma - mark SupereraSDKNativeAdDelegate

- (void)onNativeAdDidShown:(NSString *)entry {
    [MopubSDKManager callUnityMethod:unity_method_native_shown withStrArg:entry];
}
- (void)onNativeAdDismissed:(NSString *)entry {
    [MopubSDKManager callUnityMethod:unity_method_native_dismissed withStrArg:entry];
}
- (void)onNativeAdClicked:(NSString *)entry {
    [MopubSDKManager callUnityMethod:unity_method_native_clicked withStrArg:entry];
}

#pragma mark - SupereraSDKAddictionPreventionDelegate

//- (void)preventionDidTrigger {
//    [MopubSDKManager callUnityMethod:unity_method_addiction_prevention_did_trriger withStrArg:nil];
//}

#pragma - mark payment
+ (void)fetchPaymentItemDetails {
    [[SupereraSDKPaymentManager sharedInstance] fetchPaymentItemDetails:nil success:^(NSArray<SupereraSDKPaymentItemDetails *> * _Nonnull paymentItems) {
        NSMutableArray *items = [NSMutableArray array];
        for (SupereraSDKPaymentItemDetails *details in paymentItems) {
            [items addObject:[details toJSON]];
        }
        [MopubSDKManager callUnityMethod:unity_method_fetchPaymentItemDetails_success withArrArgs:items];
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_fetchPaymentItemDetails_failed withStrArg:[error toJSON]];
    }];
}

+ (void)fetchUnconsumedPurchasedItems {
    [[SupereraSDKPaymentManager sharedInstance] fetchUnconsumedPurchasedItemsWithCharacterID:nil result:^(NSArray<SupereraSDKPurchasedItem *> * _Nullable unconsumedPurchasedItems) {
        NSMutableArray *items = [NSMutableArray array];
        for (SupereraSDKPurchasedItem *item in unconsumedPurchasedItems) {
            [items addObject:[item toJSON]];
        }
        [MopubSDKManager callUnityMethod:unity_method_fetchUnconsumedPurchasedItems withArrArgs:items];
    }];
}

+ (void)fetchAllPurchasedItems {
    [[SupereraSDKPaymentManager sharedInstance] fetchAllPurchasedItemsWithCharacterID:nil result:^(NSArray<SupereraSDKPurchasedItem *> * _Nullable allPurchasedItems) {
        NSMutableArray *items = [NSMutableArray array];
        for (SupereraSDKPurchasedItem *item in allPurchasedItems) {
            [items addObject:[item toJSON]];
        }
        [MopubSDKManager callUnityMethod:unity_method_fetchAllPurchasedItems withArrArgs:items];
    }];
}

+ (void)consumePurchaseWithSDKOrderID:(NSString *)sdkOrderID {
    [[SupereraSDKPaymentManager sharedInstance] consumePurchasedItemWithSDKOrderID:sdkOrderID];
}
     
+ (void)setUnconsumedItemUpdatedListener {
    [[SupereraSDKPaymentManager sharedInstance] setUnconsumedItemUpdatedListener:[MopubSDKManager sharedInstance] characterID:nil];
}

+ (void)startPaymentWithItemID:(NSString *)itemID cpOrderID:(nullable NSString *)cpOrderID characterName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID level:(int)level {
    SupereraSDKPaymentInfo *payInfo = [[SupereraSDKPaymentInfo alloc] initWithItemID:itemID cpOrderID:cpOrderID characterName:characterName characterID:characterID serverName:serverName serverID:serverID level:level];
    [[SupereraSDKPaymentManager sharedInstance] startPaymentWithPaymentInfo:payInfo callback:[MopubSDKManager sharedInstance]];
}

#pragma - mark subscription
+ (void)fetchAllPurchasedSubscriptionItem {
    [[SupereraSDKPaymentManager sharedInstance] fetchAllPurchasedSubscriptionItemsWithCharacterID:nil result:^(NSArray<SupereraSDKSubscriptonItem *> * _Nullable allSubscriptonItems) {
        
        NSMutableArray *items = [NSMutableArray array];
        for (SupereraSDKSubscriptonItem *item in allSubscriptonItems) {
            [items addObject:[item toJSON]];
        }
        [MopubSDKManager callUnityMethod:unity_method_fetchAllPruchasedSubscription withArrArgs:items];
    }];
}

+ (void)restoreSubscription {
    [[SupereraSDKPaymentManager sharedInstance] restoreSubscriptionWithSuccess:^{
        [MopubSDKManager callUnityMethod:unity_method_restoreSubscriptionSuccess withArgs:nil];
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_restoreSubscriptionFailed withStrArg:[error toJSON]];
    }];
}

+ (void)setSubscriptionItemUpdatedListener {
    [[SupereraSDKPaymentManager sharedInstance] setSubscriptionItemUpdatedListener:[MopubSDKManager sharedInstance]  characterID:nil];
}

#pragma - mark SupereraSDKUnconsumedItemUpdatedDelegate
- (void)unconsumedItemUpdated:(NSArray<SupereraSDKPurchasedItem *> *) unconsumedItems {
    NSMutableArray *items = [NSMutableArray array];
    for (SupereraSDKPurchasedItem *item in unconsumedItems) {
        [items addObject:[item toJSON]];
    }
    [MopubSDKManager callUnityMethod:unity_method_unconsumedItemUpdated withArrArgs:items];
}

#pragma - mark SupereraSDKSubscriptionItemUpdatedDelegate
- (void)subscriptionItemUpdated:(NSArray<SupereraSDKSubscriptonItem *> *) subscriptionItems {
    NSMutableArray *items = [NSMutableArray array];
    for (SupereraSDKSubscriptonItem *item in subscriptionItems) {
        [items addObject:[item toJSON]];
    }
    [MopubSDKManager callUnityMethod:unity_method_subscriptionItemUpdated withArrArgs:items];
}

#pragma - mark SupereraSDKPaymentDelegate

- (void)paymentProcessingWithPaymentInfo:(SupereraSDKPaymentInfo *)paymentInfo {
    [MopubSDKManager callUnityMethod:unity_method_paymentProcessing withStrArg:[paymentInfo toJSON]];
    
}

- (void)paymentFailedWithPaymentInfo:(SupereraSDKPaymentInfo *)paymentInfo error:(SupereraSDKError *)error {
    [MopubSDKManager callUnityMethod:unity_method_paymentFailed withArgs:@{unity_args_key_paymentInfo: [paymentInfo toJSON], unity_args_key_error: [error toJSON]}];
}

+ (void)log:(NSString *)log {
    NSLog(@"%@",log);
    [self callUnityMethod:@"sdkLog" withArgs:@{@"testFromiOS":@"test"}];
}

#pragma - mark ingame
+ (void)setIngameParamsListener {
#if HAS_AD
    [[SupereraSDKAd sharedInstance] setInGameOnlineParamsCallback:^(NSDictionary *params) {
        //根据在线参数改变广告播放策略
        NSLog(@"ready to call ingame params %@", params);
        [MopubSDKManager callUnityMethod:unity_method_ingameParamsUpdated withArgs:params];
    }];
#endif
}

#pragma -mark social and other

+ (BOOL)openJoinChatGroup {
    return [[SupereraSDKSocialManager sharedInstance] openJoinChatGroup];
}

+ (BOOL)openJoinWhatsappChatting {
    return [[SupereraSDKSocialManager sharedInstance] openJoinWhatsappChatting];
}

+ (BOOL)openRatingView {
    return [[SupereraSDKCore sharedInstance] openRatingView];
}

+ (BOOL)openRating {
    return [[SupereraSDKCore sharedInstance] openRating];
}

+ (BOOL) addLocalNotifaciton:(NSString *)title content:(NSString *)content date:(NSString *)date hour:(NSString *)hour min:(NSString *)min{
    SupereraSDKLocalMsg *msg = [[SupereraSDKLocalMsg alloc] init];
        msg.title = title;
        msg.content = content;
        msg.date = date;
        msg.hour = hour;
        msg.min = min;
        BOOL add =[SupereraSDKPushManager addLocalNotification:msg];
    return add;

}

#pragma -mark game log
+ (void)logPlayerInfoWithCahrName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID characterLevel:(int)characterLevel serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID {
    [[SupereraSDKLoginManager sharedInstance] logPlayerInfoWithCahrName:characterName characterID:characterID characterLevel:characterLevel serverName:serverName serverID:serverID];
}

+ (void)logPlayerInfoWithCahrName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID characterLevel:(int)characterLevel serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID extraData:(NSString *)extraDataJSONString {
    [[SupereraSDKLoginManager sharedInstance] logPlayerInfoWithCahrName:characterName characterID:characterID characterLevel:characterLevel serverName:serverName serverID:serverID extraData: [NSDictionary dictionaryWithJSONString:extraDataJSONString]];
}

+ (void)logStartLevel:(nullable NSString *)levelName {
    [SupereraSDKEvents logStartLevelWithName:levelName];
}

+ (void)logFinishLevel:(nullable NSString *)levelName {
    [SupereraSDKEvents logFinishLevelWithName:levelName];
}

+ (void)logUnlockLevel:(nullable NSString *)levelName {
    [SupereraSDKEvents logUnlockLevelWithName:levelName];
}

+ (void)logSkipLevel:(nullable NSString *)levelName {
    [SupereraSDKEvents logSkipLevelWithName:levelName];
}

+ (void)logSkinUsed:(nullable NSString *)skinName {
    [SupereraSDKEvents logSkinUsedWithName:skinName];
}

+ (void)logAdEntrancePresent:(nullable NSString *)name {
    [SupereraSDKEvents logAdEntrancePresentWithName:name];
}


#pragma mark - SupereraSDKAppsflyerManagerDelegate
+ (void)setAFDataDelegate {
    [[SupereraSDKCore sharedInstance] setAppsFlyerManagerDelegate:[[self class] sharedInstance]];
}

- (void)onConversionDataReceived:(NSDictionary *)installData {
    NSLog(@"installData: %@", installData);
    NSMutableDictionary *mInstallData = [installData mutableCopy];
    for (NSString *key in installData) {
        if ([installData[key] isMemberOfClass:[NSNull class]]) {
            mInstallData[key] = nil;
        }
    }
    
    [MopubSDKManager callUnityMethod:unity_method_aFInstallConversionData withArgs:[mInstallData copy]];
}

#pragma mark - old addiction prevention
+ (void)openAddctionPrevention {
    [[SupereraSDKAddictionPreventionManager sharedInstance] openPrevention];
}

+ (long)getTotalOnlineTime {
    long time = (int)[[SupereraSDKAddictionPreventionManager sharedInstance] getTotalOnlineTime_ms];
    return time;
}


+ (void)verifyIdCardWithName:(NSString *)name cardNumber:(NSString *)cardNumber {
    [[SupereraSDKAddictionPreventionManager sharedInstance] verifyIdCardWithName:name cardNumber:cardNumber success:^{
        
        [MopubSDKManager callUnityMethod:unity_method_verify_id_card_success withStrArg:nil];
        
    } failure:^(SupereraSDKError *error) {
        
        [MopubSDKManager callUnityMethod:unity_method_verify_id_card_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)fetchIdCardInfo {
    [[SupereraSDKAddictionPreventionManager sharedInstance] fetchIdCardInfo:^(SupereraSDKIdCardInfo *info) {
            
        [MopubSDKManager callUnityMethod:unity_method_fetch_id_card_info_success withStrArg:[info toJSON]];
            
    } failure:^(SupereraSDKError *error) {
            
        [MopubSDKManager callUnityMethod:unity_method_fetch_id_card_info_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)fetchPaidAmount:(int)amount {
    [[SupereraSDKRealnameManager sharedInstance] fetchPaidAmount:amount success:^(SupereraSDKRealnamePaidAmountInfo *info) {
        
        [MopubSDKManager callUnityMethod:unity_method_fetch_paid_amount_success withStrArg:[NSString stringWithFormat:info.toJSON]];
        
    } failure:^(SupereraSDKError *error) {
        
        [MopubSDKManager callUnityMethod:unity_method_fetch_paid_amount_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)logout {
    [[SupereraSDKLoginManager sharedInstance] logoutSuccess:^{
        
        [MopubSDKManager callUnityMethod:unity_method_logout_success withStrArg:nil];
        
    } failure:^(SupereraSDKError *error) {
        
        [MopubSDKManager callUnityMethod:unity_method_logout_failed withStrArg:[error toJSON]];
        
    }];
}

#pragma mark - new addiction prevention

+ (void)openRealnamePrevention {
    [[SupereraSDKRealnameManager sharedInstance] openPrevention];
}

+ (long)getPreventionTotalOnlineTime {
    return [[SupereraSDKRealnameManager sharedInstance] getTotalOnlineTime_ms];
}

+ (NSString *)getRealnameHeartBeat {
    return [[SupereraSDKRealnameManager sharedInstance] getHeartBeat].toJSON;
}


+ (void)realnameAuthentication:(NSString *)name cardNumber:(NSString *)cardNumber {
    
    [[SupereraSDKRealnameManager sharedInstance] realnameAuthentication:name cardNumber:cardNumber success:^(SupereraSDKRealnameInfo *info){
        
        [MopubSDKManager callUnityMethod:unity_method_realname_authentication_success withStrArg:[info toJSON]];
        
    } failure:^(SupereraSDKError *error) {
        [MopubSDKManager callUnityMethod:unity_method_realname_authentication_failed withStrArg:[error toJSON]];
    }];
}

+ (void)queryRealnameInfo {
    [[SupereraSDKRealnameManager sharedInstance] queryRealnameInfo:^(SupereraSDKRealnameInfo *info) {
            
        [MopubSDKManager callUnityMethod:unity_method_query_realname_info_success withStrArg:[info toJSON]];
            
    } failure:^(SupereraSDKError *error) {
            
        [MopubSDKManager callUnityMethod:unity_method_query_realname_info_failed withStrArg:[error toJSON]];
        
    }];
}

+ (void)startRealnameAuthenticationWithUI {
    [[SupereraSDKRealnameUIManager shared] startAuthentication];
}

+ (void)fetchPaidAmountMonthly:(int)amount {
    [[SupereraSDKRealnameManager sharedInstance] fetchPaidAmount:amount success:^(SupereraSDKRealnamePaidAmountInfo *info) {
        
        [MopubSDKManager callUnityMethod:unity_method_fetch_paid_amount_monthly_success withStrArg:[NSString stringWithFormat:@"%@", info.toJSON]];
        
    } failure:^(SupereraSDKError *error) {
        
        [MopubSDKManager callUnityMethod:unity_method_fetch_paid_amount_monthly_failed withStrArg:[error toJSON]];
        
    }];
}

+ (BOOL)isUserNotificationEnable {
    return [SupereraSDKUserNotificationManager isUserNotificationEnable];
}

+ (void)openSystemNotificationSetting {
    [SupereraSDKUserNotificationManager openSystemNotificationSetting];
}

#pragma mark - wechat share

+ (void)openShareToWechatForEntrance:(NSString *)entrance shareData:(NSString *)shareData {
    
    NSDictionary *shareDataDic = [NSDictionary dictionaryWithJSONString:shareData];
    
    SupereraSDKWeChatShareScene shareScene;
    if ([shareDataDic[@"shareScene"] integerValue] == 1) {
        NSLog(@"shareScene: %@", shareDataDic[@"shareScene"]);
        shareScene = SupereraSDKWeChatShareSceneTimeline;
    }
    else {
        NSLog(@"shareScene: %@", shareDataDic[@"shareScene"]);
        shareScene = SupereraSDKWeChatShareSceneSession;
    }
    
    SupereraSDKWeChatShareMediaType mediaType;
    if ([shareDataDic[@"mediaType"] integerValue] == 1) {
        mediaType = SupereraSDKWeChatShareMediaTypeImage;
    }
    else {
        mediaType = SupereraSDKWeChatShareMediaTypeMiniProgram;
    }
    
    SupereraSDKAppsharedCampaignType campaignType;
    if ([shareDataDic[@"campaignType"] integerValue] == 0) {
        campaignType = SupereraSDKAppsharedCampaignTypeShared;
    }
    else {
        campaignType = SupereraSDKAppsharedCampaignTypeInvited;
    }
    
    SupereraSDKWeChatSharedMediaObject *object = nil;
    if (mediaType == SupereraSDKWeChatShareMediaTypeImage) {
        NSLog(@"media type: image, %@", shareDataDic[@"mediaObject"]);
        SupereraSDKWeChatSharedImageObject *imageObject = [[SupereraSDKWeChatSharedImageObject alloc] init];
        
        if (((NSString *)shareDataDic[@"mediaObject"][@"imageFilePath"]).length > 0) {
            imageObject.imageFilePath = shareDataDic[@"mediaObject"][@"imageFilePath"];
        }
        
        
        if (shareDataDic[@"mediaObject"][@"imageData"]) {
            NSLog(@"image data: %@", shareDataDic[@"mediaObject"][@"imageData"]);
            NSData *imageData = [[NSData alloc] initWithBase64EncodedString:shareDataDic[@"mediaObject"][@"imageData"] options:NSDataBase64DecodingIgnoreUnknownCharacters];
            imageObject.imageData = imageData;
        }
        
        object = imageObject;
    }
    else {
        NSLog(@"media type: mini program, %@", shareDataDic[@"mediaObject"]);
        SupereraSDKWeChatSharedMiniProgramObject *miniProgramObject = [[SupereraSDKWeChatSharedMiniProgramObject alloc] init];
        miniProgramObject.path = shareDataDic[@"mediaObject"][@"path"];
        miniProgramObject.originId = shareDataDic[@"mediaObject"][@"originId"];
        miniProgramObject.webPageUrl = shareDataDic[@"mediaObject"][@"webPageUrl"];
        
        object = miniProgramObject;
    }
    
    NSData *thumbData = nil;
    if (shareDataDic[@"thumbData"]) {
        thumbData = [[NSData alloc] initWithBase64EncodedString:shareDataDic[@"thumbData"] options:NSDataBase64DecodingIgnoreUnknownCharacters];
    }
    
    NSString *title = shareDataDic[@"title"];
    NSString *description = shareDataDic[@"description"];
    
    SupereraSDKWeChatShareData *shareDataObject = [[SupereraSDKWeChatShareData alloc] init];
    shareDataObject.campaignType = campaignType;
    shareDataObject.title = title;
    shareDataObject.note = description;
    shareDataObject.shareScene = shareScene;
    shareDataObject.mediaType = mediaType;
    shareDataObject.mediaObject = object;
    shareDataObject.thumbData = thumbData;
    
    [[SupereraSDKAppSharedManager sharedInstance] openShareToWeChatForEntrance:entrance shareData:shareDataObject completion:^(BOOL isSuccess, SupereraSDKError * _Nonnull error) {
       
        
        if (isSuccess) {
            [MopubSDKManager callUnityMethod:unity_method_app_share_success withStrArg:nil];
        }
        else {
            [MopubSDKManager callUnityMethod:unity_method_app_share_failed withStrArg:[error toJSON]];
        }
    }];
//    NSLog(@"open share to wechat: %@, %@", entrance, shareData);
}

#pragma mark - qq share

+ (void)openShareToQQForEntrance:(NSString *)entrance shareData:(NSString *)shareData {
    NSDictionary *shareDataDic = [NSDictionary dictionaryWithJSONString:shareData];
    
    SupereraSDKAppSharedScene shareScene = SupereraSDKAppSharedSceneSession;
    SupereraSDKAppSharedMediaType mediaType = SupereraSDKAppSharedMediaTypeImage;
    NSString *title = shareDataDic[@"title"];
    NSString *description = shareDataDic[@"description"];
    NSString *imageFilePath = shareDataDic[@"image"];
    
    SupereraSDKQQSharedData *shareDataObj = [[SupereraSDKQQSharedData alloc] init];
    shareDataObj.title = title;
    shareDataObj.note = description;
    shareDataObj.imageFilePath = imageFilePath;
    shareDataObj.shareScene = shareScene;
    shareDataObj.mediaType = mediaType;
    
    [[SupereraSDKAppSharedManager sharedInstance] openShareToQQForEntrance:entrance shareData:shareDataObj completion:^(BOOL isSuccess, SupereraSDKError * _Nonnull error) {
       
        
        if (isSuccess) {
            [MopubSDKManager callUnityMethod:unity_method_app_share_success withStrArg:nil];
        }
        else {
            [MopubSDKManager callUnityMethod:unity_method_app_share_failed withStrArg:[error toJSON]];
        }
    }];
}

#pragma mark - Invite Bonus

+ (void)fetchInviteBonusList {
    [[SupereraSDKAppSharedManager sharedInstance] fetchBonusListWithCompletion:^(NSArray<SupereraSDKAppSharedBonusCategory *> * _Nullable bonusArray, SupereraSDKError * _Nullable error) {
        
        if (error) {
            [MopubSDKManager callUnityMethod:unity_method_fetch_invite_bonus_list_failed withStrArg:[error toJSON]];
        }
        else {
            NSMutableArray *dicArray = [NSMutableArray array];
            for (SupereraSDKAppsharedBonus *bonus in bonusArray) {
                [dicArray addObject:[bonus toDictionary]];
            }
            
            NSData *data = [NSJSONSerialization dataWithJSONObject:dicArray options:0 error:nil];
            NSString *jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            
            [MopubSDKManager callUnityMethod:unity_method_fetch_invite_bonus_list_success withStrArg:jsonString];
        }
        
    }];
}

+ (void)acceptInviteBonusWithInviteId:(NSString *)inviteId rewardedId:(NSString *)rewardedId {
    
    [[SupereraSDKAppSharedManager sharedInstance] acceptBonusWithInviteId:inviteId rewardedId:rewardedId completion:^(SupereraSDKError * _Nullable error) {
       
        if (error) {
            [MopubSDKManager callUnityMethod:unity_method_accept_bonus_failed withStrArg:[error toJSON]];
        }
        else {
            [MopubSDKManager callUnityMethod:unity_method_accept_bonus_success withStrArg:nil];
        }
    }];
    
}

+ (NSDictionary *)getCurrentSharedCampaign {
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    
    SupereraSDKAppSharedCampaign *campaign = [[SupereraSDKAppSharedManager sharedInstance] getCurrentShareCampaign];
    dic[@"name"] = campaign.name;
    dic[@"startTime"] = @(campaign.startTimestamp_s);
    dic[@"endTime"] = @(campaign.endTimestamp_s);
    dic[@"campaignType"] = @(campaign.type);
    
    return [dic copy];
}

+ (NSDictionary *)getCurrentInvitedCampaign {
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    
    SupereraSDKAppSharedCampaign *campaign = [[SupereraSDKAppSharedManager sharedInstance] getCurrentInviteCampaign];
    dic[@"name"] = campaign.name;
    dic[@"startTime"] = @(campaign.startTimestamp_s);
    dic[@"endTime"] = @(campaign.endTimestamp_s);
    dic[@"campaignType"] = @(campaign.type);
    
    return [dic copy];
}

#pragma mark - SupereraSDKRealnameUIDelegate

- (void)manager:(SupereraSDKRealnameUIManager *)manager authSuccess:(SupereraSDKUIRealnameInfo *)realnameInfo {
    [MopubSDKManager callUnityMethod:unity_method_realname_ui_start_success withStrArg:@"success"];
    
    [MopubSDKManager callUnityMethod:unity_method_show_realname_ui_success withStrArg:@"success"];
}

- (void)manager:(SupereraSDKRealnameUIManager *)manager authFailed:(NSError *)error {
    [MopubSDKManager callUnityMethod:unity_method_realname_ui_start_failed withStrArg:@"failed"];

//    [MopubSDKManager callUnityMethod:unity_method_show_realname_ui_failed withStrArg:@"failed"];
}

- (void)manager:(SupereraSDKRealnameUIManager *)manager addictionWithRealnameInfo:(SupereraSDKUIRealnameInfo *)realnameInfo {
    
    SupereraSDKError *error = [SupereraSDKError errorWithClientCode:SupereraSDKErrorCodeUnknown clientMessage:@"addiction" domain:@"com.superera.sdk.unity.error" domainCode:1 domainMessage:@"addiction"];
    
    [MopubSDKManager callUnityMethod:unity_method_realname_ui_in_addiction withStrArg:[error toJSON]];
    
    [MopubSDKManager callUnityMethod:unity_method_show_realname_ui_failed withStrArg:[error toJSON]];
}


+ (void)saveCloudCacheWithUid:(NSString *)uid version:(NSInteger)version data:(NSString *)data {
    [SupereraSDKCloudCacheRequest saveCacheForUid:uid version:version data:data success:^{
        
        [MopubSDKManager callUnityMethod:unity_method_save_cloud_cache_success withStrArg:nil];
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        
        [MopubSDKManager callUnityMethod:unity_method_save_cloud_cache_failed withStrArg:[error toJSON]];
    }];
}

+ (void)getCloudCacheWithUid:(NSString *)uid {
    
    [SupereraSDKCloudCacheRequest getCacheForUid:uid success:^(SupereraSDKCloudCache * _Nonnull cache) {
        
        [MopubSDKManager callUnityMethod:unity_method_get_cloud_cache_success withStrArg:[cache toJSON]];
        
    } failure:^(SupereraSDKError * _Nonnull error) {
        
        [MopubSDKManager callUnityMethod:unity_method_get_cloud_cache_failed withStrArg:[error toJSON]];
        
    }];
    
}

+ (void)getRedeemWithCode:(NSString *)code {
    
    [SupereraSDKExchangeRequest exchangeWithCode:code success:^(NSString *string) {
        [MopubSDKManager callUnityMethod:unity_method_get_redeem_success withStrArg:string];
    } failure:^(SupereraSDKError * _Nonnull error) {
        [MopubSDKManager callUnityMethod:unity_method_get_redeem_failed withStrArg:[error toJSON]];
    }];
}

+ (void)pushSDKInitWithCgi:(NSString *)cgi appid:(NSString *)appid appSecret:(NSString *)appSecret passThroughEnable:(BOOL)passThroughEnable logHost:(NSString *)logHost logPath:(NSString *)logPath {
}

+ (void)setPushAlias:(NSString *)alias {
#if HAS_JODO_PUSH
    [[JodoPushClient shared] setAlias:alias];
#endif
}

#pragma mark - AppDelegateListener

- (void)didRegisterForRemoteNotificationsWithDeviceToken:(NSNotification *)notification {
#if HAS_JODO_PUSH
    NSData *deviceToken = (NSData *)notification.userInfo;
    [[JodoPushClient shared] bindDeviceToken:deviceToken];
#endif
}

- (void)applicationWillFinishLaunchingWithOptions:(NSNotification*)notification {
    NSLog(@"notification: %@",notification.userInfo);
#if HAS_JODO_PUSH
    NSBundle *bundle = [NSBundle mainBundle];
    if ([[bundle objectForInfoDictionaryKey:@"CFBundleIdentifier"] isEqualToString:@"ioa.chenz.dwelst.gn"]) {//蜗居
        JodoPushClientConfig *pushConfig = [[JodoPushClientConfig alloc] init];
        pushConfig.debug = YES;
        pushConfig.passThroughEnable = YES;
        pushConfig.logUploadHost = @"https://logr.push.casdk.cn";
        pushConfig.logUploadPath = @"clientlog/light_client_log";
        
        [[JodoPushClient shared] registerJodoPushWithCgi:@"chenzong_dwelst" appid:@"94" appSecret:@"f1ab883e89994936b3ba685ca0cd091f" config:pushConfig delegate:[MopubSDKManager sharedInstance]];
        [[JodoPushClient shared] applicationDidFinishLaunchingWithOptions:notification.userInfo];
        [[UNUserNotificationCenter currentNotificationCenter] setDelegate:[MopubSDKManager sharedInstance]];
        [[UNUserNotificationCenter currentNotificationCenter] requestAuthorizationWithOptions:UNAuthorizationOptionAlert completionHandler:^(BOOL granted, NSError * _Nullable error) {
        }];
        [[UIApplication sharedApplication] registerForRemoteNotifications];
    } else{
        if (IS_GAT) {
            if (USE_JODO_PUSH) {
                JodoPushClientConfig *pushConfig = [[JodoPushClientConfig alloc] init];
                pushConfig.debug = YES;
                pushConfig.passThroughEnable = YES;
                pushConfig.logUploadHost = @"https://logrepo.sdkapi.ninetrial.com";
                pushConfig.logUploadPath = @"clientlog/light_client_log";

                [[JodoPushClient shared] registerJodoPushWithCgi:@"chenzong_vampiregat" appid:@"82" appSecret:@"6976df46e2ad4b42bd24d62530f2a050" config:pushConfig delegate:[MopubSDKManager sharedInstance]];
                [[JodoPushClient shared] applicationDidFinishLaunchingWithOptions:notification.userInfo];
            }
        } else {
            if (USE_JODO_PUSH) {
                JodoPushClientConfig *pushConfig = [[JodoPushClientConfig alloc] init];
                pushConfig.debug = YES;
                pushConfig.passThroughEnable = YES;
                pushConfig.logUploadHost = @"https://logr.push.casdk.cn";
                pushConfig.logUploadPath = @"clientlog/light_client_log";

                [[JodoPushClient shared] registerJodoPushWithCgi:@"chenzong_vampire" appid:@"71" appSecret:@"fbc2a7afeab44cefb100ad868bda2556" config:pushConfig delegate:[MopubSDKManager sharedInstance]];
                [[JodoPushClient shared] applicationDidFinishLaunchingWithOptions:notification.userInfo];
            }
        }
        [[UNUserNotificationCenter currentNotificationCenter] setDelegate:[MopubSDKManager sharedInstance]];
        [[UNUserNotificationCenter currentNotificationCenter] requestAuthorizationWithOptions:UNAuthorizationOptionAlert completionHandler:^(BOOL granted, NSError * _Nullable error) {
        }];
        [[UIApplication sharedApplication] registerForRemoteNotifications];
    }
#endif
}

- (void)onOpenURL:(NSNotification*)notification {
    
    [[SupereraSDKLoginManager sharedInstance] application:[UIApplication sharedApplication] handleOpenURL:notification.userInfo[@"url"]];
    [SupereraSDKHandleAppDelegateEvent application:[UIApplication sharedApplication] openURL:notification.userInfo[@"url"] options:notification.userInfo[@"options"]];
}


#pragma mark - JodoPushClientDelegate
///收到透传信息
///
- (void)jodoPushClientReceiveNotification:(NSDictionary *)data {

}
///注册成功
- (void)jodoPushRegisterSuccess {
    
}
///注册失败
- (void)jodoPushRegisterFailed:(NSError *)error {
    
}
///设置别名失败
- (void)jodoPushClientSetAliasFailed:(NSError *)error {
    
}
///设置别名成功
- (void)jodoPushClientSetAliasSuccess:(NSString *)alias {
    
}

///设置标签成功
- (void)jodoPushSetTagSuccess {
    
}
///设置标签失败
- (void)jodoPushSetTagFailed:(NSError *)error {
    
}

#pragma mark - UNUserNotificationCenterDelegate

- (void)userNotificationCenter:(UNUserNotificationCenter *)center didReceiveNotificationResponse:(UNNotificationResponse *)response withCompletionHandler:(void (^)(void))completionHandler {

    [[JodoPushClient shared] userNotificationCenter:center didReceiveNotificationResponse:response];
    completionHandler();
}

- (void)userNotificationCenter:(UNUserNotificationCenter *)center willPresentNotification:(UNNotification *)notification withCompletionHandler:(void (^)(UNNotificationPresentationOptions))completionHandler {
    
    NSLog(@"!@#!@#!@#!@#!@#!@# notification2");
    [[JodoPushClient shared] userNotificationCenter:center willPresentNotification:notification];
}


- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult))completionHandler {
    NSLog(@"!@#!@#!@#!@#!@#!@# notification");
    [[JodoPushClient shared] application:application didReceiveRemoteNotification:userInfo];

    completionHandler(UIBackgroundFetchResultNewData);
}

#pragma mark - SupereraSDKPackageUpdateManagerDelegate
- (void)packageUpdateSuccess {
    [MopubSDKManager callUnityMethod:unity_method_package_updated_success withStrArg:nil];
}


#pragma mark - setATTDataUpdatedListener
- (void)seAttributeDataSuccess:(NSDictionary *)installData {
    [MopubSDKManager callUnityMethod:unity_args_key_aTTInstallConversionData withArgs:installData];
}

@end
