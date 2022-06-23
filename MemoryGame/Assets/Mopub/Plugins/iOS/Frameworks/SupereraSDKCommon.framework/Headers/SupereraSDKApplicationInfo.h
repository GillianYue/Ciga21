//
//  SupereraSDKApplicationInfo.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/19.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKApplicationInfo : NSObject


/**
 从 main bundle 的 info.plist 中获取字符串

 @param key key
 @return value
 */
+ (nullable NSString *)getMainInfoStringForKey:(NSString *)key;

/**
 从 main bundle 的 info.plist 中获取值
 
 @param key key
 @return value
 */
+ (id)getMainInfoValueForKey:(NSString *)key;

/**
 从 class 所在的 bundle 的 info.plist 中获取字符串

 @param key key
 @param clazz 指定的class
 @return value
 */
+ (nullable NSString *)getBundleInfoStringForKey:(NSString *)key bundleCalss:(Class)clazz;

+ (nullable NSDictionary *)getMainInfo;

/**
 获取指定class所在bundle的shortVersion
 
 @param clazz class
 @return shortVersion
 */
+ (nullable NSString *)getBundleVersion:(Class)clazz;

/**
 获取app包名
 
 @return 包名
 */
+ (nullable NSString *)appBundleID;

/**
 获取app shortVersion
 
 @return shortVersion
 */
+ (nullable NSString *)appBundleShortVersion;


/**
 将 shortVersion 转换为int

 @return int形式version
 */
+ (int)appBundleVersionCode;

/**
 获取app build
 
 @return build
 */
+ (nullable NSString *)appBuildVersion;

/**
 应用商ID
 
 @return IDFV
 */
+ (nullable NSString *)idfv;

/**
 广告ID
 
 @return IDVA
 */
+ (NSString *)idfa;

/**
 获取设备ID
 
 @return deviceID
 */
+ (NSString *)deviceID;

+ (void)resetDeviceIDToRandom;


/**
 读取一个内容为json的文件

 @param fileName 文件名
 @param subPath 相对于沙河的目录
 */
+ (nullable NSDictionary *)readJSONFileName:(NSString *)fileName subPath:(nullable NSString *)subPath;

/**
 读取一个json资源文件，会分别从沙河根目录和指定子目录读取

 @return file
 */
+ (nullable NSDictionary *)readJSONFileName:(NSString *)fileName inRootORSubPath:(nullable NSString *)subPath;
@end

NS_ASSUME_NONNULL_END
