//
//  SupereraSDKCore.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKError.h>

#import "SupereraSDKLifeCycleObserving.h"
#import "SupereraSDKConfiguration.h"
#import "FetchGameConfigurationRequest.h"
#import "FetchGameConfigurationResponse.h"
#import "SupereraSDKSettings.h"
#import "SupereraSDKAppsFlyerManagerDelegate.h"

NS_ASSUME_NONNULL_BEGIN


@interface SupereraSDKCore : NSObject

/**
 获取 SDKCore 实例

 @return 实例
 */
+ (instancetype)sharedInstance;


/**
 初始化SDK

 @return 初始化结果，如果返回nil说明成功
 */

/**
 初始化SDK

 @param configuration 初始化所需参数
 @param successCallback 成功回调
 @param errorCallback 错误回调
 */
- (void)SDKInitWithConfig:(SupereraSDKConfiguration *)configuration success:(void(^)(void))successCallback failure:(ErrorCallback)errorCallback;


/**
 异步获取远程游戏配置

 @param request 请求参数
 @param successCallback 成功回调
 @param errorCallback 错误回调
 */
- (void)fetchGameConfiguration:(FetchGameConfigurationRequest *)request success:(void(^)(FetchGameConfigurationResponse *response))successCallback failure:(ErrorCallback)errorCallback;


/**
 设置游戏的用户唯一标识（可使用用户ID或存档ID）

 @param userID 唯一标识
 */
- (void)setGameUserID:(NSString *)userID;

/**
 获取SDKSettings，如果为空，说明还未初始化

 @return settings
 */
- (nullable SupereraSDKSettings *)getSDKSettings;

#define ADD_LIFECYCLE_OBSERVER(observer) \
+ (void)load \
{ \
    [[SupereraSDKCore sharedInstance] addObserver:observer]; \
}

/**
 添加一个 SDK 生命周期监听
 
 @param observer 监听者
 */
- (void)addObserver:(id<SupereraSDKLifeCycleObserving>)observer;

/**
 移除一个 SDK 生命周期监听
 
 @param observer 监听者
 */
- (void)removeObserver:(id<SupereraSDKLifeCycleObserving>)observer;


/**
  添加对 SDKWillFinishInit 的 Notification 监听

 @param observer 监听者
 @param aSelector 监听方法，方法可以接受参数 NSNotification，object 为 SupereraSDKSettings
 */
+ (void)addSDKWillFinishInitObserver: (id)observer selector:(SEL)aSelector;


/**
 移除对 SDKWillFinishInit 的监听
 */
+ (void)removeSDKWillFinishInitObserver: (id)observer;


/**
 根据模块名称和keyName获取在线参数的配置
 */
- (nullable NSString *)getOpenParamWithModule:(NSString*)moduleName key:(NSString*)keyName;

- (nullable NSString *)getOpenParamWithKey:(NSString*)keyName;

- (nullable NSDictionary *)getOpenParamsWithModule:(NSString*)moduleName key:(NSString*)keyName;

/**
 获取 SDK 版本号

 @return 版本号
 */
- (NSString *)getSDKVersion;


/// 打开评分引导界面
- (BOOL)openRatingView;

/**
 获取当前登录用户的uid

 @return 如果没登录返回空
 */
- (nullable NSNumber *)currentAccountID;
- (nullable NSString *)currentSessionToken;

- (void)setAppsFlyerManagerDelegate:(id<SupereraSDKAppsFlyerManagerDelegate>)delegate;
- (id<SupereraSDKAppsFlyerManagerDelegate>)getAppsFlyerManagerDelegate;

/**
 添加公共的埋点参数
 */
- (void)addGlobalLogParams:(NSDictionary *)params;

/**
 添加公共的http请求头
 */
- (void)addGlobalHeader:(NSDictionary *)params;

/**
 移除公共的http请求头
 */
- (void)removeGlobalHeader:(NSString *)param;

/**
 获取 puid
 */
- (NSString *)puid;

/**
 获取 idfa
 */
- (NSString *)idfa;

/**
 获取 pn
 */
- (NSString *)pn;

/**
 获取地区代码
 */
- (NSString *)getRegion;

/**
 获取应用版本号
 */
- (NSString *)getAppVersion;

@end

NS_ASSUME_NONNULL_END
