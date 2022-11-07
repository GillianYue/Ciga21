//
//  SupereraSDKLogger.h
//  SupereraSDKAnalytics
//
//  Created by jodotech on 2019/4/19.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@class SupereraSDKLoggerConfiguration;

@interface SupereraSDKLogger : NSObject

@property (nonatomic, copy) NSString *CGI;
@property (nonatomic, copy, readonly) NSString *uploadURL;
@property (nonatomic, copy, readonly) NSString *serverTimeURL;

/**
 获取实例

 @return logger
 */
+(instancetype)sharedInstance;

/**
 初始化logger

 @param configuration 配置信息
 */
+ (instancetype)loggerWithConfiguration:(SupereraSDKLoggerConfiguration *)configuration;


/**
 发送启动日志
 */
- (void)onStart;

/**
 缓存一条日志随之后的日志一起发送，可用于Logger还未初始化的时候需要发送日志，临时解决方案，不稳定

 @param eventName 日志名称
 @param params 日志参数
 */
+ (void)cacheEventWithName:(NSString *)eventName params:(NSDictionary<NSString *,id> *)params;

/**
 发送自定义日志

 @param eventName 日志名称
 @param params 日志参数
 */
- (void)onEventWithName:(NSString *)eventName params:(NSDictionary<NSString *,id> *)params;

/**
 发送自定义日志
 
 不需要sdk初始化
 
 @param eventName       日志名称
 @param params              日志参数
 @param cgi                     cgi
 @param uploadHost     上报的服务器
 @param uploadPath     上报的流
 */
+ (void)onEventWithName:(NSString *)eventName
                 params:(NSDictionary<NSString *,id> * _Nullable)params
                    cgi:(NSString *)cgi
                    pn:(NSString *)pn
             uploadHost:(NSString *)uploadHost
             uploadPath:(NSString *)uploadPath;

/**
 设置全局参数，全局参数会添加到每个事件参数中

 @param params 参数
 */
+ (void)addGlobalParams:(NSDictionary<NSString *,id> *)params;

/**
 删除全局参数中的某个key
 
 @param key key
 */
+ (void)removeGlobalParam:(NSString *)key;

@end
