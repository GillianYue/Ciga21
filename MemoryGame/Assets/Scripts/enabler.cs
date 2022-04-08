using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//general game level logic 
public class enabler : MonoBehaviour
{
    [Inject(InjectFrom.Anywhere)]
    public globalStateStore globalState;

    public Animator mainCam, darkCover, credits, titleScreenBtfl;

    public CamMovement cam;
    public GameObject startCanvas;
    public StartDialogueClickThrough startDialogue;
    public BlurManager blurManager;

    public Animator[] startMenuFocusObjects; //photo, mdc and report

    public int language; //0 eng, 1 chn
    public delegate void LanguageChangeHandler();
    public event LanguageChangeHandler OnChangeLanguage;

    public AudioManager audio;

    public Tester test;

    public Animator headphoneScreen;

    #if UNITY_STANDALONE
        public SteamAchievements steamAchievements;
    #endif

    public GameObject memorabiliaUI, quitUIWindow, UICanvas, menuUIWindow, vfxCanvas, menuUIButton;

    public bool gameOnPause;

    public Animator[] capsuleHintTexts;

    public CapsuleHintTexts capsuleHintController;

    [Inject(InjectFrom.Anywhere)]
    public Memorabilia mm;

#if !UNITY_STANDALONE 
    [Inject(InjectFrom.Anywhere)]
    public MopubManager mopubManager;
#endif

    [Inject(InjectFrom.Anywhere)]
    public EntryManager entryManager;

    public Button startButton;

    private void Awake()
    {

        print("platform: " + Application.platform);

        Application.targetFrameRate = (isMobile())? 60:30;

        if (cam == null) cam = FindObjectOfType<CamMovement>();
        if (globalState == null) globalState = GetComponent<globalStateStore>();
        if (blurManager == null) blurManager = GetComponent<BlurManager>();
        if (audio == null) audio = GetComponent<AudioManager>();
        if (test == null) test = GetComponent<Tester>();
        if (!memorabiliaUI.activeSelf) memorabiliaUI.SetActive(true);
        if (menuUIButton.activeSelf) menuUIButton.SetActive(false);

#if !UNITY_STANDALONE
        if (mopubManager == null) mopubManager = GetComponent<MopubManager>();
#endif

#if UNITY_STANDALONE
        if (steamAchievements == null) steamAchievements = FindObjectOfType<SteamAchievements>();
#endif

        if (mm == null) mm = FindObjectOfType<Memorabilia>();

        language = PlayerPrefs.GetInt("language", 0);

        gameOnPause = false;

       // if (!test.test)
       // {

            headphoneScreen.gameObject.SetActive(true);
            headphoneScreen.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            headphoneScreen.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            headphoneScreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
       // }

        
    }

    void Start()
    {
        StartCoroutine(startSetupCoroutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openQuitUIWindow();
        }
    }

    public IEnumerator startSetupCoroutine()
    {

        globalState.globalClickable = false;



        globalState.audio.playSFX(0, 17, 0.1f); //ambience quiet
        globalState.audio.fadeVolumeSFX(0, 17, 5, 1);

       // if (!test.test)
       // {

            yield return new WaitForSeconds(2);

            //UICanvas.gameObject.SetActive(false); //hide UICanvas on start
            menuUIWindow.gameObject.SetActive(false);

            headphoneScreen.SetTrigger("fadeIn");

            yield return new WaitForSeconds(2);

            headphoneScreen.SetTrigger("fadeOut");

      //  }

        //show title screen
        startCanvas.transform.Find("photo/lines").gameObject.SetActive(true);
        startCanvas.transform.Find("photo/whiteout").gameObject.SetActive(false);
        startCanvas.transform.Find("photo/photo_content").gameObject.SetActive(false);

        yield return new WaitForSeconds(4.5f);

        startCanvas.SetActive(true);

        enableStartCanvasInteraction(); 

        yield return new WaitForSeconds(3);
        headphoneScreen.gameObject.SetActive(false);
    }

    public void enableStartCanvasInteraction()
    {
        startButton.interactable = true;
        globalState.globalClickable = true;

        titleScreenBtfl.Play("btflDropShadow"); //entry
    }

    public IEnumerator checkLoadLevel()
    {

        /*        if (!test.test)
                {*/
        int loadLv = PlayerPrefs.GetInt("level", 0);

        foreach (Animator a in startMenuFocusObjects)
        {
            a.GetComponent<interactable>().clickable = false;
        }

        if (loadLv > 0)
        {

            darkCover.gameObject.SetActive(true);
            darkCover.SetTrigger("opaque");

            yield return new WaitForSeconds(2);

            UICanvas.gameObject.SetActive(true);
            menuUIButton.gameObject.SetActive(true);

            globalState.audio.fadeVolumeSFX(0, 17, 2, 0);
            startCanvas.SetActive(false);
            setUpLevel(loadLv);
        }
        else
        { // start from beginning

            //if equal to 0, keep ambience
            PlayerPrefs.SetInt("level", 0);

            startDialogue.gameObject.SetActive(true);
            startDialogue.enableStartDialogue();

            yield return new WaitForSeconds(1);
            mm.gameObject.SetActive(true);
            mm.GetComponent<Animator>().SetTrigger("hide");
            mm.unlockItem(0);
        }
        /*        }
                else
                {
                    startCanvas.transform.Find("photo/lines").gameObject.SetActive(true);
                    startCanvas.transform.Find("photo/whiteout").gameObject.SetActive(false);
                    startCanvas.transform.Find("photo/photo_content").gameObject.SetActive(false);
                    globalState.audio.fadeVolumeSFX(0, 17, 2, 0);

                    yield return new WaitForSeconds(3);
                    globalState.globalClickable = true;
                }*/
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startButtonPressed()
    {

        if (gameOnPause)
        {//resume previously paused content

            //int loadLv = PlayerPrefs.GetInt("level", 0);

            //globalState.revealAndHideStuff(loadLv, true);

            UICanvas.gameObject.SetActive(true);
            menuUIButton.gameObject.SetActive(true);
            vfxCanvas.gameObject.SetActive(true);

            globalState.audio.fadeVolumeSFX(0, 17, 2, 0);
            startCanvas.SetActive(false);

            StartCoroutine(resumeGameCoroutine());

            foreach (Animator a in startMenuFocusObjects)
            {
                a.GetComponent<interactable>().clickable = false;
            }

            gameOnPause = false;
        }
        else
        {
            startButton.enabled = false;

#if !UNITY_STANDALONE 
            mopubManager.realnameAuth(); //will call loadLevel if success

#else
            StartCoroutine(checkLoadLevel());
#endif
        }

    }

    IEnumerator resumeGameCoroutine()
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(5);

        Time.timeScale = 1;
    }

    //officially starts the game and goes to l1
    public void startGame()
    {
        globalState.audio.fadeVolumeSFX(0, 17, 5, 0);

        StartCoroutine(startGameCoroutine());
        GetComponent<AudioSource>().Play();
    }

    IEnumerator startGameCoroutine()
    {

        mainCam.Play("startCamZoom");
        yield return new WaitForSeconds(3);

#if UNITY_STANDALONE
        //give steam achievement
        if (steamAchievements != null) steamAchievements.ach1();
#endif

        yield return new WaitForSeconds(2);

        darkCover.SetTrigger("fadeIn");
        yield return new WaitForSeconds(2);

        mainCam.SetTrigger("idle"); //will reset cam's orthographic size and positions to (0,0) forcefully
        startCanvas.SetActive(false);
        UICanvas.gameObject.SetActive(true);
        menuUIButton.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        setUpLevel(1);

        //at this point can't see title anymore so switch to ending state already 

        darkCover.SetTrigger("fadeOut"); //enters scene
        yield return new WaitForSeconds(2);

    }

    public void setUpLevel(int l) { setUpLevel(l, false); }

    public void setUpLevel(int l, bool subScene)
    {
        PlayerPrefs.SetInt("level", l); //saving progress

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

                yield return new WaitForSeconds(2);
                globalState.interactHint(true);
                globalState.globalClickable = true;
                break;
            case 2: //vase
                audio.fadeVolumeSFX(1, 7, 1, 0);
                audio.playSFX(2, 0, 0.2f); //vase break
                audio.fadeVolumeSFX(2, 0, 3, 1);

                yield return new WaitForSeconds(2);
                audio.playSFX(2, 14); //gloom
                audio.playSFX(2, 15); //mix
                audio.setVolumeSfx(2, 15, 0);

                globalState.vaseScene.transform.Find("soccer").GetComponent<Animator>().SetTrigger("action1"); //start bounce

                darkCover.SetTrigger("fadeOut");

                yield return new WaitForSeconds(2);
                globalState.globalClickable = true;

                break;
            case 3: //tree

                if (!subScene)
                {
                    cam.cam.Play("idle");
                    cam.cam.SetTrigger("stopBreathe");
                }//reset cam pos (only when transitioning from scene 2)

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

                if (!subScene) { 
                    darkCover.SetTrigger("fadeOut");
                }

                if (subScene)
                {
                    gs.revealAndHideStuff(3, false, false); //hide main lv stuff

                    
                }
                yield return new WaitForSeconds(2);

                globalState.globalClickable = true;
                break;
            case 4: //band

                NotesRecord.currActiveInstrument = -1;
                globalState.bandScene.transform.Find("SmartPhone").GetComponent<interactable>().affectsGO = this.gameObject;
                globalState.bandScene.transform.Find("speaker").GetComponent<interactable>().affectsGO = this.gameObject;
                globalState.bandScene.transform.Find("HintButton").GetComponent<Button>().interactable = false;

                yield return new WaitForSeconds(2);

                globalState.audio.fadeVolumeSFX(3, 9, 1, 0);

                yield return new WaitForSeconds(3);

                darkCover.SetTrigger("fadeOut");

                yield return new WaitForSeconds(2);
                globalState.globalClickable = true;
                break;

            case 5: //sea

                globalState.seaScene.transform.Find("beach/friend3").gameObject.SetActive(false);
                globalState.seaScene.transform.Find("hand_beer").gameObject.SetActive(false);

                yield return new WaitForSeconds(2);
                globalState.globalClickable = true;

                audio.playSFX(5, 4); //sunset
                audio.playSFX(5, 6); //waves

                yield return new WaitForSeconds(3);

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

                yield return new WaitForSeconds(2.5f);
                globalState.interactHint(true);

                break;
            case 6: //pup

                yield return new WaitForSeconds(4);

                audio.playSFX(6, 0, 0.1f); //panting
                audio.fadeVolumeSFX(6, 0, 5, 1);

                yield return new WaitForSeconds(4);

                cam.camHolder.enabled = false;
                cam.cam.Play("idle");
                darkCover.SetTrigger("fadeOut");

                yield return new WaitForSeconds(1);

                audio.playSFX(6, 6); //bgm

                yield return new WaitForSeconds(1);
                globalState.globalClickable = true;

                break;

            case 7: //garden

                if (!subScene)
                {
                    globalState.globalClickable = false;
                    globalState.audio.playSFX(7, 15);

                    cam.edgeScroller = globalState.gardenScene.GetComponent<edgeScroller>();

                    darkCover.SetTrigger("fadeOut");

                    cam.GetComponent<MouseBasedCamShift>().setActive(false); //first turn off cam pan
                    cam.camHolder.GetComponent<Animator>().enabled = false;

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

                    yield return new WaitForSeconds(5f);
                    globalState.audio.playSFX(7, 9, 0.1f);
                    globalState.audio.playSFX(7, 10, 0);
                    globalState.audio.playSFX(7, 13, 0.2f); //heartbeat
                    globalState.audio.fadeVolumeSFX(7, 13, 5, 0.3f);
                    globalState.audio.fadeVolumeSFX(7, 9, 8, 0.7f); //fade out bgm
                }
                break;
            case 8: //bicker
                globalState.globalClickable = false;

                yield return new WaitForSeconds(10);
                globalState.globalClickable = false;

                globalState.audio.playSFX(8, 0);

                yield return new WaitForSeconds(2);

                darkCover.SetTrigger("fadeOut");

                yield return new WaitForSeconds(2);

                globalState.audio.playSFX(8, 14);
                yield return new WaitForSeconds(4);

                globalState.globalClickable = true;

                //wait on click on her

                break;

            case 9: //park
                yield return new WaitForSeconds(5);

                //setup
                Transform h = globalState.parkScene.transform.Find("hosp/Her");
                h.gameObject.SetActive(false);
                h.parent.gameObject.SetActive(false);

                GetComponent<AudioManager>().playSFX(9, 1);

                yield return new WaitForSeconds(7);
                globalState.parkScene.transform.Find("Leaf").GetComponent<interactable>().onClick(); //will trigger leaf fall

                darkCover.SetTrigger("fadeOut");

                yield return new WaitForSeconds(2);
                globalState.globalClickable = true;

                break;
            case 10: //graveyard

                yield return new WaitForSeconds(5);

                globalState.audio.playSFX(10, 0); //wind
                globalState.audio.playSFX(10, 1); //storm

                globalState.graveyardScene.transform.Find("dark_cover").GetComponent<MouseBasedCamShift>().setActive(true);

                cam.vfx.transform.Find("Noises").gameObject.SetActive(false);
                cam.cam.Play("camStormShake");
                weather w = globalState.graveyardScene.transform.Find("sky").GetComponent<weather>();
                w.startLightening();

                darkCover.gameObject.SetActive(false); //a sudden transition

                globalState.globalClickable = true;

                break;
            case 11: //home/mirror
                if (!subScene)
                {
                    yield return new WaitForSeconds(4);

                    globalState.audio.playSFX(7, 14, 0); //basically mute cam shift sound

                    cam.cam.Play("idle");
                    blurManager.centerBlur.setNewScale(2f, 0.3f); //initial blur
                    yield return new WaitForSeconds(4);

                    globalState.audio.playSFX(11, 6, 0.2f);
                    globalState.audio.fadeVolumeSFX(11, 6, 3, 1);

                    darkCover.SetTrigger("fadeOut");

                    yield return new WaitForSeconds(2);
                    globalState.globalClickable = true;
                }
                else
                { //street
                    globalState.audio.fadeVolumeSFX(11, 6, 2, 0);
                    globalState.streetScene.transform.Find("lines/b1").GetComponent<Animator>().SetTrigger("transparent");

                    yield return new WaitForSeconds(3);

                    globalState.audio.playSFX(11, 8); //ambience

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
        globalState.globalClickable = false;

        //hide menu buttons
        startCanvas.transform.Find("TitleText").gameObject.SetActive(false);
        startCanvas.transform.Find("Start").gameObject.SetActive(false);
        startCanvas.transform.Find("Language").gameObject.SetActive(false);
        startCanvas.transform.Find("Credits").gameObject.SetActive(false);
        startCanvas.transform.Find("Quit").gameObject.SetActive(false);
        startCanvas.transform.Find("Privacy").gameObject.SetActive(false);
        startCanvas.transform.Find("UserAgreement").gameObject.SetActive(false);

        darkCover.SetTrigger("fadeOut");

        Transform her = globalState.mirrorScene.transform.Find("Her");
        her.gameObject.SetActive(true);
        her.GetComponent<Animator>().SetTrigger("fadeIn");
        yield return new WaitForSeconds(4);

        Transform hd = globalState.mirrorScene.transform.Find("MyHand");
        hd.gameObject.SetActive(true);
        hd.GetComponent<Animator>().SetTrigger("action1");

        yield return new WaitForSeconds(14);

        darkCover.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(3);

        //sfx, dawn
        globalState.audio.playSFX(0, 9, 0.2f);
        globalState.audio.fadeVolumeSFX(0, 9, 2, 1);

        yield return new WaitForSeconds(5);

        globalState.mirrorScene.SetActive(false);
        startCanvas.SetActive(true);
        startCanvas.transform.Find("photo/lines").gameObject.SetActive(false);
        startCanvas.transform.Find("photo/whiteout").gameObject.SetActive(true);

        //show photo content
        startCanvas.transform.Find("photo/photo_content").gameObject.SetActive(true);

        //sfx, get up + footsteps
        globalState.audio.playSFX(0, 10);
        globalState.audio.fadeVolumeSFX(0, 9, 5, 0);

        yield return new WaitForSeconds(15);

        startCanvas.SetActive(true);
        darkCover.SetTrigger("fadeOutWhite");

        yield return new WaitForSeconds(3);
        cam.vfx.Play("blink");

        yield return new WaitForSeconds(3);
        cam.cam.Play("endCameraZoom");

        yield return new WaitForSeconds(8);

        startCanvas.transform.Find("photo/whiteout").GetComponent<Animator>().Play("singleImageFadeOut");

        yield return new WaitForSeconds(2);

#if UNITY_STANDALONE
        steamAchievements.updateAch2Progress(11); //unlock achievement 2
#endif

        mm.gameObject.SetActive(true);
        mm.GetComponent<Animator>().SetTrigger("hide");
        mm.unlockItem(12);

        cam.vfx.transform.Find("sakura").gameObject.SetActive(true);
        globalState.audio.playSFX(0, 13); //memories

        yield return new WaitForSeconds(2);
        Transform endTitle = startCanvas.transform.Find("EndingTitle"), endCredits = startCanvas.transform.Find("EndingCredits"),
            thankYouNote = startCanvas.transform.Find("ThankYouNote");
        endTitle.gameObject.SetActive(true);

        endTitle.GetComponent<Animator>().SetTrigger("fadeInText");

        yield return new WaitForSeconds(8);
        endCredits.gameObject.SetActive(true);
        endCredits.GetComponent<Animator>().SetTrigger("fadeInText");

        yield return new WaitForSeconds(5);
        thankYouNote.gameObject.SetActive(true);
        thankYouNote.GetComponent<Animator>().SetTrigger("fadeInText");
        PlayerPrefs.SetInt("level", 0);

        yield return new WaitForSeconds(25);
        darkCover.SetTrigger("fadeInSlow");
        yield return new WaitForSeconds(8);

        Transform gpq = startCanvas.transform.Find("GamePassQuit");
        gpq.gameObject.SetActive(true);
        gpq.GetChild(0).GetComponent<Animator>().SetTrigger("fadeInText");

        //Application.Quit();
    }

    public void showCredits()
    {
        credits.Play("textFade");
    }

    public void switchLanguage()
    {
        language = (language == 1) ? 0 : 1;
        PlayerPrefs.SetInt("language", language);

        OnChangeLanguage(); //will trigger lang switch in all textAutoLanguage

        /*        textAutoLanguage[] activeTexts = FindObjectsOfType<textAutoLanguage>();

                foreach(textAutoLanguage t in activeTexts)
                {
                    t.switchTextDisplayToCurrentLanguage(); //manual switch when button been clicked
                }*/
    }

    public void resetAllProgressAndQuit()
    {
#if UNITY_STANDALONE
        steamAchievements.resetAchievements();
#endif

        PlayerPrefs.DeleteKey("level");

        Scene curr = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curr.name);
    }

    public void reloadCurrentLevel()
    {
        Scene curr = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curr.name);
    }

    public void openMemorabiliaUI()
    {
        globalState.globalUIClickOnly = true;
        Vector3 pos = mm.ContentGO.GetComponent<RectTransform>().anchoredPosition;
        pos.y = 0;
        mm.ContentGO.GetComponent<RectTransform>().anchoredPosition = pos;

        memorabiliaUI.GetComponent<interactable>().clickable = true;

        Time.timeScale = 0;

        memorabiliaUI.gameObject.SetActive(true);
        mm.randomizeItem1();
        memorabiliaUI.GetComponent<Animator>().SetTrigger("fadeIn");

        menuUIWindow.gameObject.SetActive(false);
    }

    public void closeMemorabiliaUI()
    {
        globalState.globalUIClickOnly = false;
        globalState.globalClickable = false;

        memorabiliaUI.GetComponent<interactable>().clickable = false;

        memorabiliaUI.GetComponent<Animator>().SetTrigger("fadeOut");
        Time.timeScale = 1;

        StartCoroutine(setMemorabiliaWindowActive(1, false));
    }

    IEnumerator setMemorabiliaWindowActive(float waitTime, bool active)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        if (mm && mm.enabled) mm.onDisplayItemOnClick();
        memorabiliaUI.gameObject.SetActive(active);

        globalState.globalClickable = true; //end animation enable global clickable
    }

    public void openQuitUIWindow()
    {
        globalState.globalUIClickOnly = true;

        quitUIWindow.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeQuitUIWindow()
    {
        globalState.globalUIClickOnly = false;

        quitUIWindow.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void openMenuUIWindow()
    {
        globalState.globalUIClickOnly = true;

        menuUIWindow.gameObject.SetActive(true);
        Time.timeScale = 0;

        capsuleHintController.updateCapsuleHint(capsuleHintTexts, language == 1, 0); //clear out hint texts
    }

    public void closeMenuUIWindow()
    {
        globalState.globalUIClickOnly = false;

        menuUIWindow.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void capsuleUIOnClick(int capsuleIndex)
    {
        globalState.menuCapsuleSelectedIndex = capsuleIndex;

        StartCoroutine(capsuleUIOnClickCoroutine(capsuleIndex));
    }

    IEnumerator capsuleUIOnClickCoroutine(int capsuleIndex)
    {
        //hide hint texts
        foreach (Animator t in capsuleHintTexts)
        {
            t.Play("transparentText");
        }

        yield return new WaitForSecondsRealtime(0.15f);

        //reset texts 
        capsuleHintController.updateCapsuleHint(capsuleHintTexts, language == 1, capsuleIndex);

        yield return new WaitForSecondsRealtime(0.15f);

        //show hint texts
        foreach (Animator t in capsuleHintTexts)
        {
            t.SetTrigger("fadeInTextFast");
        }
    }

    public void capsuleYes()
    {
        print("capsuling " + globalState.menuCapsuleSelectedIndex);

        switch (globalState.menuCapsuleSelectedIndex)
        {
            case 1: //reset curr lv
                reloadCurrentLevel();
                break;
            case 2: //reset all progress
                resetAllProgressAndQuit();
                break;
            case 3: //memorabilia
                openMemorabiliaUI();
                break;
            case 4: //return to main menu
                //int loadLv = PlayerPrefs.GetInt("level", 0);

                //globalState.revealAndHideStuff(loadLv, false);
                startCanvas.SetActive(true);
                foreach (Animator a in startMenuFocusObjects)
                {
                    a.GetComponent<interactable>().clickable = true;
                }

                titleScreenBtfl.Play("btflDropShadowIdle");
                UICanvas.gameObject.SetActive(false); //hide UICanvas

                gameOnPause = true;
                Time.timeScale = 0; //still pause time bc game still running

                break;
            case 5: //quit
                Application.Quit();
                break;

        }

        closeMenuUIWindow();
    }

    public void capsuleNo()
    {
        closeMenuUIWindow();

        //fade hint texts
        foreach (Animator t in capsuleHintTexts)
        {
            t.SetTrigger("fadeOutText");
        }

        globalState.menuCapsuleSelectedIndex = 0; //reset
        capsuleHintController.updateCapsuleHint(capsuleHintTexts, language == 1, 0);
    }

    public void buttonHover()
    {
        globalState.audio.playSFX(0, 12);
    }

    public void buttonSelect()
    {
        globalState.audio.playSFX(0, 11);
    }

    public static bool isMobile()
    {
        return SystemInfo.deviceType == DeviceType.Handheld;
    }

    public void openUrl(string url)
    {
        Application.OpenURL(url);
    }

}
