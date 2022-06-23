//
//  SupereraSDKSubscriptionItemUpdatedDelegate.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2019/10/16.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupereraSDKSubscriptonItem.h"

NS_ASSUME_NONNULL_BEGIN

@protocol SupereraSDKSubscriptionItemUpdatedDelegate <NSObject>
/**
 当前登录账号下有新的已购订阅商品

 @param subscriptionItems 已购订阅商品
 */
- (void)subscriptionItemUpdated:(NSArray<SupereraSDKSubscriptonItem *> *) subscriptionItems;

@end

NS_ASSUME_NONNULL_END
