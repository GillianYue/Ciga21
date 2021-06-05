using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public enabler enable;

    public bool test;
    public int startLevel;
    public int speedUpRate;

    void Start()
    {
        if (speedUpRate == 0) speedUpRate = 5;

        if (enable == null) enable = FindObjectOfType<enabler>();


        if (test && startLevel > 0)
        {
            enable.startCanvas.SetActive(false);
            enable.setUpLevel(startLevel);
        }
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