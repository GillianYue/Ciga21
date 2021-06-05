using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelAudio : MonoBehaviour
{
    GameObject gameControl;


    void Awake()
    {
        gameControl = GameObject.FindGameObjectWithTag("GameController");
    }

    public void playL1SFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(1, index);
    }

    public void playL2SFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(2, index);
    }

    public void playL3SFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(3, index);
    }
}
