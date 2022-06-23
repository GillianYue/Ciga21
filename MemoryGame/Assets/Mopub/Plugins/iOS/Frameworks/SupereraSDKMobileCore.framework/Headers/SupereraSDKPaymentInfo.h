//
//  SupereraSDKPaymentInfo.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2019/5/29.
//  Copyright © 2019 superera. All rights reserved.
//

#import "SupereraSDKPaymentItemDetails.h"

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKPaymentInfo : NSObject <JSONSerializable, MapSerializable>

@property (nonatomic, copy) NSString *itemID;

@property (nonatomic, copy) NSString *cpOrderID;
@property (nonatomic, copy) NSString *sdkOrderID;

@property (nonatomic, assign) NSInteger price;
@property (nonatomic, copy) NSString *formatedPrice;
@property (nonatomic, copy) NSString *currency;

@property (nonatomic, assign) PaymentItemType type;

@property (nonatomic, copy) NSString *itemName;

@property (nonatomic, copy) NSString *characterName;
@property (nonatomic, copy) NSString *characterID;
@property (nonatomic, copy) NSString *serverName;
@property (nonatomic, copy) NSString *serverID;

@property (nonatomic, copy, nullable) NSString *paymentURL;
@property (nonatomic, copy, nullable) NSString *referer;
@property (nonatomic, copy, nullable) NSString *htmlString;

/**
 额外参数
 */
@property (nonatomic, copy) NSString *extraData;


- (instancetype)initWithItemID:(NSString *)itemID cpOrderID:(nullable NSString *)cpOrderID characterName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;

- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;
@end

NS_ASSUME_NONNULL_END
