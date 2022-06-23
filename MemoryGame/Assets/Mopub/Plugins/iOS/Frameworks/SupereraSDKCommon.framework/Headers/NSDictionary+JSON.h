//
//  NSDictionary+JSON.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/17.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface NSDictionary (JSON)

+ (nullable NSDictionary *)dictionaryWithJSONString:(nonnull NSString *)JSON;
+ (nullable NSDictionary *)dictionaryWithJSONSData:(nonnull NSData *)JSON;
@end
