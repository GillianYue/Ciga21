//
//  MapSerializable.h
//  SupereraSDKCommon
//
//  Created by jodotech on 2019/4/17.
//  Copyright © 2019年 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol MapSerializable <NSObject>
- (nullable id)initWithDictionary:(nonnull NSDictionary *)dictionary;
- (nullable NSDictionary *)toDictionary;
@end
