using UnityEngine;
using System.Collections;

namespace MopubNS
{
    public interface MopubNativeAdListener
    {
        void onNativeAdDidShown(string gameEntry);
        void onNativeAdDismissed(string gameEntry);
        void onNativeAdClicked(string gameEntry);
    }
}
