using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabler : MonoBehaviour
{
    public Animator mainCam, darkCover, credits;
    public imgSwitcher titleImg;
    public GameObject startCanvas;
    public StartDialogueClickThrough startDialogue;

    public bool test;
    public int startLevel;

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



    //officially starts the game and goes to l1
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
        startCanvas.SetActive(false);

        mainCam.SetTrigger("idle");
        yield return new WaitForSeconds(1);

        setUpLevel(1);

        //at this point can't see title anymore so switch to ending state already 
        titleImg.switchToImgState(1);
        darkCover.SetTrigger("fadeOut");
        yield return new WaitForSeconds(2);
        
    }



    public void setUpLevel(int l)
    {
        StartCoroutine(setUpLevelCoroutine(l));
    }

    IEnumerator setUpLevelCoroutine(int l)
    {

        globalStateStore gs = GetComponent<globalStateStore>();
        gs.globalCounter = 0;
        if (l != 1) gs.revealAndHideStuff(l - 1, false);
        gs.revealAndHideStuff(l, true);

        if (l == 1) GetComponent<AudioManager>().playSFX(1, 7);
        if (l == 2) GetComponent<AudioManager>().audioL1.GetComponents<AudioSource>()[7].Stop();
        if (l == 3) GetComponent<AudioManager>().playSFX(3, 1);

        darkCover.SetTrigger("fadeOut");

        if (l == 3)
        {
            yield return new WaitForSeconds(7);
            GetComponent<BlurManager>().leaf.OnPointerClick(null);
        }
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
