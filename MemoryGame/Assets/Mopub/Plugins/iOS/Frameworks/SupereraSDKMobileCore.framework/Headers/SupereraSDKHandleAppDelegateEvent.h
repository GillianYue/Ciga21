//
//  SupereraSDKHandleAppDelegateEvent.h
//  SDKMobileCore
//
//  Created by 梁嘉浩 on 2020/9/16.
//  Copyright © 2020 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKHandleAppDelegateEvent : NSObject

+ (void)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions;

// Open URI-scheme for iOS 9 and above
+ (BOOL)application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary *) options;

// Open URI-scheme for iOS 8 and below
+ (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation;

// Open Universal Links
+ (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler;

// Report Push Notification attribution data for re-engagements
+ (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult))completionHandler;

@end

NS_ASSUME_NONNULL_END
