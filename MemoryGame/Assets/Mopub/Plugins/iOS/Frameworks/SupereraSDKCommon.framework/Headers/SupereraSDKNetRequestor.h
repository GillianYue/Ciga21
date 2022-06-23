//
//  SupereraNetRequestor.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/11.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSUInteger, SupereraReqSignWay) {
    SupereraReqSignWayNone = 0,
    SupereraReqSignWaySortSaltMD5,
    SupereraReqSignWaySortSHA256,
};

/// typedef for FBSDKHTTPMethod
typedef NSString * SupereraHTTPMethod NS_TYPED_EXTENSIBLE_ENUM;
///body为querry格式的post
FOUNDATION_EXPORT SupereraHTTPMethod SupereraHTTPMethodPOST;
///GET
FOUNDATION_EXPORT SupereraHTTPMethod SupereraHTTPMethodGET;
///参数中的"body"参数将添加到请求体中，其他参数在url中,content-type 为josn
FOUNDATION_EXPORT SupereraHTTPMethod SupereraHTTPMethodJSON_BODY;
///参数中的"body"参数将添加到请求体中，无其他参数，content-type 为默认
FOUNDATION_EXPORT SupereraHTTPMethod SupereraHTTPMethodPOST_BODY;
///body为json格式的post
FOUNDATION_EXPORT SupereraHTTPMethod SupereraHTTPMethodPOST_JSON;

@interface SupereraSDKNetRequestor : NSMutableURLRequest

+ (void)addGlobalHeader: (NSDictionary<NSString *, NSString *> *)headers;

+ (void)removeGlobalHeader:(NSString *)header;

+ (void)setDefaultSignKey:(NSString *)key;

/**
 创建一条随机的requestID，标识一个请求过程

 @return requestID
 */
+ (NSString *)createRequestID;

@property (nonatomic, copy, nullable) NSString *requestID;

- (instancetype)initWithURL:(NSURL *)URL cachePolicy:(NSURLRequestCachePolicy)cachePolicy timeoutInterval:(NSTimeInterval)timeoutInterval;

/**
 URL, params, requestorId

 @param URL URL
 @param params 参数
 @param requestorId 用于记录前后文的id
 @return request
 */
- (instancetype)initWithURL:(NSURL *)URL params:(NSDictionary<NSString*,id> *)params requestorId:(NSString *)requestorId;

/**
 URL, params, signWay, requestorId
 
 @param URL URL
 @param params 参数
 @param signWay 签名方式
 @param requestorId 用于记录前后文的id
 @return request
 */
- (instancetype)initWithURL:(NSURL *)URL params:(NSDictionary<NSString*,id> *)params signWay:(SupereraReqSignWay)signWay requestorId:(NSString *)requestorId;


/**
 URL, params, HTTPMethod, signWay, addCommonHeader, requestorId

 @param URL URL
 @param params 参数
 @param method 请求方式
 @param signWay 签名方式
 @param addCommonHeader 是否添加通用header
 @param requestorId 用于记录前后文的id
 @return request
 */
- (instancetype)initWithURL:(NSURL *)URL params:(NSDictionary<NSString*,id> *)params HTTPMethod:(SupereraHTTPMethod)method signWay:(SupereraReqSignWay)signWay addCommonHeader:(BOOL)addCommonHeader requestorId:(NSString *)requestorId;


/**
 URL, params, HTTPMethod, signWay, addCommonHeader, isUseGzip, requestorId

 @param URL URL
 @param params 参数
 @param method 请求方式
 @param signWay 签名方式
 @param addCommonHeader 是否添加通用header
 @param isUseGzip 是否压缩
 @param requestorId 用于记录前后文的id
 @return request
 */
- (instancetype)initWithURL:(NSURL *)URL params:(NSDictionary<NSString*,id> *)params HTTPMethod:(SupereraHTTPMethod)method signWay:(SupereraReqSignWay)signWay addCommonHeader:(BOOL)addCommonHeader gzip:(BOOL)isUseGzip requestorId:(NSString *)requestorId;


/**
 URL, params, HTTPMethod, signWay, signKey, addCommonHeader, isUseGzip, timeoutInterval

 @param URL URL
 @param params 参数
 @param method 请求方式
 @param signWay 签名方式
 @param addCommonHeader 是否添加通用header
 @param isUseGzip 是否压缩
 @param timeoutInterval 超时时间
 @return request
 */
- (instancetype)initWithURL:(NSURL *)URL params:(NSDictionary<NSString*,id> *)params HTTPMethod:(SupereraHTTPMethod)method signWay:(SupereraReqSignWay)signWay signKey:(NSString *)signKey addCommonHeader:(BOOL)addCommonHeader gzip:(BOOL)isUseGzip timeoutInterval:(NSTimeInterval)timeoutInterval;
@end




