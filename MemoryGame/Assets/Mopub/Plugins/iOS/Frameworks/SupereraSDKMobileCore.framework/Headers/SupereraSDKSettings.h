//
//  SupereraSDKSettings.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupereraSDKConfiguration.h"

NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKSettings : NSObject

@property (nonatomic, strong) SupereraSDKConfiguration *SDKConfiguration;
@property (nonatomic, copy, readonly) NSString *gameID;
@property (nonatomic, copy, readonly) NSString *deviceID;
@property (nonatomic, copy, readonly) NSString *packageName;
@property (nonatomic, copy, nullable) NSString *appID;

/**
 发行方
 */
@property (nonatomic, copy, readonly) NSString *publisher;

/**
 分发者 GOOGLE_PLAY / APPLE_SPP_STORE / COMMUNITY
 */
@property (nonatomic, copy, readonly) NSString *distributor;

/**
 游戏用户唯一标识
 */
@property (nonatomic, copy) NSString *CPUserID;

@property (nonatomic, assign) BOOL seAttribute;

- (instancetype)initWithGameID:(NSString *)gameID deviceID:(NSString *)deviceID packageName:(NSString *)packageName publisher:(NSString *)publisher distributor:(NSString *)distributor SDKConfiguration:(nullable SupereraSDKConfiguration *)SDKConfiguration;

@end

NS_ASSUME_NONNULL_END
