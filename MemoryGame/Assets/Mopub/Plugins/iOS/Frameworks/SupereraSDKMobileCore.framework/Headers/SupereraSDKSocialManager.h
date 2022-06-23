//
//  SupereraSDKSocialManager.h
//  SupereraSDKMobileCore
//
//  Created by qus on 2019/7/31.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN


@interface SupereraSDKSocialManager : NSObject

/**
 获取 SDKCore 实例
 
 @return 实例
 */
+ (instancetype)sharedInstance;

/**
  打开（跳转）加群组链接 从在线参数中获取链接并跳转

 @return 跳转结果
 */
- (BOOL)openJoinChatGroup;

/**
 open and join whatsapp chatting
 */
- (BOOL)openJoinWhatsappChatting;

/**
 打开用户协议
 */
- (void)openUserProxy;

/**
 打开隐私协议
 */
- (void)openPrivateProxy;

@end

NS_ASSUME_NONNULL_END
