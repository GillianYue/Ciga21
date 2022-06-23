//
//  NSString+URL.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/12.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface NSString (URL)
- (nullable NSString *)urlHostAllowedEncoding;

- (nullable NSString *)urlPathAllowedEncoding;
@end
