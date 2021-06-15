using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject[] levelAudios;
    private List<AudioSource[]> audioSources;
    public globalStateStore globalState;

    private void Awake()
    {
        if (globalState == null) globalState = GetComponent<globalStateStore>();

        audioSources = new List<AudioSource[]>();
        for(int l=0; l<levelAudios.Length; l++)
        {
            AudioSource[] clips = levelAudios[l].GetComponents<AudioSource>();
            audioSources.Add(clips);
        }
    }


    public void playSFX(int level, int index)
    {
        if (level > levelAudios.Length) { Debug.LogError("level exceeds levelAudios array"); return; }

        audioSources[level-1][index].Play();

    }


    public void stopSFX(int level, int index)
    {
        if (level > levelAudios.Length) { Debug.LogError("level exceeds levelAudios array"); return; }

        audioSources[level - 1][index].Stop();

    }

    public void playInstrumentTrackInSync(int instrumentIndex)
    {

        bool lastOne = (globalState.guitar && globalState.drums && globalState.accordion);

        int clipIndex = -1;
        switch (instrumentIndex)
        {
            case 0: clipIndex = 9; break;
            case 1: clipIndex = 11; break;
            case 2: clipIndex = 10; break;
        }

        if (lastOne)
        {//if last instrument, start everything from beginning 

            for(int s=9; s<12; s++)
            {
                audioSources[3][s].Stop();
                audioSources[3][s].time = 0;
                audioSources[3][s].loop = false;
                audioSources[3][s].volume = 0.8f;
                audioSources[3][s].Play();
            }
            
        }
        else
        {
            audioSources[3][clipIndex].time = Mathf.Max(audioSources[3][9].time, audioSources[3][10].time, audioSources[3][11].time);
            audioSources[3][clipIndex].Play();
        }
        
        
    }


}
