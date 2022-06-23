	//
//  SupereraSDK.h
//  JDAdSDK
//
//  Created by Kubrick.G on 2019/4/8.
//  Copyright © 2019 kubrcik. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "SupereraInteractDelegate.h"
#import "SupereraInterstitialDelegate.h"
#import "SupereraRewardedVideoDelegate.h"
#import "SupereraSDKNativeAdDelegate.h"
#import "SupereraAdEventLogger.h"
#import "common_header.h"
#import "BannerPosition.h"
#import "SDKNativeAdPosition.h"
#import "SupereraSDKAdAFLoggerDelegate.h"
#import "SupereraAdInfo.h"
#import "SupereraImageInteractAdLoader.h"
#import "SupereraImageInteractDelegate.h"
#import "SupereraVideoInteractDelegate.h"
#import "SupereraSDKNativeAdLoader.h"

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM (NSInteger, SupereraSDKAdThirdMediation) {
    SupereraSDKAdThirdMediationNone = 0,
    SupereraSDKAdThirdMediationMax
};

@interface SupereraSDKAd : NSObject

@property (nonatomic, assign) SupereraSDKAdThirdMediation thirdMediation;

@property (nonatomic, assign) BOOL containThirdMediation;

+ (NSString *)SDKVersion;

+ (nullable NSString *)PUID;

+ (instancetype)sharedInstance;

+ (void)setTestEnvironment:(BOOL)isTest;


/**
 初始化SDK -- idlecook 游戏使用

 @param gameID 游戏代号
 @param puid 设备唯一标识符
 */
- (void)initSDKWithGameID:(NSString *)gameID puid:(NSString *)puid;

/**
  初始化SDK

 @param gameID 游戏代号
 @param puid 设备唯一标识符
 @param logger 日志上传对象，提供日志上传接口
 */
- (void)initSDKWithGameID:(NSString *)gameID puid:(NSString *)puid logger:(id<SupereraAdEventLogger>)logger;

/**
  初始化SDK

 @param gameID 游戏代号
 @param puid 设备唯一标识符
 @param logger 日志上传对象，提供日志上传接口
 @param routerConditions  分路条件
 */
- (void)initSDKWithGameID:(NSString *)gameID puid:(NSString *)puid logger:(id<SupereraAdEventLogger>)logger routerConditions:(NSDictionary *)routerConditions;

/**
 初始化SDK -- idlecook 游戏使用

 @param gameID 游戏代号
 @param puid 设备唯一标识符
 @param routerConditions  分路条件
 */
- (void)initSDKWithGameID:(NSString *)gameID puid:(NSString *)puid routerConditions:(NSDictionary *)routerConditions;


/**
 设置玩家角色ID

 @param userID 角色ID
 */
- (void)setUserID:(NSString *)userID;

/**
 设置激励视频广告回调对象

 @param delegate 实现了SupereraRewaredVideoDelegate协议的代理对象
 */
- (void)setRewardedVideoDelegate:(id<SupereraRewaredVideoDelegate>)delegate;


/**
 设置插屏广告回调对象

 @param delegate 实现了SupereraInterstitialDelegate协议的代理对象
 */
- (void)setInterstitialDelegate:(id<SupereraInterstitialDelegate>)delegate;

/**
 设置AF日志上报的回调
 */
- (void)setAFAdLoggerDelegate:(id<SupereraSDKAdAFLoggerDelegate>)delegate;

/**
 加载激励视频广告

 @param entry 用于展示激励视频广告的游戏入口
 */
- (void)loadRewardedVideoWithEntry:(NSString *)entry;


/**
 加载插屏广告

 @param entry 用于展示插屏广告的游戏入口
 */
- (void)loadInterstitialWithEntry:(NSString *)entry;

/**
 加载原生广告
 */
- (void)loadNativeAdWithLoader:(SupereraSDKNativeAdLoader *)loader;


/**
 查询游戏入口对应激励视频广告是否加载完成

 @param entry 用于展示激励视频广告的游戏入口
 */
- (BOOL)hasRewardedVideoForEntry:(NSString *)entry;

/**
 设置互动广告的回调
 */
- (void)setInteractDelegate:(id<SupereraInteractDelegate>)delegate;

/**
 设置视频互动广告的回调
 */
- (void)setVideoInteractDelegate:(id<SupereraVideoInteractDelegate>)delegate;

/**
 查询游戏入口对应插屏广告是否加载完成

 @param entry 用于展示插屏广告的游戏入口
 */
- (BOOL)hasInterstitialForEntry:(NSString *)entry;

/**
 查询游戏入口是否有对应互动广告
 
 @param entry 用于展示互动广告的游戏入口
 */
- (BOOL)hasInteractForEntry:(NSString *)entry;

/**
 查询游戏入口是否有对应的视频互动广告
 
 @param entry 用于展示视频互动广告的游戏入口
 */
- (BOOL)hasVideoInteractForEntry:(NSString *)entry;

/**
 查询游戏入口是否有对应的图片互动广告
 
 @param entry 用于展示图片互动广告的游戏入口
 @param size   图片尺寸
 */
- (BOOL)hasImageInteractForEntry:(NSString *)entry size:(CGSize)size;

/**
 查询游戏入口是否有对应智能广告
 */
- (BOOL)hasSmartAdForEntry:(NSString *)entry;

/**
 展示智能广告
 
 @param entry 用于展示激励视频广告的游戏入口
 @param controller 当前视图控制器
 */
- (void)showSmartAdFromWithEntry:(NSString *)entry fromController:(UIViewController *)controller;

/**
 展示激励视频广告

 @param entry 用于展示激励视频广告的游戏入口
 @param controller 当前视图控制器
 */
- (void)showVideoAdWithEntry:(NSString *)entry fromController:(UIViewController *)controller;


/**
 展示插屏广告

 @param entry 用于展示插屏广告的游戏入口
 @param controller 当前视图控制器
 */
- (void)showInterstitialAdWithEntry:(NSString *)entry fromController:(UIViewController *)controller;

/**
 展示互动广告
 
 @param entry 用于展示互动广告的游戏入口
 @param controller 当前视图控制器
 */
- (void)showInteractAdWithEntry:(NSString *)entry fromController:(UIViewController *)controller;

/**
 展示视频互动广告
 
 @param entry                 用于展示视频互动广告的游戏入口
 @param controller      当前视图控制器
 */
- (void)showVideoInteractAdWithEntry:(NSString *)entry fromController:(UIViewController *)controller;

/**
 加载图片互动广告
 */
- (void)loadImageInteractAdWithLoader:(SupereraImageInteractAdLoader *)loader;

/**
 show the banner

 @param position Banner display position
 */
- (void)showBannerAt:(BannerPosition)position;


/**
 dismiss the banner
 */
- (void)dismissBanner;


/**
 上传游戏事件

 @param eventName 事件名
 @param eventInfo 事件参数
 */
- (void)uploadGameEvent:(NSString *)eventName eventInfo:(nullable NSDictionary *)eventInfo;


/**
 监听游戏内在线参数，当拉取到游戏内在线参数时将回调

 @param paramsUpdatedCallback 回调
 */
- (void)setInGameOnlineParamsCallback:(void(^)(NSDictionary *params))paramsUpdatedCallback;

#pragma mark - native ad
/**
 设置原生广告回调对象
 */
- (void)setNativeAdDelegate:(id<SupereraSDKNativeAdDelegate>)delegate;

/**
 是否有原生广告
 */
- (BOOL)hasNaviteAdForEntry:(NSString *)entry;

/**
 显示固定样式的原生广告
 @param entry 入口
 @param position 位置
 @param spacing 间距，如果位置为顶部，则为距顶部间距；位置为底部，则为距底部间距
 */
- (void)showNaviteAdFixedForEntry:(NSString *)entry position:(SDKNativeAdPosition)position spacing:(float)spacing;

/**
 关闭原生广告
 */
- (void)closeNaviteAdForEntry:(NSString *)entry;

@end

NS_ASSUME_NONNULL_END
