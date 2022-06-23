//
//  NSBundle+local.h
//  SupereraSDKCommon
//
//  Created by 王城 on 2019/5/23.
//  Copyright © 2019 yueyoo. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface NSBundle (local)


/**
 获取当前系统语言的语言bundle

 @param clazz 语言bundle所在的父bundle的class，如果为空，则获取mainBundle中的语言bundle
 @return bundle
 */
+ (instancetype)currentLanguageBundle:(nullable Class)clazz;
@end

NS_ASSUME_NONNULL_END
