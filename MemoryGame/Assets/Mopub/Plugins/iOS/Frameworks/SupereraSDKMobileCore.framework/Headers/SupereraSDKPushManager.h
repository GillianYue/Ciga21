//
//  SESDKPush.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2020/1/10.
//  Copyright © 2020 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "SupereraSDKLocalMsg.h"

NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKPushManager : NSObject


/// set one new local push msg
/// @param localMsg lcoalMsg
+ (BOOL)addLocalNotification:(SupereraSDKLocalMsg *)localMsg;

@end

NS_ASSUME_NONNULL_END
