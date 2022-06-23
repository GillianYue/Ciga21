using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MopubNS
{
	public interface MopubRewardedVideoListener
	{
//		void onRewardedVideoLoadSuccess (string gameEntry);
//
//		void onRewardedVideoLoadFailure (string gameEntry, int errorCode, string msg);

		void onRewardedVideoStarted (string gameEntry, MopubAdInfo adInfo);

		void onRewardedVideoPlaybackError (string gameEntry, int errorCode, string msg);

		void onRewardedVideoClicked (string gameEntry, MopubAdInfo adInfo);

		void onRewardedVideoClosed (string gameEntry, MopubAdInfo adInfo);

		void onRewardedVideoCompleted (string gameEntry, MopubAdInfo adInfo);
	}
}
