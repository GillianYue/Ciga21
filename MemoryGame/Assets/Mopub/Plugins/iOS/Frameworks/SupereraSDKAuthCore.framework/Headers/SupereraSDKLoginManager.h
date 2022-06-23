//
//  SupereraSDKLoginManager.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/4/28.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKError.h>

#import "SupereraSDKLoginWithDeviceRequest.h"
#import "SupereraSDKLoginWithGameCenterRequest.h"
#import "SupereraSDKLoginResult.h"
#import "SupereraSDKLinkRequest.h"
#import "SupereraSDKLinkResult.h"
#import "FetchLinkedAccountResult.h"
#import "FetchLinkedAccountRequest.h"
#import "SupereraSDKLoginWithWeChatRequest.h"
#import "SupereraSDKLoginWithFacebookRequest.h"
#import "SupereraSDKLoginWithVisitorRequest.h"
#import "SupereraSDKLoginWithMobileATAuthRequest.h"
#import "SupereraSDKLoginWithAppleRequest.h"

NS_ASSUME_NONNULL_BEGIN

@class SupereraSDKLoginWithEmailRequest;
@class SupereraSDKLinkWithEmailRequest;

/**
 登录成功结果回调

 @param loginResult 登录结果信息
 */
typedef void(^SDKLoginSuccessCallback)(SupereraSDKLoginResult *loginResult);


/**
 绑定成功结果回调

 @param linkResult 绑定结果信息
 */
typedef void(^SDKLinkSuccessCallback)(SupereraSDKLinkResult *linkResult);

///SDK错误来源：登录验证
FOUNDATION_EXPORT SupereraSDKErrorDomain SupereraSDKErrorDomainSDKAuthCore;

@interface SupereraSDKLoginManager : NSObject

+ (instancetype)sharedInstance;

#pragma mark - Auto Login
/**
 Automatically log in and link the specified type. Will link the type of server configuration if the specified type is Unknown. When the link operation fails, the account will be a device type account with no linked account.

 @param type third party account type to auto link
 @param successCallback success callback
 @param errorCallback failed callback
 */
- (void)autoLoginAndLinkWithType:(SupereraSDKAccountType)type success:(SDKLoginSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

/**
 Automatically log in and link the specified type. Will link the type of server configuration if the specified type is Unknown. When the link operation fails, the account will be a device type account with no linked account.

 @param type third party account type to auto link
 @param activeCode if not need, fill nil
 @param successCallback success callback
 @param errorCallback failed callback
 */
- (void)autoLoginAndLinkWithType:(SupereraSDKAccountType)type activeCode:(nullable NSString *)activeCode success:(SDKLoginSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

#pragma mark - Device
/**
 使用设备进行登录

 @param request 登录请求参数
 @param successCallback 登录成功回调
 @param errorCallback 登录失败回调
 */
- (void)loginWithDevice:(SupereraSDKLoginWithDeviceRequest *)request success:(SDKLoginSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

#pragma mark - Game Center

/**
 使用 Game Center 进行登录

 @param request 登录请求参数
 @param successCallback 登录成功回调
 @param errorCallback 登录失败回调
 */
- (void)loginWithGameCenter:(SupereraSDKLoginWithGameCenterRequest *)request success:(SDKLoginSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

/**
使用 Game Center 进行绑定

 @param successCallback 绑定成功回调
 @param errorCallback 绑定失败回调
 */
- (void)linkGameCenterWithSuccess:(SDKLinkSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

/**
 使用 Game Center 进行绑定

 @param request 绑定请求参数
 @param successCallback 绑定成功回调
 @param errorCallback 绑定失败回调
 */
- (void)linkGameCenterWithRequest:(SupereraSDKLinkRequest *)request success:(SDKLinkSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

#pragma mark - WeChat
/**
 使用 We Chat 进行登录
 
 @param request 登录请求参数
 @param successCallback 登录成功回调
 @param errorCallback 登录失败回调
 */
- (void)loginWithWeChat:(SupereraSDKLoginWithWeChatRequest *)request
    success:(SDKLoginSuccessCallback)successCallback
    failure:(ErrorCallback)errorCallback;

/**
使用 We Chat 进行绑定

 @param successCallback 绑定成功回调
 @param errorCallback 绑定失败回调
 */
- (void)linkWeChatWithSuccess:(SDKLinkSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

/**
 使用 We Chat 进行绑定

 @param request 绑定请求参数
 @param successCallback 绑定成功回调
 @param errorCallback 绑定失败回调
 */
- (void)linkWeChatWithRequest:(SupereraSDKLinkRequest *)request success:(SDKLinkSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

#pragma mark - Facebook
/**
 使用 Facebook 进行登录
 
 @param request 登录请求参数
 @param successCallback 登录成功回调
 @param errorCallback 登录失败回调
 */
- (void)loginWithFacebook:(SupereraSDKLoginWithFacebookRequest *)request
    success:(SDKLoginSuccessCallback)successCallback
    failure:(ErrorCallback)errorCallback;

/**
 使用 Facebook 进行绑定

 @param request 绑定请求参数
 @param successCallback 绑定成功回调
 @param errorCallback 绑定失败回调
 */
- (void)linkFacebookWithRequest:(SupereraSDKLinkRequest *)request success:(SDKLinkSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

/**
使用 Facebook 进行绑定

 @param successCallback 绑定成功回调
 @param errorCallback 绑定失败回调
 */
- (void)linkFacebookWithSuccess:(SDKLinkSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;

#pragma mark - Mobile
/**
 手机短信登录
 
 @param request 登录请求参数
 @param successCallback 登录成功回调
 @param errorCallback 登录失败回调
 */
-(void)loginWithMobile:(id)request
               success:(SDKLoginSuccessCallback)successCallback
               failure:(ErrorCallback)errorCallback;

/**
 绑定手机号
 
 @param phoneNumber 手机号码
 @param code 验证码
 @param success 成功回调
 @param failure 失败回调
 */
- (void)linkMobile:(NSString *)phoneNumber
              code:(NSString *)code
           success:(void(^)(SupereraSDKLinkResult *linkResult))success
           failure:(void(^)(SupereraSDKError *error))failure;

/**
 获取手机验证码
 @param phoneNumber 手机号码
 */
- (void)fetchMobileAuthCodeWithPhoneNumber:(NSString *)phoneNumber success:(void(^)(void))success failure:(void(^)(SupereraSDKError *error))failure;

/**
 使用设备进行登录
 
 @param request 登录请求参数
 @param successCallback 登录成功回调
 @param errorCallback 登录失败回调
 */
- (void)loginWithMobileATAuth:(SupereraSDKLoginWithMobileATAuthRequest *)request success:(SDKLoginSuccessCallback)successCallback failure:(ErrorCallback)errorCallback;


#pragma mark - Email
/**
 邮箱登录
 
 @param request 请求参数
 @param successCallback 登录成功回调
 @param errorCallback 登录失败回调
 */
- (void)loginWithEmail:(SupereraSDKLoginWithEmailRequest *)request
                 success:(SDKLoginSuccessCallback)successCallback
                 failure:(ErrorCallback)errorCallback;

/**
 绑定邮箱
 
 @param email 邮箱
 @param password 密码
 @param code 验证码
 @param success 成功回调
 @param failure 失败回调
 */
- (void)linkEmail:(NSString *)email
         password:(NSString *)password
             code:(NSString *)code
          success:(void(^)(SupereraSDKLinkResult *linkResult))success
          failure:(void(^)(SupereraSDKError *error))failure;

/**
 获取邮箱验证码
 
 @param email 邮箱
 @param success 成功回调
 @param failure 失败回调
 */
- (void)fetchEmailAuthCodeWithEmail:(NSString *)email success:(void(^)(void))success failure:(void(^)(SupereraSDKError *error))failure;


/**
 重置邮箱密码
 
 @param email 邮箱
 @param newPassword 新密码
 @param code 验证码
 @param success 成功回调
 @param failure 失败回调
 */
- (void)resetPasswordWithEmail:(NSString *)email
                   newPassword:(NSString *)newPassword
                          code:(NSString *)code
                       success:(void(^)(void))success
                       failure:(void(^)(SupereraSDKError *error))failure;

#pragma mark - Vistor

/**
 游客登录
 
 @param request 登录请求参数
 @param successCallback 成功回调
 @param errorCallback 失败回调
*/
- (void)loginWithVisitor:(SupereraSDKLoginWithVisitorRequest *)request
                 success:(SDKLoginSuccessCallback)successCallback
                 failure:(ErrorCallback)errorCallback;

#pragma mark - Sign in with apple

- (void)loginWithApple:(SupereraSDKLoginWithAppleRequest *)request
               success:(SDKLoginSuccessCallback)successCallback
               failure:(ErrorCallback)errorCallback API_AVAILABLE(ios(13.0));

#pragma mark - 

/**
 获取当先登录帐号所绑定的第三方帐号信息

 @param request 获取请求参数
 @param successCallback 成功回调
 @param errorCallback 失败回调
 */
- (void)fetchCurrentLinkedAccountWithRequest:(FetchLinkedAccountRequest *)request success:(void(^)(FetchLinkedAccountResult *result))successCallback failure:(ErrorCallback)errorCallback;


/**
 获取当先登录帐号所绑定的第三方帐号信息

 @param successCallback 成功回调
 @param errorCallback 失败回调
 */
- (void)fetchCurrentLinkedAccountWithSuccess:(void(^)(FetchLinkedAccountResult *result))successCallback failure:(ErrorCallback)errorCallback;

- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url;

- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url options:(NSDictionary<NSString*, id>*)options;

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;

- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void(^)(NSArray<id<UIUserActivityRestoring>> * __nullable restorableObjects))restorationHandler;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(nonnull NSDictionary *)launchOptions;
/**
 获取上一次登录方式，如果没有历史登录记录，返回 0

 @return SupereraSDKAccountType
 */
- (SupereraSDKAccountType)lastLoginType;

#pragma mark - logout

/**
 登出
 */
- (void)logoutSuccess:(void(^)(void))success failure:(void(^)(SupereraSDKError *error))failure;

#pragma mark - verify session token

/**
 验证 session token
 @param token token
 @param success 成功回调，返回token是否有效
 @param failure 失败回调
 */
- (void)verifySessionToken:(NSString *)token success:(void(^)(void))success failure:(void(^)(SupereraSDKError *error))failure;

#pragma -mark game log

/// 上报玩家信息，只要游戏具有以下任意参数，就需要上报
/// @param characterName 角色名
/// @param characterID 角色ID
/// @param characterLevel 角色等级
/// @param serverName 服务器名
/// @param serverID 服务器ID
- (void)logPlayerInfoWithCahrName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID characterLevel:(int)characterLevel serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID;

#pragma mark - set test environment
+ (void)setTestEnvironment:(BOOL)isTest;

@end
                        
NS_ASSUME_NONNULL_END
