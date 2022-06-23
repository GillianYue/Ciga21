//
//  SupereraAFLogger.h
//  SupereraSDKMobileCore
//
//  Created by 梁嘉浩 on 2020/6/28.
//  Copyright © 2020 superera. All rights reserved.
//


#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface SupereraAFLogger : NSObject

+ (void)logCustomAFEvent:(NSString *)event params:(nullable NSDictionary *)params;

/**
 上报登录事件
 */
+ (void)logAFLoginEvent;

/**
 上报登录留存事件
 */
+ (void)logAFLoginRetentionEvent;

@end

NS_ASSUME_NONNULL_END
