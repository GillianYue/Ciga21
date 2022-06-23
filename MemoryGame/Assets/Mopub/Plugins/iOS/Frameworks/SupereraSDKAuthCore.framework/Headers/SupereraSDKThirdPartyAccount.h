//
//  SupereraSDKAccount.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/4/28.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKCommon.h>
NS_ASSUME_NONNULL_BEGIN


/**
 帐号类型
 */
typedef NS_ENUM(NSUInteger, SupereraSDKAccountType) {
    ///未知类型
    SupereraSDKAccountTypeUnknown,
    ///设备类型
    SupereraSDKAccountTypeDevice,
    ///GameCenter类型
    SupereraSDKAccountTypeGameCenter,
    //GooglePlayGame类型
    SupereraSDKAccountTypeGooglePlay,
    ///微信类型
    SupereraSDKAccountTypeWeChat,
    ///facebook类型
    SupereraSDKAccountTypeFacebook,
    ///手机类型
    SupereraSDKAccountTypeMobile,
    ///邮箱类型
    SupereraSDKAccountTypeEmail,
    ///游客类型
    SupereraSDKAccountTypeVisitor,
    ///手机一键登录
    SupereraSDKAccountTypeMobileATAuth,
    ///Apple登录
    SupereraSDKAccountTypeApple,
    
};

/**
 第三方帐号数据
 */
@interface SupereraSDKThirdPartyAccount : NSObject<NSSecureCoding,MapSerializable ,JSONSerializable>

/**
 帐号类型（该帐号对应的登录方式）
 */
@property (nonatomic, assign) SupereraSDKAccountType accountType;

- (NSString *)getNickname;

- (NSString *)getAccountID;

- (NSString *)getAccountTypeString;

- (NSString *)getPicture;


- (instancetype)initWithType:(SupereraSDKAccountType)type;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;

- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;
@end

NS_ASSUME_NONNULL_END
