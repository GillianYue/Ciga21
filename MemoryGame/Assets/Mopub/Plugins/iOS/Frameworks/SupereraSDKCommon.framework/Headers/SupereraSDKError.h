//
//  SupereraSDKError.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/16.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "JSONSerializable.h"
#import "MapSerializable.h"
#import "NSString+multiLang.h"

NS_ASSUME_NONNULL_BEGIN

///type of domain
typedef NSString * SupereraSDKErrorDomain NS_TYPED_EXTENSIBLE_ENUM;

///SDK客户端来源
FOUNDATION_EXPORT SupereraSDKErrorDomain SupereraSDKErrorDomainSDKClient;
///系统来源
FOUNDATION_EXPORT SupereraSDKErrorDomain SupereraSDKErrorDomainSDKSystem;
///SDK服务器来源
FOUNDATION_EXPORT SupereraSDKErrorDomain SupereraSDKErrorDomainSDKServer;
///GameCenter来源
FOUNDATION_EXPORT SupereraSDKErrorDomain SupereraSDKErrorDomainSDKGameCenter;

/**
 SDK 错误码
 */
typedef NS_ERROR_ENUM(SupereraSDKErrorDomainSDKClient, SupereraSDKErrorCode){
//    SupereraSDKErrorCodeUnknown = 0,
    ///SDK core 未初始化1
    SupereraSDKErrorCodeCoreHasNotInit,
    ///SDK core 已经初始化，请勿重复初始化2
    SupereraSDKErrorCodeSDKAlreadyInit,
    ///SDK core 初始化失败3
    SupereraSDKErrorCodeSDKInitFailed,
    ///网络请求失败4
    SupereraSDKErrorCodeNetWorkError,
    ///服务器请求错误，返回code非200 5
    SupereraSDKErrorCodeServerRequestError,
    ///服务器请求错误，返回错误结果/code 非0
    SupereraSDKErrorCodeServerResponseError,
    ///服务器返回的data内容错误
    SupereraSDKErrorCodeServerResponseDataError,
    ///GameCenter 登录授权错误
    SupereraSDKErrorCodeGameCenterLoginError,
    ///GameCenter 用户未登录
    SupereraSDKErrorCodeGameCenterLogout,
    ///GameCenter 获取验证票据错误 10
    SupereraSDKErrorCodeGameCenterGenerateSignatureError,
    ///WeChat 登录授权错误
    SupereraSDKErrorCodeWeChatLoginError,
    ///当前登录方式下没有关联的游戏帐号
    SupereraSDKErrorCodeLoginMechanismNoAssociatedAccount,
    ///当前操作 sessionToken 数据异常
    SupereraSDKErrorCodeSessionTokenError,
    ///用户未登录，当前的 accessToken 为空
    SupereraSDKErrorCodeUserNotLoggedIn,
    ///用户取消当前操作
    SupereraSDKErrorCodeUserCanceled,
    ///当前操作超时 15
    SupereraSDKErrorCodeTimeOut,
    ///当前账号类型错误
    SupereraSDKErrorAccountTypeError,
    ///current operation is in progress
    SupereraSDKErrorOperationProcessing,
    ///IAP 商品查询不存在
    SupereraSDKErrorIAPProductDontExist,
    ///IAP 苹果返回失败
    SupereraSDKErrorIAPSKPaymentTransactionFailed,
    ///IAP 支付回调receipt为空 20
    SupereraSDKErrorIAPReceiptDataEmpty,
    ///IAP 票据校验失败
    SupereraSDKErrorIAPReceiptValidateFailed,
    ///IAP 无法使用
    SupereraSDKErrorIAPCantMakePayments,
    ///paymentInfo 信息错误
    SupereraSDKErrorPaymentInfoDataError,
    ///上一比支付进行中
    SupereraSDKErrorLastPaymentProcessing,
    ///参数错误 25
    SupereraSDKErrorParamsError,
    ///签名错误
    SupereraSDKErrorSignError,
    
    // 从左到右
    // 6       5-4               3                    2-1
    // 固定1    00: 所有功能       0: 双端统一错误码       00-99
    //         01: 一键登录       1: 安卓自定义错误码     各个功能错误码
    //         02: facebook登录  2: iOS自定义错误码
    //         01: 苹果登录
    ///未知错误
    SupereraSDKErrorCodeUnknown = 100000,
    ///没有配置一键登录密钥
    SupereraSDKErrorCodeMobileATAuthKeyNotConfig = 101001,
    ///唤起授权页失败
    SupereraSDKErrorCodeMobileATAuthGetAuthenticationPageFailed = 101002,
    ///获取运营商配置信息失败(创建工单联系工程师)
    SupereraSDKErrorCodeMobileATAuthGetCarrierConfigFailed = 101003,
    ///未检测到sim卡（提示用户检查SIM卡后重试）
    SupereraSDKErrorCodeMobileATAuthCannotFindSimCard = 101004,
    ///蜂窝网络未开启(提示用户开启移动网络后重启)
    SupereraSDKErrorCodeMobileATAuthGPRSClose = 101005,
    ///无法判断运营商(创建工单联系工程师)
    SupereraSDKErrorCodeMobileATAuthNotKnowWhichCarrier = 101006,
    ///获取token失败(切换到其他登录方式)
    SupereraSDKErrorCodeMobileATAuthGetTokenFailed = 101007,
    ///预取号失败（切换到其他登录方式）
    SupereraSDKErrorCodeMobileATAuthPreGetNumberFailed = 101008,
    ///运营商维护升级,该功能不可用(创建工单联系工程师)
    SupereraSDKErrorCodeMobileATAuthCarrierUpdating = 101009,
    ///运营商维护升级，该功能已达最大调用次数(创建工单联系工程师)
    SupereraSDKErrorCodeMobileATAuthMaxCount = 101010,
    ///接口超时（切换到其他登录方式）
    SupereraSDKErrorCodeMobileATAuthTimeout = 101011,
    ///AppID、Appkey解析失败(创建工单联系工程师)
    SupereraSDKErrorCodeMobileATAuthAppIDAppkeyError = 101012,
    ///点击登录时检测到运营商已切换
    SupereraSDKErrorCodeMobileATAuthCarrierChange = 101013,
    ///终端环境检测失败（终端不支持认证/终端检测参数错误)(检查接口调用传参是否正确)
    SupereraSDKErrorCodeMobileATAuthEnvironmentError = 101014,
    ///授权页已加载时不允许调用加速或预取号接口
    SupereraSDKErrorCodeMobileATAuthNotAllowPreGetNumber = 101015,
    ///授权页点击了取消或者返回按钮
    SupereraSDKErrorCodeMobileATAuthClickCancel = 101016,
    ///苹果登录授权失败
    SupereraSDKErrorCodeAppleLoginAuthFailed = 101201,
    ///苹果登录取消授权
    SupereraSDKErrorCodeAppleLoginAuthCancel = 101202,
    ///facebook登录发生未知错误
    SupereraSDKErrorCodeFacebookLoginUnknownError = 102000,
    ///facebook登录授权取消
    SupereraSDKErrorCodeFacebookLoginAuthCancel = 102001,
    ///facebook登录授权失败
    SupereraSDKErrorCodeFacebookLoginAuthFailed = 102002,
    
};

@class SupereraSDKError;

/**
 通用错误回调

 @param error 错误
 */
typedef void(^ErrorCallback)(SupereraSDKError *error);


/**
 错误描述类
 */
@interface SupereraSDKError : NSError <JSONSerializable,MapSerializable>

/**
 错误来源
 */
@property (nonatomic, copy, readonly) SupereraSDKErrorDomain errorDomain;

/**
 错误来源的原始错误码，如果来源自SDK，则值与clientCode相同
 */
@property (nonatomic, assign, readonly) NSInteger domainCode;

/**
 SDK错误码
 */
@property (nonatomic, assign, readonly) SupereraSDKErrorCode clientCode;

/**
 客户端错误信息
 */
@property (nonatomic, copy, readonly) NSString *clientMessage;

/**
 错误来源的错误信息
 */
@property (nonatomic, copy, readonly) NSString *domainMessage;

/**
 详细异常信息
 */
@property (nonatomic, copy, readonly, nullable) NSDictionary<NSString *,id> *exception;


/**
 底层error
 */
@property (nonatomic, strong, readonly, nullable) NSError *underlyingError;

- (NSString *)description;

- (nullable instancetype)initWithJSON:(NSString *)JSON;
- (NSString *)toJSON;

- (nullable instancetype)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nonnull NSDictionary *)toDictionary;

/**
 使用cilentCode、clientMessage创建error

 @param cilentCode SDK错误码
 @param clientMessage 内容
 @return error
 */
+ (instancetype)errorWithClientCode:(SupereraSDKErrorCode)cilentCode clientMessage:(NSString *)clientMessage;

/**
 使用cilentCode、clientMessage、exception创建error
 
 @param cilentCode SDK错误码
 @param clientMessage 内容
 @param exception 详细异常
 @return error
 */
+ (instancetype)errorWithClientCode:(SupereraSDKErrorCode)cilentCode clientMessage:(NSString *)clientMessage exception:(nullable NSDictionary<NSString *,id> *)exception;


/**
 使用cilentCode、clientMessage、errorDomain、domainMessage创建error

 @param cilentCode SDK错误码
 @param clientMessage 内容
 @param errorDomain 错误来源
 @param domainCode 来源错误码
 @param domainMessage 来源错误信息
 @return error
 */
+ (instancetype)errorWithClientCode:(SupereraSDKErrorCode)cilentCode clientMessage:(NSString *)clientMessage domain:(nullable SupereraSDKErrorDomain)errorDomain domainCode:(NSInteger)domainCode domainMessage:(nullable NSString *)domainMessage;


/**
 使用cilentCode、clientMessage、errorDomain、domainMessage、exception创建error

 @param cilentCode SDK错误码
 @param clientMessage 内容
 @param errorDomain 错误来源
 @param domainCode 来源错误码
 @param domainMessage 来源错误信息
 @param exception exception
 @return error
 */
+ (instancetype)errorWithClientCode:(SupereraSDKErrorCode)cilentCode clientMessage:(NSString *)clientMessage domain:(nullable SupereraSDKErrorDomain)errorDomain domainCode:(NSInteger)domainCode domainMessage:(NSString *)domainMessage exception:(nullable NSDictionary<NSString *,id> *)exception;

/**
 使用cilentCode、clientMessage、errorDomain、domainMessage、exception、underlyingError创建error
 
 @param cilentCode SDK错误码
 @param clientMessage 内容
 @param errorDomain 错误来源
 @param domainCode 来源错误码
 @param domainMessage 来源错误信息
 @param exception exception
  @param underlyingError underlyingError
 @return error
 */
+ (instancetype)errorWithClientCode:(SupereraSDKErrorCode)cilentCode clientMessage:(NSString *)clientMessage domain:(nullable SupereraSDKErrorDomain)errorDomain domainCode:(NSInteger)domainCode domainMessage:(NSString *)domainMessage exception:(nullable NSDictionary<NSString *,id> *)exception underlyingError:(nullable NSError *)underlyingError;



NS_ASSUME_NONNULL_END
@end
