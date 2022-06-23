//
//  SupereraSDKDeviceInfo.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/19.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface SupereraSDKDeviceInfo : NSObject


/**
 获取地区信息 格式：languageCode-countryCode

 @return languageCode-countryCode
 */
+ (NSString *)getLocale;


/**
 设备总存储容量

 @return 字节
 */
+ (long long)getDeviceStorage;


/**
 设备可用容量

 @return 字节
 */
+ (long long)getDeviceFreeStorage;


/**
 获取一系列设备信息的JSON

 @return JSON
 */
+ (NSString *)deviceInfoJSON;


/**
 获取系统版本

 @return 系统版本
 */
+ (NSString *)getSystemVersion;

/**
 获取设备型号

 @param rawValue 原始值还是可读值
 @return 型号
 */
+ (NSString *)getDeviceVersionWithRaw:(BOOL)rawValue;

+ (NSString *)getCPUType;
@end
