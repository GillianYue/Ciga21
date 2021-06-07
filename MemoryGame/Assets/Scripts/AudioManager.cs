using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioL1, audioL2, audioL3;


    public void playSFX(int level, int index)
    {
        switch (level)
        {
            case 1:
                audioL1.GetComponents<AudioSource>()[index].Play();
                break;
            case 2:
                audioL2.GetComponents<AudioSource>()[index].Play();
                break;
            case 3:
                audioL3.GetComponents<AudioSource>()[index].Play();
                break;
        }
    }
}
