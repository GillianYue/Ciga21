//
//  SupereraSDKLifeCyclesObserving.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/26.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "SupereraSDKSettings.h"
#import "SupereraSDKOnlineConfig.h"

NS_ASSUME_NONNULL_BEGIN

/// typedef for SupereraSDKLifeCycleNotificationName
typedef NSString * SupereraSDKLifeCycleNotificationName NS_TYPED_EXTENSIBLE_ENUM;

///SDK 即将完成初始化监听名称
FOUNDATION_EXPORT SupereraSDKLifeCycleNotificationName SupereraSDKLifeCycleSDKWillFinishInitNotification;

/// 登录相关的通知名称
typedef NSString * SupereraSDKLoginNotification NS_TYPED_EXTENSIBLE_ENUM;
typedef NSString * SupereraSDKLogoutNotification NS_TYPED_EXTENSIBLE_ENUM;
///SDK 登录成功并且已经回调
FOUNDATION_EXPORT SupereraSDKLoginNotification SupereraSDKLoginNotificationLoginSuccessAfterCallback;

FOUNDATION_EXPORT SupereraSDKLogoutNotification SupereraSDKLogoutNotificationLogoutSuccess;

static NSString *KEY_ACCOUNT_ID = @"account_id";
static NSString *KEY_SESSION_TOKEN = @"session_token";

@protocol SupereraSDKLifeCycleObserving <NSObject>

@optional

/**
 SDK 完成初始化，该回调会阻塞初始化返回，请勿做耗时操作

 @param settings settings
 */
- (void)SDKWillFinishInitWithSettings:(SupereraSDKSettings *)settings;

/**
 SDK 完成初始化，该回调会在初始化返回后执行，不会阻塞
 
 @param settings settings
 */
- (void)SDKDidFinishInitWithSettings:(SupereraSDKSettings *)settings;

/**
 SDK 完成获取在线配置

 @param onlineConfig 在线配置
 */
- (void)SDKDidFinishFetchOnlineConfig:(SupereraSDKOnlineConfig *)onlineConfig;


/**
 SDK登录成功并且已经回调

 @param accountID uid
 @param sessionToken token
 */
- (void)SDKLoginSuccessAfterCallbackWithAccountID:(NSInteger)accountID sessionToken:(NSString *)sessionToken;

/**
 SDK登出成功
 */
- (void)SDKLogoutSuccess;

@end

NS_ASSUME_NONNULL_END
