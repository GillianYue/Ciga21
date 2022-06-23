//
//  Mopub_ios_sdk_unity.m
//  Mopub-ios-sdk-unity
//
//  Created by 王城 on 2019/6/5.
//  Copyright © 2019 Mopub. All rights reserved.
//

#import "MopubSDKManager.h"

#pragma - mark help

// Converts C style string to NSString
#define GetNSStringParam(_x_) ((_x_) != NULL ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""])
#define GetNullableNSStringParam(_x_) ((_x_) != NULL ? [NSString stringWithUTF8String:_x_] : nil)

// Converts an NSString into a const char* ready to be sent to Unity
static char* cStringWithNSString(NSString* input)
{
    const char* string = [input UTF8String];
    return string ? strdup(string) : NULL;
}

#pragma - mark bridge C method for unity

#define SDKManager MopubSDKManager


void _init(char* gameVersionContent) {
    [SDKManager SDKInit: GetNullableNSStringParam(gameVersionContent)];
}

void _login() {
    [SDKManager login];
}

//void _loginWithDevice() {
//    [SDKManager loginWithDevice];
//}

void _loginWithDevice(char *activeCode) {
    [SDKManager loginDeviceWithActiveCode:GetNullableNSStringParam(activeCode)];
}

//void _loginWithWeChat() {
//    [SDKManager loginWithWeChat];
//}

void _loginWithWeChat(char *activeCode) {
    [SDKManager loginWeChatWithActiveCode: GetNullableNSStringParam(activeCode)];
}

void _fetchMobileAuthCode(char *phoneNumber) {
    [SDKManager fetchMobileAuthCodeWithPhoneNumber: GetNullableNSStringParam(phoneNumber)];
}

void _loginWithMobile(BOOL isOneKeyLogin, char *phoneNumber, char *authCode, char *activeCode) {
    [SDKManager loginWithMobile: GetNullableNSStringParam(phoneNumber)
                       authCode: GetNullableNSStringParam(authCode)
                     activeCode:GetNullableNSStringParam(activeCode)
                  isOneKeyLogin:isOneKeyLogin];
}

void _linkWithMobile(char *phoneNumber, char *authCode) {
    [SDKManager linkWithMobile: GetNullableNSStringParam(phoneNumber)
                      authCode: GetNullableNSStringParam(authCode)];
}

void _fetchEmailAuthCode(char *email) {
    [SDKManager fetchEmailAuthCodeWithEmail: GetNullableNSStringParam(email)];
}

void _loginWithEmail(char *email, char *password, char *authCode, char *activeCode) {
    [SDKManager loginWithEmail:GetNullableNSStringParam(email)
                      password:GetNullableNSStringParam(password)
                      authCode:GetNullableNSStringParam(authCode)
                    activeCode:GetNullableNSStringParam(activeCode)];
}

void _resetPasswordWithEmail(char *email, char *password, char *authCode) {
    [SDKManager resetPasswordWithEmail:GetNullableNSStringParam(email)
                           newPassword:GetNullableNSStringParam(password)
                              authCode:GetNullableNSStringParam(authCode)];
}

void _linkWithEmail(char *email, char *password, char *authCode) {
    [SDKManager linkWithEmail:GetNullableNSStringParam(email)
                     password:GetNullableNSStringParam(password)
                     authCode:GetNullableNSStringParam(authCode)];
}

void _loginWithVisitor(char *activeCode) {
    [SDKManager loginVisitorWithActiveCode: GetNullableNSStringParam(activeCode)];
}

void _setGameUserID (char* gameUserID) {
    
}

char* _currentAccessToken () {
    return cStringWithNSString([SDKManager currentAccessTokenJSON]) ;
}

void _verifySessionToken(char *sessionToken) {
    [SDKManager verifySessionToken: GetNullableNSStringParam(sessionToken)];
}

void _createInviteCode() {
    [SDKManager createInviteCode];
}

void _fetchInviteeList() {
    [SDKManager fetchInviteeList];
}

void _uploadInviteCode(char *inviteCode) {
    [SDKManager uploadInviteCode: GetNullableNSStringParam(inviteCode)];
}

char *_getPuid() {
    return cStringWithNSString([SDKManager getPuid]);
}

char *_getRegion() {
    return cStringWithNSString([SDKManager getRegion]);
}

char *_getGameVersion() {
    return cStringWithNSString([SDKManager getGameVersion]);
}

void _linkWithGameCenter (){
    [SDKManager linkWithGameCenter];
}

void _linkWithWeChat () {
    [SDKManager linkWithWeChat];
}

void _linkWithFacebook() {
    [SDKManager linkWithFacebook];
}

void _logCustomEvent (char* eventName,  char* dataJSON) {
    [SDKManager logCustomEventWithEventName:GetNullableNSStringParam(eventName) jsonParams:GetNullableNSStringParam(dataJSON)];
}

void _logCustomAFEvent (char* eventName,  char* dataJSON) {
    [SDKManager logCustomAFEventWithEventName:GetNullableNSStringParam(eventName) jsonParams:GetNullableNSStringParam(dataJSON)];
}

///ad

bool _hasRewardedVideo(char* gameEntry){
    return [SDKManager hasRewardedVideoWithGameEntry:GetNullableNSStringParam(gameEntry)];
}

void _showRewardVideoAd(char* gameEntry) {
    [SDKManager showRewardVideoAdWithGameEntry:GetNullableNSStringParam(gameEntry)];
}

bool _hasInterstitial(char* gameEntry) {
    return [SDKManager hasInterstitialWithGameEntry:GetNullableNSStringParam(gameEntry)];
}

void _showInterstitialAd(char* gameEntry) {
    [SDKManager showInterstitialAdWithGameEntry:GetNullableNSStringParam(gameEntry)];
}

///payment
void _fetchPaymentItemDetails() {
    [SDKManager fetchPaymentItemDetails];
}

void _fetchUnconsumedPurchasedItems() {
    [SDKManager fetchUnconsumedPurchasedItems];
}

void _fetchAllPurchasedItems() {
    [SDKManager fetchAllPurchasedItems];
}

void _consumePurchase(char* sdkOrderID) {
    [SDKManager consumePurchaseWithSDKOrderID:GetNullableNSStringParam(sdkOrderID)];
}

void _setUnconsumedItemUpdatedListener() {
    [SDKManager setUnconsumedItemUpdatedListener];
}

void _startPayment(char* itemID, char* cpOrderID, char* characterName, char* characterID, char* serverName, char* serverID) {
    [SDKManager startPaymentWithItemID:GetNullableNSStringParam(itemID) cpOrderID:GetNullableNSStringParam(cpOrderID) characterName:GetNullableNSStringParam(characterName) characterID:GetNullableNSStringParam(characterID) serverName:GetNullableNSStringParam(serverName) serverID:GetNullableNSStringParam(serverID)];
}

///scription
void _fetchAllPurchasedSubscriptionItems(){
    [SDKManager fetchAllPurchasedSubscriptionItem];
}

void _restoreSubscription(){
    [SDKManager restoreSubscription];
}

void _setSubscriptionItemUpdatedListener(){
    [SDKManager setSubscriptionItemUpdatedListener];
}

void _logNative(char* log) {
    [SDKManager log:GetNullableNSStringParam(log)];
}

///ingame
void _setIngameParamsListener() {
    [SDKManager setIngameParamsListener];
}

///banner
void _showBanner(int position) {
    [SDKManager showBanner:position];
}

void _dismissBanner() {
    [SDKManager dismissBanner];
}

bool _openJoinChatGroup() {
    return [SDKManager openJoinChatGroup];
}

bool _openJoinWhatsappChatting() {
    return [SDKManager openJoinWhatsappChatting];
}


///native ad
 bool _hasNativeAd(char* gameEntry){
    return [SDKManager hasNativeAd:GetNullableNSStringParam(gameEntry)];
 }

 void _showNativeAdFixed(char* gameEntry, int position, float spacing){
     [SDKManager showNativeAdFixed:GetNullableNSStringParam(gameEntry) position:position spacing:spacing];
 }

 void _closeNativeAd(char* gameEntry){
     [SDKManager closeNativeAd:GetNullableNSStringParam(gameEntry)];
 }

 ///LOG
void _logPlayerInfo(char* characterName, char* characterID, int characterLevel, char* serverName, char* serverID){
    [SDKManager logPlayerInfoWithCahrName:GetNullableNSStringParam(characterName) characterID:GetNullableNSStringParam(characterID) characterLevel:characterLevel serverName:GetNullableNSStringParam(serverName) serverID:GetNullableNSStringParam(serverID)];
}

void _logStartLevel(char* levelName) {
    [SDKManager logStartLevel:GetNullableNSStringParam(levelName)];
}

void _logFinishLevel(char* levelName) {
    [SDKManager logFinishLevel:GetNullableNSStringParam(levelName)];
}

void _logUnlockLevel(char* levelName) {
    [SDKManager logUnlockLevel:GetNullableNSStringParam(levelName)];
}

void _logSkipLevel(char* levelName) {
    [SDKManager logSkipLevel:GetNullableNSStringParam(levelName)];
}

void _logSkinUsed(char* skinName) {
    [SDKManager logSkinUsed:GetNullableNSStringParam(skinName)];
}

void _logAdEntrancePresent(char* name) {
    [SDKManager logAdEntrancePresent:GetNullableNSStringParam(name)];
}

///other
bool _openRatingView(){
    return [SDKManager openRatingView];
}

bool _addLocalNotifaciton(char* title, char* content, char* date, char* hour, char* min) {
    return [SDKManager addLocalNotifaciton:GetNullableNSStringParam(title) content:GetNullableNSStringParam(content) date:GetNullableNSStringParam(date) hour:GetNullableNSStringParam(hour) min:GetNullableNSStringParam(min)];
}

void _setADIngameParamsListener(){
    
}

void _setAFDataDelegate() {
    [SDKManager setAFDataDelegate];
}

void _launchCustomer() {
    [SDKManager launchCustomer];
}

void _launchCustomerWithTransitPage(char *pageType) {
    [SDKManager launchCustomerWithTransitPage:GetNullableNSStringParam(pageType)];
}

void _launchCustomerWithTransitPageEmail(char *pageType, char *email) {
    [SDKManager launchCustomerWithTransitPage:GetNullableNSStringParam(pageType) email:GetNullableNSStringParam(email)];
}

void _setUnreadMessageUpdatedListener() {
    [SDKManager setUnreadMessageUpdatedListener];
}

// old addiction prevention
void _openPrevention() {
    [SDKManager openAddctionPrevention];
}

void _fetchIdCardInfo() {
    [SDKManager fetchIdCardInfo];
}

long _getTotalOnlineTime() {
    return [SDKManager getTotalOnlineTime];
}

char *_getRealnameHeartBeat() {
    
    NSString *str = [SDKManager getRealnameHeartBeat];
    NSLog(@"heartbeatString: %@", str);
    if(!str){
        str = @"";
    }
    
    char *res = (char *)malloc(str.length + 1);
    strcpy(res, str.UTF8String);
    
    return res;
}

void _verifyIdCard(char *name, char *cardNumber) {
    [SDKManager verifyIdCardWithName:GetNullableNSStringParam(name)
                          cardNumber:GetNullableNSStringParam(cardNumber)];
}

void _fetchPaidAmount(int amount) {
    [SDKManager fetchPaidAmount:amount];
}

void _openRealnamePrevention() {
    [SDKManager openRealnamePrevention];
}

long _getPreventionTotalOnlineTime() {
    [SDKManager getPreventionTotalOnlineTime];
}

void _queryRealnameInfo() {
    [SDKManager queryRealnameInfo];
}

void _realnameAuthentication(char *name, char *cardNumber) {
    [SDKManager realnameAuthentication:GetNullableNSStringParam(name)
                            cardNumber:GetNullableNSStringParam(cardNumber)];
}

void _fetchPaidAmountMonthly(int amount) {
    [SDKManager fetchPaidAmountMonthly:amount];
}

void _startRealnameAuthenticationWithUI() {
    [SDKManager startRealnameAuthenticationWithUI];
}

void _logout() {
    [SDKManager logout];
}

bool _isUserNotificationEnable() {
    return [SDKManager isUserNotificationEnable];
}

void _openSystemNotificationSetting() {
    return [SDKManager openSystemNotificationSetting];
}

void _fetchRanking(char *uid, int page, int size) {
    return [SDKManager fetchRankingWithUid:GetNullableNSStringParam(uid) page:page size:size];
}
