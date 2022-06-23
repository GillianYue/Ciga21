//
//  RestoreSubscriptionDelegate.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2019/10/16.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKError.h>

NS_ASSUME_NONNULL_BEGIN

@protocol RestoreSubscriptionDelegate <NSObject>

//用户有登录态，可进行苹果支付，返回成功
- (void)restoreSuccess;

//否则返回失败
- (void)restoreFailed:(SupereraSDKError *)error;

@end

NS_ASSUME_NONNULL_END
