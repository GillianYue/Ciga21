using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject[] levelAudios;


    public void playSFX(int level, int index)
    {
        if (level > levelAudios.Length) { Debug.LogError("level exceeds levelAudios array"); return; }

        levelAudios[level-1].GetComponents<AudioSource>()[index].Play();

    }


    public void stopSFX(int level, int index)
    {
        if (level > levelAudios.Length) { Debug.LogError("level exceeds levelAudios array"); return; }

        levelAudios[level-1].GetComponents<AudioSource>()[index].Stop();

    }
}
