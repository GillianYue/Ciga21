//
//  SupereraSDKOnlineParams.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/MapSerializable.h>
#import "SupereraSDKGameConfiguration.h"


NS_ASSUME_NONNULL_BEGIN

//SDK在线配置
@interface SupereraSDKOnlineConfig : NSObject<MapSerializable>

/**
 游戏的在线参数
 */
@property (nonatomic, strong) SupereraSDKGameConfiguration *gameConfig;

/**
 根据模块名称获取在线参数的配置
 */
- (nullable NSString *)getOpenParamWithModule:(NSString*)moduleName key:(NSString*)keyName;

- (nullable NSDictionary *)getOpenParamsWithModule:(NSString *)moduleName key:(NSString *)keyname;

- (nullable NSString *)getOpenParamWithKey:(NSString *)keyName;

@end

NS_ASSUME_NONNULL_END
