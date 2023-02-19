using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Tester : MonoBehaviour
{
    public enabler enable;

    public bool test, testMobile; //if on mobile but testing in unity editor, needs to enable testMobile
    public int startLevel;
    public float speedUpRate;

    public InputField goToLevelTF;

    public imgSwitcher swc;

    public GameObject mopubDebug;

    void Awake()
    {
        if (speedUpRate == 0) speedUpRate = 5;

        if (enable == null) enable = FindObjectOfType<enabler>();

        if (test)
        {
            mopubDebug.SetActive(true);

            if (enabler.isMobile()) testMobile = true;

            if (startLevel != -1) //将执行test里面填的测试关卡（-1=直接测试结局画面）
            {
                PlayerPrefs.SetInt("level", startLevel);

                for (int i = 0; i < 13; i++)
                {
                    //PlayerPrefs.SetInt("item" + i, 1);
                    PlayerPrefs.SetInt("item" + i, 0);
                }
            }
            else
            {
                //will trigger gamePass after waking
            }
        }
        else
        {
            mopubDebug.SetActive(false);
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

    void Start()
    {
        StartCoroutine(startCoroutine());
    }

    IEnumerator startCoroutine()
    {
        if (startLevel == -1) //if trying to test ending, trigger after 4 sec
        {
            yield return new WaitForSeconds(10);
            enable.gamePass();
        }
    }

    void Update()
    {
        if (test)
        {
            if (Input.GetKeyDown("space") ) //space for speeding time up
            {
                Time.timeScale = 1.0f * speedUpRate;
            }

            if (Input.GetKeyUp("space") ) //release to go back to normal time
            {
                Time.timeScale = 1.0f;
            }

/*            if (Input.GetKeyDown("s"))
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
            }*/
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


    public void goToLevel()
    {
        int lv = int.Parse(goToLevelTF.text);

        PlayerPrefs.SetInt("level", lv);

        StartCoroutine(enable.checkLoadLevel());
    }
}