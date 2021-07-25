using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{
	void Start()
	{

	}

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
}
