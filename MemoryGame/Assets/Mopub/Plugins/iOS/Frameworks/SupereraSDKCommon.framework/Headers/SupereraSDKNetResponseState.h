//
//  SupereraSDKNetResponseState.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/15.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface SupereraSDKNetResponseState : NSObject
NS_ASSUME_NONNULL_BEGIN

@property (nonatomic, assign) int code;
@property (nonatomic, copy) NSString *msg;
@property (nonatomic, copy) NSDictionary *rawDic;

- (instancetype) initWithDictionary:(nullable NSDictionary *)dictionary;

NS_ASSUME_NONNULL_END
@end
