//
//  NSString+UUID.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/17.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface NSString (UUID)

+ (nonnull instancetype)UUID;
+ (nonnull instancetype)UUIDWithPrefix:(nonnull NSString *)prefix;

@end
