//
//  SupereraSDKServerConfigurationManager.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKError.h>

#import "SupereraSDKSettings.h"
#import "SupereraSDKOnlineConfig.h"

typedef NS_ENUM(NSUInteger, SEServiceState) {
    SEServiceStateReady = 0,
    SEServiceStateProcessing,
    SEServiceStateSuccess,
    SEServiceStateFailed,
};

@interface SupereraSDKServerConfigurationManager : NSObject

@property (nonatomic, assign, class, readonly)SEServiceState onlineConfigState;

@property (nonatomic, assign, class, readonly) NSInteger sdkInitServerTime_ms;


/**
 向服务器请求在线参数

 @param settings settings
 @param successCallback 成功回调
 @param errorCallback 失败回调
 */
+ (void)fetchOnlineConfigWithSettings:(SupereraSDKSettings *)settings success:(void(^)(SupereraSDKOnlineConfig *onlineConfig))successCallback failure:(ErrorCallback)errorCallback;

@end
