//
//  SupereraSDKConfiguration.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/24.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

//SDK配置，用于SDK初始化时获取CP相关的配置参数
@interface SupereraSDKConfiguration : NSObject


/**
 游戏内容版本号
 */
@property (nonatomic, copy, nullable) NSString *gameContentVersion;

/**
 广告分路条件
 */
@property (nonatomic, copy, nullable) NSDictionary *adRouteConditions;

/**
 是否测试环境
 */
@property (nonatomic, assign) BOOL isTestEnvironment;

/**
 服务器归因是否延迟到实名认证
 */
@property (nonatomic, assign) BOOL attributeAfterRealname;

/**
 是否在归因之后才初始化广告
 
 默认为 false
 */
@property (nonatomic, assign) BOOL adInitAfterAttribute;

@end
