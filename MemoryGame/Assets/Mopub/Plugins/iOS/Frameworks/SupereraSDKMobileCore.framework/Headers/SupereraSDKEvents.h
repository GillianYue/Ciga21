//
//  SupereraSDKEvents.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKError.h>
#import "SupereraSDKModuleInfo.h"

#define SDKEvents SupereraSDKEvents

NS_ASSUME_NONNULL_BEGIN

@class SupereraSDKModuleInfo, SupereraSDKError;

/// typedef for SupereraSDKLogType
typedef NSString * SupereraSDKLogType NS_TYPED_EXTENSIBLE_ENUM;

///表示 info 类型日志
FOUNDATION_EXPORT SupereraSDKLogType SupereraSDKLogTypeInfo;
///表示 error 类型日志
FOUNDATION_EXPORT SupereraSDKLogType SupereraSDKLogTypeError;
///表示 warning 类型日志
FOUNDATION_EXPORT SupereraSDKLogType SupereraSDKLogTypeWarning;

@interface SupereraSDKEvents : NSObject

/**
 是否上报 SDK 日志
 */
+ (void)openLogSDKEvent:(BOOL)open;

/**
 发送自定义日志

 @param eventName 日志名称
 @param params 日志参数
 */
+ (void)logCustomEvent:(NSString *)eventName params:(nullable NSDictionary *)params;
+ (void)logCustomEvent:(NSString *)eventName jsonParams:(nullable NSString *)jsonParams;

/**
 发送 SDK 日志
 
 @param eventName 日志名称
 @param params 日志参数
 @param module 所属模块
 @param type 日志类型
 */
+ (void)logSDKEvent:(NSString *)eventName params:(nullable NSDictionary *)params module:(nullable SupereraSDKModuleInfo *)module type:(SupereraSDKLogType)type;

/**
 发送 info 类型 SDK 日志
 
 @param eventName 日志名称
 @param params 日志参数
 @param module 所属模块
 */
+ (void)logSDKInfo:(NSString *)eventName params:(nullable NSDictionary *)params module:(nullable SupereraSDKModuleInfo *)module;

/**
 发送 error 类型 SDK 日志

 @param errorName 错误名称
 @param params 错误参数
 @param error 错误
 @param module 所属模块
 */
+ (void)logSDKError:(NSString *)errorName params:(nullable NSDictionary *)params error:(SupereraSDKError *)error module:(nullable SupereraSDKModuleInfo *)module;

/**
 发送 warning 类型 SDK 日志
 
 @param errorName 错误名称
 @param params 错误参数
 @param error 错误
 @param module 所属模块
 */
+ (void)logSDKWarning:(NSString *)errorName params:(nullable NSDictionary *)params error:(nullable SupereraSDKError *)error module:(nullable SupereraSDKModuleInfo *)module;


/**
 可变参数

 */
+ (void)logSDKEV:(NSString *)ev error:(nullable SupereraSDKError *)error module:(nullable NSString *)module type:(nullable SupereraSDKLogType)type line:(int)line time:(nullable NSString *)time file:(nullable NSString *)file method:(nullable NSString *)method args:(nullable id)firstArg, ... NS_REQUIRES_NIL_TERMINATION;

/**
 字典参数
 
 */
+ (void)logSDKEV:(NSString *)ev error:(nullable SupereraSDKError *)error module:(nullable NSString *)module type:(nullable SupereraSDKLogType)type line:(int)line time:(nullable NSString *)time file:(nullable NSString *)file method:(nullable NSString *)method params:(nullable NSDictionary *)params;

#pragma -mark game log

/// 上报玩家信息，只要游戏具有以下任意参数，就需要上报
/// @param characterName 角色名
/// @param characterID 角色ID
/// @param characterLevel 角色等级
/// @param serverName 服务器名
/// @param serverID 服务器ID
+ (void)logPlayerInfoWithCahrName:(nullable NSString *)characterName characterID:(nullable NSString *)characterID characterLevel:(int)characterLevel serverName:(nullable NSString *)serverName serverID:(nullable NSString *)serverID;


/// 记录关卡开始。内部缓存一个对应该levelName的当前时间戳（开始时间）
/// @param levelName 关卡名称
+ (void)logStartLevelWithName:(NSString *)levelName;

/// 记录完成关卡（关卡名）。内部拿当前时间减去开始时间，得到完成时间
/// @param levelName 关卡名称
+ (void)logFinishLevelWithName:(NSString *)levelName;

/// 记录解锁关卡
/// @param levelName 关卡名称
+ (void)logUnlockLevelWithName:(NSString *)levelName;

/// 记录跳过关卡
/// @param levelName 关卡名称
+ (void)logSkipLevelWithName:(NSString *)levelName;

/// 记录皮肤被使用
/// @param skinName 皮肤名称
+ (void)logSkinUsedWithName:(NSString *)skinName;

/// 记录某个用于展示广告的入口被展示给玩家
/// @param name 入口名
+ (void)logAdEntrancePresentWithName:(NSString *)name;
@end

NS_ASSUME_NONNULL_END
