//
//  SupereraSDKLocalMsg.h
//  SupereraSDKMobileCore
//
//  Created by 王城 on 2020/1/13.
//  Copyright © 2020 superera. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface SupereraSDKLocalMsg : NSObject

@property (nonatomic, copy) NSString *title;
@property (nonatomic, copy, nullable) NSString *content;
//format: yyyyMMdd  20191202
@property (nonatomic, copy) NSString *date;
//not nil
@property (nonatomic, copy) NSString *hour;
//not nil
@property (nonatomic, copy) NSString *min;

@end

NS_ASSUME_NONNULL_END
