using UnityEngine;

public class PlayLevelAudio : MonoBehaviour
{
    GameObject gameControl;

    void Awake()
    {
        gameControl = GameObject.FindGameObjectWithTag("GameController");
    }

    public void playL1SFX(int index) //dine
    {
        print("before sound");
        gameControl.GetComponent<AudioManager>().playSFX(1, index);
        print("after sound");
    }

    public void playVaseSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(2, index);
    }

    public void playTreeSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(3, index);
    }

    public void playBandSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(4, index);
    }

    public void playSeaSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(5, index);
    }

    public void playPupSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(6, index);
    }

    public void playGardenSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(7, index);
    }

    public void playBickerSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(8, index);
    }

    public void playParkSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(9, index);
    }

    public void playGraveyardSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(10, index);
    }

    public void playHomeSceneSFX(int index)
    {
        gameControl.GetComponent<AudioManager>().playSFX(11, index);
    }

}
