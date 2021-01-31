using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabler : MonoBehaviour
{
    public Animator mainCam, darkCover, credits;
    public imgSwitcher titleImg;
    public GameObject startCanvas;


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

    public void startGame()
    {
        StartCoroutine(startGameCoroutine());
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

        startCanvas.SetActive(false);
        darkCover.SetTrigger("fadeOut");
    }

    public void gamePass()
    {
        StartCoroutine(gamePassCoroutine());
    }

    IEnumerator gamePassCoroutine()
    {
        //return to title
        GetComponent<BlurManager>().darkCover.SetTrigger("fadeIn");
        startCanvas.SetActive(true);
        titleImg.switchToNextImgState();
        yield return new WaitForSeconds(3);
        GetComponent<BlurManager>().darkCover.SetTrigger("fadeOut");

    }

    public void showCredits()
    {
        credits.Play("textFade");
    }
}
