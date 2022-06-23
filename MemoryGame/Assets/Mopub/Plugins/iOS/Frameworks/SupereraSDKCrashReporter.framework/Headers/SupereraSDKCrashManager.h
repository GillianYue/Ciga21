//
//  SupereraSDKCrashManager.h
//  SupereraSDKCrashReporter
//
//  Created by 王城 on 2019/9/5.
//  Copyright © 2019 superera. All rights reserved.
//

#import <Foundation/Foundation.h>


NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKCrashManager : NSObject

// 向 Bugly 上报错误
+ (void)buglyReportError:(NSError *)error;
+ (void)buglyReportException:(nonnull NSException *)exception;

@end

NS_ASSUME_NONNULL_END
