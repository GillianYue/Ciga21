//
//  SupereraSDKUIResUtil.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/5/13.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKUIResUtil : NSObject


/**
 从指定bundle中的nib创建view

 @param nibName nib name
 @param bundle bundle
 @return view
 */
+ (nullable id)createViewWithNibName:(NSString *)nibName fromBundle:(NSBundle *)bundle;


/**
 使用模态方式在rootViewController上弹出viewController

 @param viewController viewController 
 */
+ (void)showViewController:(UIViewController *)viewController completion:(void (^ __nullable)(void))completion;

@end

NS_ASSUME_NONNULL_END
