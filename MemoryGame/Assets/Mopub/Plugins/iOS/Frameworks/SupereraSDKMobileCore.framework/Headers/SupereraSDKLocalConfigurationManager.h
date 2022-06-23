//
//  SupereraSDKLocalConfigurationManager.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
NS_ASSUME_NONNULL_BEGIN

@class SupereraSDKLogger, SupereraSDKSettings;

///负责本地初始化
@interface SupereraSDKLocalConfigurationManager : NSObject


/**
 加载本地各项配置

 @return SupereraSDKSettings
 */
+ (SupereraSDKSettings *)loadLocalConfigration;

/**
 读取 info.plist 中的 gameID

 @return CGI
 */
+ (nullable NSString *)getInfoGameID;


/**
 获取设备ID

 @return 设备ID
 */
+ (NSString *)getDeviceID;


/**
 获取包名

 @return 包名
 */
+ (NSString *)getPackageName;

/**
 设置广告分路条件
 用于初始化广告sdk
 */
+ (void)setAdRouteConditions:(NSDictionary *)conditions;


/**
 初始化 SDKLogger

 @return logger
 */
+ (nullable SupereraSDKLogger *)initSDKLogger:(SupereraSDKSettings *)settings;


/**
 上报启动日志

 */
+ (void)logLaunchWithSettings:(SupereraSDKSettings *)settings ;

/**
 读取包info配置的string

 @param key key
 @param alertWhileNil 如果为空是否弹出alert
 @return string
 */
+ (nullable NSString *)getMainInfoStringForKey:(NSString *)key alertWhileNil:(BOOL)alertWhileNil;
@end
   
NS_ASSUME_NONNULL_END
