//
//  SupereraSDKNetwork.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/15.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupereraSDKNetRequestor.h"
#import "SupereraSDKNetResponse.h"
#import "SupereraSDKNetResponseState.h"

NS_ASSUME_NONNULL_BEGIN

@class SupereraSDKNetResponse, SupereraSDKNetRequestor;


/**
 网络请求回调

 @param response SupereraSDKNetResponse
 */
typedef void(^NetRequestCallback)(SupereraSDKNetResponse *response, NSInteger taskID);

@interface SupereraSDKNetwork : NSObject


+ (instancetype)sharedInstance;

/**
 设置超时时间，注意需要在sharedInstance之前调用

 @param interval 时间
 @return 是否设置成功
 */
+ (BOOL)setTimeout:(NSTimeInterval)interval;

/**
 发起一次网路请求
 
 @param request request
 @param successCallback 成功回调
 @param errorCallback 失败回调
 @return taskID，用于取消请求
 */
- (NSInteger)netRequest:(SupereraSDKNetRequestor *)request success:(NetRequestCallback)successCallback failure:(NetRequestCallback)errorCallback;

/**
 发起可自动重试的网络请求， 目前当httpStautCode=500或超时自动重试
 
 @param request request
 @param retryTime 重试次数 0为不重试
 @param retryInterval 重试时间间隔
 @param successCallback 成功回调
 @param errorCallback 失败回调
 @return taskID，用于取消请求
 */
- (NSInteger)netRequest:(SupereraSDKNetRequestor *)request retryTime:(int)retryTime retryInterval:(NSTimeInterval)retryInterval success:(NetRequestCallback)successCallback failure:(NetRequestCallback)errorCallback;
NS_ASSUME_NONNULL_END
@end
