//
//  SupereraSDKSubscriptonItem.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2019/10/15.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(int, SupereraSDKSubscriptonState) {
    SupereraSDKSubscriptonStateRefund = -2,
    SupereraSDKSubscriptonStateTerminated = -1,
    SupereraSDKSubscriptonStateInit = 0,
    SupereraSDKSubscriptonStateActive,
    SupereraSDKSubscriptonStateGrace,
    SupereraSDKSubscriptonStateReversed,
    SupereraSDKSubscriptonStateCancel,
};

//已购订阅商品信息
@interface SupereraSDKSubscriptonItem : NSObject <JSONSerializable, MapSerializable>

@property (nonatomic, copy) NSString *itemID;

@property (nonatomic, copy) NSString *sdkOrderID;

//生效时间
@property (nonatomic, assign) NSInteger validTimeMS;
//失效时间
@property (nonatomic, assign) NSInteger invalidTimeMS;

@property (nonatomic, assign) SupereraSDKSubscriptonState state;

+ (nullable instancetype)initWithData:(nonnull NSDictionary *)data;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;

- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;

@end

NS_ASSUME_NONNULL_END
