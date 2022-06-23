//
//  SupereraSDKLoginRequest.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/4/29.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 登录验证请求相关的参数，需要CP设置的
 */
@interface SupereraSDKLoginRequest : NSObject

/**
 当前登录方式不存在对应帐号时，是否创建新帐号
 */
@property (nonatomic, assign) BOOL createAccount;

/**
 激活码
 */
@property (nonatomic, copy) NSString *activeCode;

@end

NS_ASSUME_NONNULL_END
