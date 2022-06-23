//
//  SupereraSDKAdAFLoggerDelegate.h
//  JDMopubSDKAd
//
//  Created by 梁嘉浩 on 2020/6/28.
//  Copyright © 2020 kubrcik. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol SupereraSDKAdAFLoggerDelegate <NSObject>

/**
 广告点击事件
 */
- (void)logAFAdClickEvent;

/**
 广告观看事件
 */
- (void)logAFAdViewEvent;

/**
 激励播放事件
 */
- (void)logAFRewardAdStartPlayEvent;

@end

NS_ASSUME_NONNULL_END
