//
//  SupereraSDKAppLifeCyclesDelegate.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/26.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol SupereraSDKApplicationObserving <NSObject>

@optional

- (BOOL)application:(UIApplication *)application
didFinishLaunchingWithOptions:(nullable NSDictionary<UIApplicationLaunchOptionsKey, id> *)launchOptions;

//应用进入前台并处于活动状态时调用该方法并发出通知
- (void)applicationDidBecomeActive:(UIApplication *)application;

//应用从活动态进入非活动态时调用该方法并发出通知
- (void)applicationWillResignActive:(UIApplication *)application;

//应用进入后台时调用该方法并发出通知
- (void)applicationDidEnterBackground:(nullable UIApplication *)application;

//应用进入前台，但是还没有处于活动状态时调用该方法并发出通知
- (void)applicationWillEnterForeground:(UIApplication *)application;

//进程将死亡
- (void)applicationWillTerminate:(UIApplication *)application;

- (BOOL)application:(UIApplication *)application
            openURL:(NSURL *)url
  sourceApplication:(nullable NSString *)sourceApplication
         annotation:(nullable id)annotation;
@end

NS_ASSUME_NONNULL_END
