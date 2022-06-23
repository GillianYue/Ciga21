//
//  SupereraSDKPaymentItemDetails.h
//  SupereraSDKMobileCore
//
//  Created by Kubrick.G on 2019/5/30.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

/**
 商品类型

 - PaymentItemTypeConsumable: 可消耗类型
 */
typedef NS_ENUM(int, PaymentItemType) {
    PaymentItemTypeConsumable = 1,
    PaymentItemTypeSubscription = 2,
};
@interface SupereraSDKPaymentItemDetails : NSObject <JSONSerializable, MapSerializable>

@property (nonatomic, copy) NSString *itemID;

@property (nonatomic, copy) NSString *displayName;

@property (nonatomic, copy) NSNumber *price;

@property (nonatomic, copy) NSString *currency;

@property (nonatomic, copy) NSString *formattedPrice;

@property (nonatomic, assign) PaymentItemType type;

+ (nullable instancetype)initWithData:(NSDictionary *)data;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;

- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;
@end

