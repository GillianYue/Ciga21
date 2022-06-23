//
//  SupereraSDKLinkResult.h
//  SupereraSDKAuthCore
//
//  Created by jodotech on 2019/5/9.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "SupereraSDKLinkedAccounts.h"

@interface SupereraSDKLinkResult : NSObject

/**
 当前帐号票据
 */
@property (nonatomic, strong) SupereraSDKLinkedAccounts *linkedAccounts;

@end
