//
//  SupereraSDKPaymentManager.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2019/5/29.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SupereraSDKCommon/SupereraSDKError.h>
#import "SupereraSDKPaymentItemDetails.h"
#import "SupereraSDKPaymentInfo.h"
#import "SupereraSDKUnconsumedItemUpdatedDelegate.h"
#import "SupereraSDKSubscriptionItemUpdatedDelegate.h"
#import "RestoreSubscriptionDelegate.h"

NS_ASSUME_NONNULL_BEGIN

@class SupereraSDKPaymentManager;


/**
 支付结果回调
 */
@protocol SupereraSDKPaymentDelegate <NSObject>

@optional

/**
 支付已经发起，正在进行中

 @param paymentInfo 支付信息
 */
- (void)paymentProcessingWithPaymentInfo:(SupereraSDKPaymentInfo *)paymentInfo;

/**
 支付未发起，已经失败

 @param paymentInfo 支付信息
 @param error 错误信息
 */
- (void)paymentFailedWithPaymentInfo:(SupereraSDKPaymentInfo *)paymentInfo error:(SupereraSDKError *)error;

@end


/**
 获取所有商品详情回调

 @param paymentItems 商品详情集合
 */
typedef void(^PaymentItemsCallback)(NSArray<SupereraSDKPaymentItemDetails *> *paymentItems);


@interface SupereraSDKPaymentManager : NSObject

+ (instancetype)sharedInstance;

/**
 设置已购的未消耗物品更新监听，当产生新的未消耗的已购商品时会回调，请在回调中发货
 
 @param listener 监听对象
 @param characterID 角色ID，如果没有角色ID，SDK将默认使用用户ID
 */
- (void)setUnconsumedItemUpdatedListener:(id<SupereraSDKUnconsumedItemUpdatedDelegate>)listener characterID:(nullable NSString *)characterID;

/**
 设置已购的订阅物品更新监听，当产生新的已购订阅商品时会回调，请在回调中开启订阅内容
 
 @param listener 监听对象
 @param characterID 角色ID，如果没有角色ID，SDK将默认使用用户ID
 */
- (void)setSubscriptionItemUpdatedListener:(id<SupereraSDKSubscriptionItemUpdatedDelegate>)listener characterID:(nullable NSString *)characterID;

/**
 获取品项价格列表
 
 @param itemIDs 请求的品项ID数组，若为空则返回全部品项
 @param success 成功回调，返回请求请求品项对象数组
 @param failure 失败回调，返回错误对象
 */
- (void)fetchPaymentItemDetails:(nullable NSArray<NSString *> *)itemIDs success:(nullable PaymentItemsCallback)success failure:(nullable ErrorCallback)failure;

/**
 发起支付

 @param paymentInfo 支付信息
 @param paymentCallback 结果监听
 */
- (void)startPaymentWithPaymentInfo:(SupereraSDKPaymentInfo *)paymentInfo callback:(id<SupereraSDKPaymentDelegate>)paymentCallback;

/**
 消耗已购买品项
 
 @param sdkOrderID 购买品项对应的游戏订单号
 */
- (void)consumePurchasedItemWithSDKOrderID:(NSString *)sdkOrderID;


/**
 获取当前登录用户下指定角色ID名下的所有未消耗已购商品

 @param characterID 角色ID，如果没有角色ID，默认使用用户ID作为角色ID
 @param resultCallback 结果回调
 */
- (void)fetchUnconsumedPurchasedItemsWithCharacterID:(nullable NSString *)characterID result:(void(^)( NSArray<SupereraSDKPurchasedItem *> * _Nullable unconsumedPurchasedItems))resultCallback;

/**
 获取当前登录用户下指定角色ID名下的所有已购商品

 @param characterID 角色ID，如果没有角色ID，默认使用用户ID作为角色ID
 @param resultCallback 结果回调
 */
- (void)fetchAllPurchasedItemsWithCharacterID:(nullable NSString *)characterID result:(void(^)( NSArray<SupereraSDKPurchasedItem *> * _Nullable allPurchasedItems))resultCallback;

/**
 获取当前登录用户下指定角色ID名下的所有已购订阅商品

 @param characterID 角色ID，如果没有角色ID，默认使用用户ID作为角色ID
 @param resultCallback 结果回调
 */
- (void)fetchAllPurchasedSubscriptionItemsWithCharacterID:(nullable NSString *)characterID result:(void(^)( NSArray<SupereraSDKSubscriptonItem *> * _Nullable allSubscriptonItems))resultCallback;

#pragma - mark subscription
//恢复订阅
- (void)restoreSubscriptionWithSuccess:(void(^)(void))success failure:(nullable ErrorCallback)failure;
@end

NS_ASSUME_NONNULL_END


