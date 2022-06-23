//
//  SupereraSDKLinkedAccount.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/4/28.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKCommon.h>
#import "SupereraSDKThirdPartyAccount.h"


NS_ASSUME_NONNULL_BEGIN


/**
 已经绑定的其他登录方式（其他登录方式对应的第三方帐号）
 */
@interface SupereraSDKLinkedAccounts : NSObject<NSSecureCoding, JSONSerializable,MapSerializable>

/**
 已绑定的第三方帐号
 */
@property (nonatomic, copy, readonly, nullable) NSArray<SupereraSDKThirdPartyAccount *> *accounts;

/**
 获取指定类型的帐号，如果返回空，则说明未绑定该类型

 @return 已绑定的帐号信息
 */
- (nullable SupereraSDKThirdPartyAccount *)getAccountWithType:(SupereraSDKAccountType)type;


/**
 使用JSON Array 初始化

 @param accounts 元素为 account 类型字典的数组
 @return account
 */
- (nullable instancetype)initWithAccountsArray:(NSArray *)accounts;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;

- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;

@end

NS_ASSUME_NONNULL_END
