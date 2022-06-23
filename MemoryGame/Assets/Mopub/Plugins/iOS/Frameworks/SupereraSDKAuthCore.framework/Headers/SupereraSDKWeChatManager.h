//
//  SupereraSDKWeChatManager.h
//  SupereraSDKAuthCore
//
//  Created by mac on 2020/7/30.
//  Copyright © 2020 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <SupereraSDKCommon/SupereraSDKError.h>

NS_ASSUME_NONNULL_BEGIN

@class SupereraSDKWeChatPlayer;

@interface SupereraSDKWeChatManager : NSObject

/**
 登录调用记数，用于判断现在是否有在等待返回的登录请求
 */
@property (nonatomic, assign) int loginRequestCount;

@property (nonatomic, strong) void (^internalCallback)(SupereraSDKWeChatPlayer *player);

@property (nonatomic, strong) void (^internalErrCallback)(SupereraSDKError *error);

+ (instancetype)sharedInstance;

-(void)startWeChatLogin:(void(^)(SupereraSDKWeChatPlayer *player))successCallback failure:(ErrorCallback)errorCallback;

@end

NS_ASSUME_NONNULL_END
