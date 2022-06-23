//
//  SupereraSDKApplicationDelegate.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/25.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupereraSDKApplicationObserving.h"

NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKApplicationDelegate : NSObject

+ (instancetype)sharedInstance;

//启动
- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions;

//openURL跳转
#if __IPHONE_OS_VERSION_MAX_ALLOWED > __IPHONE_9_0
- (BOOL)application:(UIApplication *)application
            openURL:(NSURL *)url
            options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options;
#endif

//openURL跳转
- (BOOL)application:(UIApplication *)application
            openURL:(NSURL *)url
  sourceApplication:(nullable NSString *)sourceApplication
         annotation:(nullable id)annotation;


/**
 添加一个 Application 生命周期监听

 @param observer 监听者
 */
- (void)addObserver:(id<SupereraSDKApplicationObserving>)observer;


/**
 移除一个 Application 生命周期监听

 @param observer 监听者
 */
- (void)removeObserver:(id<SupereraSDKApplicationObserving>)observer;



//推送
- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult result))completionHandler;


-(void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error;

- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken;


- (void)application:(UIApplication *)application didReceiveLocalNotification:(UILocalNotification *)notification;

- (void)application:(UIApplication *)application didRegisterUserNotificationSettings:(UIUserNotificationSettings *)notificationSettings;
@end

NS_ASSUME_NONNULL_END
