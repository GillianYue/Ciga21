//
//  SupereraSDKPurchasedItem.h
//  SupereraSDKMobileCore
//
//  Created by Kubrick.G on 2019/5/30.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKCommon.h>

/**
 已购商品消耗状态

 - SupereraSDKItemConsumeStateUnconsumed: 未消耗
 - SupereraSDKItemConsumeStateConsumed: 已小号
 */
typedef NS_ENUM(int, SupereraSDKItemConsumeState) {
    SupereraSDKItemConsumeStateUnconsumed = 0,
    SupereraSDKItemConsumeStateConsumed = 1,
    
    
};

@interface SupereraSDKPurchasedItem : NSObject <JSONSerializable, MapSerializable>

@property (nonatomic, copy) NSString *itemID;

@property (nonatomic, copy) NSString *sdkOrderID;

@property (nonatomic, assign) NSInteger purchaseTimeMS;

@property (nonatomic, assign) NSInteger consumeTimeMS;
///状态：0-未消耗，1-已消耗
@property (nonatomic, assign) SupereraSDKItemConsumeState consumeState;

@property (nonatomic, copy) NSString *cpOrderID;

+ (nullable instancetype)initWithData:(nonnull NSDictionary *)data;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;

- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;

@end

