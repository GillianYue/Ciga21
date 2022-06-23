//
//  SupereraInterstitialDelegate.h
//  JDAdSDK
//
//  Created by Kubrick.G on 2019/4/8.
//  Copyright © 2019 kubrcik. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "common_header.h"
#import "SupereraAdInfo.h"

@protocol SupereraInterstitialDelegate <NSObject>

@optional

/**
 插屏广告加载完成

 @param entry 游戏入口
 */
- (void)interstitialDidLoad:(NSString *)entry;


/**
 插屏广告加载失败
 NOTE: 请勿在该回调中立刻调用加载广告接口，而是设置一定间隔后再调用.

 @param entry 游戏入口
 @param error 失败原因
 */
- (void)interstitialDidFailToLoad:(NSString *)entry error:(NSError *)error;

/**
 插屏广告展示失败
 */
- (void)interstitialDidFailToPlay:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo error:(NSError *)error;

/**
 插屏广告展现

 @param entry 游戏入口
 */
- (void)interstitialDidAppear:(NSString *)entry;
- (void)interstitialDidAppear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo;


/**
 插屏广告关闭

 @param entry 游戏入口
 */
- (void)interstitialDidDisappear:(NSString *)entry;
- (void)interstitialDidDisappear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo;


/**
 用户点击插屏广告

 @param entry 游戏入口
 */
- (void)interstitialDidReceiveTap:(NSString *)entry;
- (void)interstitialDidReceiveTap:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo;

@end
