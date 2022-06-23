//
//  SupereraSDKKeychain.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/19.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
NS_ASSUME_NONNULL_BEGIN

///调用allKeys获取的数组里元素为一个个字典，每个字典中的acct的value为实际的key
static NSString *KEY_NAME_ALL = @"acct";

@interface SupereraSDKKeychain : NSObject

/**
 获取keychain中所有的key

 @return 所有的key
 */
+ (nullable NSArray *)allKeys;


/**
 获取指定域下所有key

 @param serviceName 域名
 @return key
 */
+ (nullable NSArray *)allKeysForService:(NSString *)serviceName;

/**
 获取默认域名下的指定string value

 @param key key
 @return string value
 */
+ (nullable NSString *)valueForKey:(NSString *)key;

/**
 获取默认域名下的指定data value
 
 @param key key
 @return data value
 */
+ (nullable NSData *)valueDataForKey:(NSString *)key;


/**
 获取指定域下的指定string value

 @param serviceName 域名
 @param key key
 @return string value
 */
+ (nullable NSString *)valueForService:(NSString *)serviceName key:(NSString *)key;

/**
 获取指定域下的指定data value
 
 @param serviceName 域名
 @param key key
 @return data value
 */
+ (nullable NSData *)valueDataForService:(NSString *)serviceName key:(NSString *)key;


/**
 删除默认域名下的指定value

 @param key key
 @return 结果
 */
+ (BOOL)deleteValueForKey:(NSString *)key;


/**
 删除指定域名下的指定value

 @param serviceName 域名
 @param key key
 @return 结果
 */
+ (BOOL)deleteValueForService:(NSString *)serviceName key:(NSString *)key;


/**
 向默认域名添加value

 @param value value
 @param key key
 @return 结果
 */
+ (BOOL)setValue:(NSString *)value forKey:(NSString *)key;

/**
  向指定域名添加value

 @param value value
 @param serviceName 域名
 @param key key
 @return 结果
 */
+ (BOOL)setValue:(NSString *)value forService:(NSString *)serviceName key:(NSString *)key error:(NSError **)error;

/**
 向默认域名添加data
 
 @param data data
 @param key key
 @return 结果
 */
+ (BOOL)setData:(NSData *)data forKey:(NSString *)key;

/**
 向指定域名添加data
 
 @param data data
 @param serviceName 域名
 @param key key
 @return 结果
 */
+ (BOOL)setData:(NSData *)data forService:(NSString *)serviceName key:(NSString *)key error:(NSError **)error;

@end


NS_ASSUME_NONNULL_END
