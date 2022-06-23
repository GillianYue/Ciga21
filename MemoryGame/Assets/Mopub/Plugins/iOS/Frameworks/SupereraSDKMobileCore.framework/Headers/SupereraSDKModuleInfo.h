//
//  SupereraSDKModule.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
NS_ASSUME_NONNULL_BEGIN

#define SDKModule SupereraSDKModuleInfo

/// typedef for SupereraSDKModuleName
typedef NSString * SupereraSDKModuleName NS_TYPED_EXTENSIBLE_ENUM;

///表示游戏
FOUNDATION_EXPORT SupereraSDKModuleName SupereraSDKModuleGame;
///表示SDK
FOUNDATION_EXPORT SupereraSDKModuleName SupereraSDKModuleSDK;
///表示SDKCore
FOUNDATION_EXPORT SupereraSDKModuleName SupereraSDKModuleSDKCore;
///表示SDKAuthCore
FOUNDATION_EXPORT SupereraSDKModuleName SupereraSDKModuleSDKAuthCore;

@interface SupereraSDKModuleInfo : NSObject

@property (nullable, nonatomic, copy) SupereraSDKModuleName module;
@property (nullable,nonatomic, copy) NSString *subModule;

/**
 创建一个module信息
 
 @param module mudule名称
 @param subModule subModule 名称
 @return module
 */
+ (instancetype)moduleName:(nullable SupereraSDKModuleName)module subModule:(nullable NSString *)subModule;

+ (instancetype)moduleName:(nullable SupereraSDKModuleName)module;

/**
 创建一个module信息

 @param module mudule名称
 @param subModule subModule 名称
 @return module
 */
- (instancetype)initWithModule:(nullable SupereraSDKModuleName)module subModule:(nullable NSString *)subModule;

@end

NS_ASSUME_NONNULL_END
