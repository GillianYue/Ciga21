//
//  SupereraSDKCoreConstant.h
//  SupereraSDKMobileCore
//
//  Created by jodotech on 2019/4/26.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

#pragma - mark notification
extern NSString * const SupereraSDKFinishInitNotification;
extern NSString * const SESDKMsg_paymentSucceed;
extern NSString * const SESDKMsg_unityDidReceiveRemoteNotification;
extern NSString * const SESDKMsg_didReceiveRemoteNotification;

void SESDK_SendNotification(NSString* name);
void SESDK_SendNotificationWithArg(NSString* name, id sender, id arg);

#pragma - mark constant

@interface SupereraSDKCoreConstant : NSObject

//info中的key
@property (class, nonatomic, copy, readonly) NSString *key_info_CGI;
@property (class, nonatomic, copy, readonly) NSString *key_info_LOGZIP;
@property (class, nonatomic, copy, readonly) NSString *key_info_log_sdk_event;
@property (class, nonatomic, copy, readonly) NSString *key_info_publisher;
@property (class, nonatomic, copy, readonly) NSString *key_info_distributor;
@property (class, nonatomic, copy, readonly) NSString *key_info_apple_appID;
@property (class, nonatomic, copy, readonly) NSString *key_info_toutiao_appid;
@property (class, nonatomic, copy, readonly) NSString *key_info_toutiao_channel;
@property (class, nonatomic, copy, readonly) NSString *key_info_toutiao_appname;
@property (class, nonatomic, copy, readonly) NSString *key_info_appsflyer_devkey;
//广点通（优量汇）
@property (class, nonatomic, copy, readonly) NSString *key_info_gdt_actionSetId;
@property (class, nonatomic, copy, readonly) NSString *key_info_gdt_secretKey;
//信鸽
@property (class, nonatomic, copy, readonly) NSString *key_info_xg_app_id;
@property (class, nonatomic, copy, readonly) NSString *key_info_xg_app_key;

//微信登录
@property (class, nonatomic, copy, readonly) NSString *key_info_we_chat_appid;
@property (class, nonatomic, copy, readonly) NSString *key_info_we_chat_universal_links;

//用户协议
@property (class, nonatomic, copy, readonly) NSString *key_info_user_proxy;
//隐私协议
@property (class, nonatomic, copy, readonly) NSString *key_info_private_proxy;

//手机一键登录
@property (class, nonatomic, copy, readonly) NSString *key_info_mobile_atauth_key;

/**
 appsFlyer 的devKey
 */
@property (class, nonatomic, copy, readonly) NSString *appsFlyerDevKey;

@property (class, nonatomic, copy, readonly) NSURL *url_hostConfig;

/**
 获取支付参数URL
 */
@property (class, nonatomic, copy, readonly) NSURL *url_getPaymentParams;

/**
 校验苹果票据URL
 */
@property (class, nonatomic, copy, readonly) NSURL *url_validateAppleAppStoreReceipt;

@property (class, nonatomic, copy, readonly) NSURL *url_getPurchasedItems;

@property (class, nonatomic, copy, readonly) NSURL *url_consumePurchasedItem;

@property (class, nonatomic, copy, readonly) NSURL *url_getCatalog;

/**
 获取支付列表
 */
@property (class, nonatomic, copy, readonly) NSURL *url_getPaymentMethods;

/**
 防沉迷
 */
@property (class, nonatomic, copy, readonly) NSURL *url_addiction_identifyQuery;
@property (class, nonatomic, copy, readonly) NSURL *url_addiction_idCardIdentify;
@property (class, nonatomic, copy, readonly) NSURL *url_addiction_paidAmountQuery;
@property (class, nonatomic, copy, readonly) NSURL *url_addiction_heartBeat;

/**
 客服系统
 */
@property (class, nonatomic, copy, readonly) NSURL *url_customer;

+ (void)setTestEnvironment:(BOOL)isTest;


@end
