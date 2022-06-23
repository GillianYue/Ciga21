using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MopubNS
{
	public interface MopubInterstitialAdListener {
//		void onInterstitialLoaded(string gameEntry);
//		void onInterstitialFailed(string gameEntry, int errorCode,string msg);
		void onInterstitialShown(string gameEntry, MopubAdInfo adInfo);
		void onInterstitialClicked(string gameEntry, MopubAdInfo adInfo);
		void onInterstitialDismissed(string gameEntry, MopubAdInfo adInfo);
	}
}
