

using UnityEngine;
using System.Collections;

#if UNITY_STANDALONE
using Steamworks;
#endif

public class SteamScript : MonoBehaviour
{
#if UNITY_STANDALONE

	void Start()
	{
		if (SteamManager.Initialized)
		{
			string name = SteamFriends.GetPersonaName();
			Debug.Log(name);
		}
	}

#endif
}

