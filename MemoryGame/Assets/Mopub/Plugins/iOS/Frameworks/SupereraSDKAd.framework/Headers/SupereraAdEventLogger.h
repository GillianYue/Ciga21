//
//  SupereraAdEventLogger.h
//  JDAdSDK
//
//  Created by Kubrick.G on 2019/5/27.
//  Copyright © 2019 kubrcik. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "common_header.h"

@protocol SupereraAdEventLogger <NSObject>

@required
- (void)logCustomEvent:(NSString *)eventName params:(NSDictionary *)bodyDic;

@end
