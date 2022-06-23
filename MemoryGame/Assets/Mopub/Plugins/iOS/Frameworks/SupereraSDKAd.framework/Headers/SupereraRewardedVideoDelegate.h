//
//  SupereraRewardedVideoDelegate.h
//  JDAdSDK
//
//  Created by Kubrick.G on 2019/4/8.
//  Copyright © 2019 kubrcik. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "common_header.h"
#import "SupereraAdInfo.h"

@protocol SupereraRewaredVideoDelegate <NSObject>

@optional

/**
 激励视频广告加载完成

 @param entry 游戏入口
 */
- (void)rewardedVideoDidLoad:(NSString *)entry;


/**
 激励视频广告加载失败
 NOTE: 请勿在该回调中立刻调用加载广告接口，而是设置一定间隔后再调用.

 @param entry 游戏入口
 @param error 失败原因
 */
- (void)rewardedVideoDidFailToLoad:(NSString *)entry error:(NSError *)error;



/**
 激励视频广告展现

 @param entry 游戏入口
 */
- (void)rewardedVideoDidAppear:(NSString *)entry;
- (void)rewardedVideoDidAppear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo;

/**
 激励视频广告关闭

 @param entry 游戏入口
 */
- (void)rewardedVideoDidDisappear:(NSString *)entry;
- (void)rewardedVideoDidDisappear:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo;


/**
 用户点击激励视频广告

 @param entry 游戏入口
 */
- (void)rewardedVideoDidReceiveTap:(NSString *)entry;
- (void)rewardedVideoDidReceiveTap:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo;


/**
 激励视频广告播放完毕

 @param entry 游戏入口
 */
- (void)rewardedVideoDidFinishPlay:(NSString *)entry;
- (void)rewardedVideoDidFinishPlay:(NSString *)entry adInfo:(SupereraAdInfo *)adInfo;


/**
 激励视频广告播放失败

 @param entry 游戏入口
 */
- (void)rewardedVideoDidFailToPlay:(NSString *)entry;
@end
