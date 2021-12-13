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
        for (int l = 0; l < levelAudios.Length; l++)
        {
            AudioSource[] clips = levelAudios[l].GetComponents<AudioSource>();
            audioSources.Add(clips);
        }
    }

    public void playSFX(int level, int index)
    {
        if (level > levelAudios.Length) { Debug.LogError("level exceeds levelAudios array"); return; }

        audioSources[level][index].Play();

    }

    public void playSFX(int level, int index, float vol)
    {
        if (level > levelAudios.Length) { Debug.LogError("level exceeds levelAudios array"); return; }

        //float origVol = audioSources[level][index].volume;
        audioSources[level][index].volume = vol;
        audioSources[level][index].Play();

    }

    public void stopSFX(int level, int index)
    {
        if (level > levelAudios.Length) { Debug.LogError("level exceeds levelAudios array"); return; }

        audioSources[level][index].Stop();

    }

    public void setVolumeSfx(int level, int index, float targetVolume) { audioSources[level][index].volume = targetVolume; }

    public void fadeVolumeSFX(int level, int index, float duration, float targetVolume)
    {
        StartCoroutine(fadeVolumeSFXCoroutine(audioSources[level][index], duration, targetVolume));
    }

    IEnumerator fadeVolumeSFXCoroutine(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float startVol = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVol, targetVolume, currentTime / duration);
            yield return null;
        }

        if (targetVolume == 0) audioSource.Stop();
        yield break;
    }

    public void playInstrumentTrackInSync(int instrumentIndex)
    {
        int bandSceneIndex = 4;

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

            for (int s = 9; s < 12; s++)
            {
                audioSources[bandSceneIndex][s].Stop();
                audioSources[bandSceneIndex][s].time = 0;
                audioSources[bandSceneIndex][s].loop = false;
                audioSources[bandSceneIndex][s].volume = 0.8f;
                audioSources[bandSceneIndex][s].Play();
            }

        }
        else
        {
            audioSources[bandSceneIndex][clipIndex].time = Mathf.Max(audioSources[bandSceneIndex][9].time, audioSources[bandSceneIndex][10].time, audioSources[bandSceneIndex][11].time);
            audioSources[bandSceneIndex][clipIndex].Play();
        }

    }

}
