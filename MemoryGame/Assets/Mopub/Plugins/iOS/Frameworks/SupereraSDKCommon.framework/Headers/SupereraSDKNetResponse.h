//
//  SupereraSDKNetResponse.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/15.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupereraSDKNetRequestor.h"

typedef NS_ENUM(NSUInteger, SupereraSDKNetVerifyStatus) {
    ///成功且服务器code==0
    SupereraSDKNetVerifyStatusSuccess = 0,
    SupereraSDKNetVerifyStatusUnknown,
    ///返回的NSData为空或者不是json字典格式
    SupereraSDKNetVerifyStatusErrorData,
    ///返回的服务器code结果不是0
    SupereraSDKNetVerifyStatusErrorServerCode,
    ///返回的NSData字典中没有data或者state字典
    SupereraSDKNetVerifyStatusErrorDataForm,
    ///httpCode非200
    SupereraSDKNetVerifyStatusErrorHTTPCode,
    ///网络请求失败
    SupereraSDKNetVerifyStatusErrorNet,
};

@class SupereraSDKNetResponseState;

@interface SupereraSDKNetResponse : NSObject
NS_ASSUME_NONNULL_BEGIN


/**
 对服务器返回的校验结果
 */
@property (nonatomic, assign) SupereraSDKNetVerifyStatus verifyStatus;

/**
 服务器返回的state
 */
@property (nonatomic, strong, nullable) SupereraSDKNetResponseState *state;
///请求返回的未处理过的Data
@property (nonatomic, strong, nullable) NSData *rawData;
@property (nonatomic, strong, nullable, readonly) NSString *rawDataString;
///如果是格式正确的返回，字典中有data，则该变量为data，否则为整个请求返回结果
@property (nonatomic, strong, nullable) NSMutableDictionary *dicData;
///返回的json字典中是否有"data"
@property (nonatomic, assign) BOOL haveData;
///httpResponseCode
@property (nonatomic, assign) NSInteger HTTPCode;
///系统错误对象
@property (nonatomic, strong, nullable) NSError *error;
///请求耗时，仅在iOS 10 以上可用，毫秒
@property (nonatomic, assign) double taskDuration;
@property (nonatomic, readonly) NSString *taskDurationString;

@property (nonatomic, strong, nullable) NSURL *URL;

@property (nonatomic, copy) NSString *sign;

/**
 网络请求结果处理
 
 @param data 网络请求返回的data
 @param response 网络请求返回的response
 @param taskDuration 网络请求耗时
 @return response
 */
- (instancetype)initWithData:(NSData *)data response:(nullable NSURLResponse *)response taskDuration:(NSInteger)taskDuration;

/**
 网络请求直接失败时使用
 
 @param error error
 @param response response
 @param taskDuration 耗时
 @return superpera response
 */
- (instancetype)initWithError:(nullable NSError *)error response:(nullable NSURLResponse *)response taskDuration:(double)taskDuration;

//校验签名
- (BOOL)verifySignWithSignWay:(SupereraReqSignWay)signWay key:(nullable NSString *)Key;

/**
 如果改请求不是请求SDK标准接口，需要直接转为成功
 */
- (void)makeResponseSucces;
NS_ASSUME_NONNULL_END
@end
