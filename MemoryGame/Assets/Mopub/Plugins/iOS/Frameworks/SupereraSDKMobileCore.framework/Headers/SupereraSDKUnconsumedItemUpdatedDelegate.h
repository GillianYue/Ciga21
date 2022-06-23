//
//  SupereraSDKUnconsumedItemUpdatedCallback.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2019/6/2.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupereraSDKPurchasedItem.h"

NS_ASSUME_NONNULL_BEGIN

@protocol SupereraSDKUnconsumedItemUpdatedDelegate <NSObject>

/**
 当前登录账号下有新的未消耗的已购商品

 @param unconsumedItems 未消耗的已购商品
 */
- (void)unconsumedItemUpdated:(NSArray<SupereraSDKPurchasedItem *> *) unconsumedItems;

@end

NS_ASSUME_NONNULL_END
