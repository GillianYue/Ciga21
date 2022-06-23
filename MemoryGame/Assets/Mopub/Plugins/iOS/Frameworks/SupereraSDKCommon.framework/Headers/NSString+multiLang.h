//
//  NSString+multiLang.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/5/5.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface NSString (multiLang)


/**
 通过key获取一条多语言字符串

 @param key key
 @param def 默认字符串，如果key查询不到
 @return 字符串
 */
+ (NSString *)msgKey:(NSString *)key def:(NSString *)def;

@end

NS_ASSUME_NONNULL_END
