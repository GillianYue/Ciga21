//
//  SupereraSDKAppsFlyerManagerDelegate.h
//  SupereraSDKMobileCore
//
//  Created by blackmac on 2020/4/7.
//  Copyright © 2020 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol SupereraSDKAppsFlyerManagerDelegate <NSObject>

/**
 `installData` contains information about install.
 Organic/non-organic, etc.
 */
- (void)onConversionDataReceived:(NSDictionary *)installData;

/**
 Any errors that occurred during the conversion request.
 */
- (void)onConversionDataRequestFailure:(NSError *)error;

/**
 `attributionData` contains information about OneLink, deeplink.
 */
- (void)onAppOpenAttribution:(NSDictionary *)attributionData;

/**
 Any errors that occurred during the attribution request.
 */
- (void)onAppOpenAttributionFailure:(NSError *)error;

@optional
- (void)didResolveDeepLink:(NSDictionary *)result;

@optional
- (void)didResolveDeepLinkNotFound;

@optional
- (void)didResolveDeepLinkFailure:(NSError *)error;

@end

NS_ASSUME_NONNULL_END
