using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//general game level logic 
public class enabler : MonoBehaviour
{
    public Animator mainCam, darkCover, credits;
    public CamMovement cam;
    public imgSwitcher titleImg;
    public GameObject startCanvas;
    public StartDialogueClickThrough startDialogue;

    private void Awake()
    {
        if (cam == null) cam = FindObjectOfType<CamMovement>();
    }

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
        if (l != 1) gs.revealAndHideStuff(l - 1, false); //hide stuff from prev lv
        gs.revealAndHideStuff(l, true); //show curr lv stuff

        switch (l)
        {
            case 1: //dine
                GetComponent<AudioManager>().playSFX(1, 7);

                break;
            case 2: //vase
                GetComponent<AudioManager>().stopSFX(1, 7);


                break;
            case 3: //tree

                break;
            case 4: //band

                break;

            case 5: //sea

                break;
            case 6: //pup

                break;

            case 7: //garden

                break;
            case 8: //bicker

                break;

            case 9: //park
                GetComponent<AudioManager>().playSFX(9, 1);

                yield return new WaitForSeconds(7);
                GetComponent<BlurManager>().leaf.OnPointerClick(null);
                break;
            case 10: //graveyard

                break;
            case 11: //home/mirror

                break;
        }


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
