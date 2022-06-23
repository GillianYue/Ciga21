//
//  FetchLinkedAccountResult.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/4/29.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupereraSDKLinkedAccounts.h"

NS_ASSUME_NONNULL_BEGIN

/**
 获取当先登录帐号所绑定的第三方帐号信息结果
 */
@interface FetchLinkedAccountResult : NSObject

/**
 已绑定的登录类型及其对应帐号
 */
@property (nonatomic, strong) SupereraSDKLinkedAccounts *linkedAccounts;

@end

NS_ASSUME_NONNULL_END
