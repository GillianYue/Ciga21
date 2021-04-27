﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabler : MonoBehaviour
{
    public Animator mainCam, darkCover, credits;
    public imgSwitcher titleImg;
    public GameObject startCanvas;
    public StartDialogueClickThrough startDialogue;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startButtonPressed()
    {
        startDialogue.gameObject.SetActive(true);
        startDialogue.enableStartDialogue();
    }

    public void startGame()
    {
        StartCoroutine(startGameCoroutine());
        GetComponent<AudioSource>().Play();
    }

    IEnumerator startGameCoroutine()
    {
        mainCam.Play("startCamZoom");
        yield return new WaitForSeconds(5);
        darkCover.SetTrigger("fadeIn");
        yield return new WaitForSeconds(2);
        mainCam.SetTrigger("idle");
        yield return new WaitForSeconds(1);

        GetComponent<BlurManager>().setUpLevel(1);

        titleImg.switchToImgState(1);
        startCanvas.SetActive(false);
        darkCover.SetTrigger("fadeOut");
    }

    public void gamePass()
    {
        StartCoroutine(gamePassCoroutine());
    }

    IEnumerator gamePassCoroutine()
    {
        yield return new WaitForSeconds(5);
        startCanvas.SetActive(true);
        startCanvas.transform.position += new Vector3(0, 0, 0.15f);
        yield return new WaitForSeconds(3);
        GetComponent<BlurManager>().darkCover.SetTrigger("fadeOut");

    }

    public void showCredits()
    {
        credits.Play("textFade");
    }
}
