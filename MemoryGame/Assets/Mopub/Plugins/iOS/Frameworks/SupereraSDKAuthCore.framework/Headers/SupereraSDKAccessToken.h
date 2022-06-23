//
//  SupereraSDKAccessToken.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/4/28.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKCommon.h>
#import "SupereraSDKLinkedAccounts.h"

NS_ASSUME_NONNULL_BEGIN

/**
 SDK用户的登录票据及相关用户信息
 */
@interface SupereraSDKAccessToken : NSObject<NSSecureCoding, JSONSerializable, MapSerializable>

/**
 帐号 ID，用户唯一标识
 */
@property (nonatomic, assign) NSInteger accountID;

/**
 当前登录账号昵称
 */
@property (nonatomic, copy, readonly) NSString *currentNickName;

/**
 当前登录账号头像
 */
@property (nonatomic, copy, readonly) NSString *picture;

/**
 当前的登录 token
 */
@property (nonatomic, copy) NSString *sessionToken;

/**
 最近一次登录使用的登录方式
 */
@property (nonatomic, assign) SupereraSDKAccountType lastLoginType;

/**
 最近一次登录成功的时间
 */
@property (nonatomic, strong) NSDate *lastLoginTimestamp;

/**
 是否新建的账号
 */
@property (nonatomic, assign) BOOL isNewAccount;

/**
 已绑定的登录类型及其对应帐号，如果为空，请通过 SupereraSDKLoginManager 异步获取
 */
@property (nullable, nonatomic, strong) SupereraSDKLinkedAccounts *linkedAccounts;

/**
 当前/最近一次的登录 token
 */
@property (nullable, nonatomic, strong, readonly, class) SupereraSDKAccessToken *currentAccessToken;

- (instancetype)initWithID:(NSInteger)accountID token:(NSString *)token type:(SupereraSDKAccountType)type loginTimestamp:(NSDate *)loginTimestamp;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;

- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;
@end

NS_ASSUME_NONNULL_END
