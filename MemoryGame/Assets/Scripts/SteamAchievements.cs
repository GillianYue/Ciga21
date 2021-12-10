
using UnityEngine;
using System.Collections;

#if UNITY_STANDALONE
using Steamworks;
#endif

public class SteamAchievements : MonoBehaviour
{
	void Start()
	{

	}

#if UNITY_STANDALONE

	public void ach1()
    {
		if (SteamManager.Initialized)
		{
			SteamUserStats.SetAchievement("ACH_FORGET");
			SteamUserStats.StoreStats();
		}
	}

	public void resetAchievements() {

		if (SteamManager.Initialized)
		{
			SteamUserStats.ResetAllStats(true);
			SteamUserStats.StoreStats();
		}

	}

	public void updateAch2Progress(int lv)
    {
		if (SteamManager.Initialized)
		{
			print("updating ach2 progress: " + lv);
			SteamUserStats.SetStat("STS_MEMORIES", lv);
			SteamUserStats.StoreStats();
		}
	}

#endif

}

