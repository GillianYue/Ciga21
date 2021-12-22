using UnityEngine;

public class Tester : MonoBehaviour
{
    public enabler enable;

    public bool test;
    public int startLevel;
    public float speedUpRate;

    public imgSwitcher swc;

    void Awake()
    {
        if (speedUpRate == 0) speedUpRate = 5;

        if (enable == null) enable = FindObjectOfType<enabler>();

        if (test)
        {
            PlayerPrefs.SetInt("level", startLevel);

            for(int i=0; i<13; i++)
            {
                PlayerPrefs.SetInt("item" + i, 1);
            }
        }

        /*        if (test && startLevel > 0)
                {
                    //enable.startCanvas.SetActive(false);
                    //enable.setUpLevel(startLevel);

                }
                else if(test && startLevel == 0)
                {
                    //if equal to 0
                    PlayerPrefs.SetInt("level", 0);
                    enable.startCanvas.SetActive(true);
                }*/
    }

    void Update()
    {
        if (test)
        {
            if (Input.GetKeyDown("space")) //space for speeding time up
            {
                Time.timeScale = 1.0f * speedUpRate;
            }

            if (Input.GetKeyUp("space")) //release to go back to normal time
            {
                Time.timeScale = 1.0f;
            }

            if (Input.GetKeyDown("s"))
            {
                Time.timeScale = 0.1f;
            }

            if (Input.GetKeyUp("s"))
            {
                Time.timeScale = 1.0f;
            }

            if (Input.GetKeyDown("t"))
            {
                swc.switchToNextImgState();
            }
        }



    }

    public static void pauseGame()
    {
        Time.timeScale = 0;
    }

    public static void resumeGame()
    {
        Time.timeScale = 1;
    }
}