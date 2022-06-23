//
//  NSString+JSON.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/17.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface NSString (JSON)

+ (nullable NSString *)JSONStringWithDictionary:(nonnull NSDictionary *)dictionary;

@end
