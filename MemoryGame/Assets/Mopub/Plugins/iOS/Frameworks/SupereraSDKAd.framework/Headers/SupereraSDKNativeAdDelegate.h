//
//  SupereraSDKNativeAdDelegate.h
//  SupereraSDKAd
//
//  Created by 王城 on 2019/11/13.
//  Copyright © 2019 kubrcik. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol SupereraSDKNativeAdDelegate <NSObject>

- (void)onNativeAdDidShown:(NSString *)entry;
- (void)onNativeAdDismissed:(NSString *)entry;
- (void)onNativeAdClicked:(NSString *)entry;

@end

NS_ASSUME_NONNULL_END
