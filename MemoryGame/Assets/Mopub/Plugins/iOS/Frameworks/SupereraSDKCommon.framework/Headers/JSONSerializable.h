//
//  JSONSerializable.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/17.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol JSONSerializable <NSObject>

- (nullable id)initWithJSON:(nonnull NSString *)JSON;
- (nonnull NSString *)toJSON;
@end
