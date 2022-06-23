//
//  SupereraSDKLoginResult.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/4/29.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKCommon.h>
#import "SupereraSDKAccessToken.h"


/**
 登录请求结果
 */
@interface SupereraSDKLoginResult : NSObject <JSONSerializable>

/**
 当前帐号票据
 */
@property (nonatomic, strong) SupereraSDKAccessToken* accessToken;

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;
@end
