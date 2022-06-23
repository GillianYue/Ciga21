//
//  SupereraSDKLoggerConfiguration.h
//  SupereraSDKAnalytics
//
//  Created by jodotech on 2019/4/19.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKLoggerConfiguration : NSObject

@property (nonatomic, copy) NSString *CGI;
@property (nonatomic, copy, nullable) NSString *deviceID;
@property (nonatomic, copy, nullable) NSString *analyticsHOST;
@property (nonatomic, assign) BOOL isZIP;
@property (nonatomic, copy) NSString *uploadPath;
@property (nonatomic, assign) BOOL isLogSDKEvent;


/**
 创建一个config

 @param CGI CGI
 @param deviceID 设备唯一ID
 @param analyticsHOST 上报HOST
 @return configuration
 */
- (instancetype)initWithCGI:(NSString *)CGI deviceID:(nullable NSString *)deviceID analyticsHOST:(nullable NSString *)analyticsHOST;

@end

NS_ASSUME_NONNULL_END
