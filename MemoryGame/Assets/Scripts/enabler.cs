﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//general game level logic 
public class enabler : MonoBehaviour
{
    public globalStateStore globalState;

    public Animator mainCam, darkCover, credits;

    public CamMovement cam;
    public imgSwitcher titleImg;
    public GameObject startCanvas;
    public StartDialogueClickThrough startDialogue;
    public BlurManager blurManager;

    public Animator[] startMenuFocusObjects; //photo, mdc and report

    public int language; //0 eng, 1 chn

    private void Awake()
    {
        if (cam == null) cam = FindObjectOfType<CamMovement>();
        if(globalState == null) globalState = GetComponent<globalStateStore>();
        if (blurManager == null) blurManager = GetComponent<BlurManager>();
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

        foreach(Animator a in startMenuFocusObjects)
        {
            a.GetComponent<Collider2D>().enabled = false;
        }
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

        mainCam.SetTrigger("idle"); //will reset cam's orthographic size and positions to (0,0) forcefully
        startCanvas.SetActive(false);

        yield return new WaitForSeconds(1);

        setUpLevel(1);

        //at this point can't see title anymore so switch to ending state already 
        titleImg.switchToImgState(1);
        darkCover.SetTrigger("fadeOut"); //enters scene
        yield return new WaitForSeconds(2);
        
    }

    public void setUpLevel(int l) { setUpLevel(l, false); }

    public void setUpLevel(int l, bool subScene)
    {
        StartCoroutine(setUpLevelCoroutine(l, subScene));
    }

    IEnumerator setUpLevelCoroutine(int l, bool subScene)
    {

        globalStateStore gs = GetComponent<globalStateStore>();
        gs.globalCounter = 0;
        if (l != 1) gs.revealAndHideStuff(l - 1, false, subScene); //hide stuff from prev lv
        gs.revealAndHideStuff(l, true, subScene); //show curr lv stuff

        switch (l)
        {
            case 1: //dine
                GetComponent<AudioManager>().playSFX(1, 7);

                darkCover.SetTrigger("fadeOut");
                break;
            case 2: //vase
                GetComponent<AudioManager>().stopSFX(1, 7);

                globalState.vaseScene.transform.Find("soccer").GetComponent<Animator>().SetTrigger("action1"); //start bounce

                darkCover.SetTrigger("fadeOut");
                break;
            case 3: //tree

                if (!subScene) { cam.cam.Play("idle"); cam.cam.SetTrigger("stopBreathe"); }//reset cam pos (only when transitioning from scene 2)
                if (subScene) gs.revealAndHideStuff(3, false, false); //hide main lv stuff

                darkCover.SetTrigger("fadeOut");
                break;
            case 4: //band
                cam.cam.SetTrigger("stopBreathe");
                darkCover.SetTrigger("fadeOut");
                break;

            case 5: //sea
                globalState.seaScene.transform.Find("beach/friend3").gameObject.SetActive(false);
                globalState.seaScene.transform.Find("hand_beer").gameObject.SetActive(false);

                yield return new WaitForSeconds(5);

                darkCover.SetTrigger("transparent");
                cam.vfx.Play("blinkOpenEyes");

                yield return new WaitForSeconds(16);

                cam.camHolder.enabled = true; //needs cam holder to be active 

                cam.camHolder.Play("camShiftRight"); //turn right
                yield return new WaitForSeconds(5);
                cam.vfx.Play("blink");

                yield return new WaitForSeconds(5f);
                cam.camHolder.Play("camShiftLeftBack"); //turn back

                Transform sun = globalState.seaScene.transform.Find("sea/dusk/sun/sunMask/sunImage");
                sun.GetComponent<interactable>().clickable = true; //enable sun interact


                break;
            case 6: //pup
                cam.camHolder.enabled = false;
                cam.cam.Play("idle");
                darkCover.SetTrigger("fadeOut");
                break;

            case 7: //garden
                if (!subScene)
                {
                    cam.edgeScroller = globalState.gardenScene.GetComponent<edgeScroller>();

                    darkCover.SetTrigger("fadeOut");

                    cam.GetComponent<MouseBasedCamShift>().active = false; //first turn off cam pan
                    cam.camHolder.GetComponent<Animator>().enabled = false;
                    globalState.globalClickable = false; //disable until opening done

                    cam.cam.Play("camGardenStart"); //art stare
                    yield return new WaitForSeconds(7);

                    cam.cam.Play("camGardenStartZoomOut");
                    yield return new WaitForSeconds(3);

                    //enable flyer pan
                    Transform flyer = globalState.gardenScene.transform.Find("center").Find("flyer");
                    flyer.GetComponent<MouseBasedCamShift>().active = true;
                    cam.GetComponent<MouseBasedCamShift>().active = true;

                    yield return new WaitForSeconds(4);

                    flyer.GetChild(0).GetComponent<Animator>().Play("flyerEntry");

                    yield return new WaitForSeconds(4);

                    Transform hand = globalState.gardenScene.transform.Find("center").Find("hand");

                    hand.gameObject.SetActive(true);
                    hand.GetComponent<Animator>().SetTrigger("action1"); //greet; will set global clickable true

                    yield return new WaitForSeconds(4);

                    cam.cam.Play("nervousBreathe");
                    cam.cam.Play("camGardenGazeShift");

                    yield return new WaitForSeconds(2);
                    cam.vfx.Play("blink2x");
                    yield return new WaitForSeconds(8);
                    flyer.gameObject.SetActive(false);
                    cam.cam.SetTrigger("stopBreathe");

                    globalState.globalClickable = true;
                }
                else
                {
                    gs.revealAndHideStuff(7, false, false); //hide main lv stuff
                    darkCover.SetTrigger("fadeOut");
                }
                break;
            case 8: //bicker

                yield return new WaitForSeconds(4);
                globalState.globalClickable = false;
                //TODO sfx

                yield return new WaitForSeconds(4);

                darkCover.SetTrigger("fadeOut");

                yield return new WaitForSeconds(6);

                globalState.globalClickable = true;

                //wait on click on her

                break;

            case 9: //park
                //setup
                Transform h = globalState.parkScene.transform.Find("hosp/Her");
                h.gameObject.SetActive(false);
                h.parent.gameObject.SetActive(false); 

                GetComponent<AudioManager>().playSFX(9, 1);

                yield return new WaitForSeconds(7);
                GetComponent<BlurManager>().leaf.onClick(); //will trigger leaf fall

                darkCover.SetTrigger("fadeOut");
                break;
            case 10: //graveyard
                cam.vfx.transform.Find("Noises").gameObject.SetActive(false);
                cam.cam.Play("camStormShake");
                weather w = globalState.graveyardScene.transform.Find("sky").GetComponent<weather>();
                w.startLightening();

                darkCover.gameObject.SetActive(false); //a sudden transition
                break;
            case 11: //home/mirror
                if (!subScene)
                {
                    cam.cam.Play("idle");
                    yield return new WaitForSeconds(2);

                    blurManager.centerBlur.setNewScale(2f, 0.3f); //initial blur

                    darkCover.SetTrigger("fadeOut");
                }
                else
                {
                    cam.cam.Play("idle");
                    yield return new WaitForSeconds(2);
                       //reset cam position
                    darkCover.SetTrigger("fadeOut");

                    //streets

                    //anim
                    //...

                    //dialogue
                    StartDialogueClickThrough dlg = globalState.streetScene.transform.Find("StartDialogue").GetComponent<StartDialogueClickThrough>();
                    dlg.gameObject.SetActive(true);
                    dlg.enableStartDialogue();
                }
                break;
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

    public void switchLanguage()
    {
        language = (language == 1) ? 0 : 1;

        textAutoLanguage[] activeTexts = FindObjectsOfType<textAutoLanguage>();

        foreach(textAutoLanguage t in activeTexts)
        {
            t.switchTextDisplayToCurrentLanguage(); //manual switch when button been clicked
        }
    }
}
