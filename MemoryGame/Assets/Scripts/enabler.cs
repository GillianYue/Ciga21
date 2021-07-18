using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public AudioManager audio;

    private void Awake()
    {
        if (cam == null) cam = FindObjectOfType<CamMovement>();
        if(globalState == null) globalState = GetComponent<globalStateStore>();
        if (blurManager == null) blurManager = GetComponent<BlurManager>();
        if (audio == null) audio = GetComponent<AudioManager>();
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
                audio.playSFX(1, 7);

                darkCover.SetTrigger("fadeOut");
                break;
            case 2: //vase
                audio.fadeVolumeSFX(1, 7, 1, 0);
                audio.playSFX(2, 0); //vase break

                yield return new WaitForSeconds(2);
                audio.playSFX(2, 14); //gloom
                audio.playSFX(2, 15); //mix
                audio.setVolumeSfx(2, 15, 0);

                globalState.vaseScene.transform.Find("soccer").GetComponent<Animator>().SetTrigger("action1"); //start bounce

                darkCover.SetTrigger("fadeOut");
                break;
            case 3: //tree
                audio.fadeVolumeSFX(2, 15, 1, 0);
                yield return new WaitForSeconds(1);

                if (!subScene)
                {
                    audio.playSFX(3, 10);
                    audio.playSFX(3, 11);
                }
                else
                {
                    audio.fadeVolumeSFX(3, 10, 2, 0);
                    audio.fadeVolumeSFX(3, 11, 2, 0);
                }

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

                    cam.GetComponent<MouseBasedCamShift>().setActive(false); //first turn off cam pan
                    cam.camHolder.GetComponent<Animator>().enabled = false;
                    globalState.globalClickable = false; //disable until opening done

                    cam.cam.Play("camGardenStart"); //art stare
                    yield return new WaitForSeconds(7);

                    cam.cam.Play("camGardenStartZoomOut");
                    yield return new WaitForSeconds(3);

                    //enable flyer pan
                    Transform flyer = globalState.gardenScene.transform.Find("center").Find("flyer");
                    flyer.GetComponent<MouseBasedCamShift>().setActive(true);
                    

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
                    cam.GetComponent<MouseBasedCamShift>().setActive(true);
                    cam.cam.SetTrigger("stopBreathe");

                    globalState.globalClickable = true;
                }
                else
                {
                    cam.mouseBasedCamShift.setActive(true);
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
                globalState.graveyardScene.transform.Find("dark_cover").GetComponent<MouseBasedCamShift>().setActive(true);

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
                { //street
                    cam.cam.Play("idle");
                    yield return new WaitForSeconds(2);
                       //reset cam position
                    darkCover.SetTrigger("fadeOut");
                    yield return new WaitForSeconds(2.5f);

                    //streets
                    Transform lines = globalState.streetScene.transform.Find("lines");
                    Animator a = lines.GetComponent<Animator>();
                    a.Play("linesFadeIn");

                    yield return new WaitForSeconds(2);
                    a.Play("streetIn");


                    //anim
                    //...
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
        darkCover.SetTrigger("fadeOut");

        Transform her = globalState.mirrorScene.transform.Find("Her");
        her.gameObject.SetActive(true);
        her.GetComponent<Animator>().SetTrigger("fadeIn");
        yield return new WaitForSeconds(4);

        Transform hd = globalState.mirrorScene.transform.Find("MyHand");
        hd.gameObject.SetActive(true);
        hd.GetComponent<Animator>().SetTrigger("action1");

        yield return new WaitForSeconds(14);

        //sfx
        darkCover.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(3);
        //sfx

        globalState.mirrorScene.SetActive(false);
        startCanvas.SetActive(true);
        startCanvas.transform.Find("photo/lines").gameObject.SetActive(false);
        //hide menu buttons
        startCanvas.transform.Find("Start").gameObject.SetActive(false);
        startCanvas.transform.Find("Language").gameObject.SetActive(false);
        startCanvas.transform.Find("Credits").gameObject.SetActive(false);
        startCanvas.transform.Find("Quit").gameObject.SetActive(false);
        startCanvas.transform.Find("TitleText").GetComponent<Text>().color = new Color(1, 1, 1, 0);

        //show photo content
        startCanvas.transform.Find("photo/photo_content").gameObject.SetActive(true);


        yield return new WaitForSeconds(5);
        startCanvas.SetActive(true);
        darkCover.SetTrigger("fadeOutWhite");

        yield return new WaitForSeconds(3);
        cam.cam.Play("endCameraZoom");

        yield return new WaitForSeconds(10);
        Transform endTitle = startCanvas.transform.Find("EndingTitle"), endCredits = startCanvas.transform.Find("EndingCredits");
        endTitle.gameObject.SetActive(true); 

        endTitle.GetComponent<Animator>().SetTrigger("fadeInText");

        yield return new WaitForSeconds(6);
        endCredits.gameObject.SetActive(true);
        endCredits.GetComponent<Animator>().SetTrigger("fadeInText");
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
