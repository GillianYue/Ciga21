using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class interactable : MonoBehaviour
{
    private int timesClicked = 0; //this should keep track of effective clicks
    public bool clickable = true;
    Animator myAnimator;
    public int numInteractions;
    public GameObject changeIntoPrefab, affectsGO; //applies when type is animThenImgChange

    public enum InteractType { animThenImgChange, anim, imgSwitcher, instrument, clickInspect, custom, mmItem };
    public InteractType interactType;

    public GameObject gameControl;
    public CamMovement camMovement;
    public MouseControl mouseControl;
    public globalStateStore globalState;

    public int var1, var2, var3; //custom variables to keep track of obj states, usage depends on specific case

    protected virtual void Awake()
    {
        myAnimator = GetComponent<Animator>();

        if (gameControl == null) gameControl = GameObject.FindGameObjectWithTag("GameController");
        if (mouseControl == null) mouseControl = gameControl.GetComponent<MouseControl>();
        if (globalState == null) globalState = gameControl.GetComponent<globalStateStore>();
        if (camMovement == null) camMovement = FindObjectOfType<CamMovement>();

        var1 = 0; var2 = 0; var3 = 0;

        tag = "Interactable"; //tagging all interactable obj
    }

    void Start()
    {
        //randomly delayed anim start for lawn flowers

        if (name[0] == 'f' && transform.parent.name.Equals("flowers"))
        {
            StartCoroutine(Global.Chain(this,
                    Global.WaitForSeconds(Random.Range(0f, 2f)),
                    Global.Do(() =>
                    {
                        if (var1 == 0) //if flower already clicked before this, will set var1 to 1
                            myAnimator.Play("lawnFloIdle");
                    })));

        }
        else if (transform.childCount > 0 && transform.GetChild(0).name.Equals("plant"))
        {
            StartCoroutine(Global.Chain(this,
                    Global.WaitForSeconds(Random.Range(0f, 2f)),
                    Global.Do(() =>
                    {
                        GetComponent<Animator>().Play("plant1");
                    })));
        }

        else if (name.Equals("ballAnimator") || name.Equals("stickAnimator"))
        {
            myAnimator.SetTrigger("action1"); //idle state
        }
        else if (name.Equals("img") && transform.parent.parent.name.Equals("objects"))
        {
            StartCoroutine(Global.Chain(this,
        Global.WaitForSeconds(Random.Range(0f, 3f)),
        Global.Do(() =>
        {
            GetComponent<Animator>().Play("darkCol");
        })));

            StartCoroutine(Global.Chain(this,
                Global.WaitForSeconds(Random.Range(0f, 2f)),
                Global.Do(() =>
                {
                    transform.parent.GetComponent<Animator>().Play("posGlitch");
                })));

        }
        else if (name.Length > 4 && name.Substring(0, 4).Equals("leaf") && name[4] != '3' && !name.Equals("leafLine"))
        {
            StartCoroutine(Global.Chain(this,
                Global.WaitForSeconds(Random.Range(0f, 2f)),
                Global.Do(() =>
                {
                    GetComponent<Animator>().SetTrigger("action2");
                })));
        }

    }

    void Update()
    {

    }

    //can be overridden though not necessary
    public virtual void onClick()
    {
        //   print("clicked: " + eventData.pointerPress.name);

        if (clickable)
        {
            timesClicked += 1;
            checkBehavior();
        }
    }

    public virtual void onEnter()
    {
        // if (clickable) mouseControl.toHand();
    }

    public virtual void onExit()
    {
        //  if (clickable) mouseControl.toMouse();
    }

    public void fadeIn()
    {
        myAnimator.SetTrigger("fadeIn");
    }

    public void fadeOut()
    {
        myAnimator.SetTrigger("fadeOut");
    }

    public static void fadeInSth(Animator a)
    {
        a.SetTrigger("fadeIn");
    }

    public static void fadeOutSth(Animator a)
    {
        a.SetTrigger("fadeOut");
    }

    //should be overidden by children?
    public void checkBehavior()
    {

        switch (interactType)
        {
            case InteractType.anim:
                myAnimator.SetTrigger("action1"); //play animation (only one in total)
                break;
            case InteractType.animThenImgChange: //play animation (multiple in total)
                if (timesClicked <= numInteractions)
                {
                    myAnimator.SetTrigger("action" + timesClicked.ToString());

                    if (transform.parent.name.Equals("rosesRotate"))
                    {
                        clickable = false;
                        globalState.rosesRotate += 1;
                        if (globalState.rosesRotate >= 6)
                        {
                            Transform hosp = globalState.parkScene.transform.Find("hosp");
                            hosp.gameObject.SetActive(true);
                            hosp.GetComponent<animEventLink>().rosesFadeOut();
                        }
                    }
                }
                break;
            case InteractType.imgSwitcher: //change base images to the next pair (with effects)
                GetComponent<imgSwitcher>().myTriggerAction();
                break;
            case InteractType.instrument:
                if (gameControl.GetComponent<globalStateStore>().globalCounter < 2) //phone and speaker not triggered sprite swap yet
                {
                    //play sound effect
                    switch (name)
                    {
                        case "guitar":
                            gameControl.GetComponent<AudioManager>().playSFX(4, 0);
                            break;
                        case "drums":
                            gameControl.GetComponent<AudioManager>().playSFX(4, 1);
                            break;
                        case "accordion":
                            gameControl.GetComponent<AudioManager>().playSFX(4, 2);
                            break;
                    }
                    timesClicked -= 1; //reset the past click
                }
                else
                {
                    //start puzzle session; if already activated, cancel all and replay solution
                    NotesRecord notesRecord = GetComponent<NotesRecord>();

                    //play sound effect (solution)
                    notesRecord.playSolution();

                    //if starts another instrument, will deactivate prev instrument
                    NotesRecord.currActiveInstrument = notesRecord.instrumentIndex;

                    notesRecord.enableNotes(); //reveal notes for this instrument if prev hidden

                    notesRecord.resetAllNoteStatus(); //reset status

                    if (!globalState.bandHintButtonPresented)
                    {
                        globalState.bandHintButtonPresented = true;
                        globalState.bandScene.transform.Find("HintButton").GetComponent<Animator>().Play("hintButtonEnlarge");
                    }
                }

                break;
            case InteractType.clickInspect: //start canvas obj
                //focus on object clicked (cam focus + pos shift)
                CamMovement cam = gameControl.GetComponent<enabler>().cam;
                cam.camFocusOnObject(transform.position);

                break;
            case InteractType.mmItem:
                //script attached on itemImg
                MemorabiliaItem mmItm = transform.parent.GetComponent<MemorabiliaItem>();
                mmItm.itemOnClick();
                break;
            case InteractType.custom:
                StartCoroutine(customBehavior());

                break;

        }

    }

    //in some cases called from the end of animation; fades out current sprite and fades in newly instantiated changeIntoPrefab (e.g. butterfly -> pepper, smartphone -> telephone)
    public GameObject swapSpriteToTarget()
    {
        GameObject target = Instantiate(changeIntoPrefab, transform.parent, false);
        target.transform.localPosition = transform.localPosition;
        target.SetActive(false);
        gameControl.GetComponent<BlurManager>().centerBlur.transform.SetAsLastSibling();
        setChildrenInvisible(target);

        myAnimator.SetTrigger("fadeOut");
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false; //if has collider, shouldn't exist anymore at this point

        target.GetComponent<Animator>().SetTrigger("fadeIn");
        return target;
    }

    public void swapSpriteToTargetAndSetStateToOne()
    {
        GameObject target = swapSpriteToTarget();
        target.GetComponent<Animator>().SetInteger("state", 1);
    }

    //triggers actions that should be done on other game objects, action triggered based on availability of certain scripts
    public void triggerTargetAction()
    {
        if (affectsGO.GetComponent<interactable>() != null)
        {
            affectsGO.GetComponent<interactable>().myTriggerAction();
        }
        else if (affectsGO.GetComponent<imgSwitcher>() != null)
        {
            affectsGO.GetComponent<imgSwitcher>().myTriggerAction();
        }
        else
        {
            affectsGO.GetComponent<globalStateStore>().myTriggerAction();
        }
    }

    //called in animations to prevent new actions triggered during animations
    public void setClickableTrue() { clickable = true; }

    public void setClickableFalse() { clickable = false; }

    public void setGlobalClickableTrue() { globalState.globalClickable = true; }

    public void setGlobalClickableFalse() { globalState.globalClickable = false; }

    static void setChildrenInvisible(GameObject obj)
    {
        Color col0 = obj.transform.GetChild(0).GetComponent<Image>().color,
            col1 = obj.transform.GetChild(1).GetComponent<Image>().color;

        obj.transform.GetChild(0).GetComponent<Image>().color = new Color(col0.r, col0.g, col0.b, 0);
        obj.transform.GetChild(1).GetComponent<Image>().color = new Color(col1.r, col1.g, col1.b, 0);

        obj.SetActive(true);
    }

    //this gets called by other interactables, indicates some activity, should be overidden
    public void myTriggerAction()
    {
        switch (name)
        {
            case "Snakes":
                StartCoroutine(snakeDisappear());

                break;
            case "Her":
                myAnimator.Play("awayFadeIn");
                break;
            case "leaf3":
                myAnimator.SetTrigger("action2");
                var1 = 1; //signals worm ready to come out by end of this clip
                break;

        }
    }

    //gets called at the end of each leaf fall (except l3)
    public void checkSquirrelAppearance()
    {
        interactable sqr = globalState.treeScene.transform.Find("squirrel").GetComponent<interactable>();
        sqr.var1 += 1;

        if (sqr.var1 >= 4)
        {
            sqr.onClick();
        }
    }

    //gets called at the end of leaf3's action2 clip
    public void checkWormAppearance()
    {
        if (var1 == 1)
        {
            clickable = false;
            affectsGO.gameObject.SetActive(true);
            affectsGO.GetComponent<Animator>().SetTrigger("action1");
        }
    }

    public void playRandomCoughSound()
    {
        globalState.audio.playSFX(9, Random.Range(25, 28));
    }

    IEnumerator snakeDisappear()
    {
        myAnimator.SetTrigger("action1"); //hiss
        yield return new WaitForSeconds(2);
        GameObject pasta = swapSpriteToTarget();
        yield return new WaitForSeconds(3);
        gameControl.GetComponent<BlurManager>().scene1Clear1();

        yield return new WaitForSeconds(3);
        globalState.l1Scene.transform.Find("Butterfly").GetComponent<interactable>().clickable = true;
    }

    public void instrumentStartPlaying()
    {
        globalStateStore gs = gameControl.GetComponent<globalStateStore>();

        myAnimator.SetInteger("state", 2);
        gs.globalCounter += 1;

        switch (name)
        {
            case "guitar":
                gs.guitar = true;
                gameControl.GetComponent<AudioManager>().playInstrumentTrackInSync(0);

                break;
            case "drums":
                gs.drums = true;
                gameControl.GetComponent<AudioManager>().playInstrumentTrackInSync(1);

                break;

            case "accordion":
                gs.accordion = true;
                gameControl.GetComponent<AudioManager>().playInstrumentTrackInSync(2);

                break;
        }

        if (gs.drums && gs.guitar && gs.accordion && !gs.hasScrolled) //everything triggered for l2, trigger scroll effect
        {
            StartCoroutine(bandWaitTillSongEnd());
        }
    }

    public IEnumerator bandWaitTillSongEnd()
    {
        globalState.toggleAnimationGlobalClickable(false);
        yield return new WaitForSeconds(24); //entirety of loop

        globalState.audio.playSFX(4, 18); //end of lv song

        

        gameControl.GetComponent<BlurManager>().levelPassEffect(4);
        gameControl.GetComponent<globalStateStore>().hasScrolled = true;
    }

    public void awayFadeInFinished()
    {
        clickable = true;
        timesClicked = 1;
    }

    public void lookOverFinished()
    {
        //gameControl.GetComponent<BlurManager>().levelPassEffect(9);

        StartCoroutine(lookOverFinishedCoroutine());

    }

    IEnumerator lookOverFinishedCoroutine()
    {
        globalState.toggleAnimationGlobalClickable(false);
        camMovement.cam.Play("naturalBreathe");

        //some setup
        Animator her = globalState.parkScene.transform.Find("Her").GetComponent<Animator>(),
            hand2 = globalState.parkScene.transform.Find("MyHand2").GetComponent<Animator>(),
            hand3 = globalState.parkScene.transform.Find("MyHand3").GetComponent<Animator>();

        Transform hosp = globalState.parkScene.transform.Find("hosp"), glitch = globalState.parkScene.transform.Find("glitch");
        hosp.gameObject.SetActive(true);
        Transform screen = hosp.Find("screen");
        screen.gameObject.SetActive(false);

        yield return new WaitForSeconds(8);
        her.Play("cough"); //will auto-transition to next state

        yield return new WaitForSeconds(7);

        camMovement.cam.Play("camTiltHead");

        yield return new WaitForSeconds(2);

        her.Play("fallAsleep");

        yield return new WaitForSeconds(5);

        camMovement.vfx.Play("blink");

        yield return new WaitForSeconds(2);

        hand2.gameObject.SetActive(true);
        hand2.SetTrigger("action2"); //offer flower

        yield return new WaitForSeconds(7);

        camMovement.vfx.Play("blink");

        yield return new WaitForSeconds(4);
        globalState.audio.fadeVolumeSFX(9, 1, 3, 0.2f);

        camMovement.cam.Play("naturalBreathe");
        yield return new WaitForSeconds(4);

        hosp.Find("bg").GetComponent<Animator>().Play("hspFadeAction1"); //fade in a bit and away

        yield return new WaitForSeconds(4);

        screen.gameObject.SetActive(true);
        Animator sr = screen.Find("screenR").GetComponent<Animator>();
        sr.gameObject.SetActive(true);
        sr.Play("screenDistantEnter");
        globalState.audio.playSFX(9, 10, 0.1f);
        globalState.audio.fadeVolumeSFX(9, 10, 8, 1f);
        sr.Play("screenHeightFluctuate");

        //blur effect in and out
        yield return new WaitForSeconds(1.5f);
        globalState.blurManager.centerBlur.setNewScale(3, 0.1f);
        yield return new WaitForSeconds(2);
        globalState.blurManager.centerBlur.setNewScale(0.2f, 0.1f);
        yield return new WaitForSeconds(2.2f);
        //blocking lines in 
        Transform lz = her.transform.Find("linesAway");
        her.Play("herLinesAway"); //toggle GO on
        lz.GetComponent<Animator>().SetTrigger("fadeIn");

        globalState.blurManager.centerBlur.setNewScale(2.7f, 0.1f);
        yield return new WaitForSeconds(2.5f);
        globalState.blurManager.centerBlur.setNewScale(0.5f, 0.1f);

        yield return new WaitForSeconds(5);

        //hand block
        hand3.gameObject.SetActive(true);
        hand3.SetTrigger("action3"); //block anim

        yield return new WaitForSeconds(5.5f);
        hosp.Find("bg").GetComponent<Animator>().Play("hspFadeAction1");

        yield return new WaitForSeconds(2);

        camMovement.vfx.Play("blink");

        yield return new WaitForSeconds(4);
        //bgm
        globalState.audio.playSFX(9, 22);

        //effects, insert glitch 
        her.Play("transparent");
        glitch.gameObject.SetActive(true);
        Animator g = glitch.Find("her").GetComponent<Animator>();
        g.Play("glitch1");

    }

    public void playSfx(string levelUnderlineIndex) //e.g. 11_2
    {
        string[] splits = levelUnderlineIndex.Split('_');
        int lv = int.Parse(splits[0]), sfx = int.Parse(splits[1]);
        globalState.audio.playSFX(lv, sfx);
    }

    IEnumerator customBehavior()
    {
        switch (name)
        {
            case "soccer":
                if (var1 == 0)
                {
                    globalState.audio.playSFX(2, 2);
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        myAnimator.SetTrigger("action" + ((Random.Range(0f, 1f) < 0.5f) ? "2" : "3")); //slight bounce or rotate

                    }
                }
                else if (var1 == 1)
                {//hiding it behind the couch

                    myAnimator.SetTrigger("action4"); // roll away

                    globalState.toggleAnimationGlobalClickable(false);

                    yield return new WaitForSeconds(7);

                    //sfx, footstep, muffled sound
                    globalState.audio.playSFX(2, 7);
                    GameObject ma = globalState.vaseScene.transform.Find("mom").gameObject,
                    blkt = globalState.vaseScene.transform.Find("blanket").gameObject,
                    vase = globalState.vaseScene.transform.Find("broken_vase").gameObject,
                    plate = globalState.vaseScene.transform.Find("plate").gameObject,
                    nurse = globalState.vaseScene.transform.Find("nurse").gameObject,
                    sofa = globalState.vaseScene.transform.Find("sofa").gameObject;

                    yield return new WaitForSeconds(8);

                    ma.SetActive(true);
                    ma.GetComponent<Animator>().SetTrigger("fadeIn");
                    globalState.audio.playSFX(2, 8);

                    yield return new WaitForSeconds(2);

                    yield return new WaitForSeconds(3);

                    ma.GetComponent<imgSwitcher>().switchToNextImgState(); //head tilt
                    yield return new WaitForSeconds(2);
                    ma.GetComponent<imgSwitcher>().switchToImgState(0);
                    yield return new WaitForSeconds(3);

                    globalState.audio.playSFX(2, 3, 0.2f); //nervous breathing
                    globalState.audio.fadeVolumeSFX(2, 3, 3, 1);
                    globalState.audio.playSFX(0, 15, 0.5f); //faint heartbeats

                    blkt.transform.Find("mask1").gameObject.SetActive(true);
                    blkt.GetComponent<Animator>().SetTrigger("action1"); //mask anim

                    yield return new WaitForSeconds(6);

                    ma.GetComponent<imgSwitcher>().switchToImgState(1); //head tilt
                    yield return new WaitForSeconds(3);
                    ma.GetComponent<imgSwitcher>().switchToImgState(2); //reach
                    yield return new WaitForSeconds(1);
                    blkt.GetComponent<Animator>().SetTrigger("fadeOut");
                    globalState.audio.playSFX(2, 5);
                    yield return new WaitForSeconds(2);

                    //unveil
                    ma.GetComponent<imgSwitcher>().switchToImgState(1);

                    yield return new WaitForSeconds(3);

                    ma.GetComponent<imgSwitcher>().switchToImgState(0); //questioning look

                    //"me" getting scared
                    yield return new WaitForSeconds(3f);
                    camMovement.vfx.Play("blink2x");
                    yield return new WaitForSeconds(0.5f);
                    camMovement.cam.Play("leftRightGlance");
                    yield return new WaitForSeconds(2.2f);

                    camMovement.cam.Play("nervousBreathe");
                    yield return new WaitForSeconds(7);

                    //flash
                    globalState.audio.playSFX(0, 14); //glitch sfx
                    ma.SetActive(false);
                    vase.SetActive(false);
                    sofa.SetActive(false);
                    nurse.SetActive(true);
                    plate.SetActive(true);
                    yield return new WaitForSeconds(0.2f);

                  //  print("s1");

                    ma.SetActive(true);
                    vase.SetActive(true);
                    sofa.SetActive(true);
                    nurse.SetActive(false);
                    plate.SetActive(false);
                    yield return new WaitForSeconds(2f);

                 //   print("s2");

                    globalState.audio.playSFX(0, 14);
                    ma.SetActive(false);
                    vase.SetActive(false);
                    sofa.SetActive(false);
                    nurse.SetActive(true);
                    plate.SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    ma.SetActive(true);
                    vase.SetActive(true);
                    sofa.SetActive(true);
                    nurse.SetActive(false);
                    plate.SetActive(false);
                    yield return new WaitForSeconds(2f);

                    globalState.audio.playSFX(0, 16);
                    ma.SetActive(false);
                    vase.SetActive(false);
                    sofa.SetActive(false);
                    nurse.SetActive(true);
                    plate.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                    nurse.GetComponent<imgSwitcher>().switchToNextImgState();
                    plate.GetComponent<imgSwitcher>().switchToNextImgState();
                    yield return new WaitForSeconds(1f);

                    ma.SetActive(true);
                    vase.SetActive(true);
                    sofa.SetActive(true);
                    nurse.SetActive(false);
                    plate.SetActive(false);
                    yield return new WaitForSeconds(2f);

                    ma.GetComponent<Animator>().SetTrigger("fadeOut");
                    yield return new WaitForSeconds(2);
                    camMovement.vfx.Play("blink");
                    yield return new WaitForSeconds(6);

                    //return with bin
                    ma.GetComponent<Animator>().SetTrigger("fadeIn");
                    Transform bin = globalState.vaseScene.transform.Find("bin");
                    bin.gameObject.SetActive(true);
                    bin.GetComponent<Animator>().SetTrigger("fadeIn");
                    globalState.audio.playSFX(2, 10);
                    camMovement.cam.Play("naturalBreathe");

                    globalState.audio.fadeVolumeSFX(2, 3, 4, 0);
                    globalState.audio.fadeVolumeSFX(0, 15, 4, 0);

                    globalState.audio.fadeVolumeSFX(2, 14, 8, 0);
                    globalState.audio.fadeVolumeSFX(2, 15, 8, 0.2f);

                    yield return new WaitForSeconds(3);

                    ma.GetComponent<imgSwitcher>().switchToImgState(3); //glove reach

                    yield return new WaitForSeconds(1);
                    globalState.audio.playSFX(2, 11);
                    yield return new WaitForSeconds(3);

                    vase.GetComponent<Animator>().SetTrigger("fadeOut");
                    ma.GetComponent<imgSwitcher>().switchToImgState(1);

                    yield return new WaitForSeconds(4);
                    ma.GetComponent<imgSwitcher>().switchToImgState(0);

                    yield return new WaitForSeconds(7);
                    camMovement.cam.Play("vaseSceneEndZoom");
                    //sfx thud
                    globalState.audio.playSFX(2, 12);
                    yield return new WaitForSeconds(4);
                    ma.GetComponent<imgSwitcher>().switchToImgState(4);
                    globalState.audio.playSFX(2, 9); //laugh
                    yield return new WaitForSeconds(1.5f);

                    //end of scene
                    gameControl.GetComponent<BlurManager>().levelPassEffect(2);

                    globalState.toggleAnimationGlobalClickable(true);
                }
                break;

            case "broken_vase":
                if (timesClicked == 1)
                {
                    globalState.vaseScene.transform.Find("soccer").GetComponent<Collider2D>().enabled = false; //disable collider

                    //fade out self
                    myAnimator.SetTrigger("fadeOut");
                    transform.Find("flowers").GetComponent<Animator>().SetTrigger("fadeOut");

                    foreach (Collider2D c in GetComponents<Collider2D>())
                    {
                        c.enabled = false;
                    }

                    //wait then reveal puzzle
                    StartCoroutine(Global.Chain(this, Global.WaitForSeconds(1),
                    Global.Do(() =>
                    {
                        GameObject pzl = globalState.vaseScene.transform.Find("puzzle").gameObject;
                        pzl.SetActive(true);
                        pzl.GetComponent<Animator>().Play("fadeIn"); //override controller
                    })));

                }
                else if (timesClicked == 2) //second time click breaks it again
                {

                    //sfx

                    StartCoroutine(Global.Chain(this,
                    Global.WaitForSeconds(1f),
                    Global.Do(() =>
                    {
                        //vase broken again
                        GetComponent<imgSwitcher>().switchToImgState(0);
                        globalState.audio.playSFX(2, 0, 0.35f);
                        Transform flo = transform.Find("flowers");
                        flo.GetComponent<Animator>().enabled = false;
                        flo.Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        flo.Find("Image (1)").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }),
                        Global.WaitForSeconds(2f),
                       //TODO blink, slight shake of cam
                       camMovement.glanceAndMoveBack(new Vector2(-200, 60), 0.1f),

                        Global.Do(() =>
                        {
                            globalState.vaseScene.transform.Find("sofa").GetComponent<interactable>().clickable = true; //enable sofa click
                            globalState.interactHint(true);
                        })
                    ));
                }

                break;

            /////////////////////
            case "leaf1":
                myAnimator.SetTrigger("action1");

                globalState.treeScene.transform.Find("hand_reach").GetComponent<Animator>().SetTrigger("action1");
                camMovement.cam.Play("camLeafReach");

                break;
            case "leaf3":
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    myAnimator.SetTrigger("action1");
                }
                else
                {
                    myAnimator.SetTrigger("action2");
                }
                break;

            case "bird":
                if (var1 == 0)
                {
                    myAnimator.SetTrigger("action1");
                    //sfx
                }
                else if (var1 == 1) //feedable
                {
                    myAnimator.SetTrigger("action1");
                    globalState.treeScene.transform.Find("hand_reach").GetComponent<Animator>().SetTrigger("action4");
                    globalState.treeScene.transform.Find("egg").GetComponent<interactable>().var1 = 1;
                    var1 = 2; //no longer feedable

                    yield return new WaitForSeconds(3f);
                    globalState.treeScene.transform.Find("hand_reach").GetComponent<imgSwitcher>().switchToImgState(0);
                }
                break;
            case "egg":
                Transform bird = globalState.treeScene.transform.Find("bird");
                Transform rech = globalState.treeScene.transform.Find("hand_reach");

                if (bird.GetComponent<interactable>().var1 == 1) //if feedable
                {
                    bird.GetComponent<Animator>().SetTrigger("action1");
                    rech.GetComponent<Animator>().SetTrigger("action4");
                    var1 = 1;
                    bird.GetComponent<interactable>().var1 = 2; //no longer feedable

                    yield return new WaitForSeconds(3f);
                    rech.GetComponent<imgSwitcher>().switchToImgState(0);

                }

                else if (var1 == 0) //not feedable yet, reaches for egg
                {
                    globalState.audio.playSFX(3, 1);
                    myAnimator.SetTrigger("action1");
                    rech.GetComponent<Animator>().SetTrigger("action2");
                    camMovement.cam.Play("camEggReach");

                    yield return new WaitForSeconds(0.5f);

                    bird.GetComponent<Animator>().SetTrigger("action1");
                }
                else if (var1 == 1) //fed and clicks egg
                {
                    //something magical
                 //   print("something magical");

                    rech.GetComponent<Animator>().SetTrigger("action2");
                    yield return new WaitForSeconds(1f);
                    camMovement.cam.Play("camTreeFall"); //the fall
                }

                break;
            case "worm":
                myAnimator.SetTrigger("fadeOut");
                Transform reach = globalState.treeScene.transform.Find("hand_reach");

                yield return new WaitForSeconds(2f);
                reach.GetComponent<imgSwitcher>().switchToImgState(1);
                reach.GetComponent<Animator>().SetTrigger("action3");
                globalState.treeScene.transform.Find("bird").GetComponent<interactable>().var1 = 1; //set to feedable
                break;

            /////////////////////
            case "ballAnimator":
                //initially non-clickable, will be enabled after interactions with pup

                Animator hand = globalState.pupScene.transform.Find("hand").GetChild(0).GetComponent<Animator>();
                Transform screenInteract = globalState.pupScene.transform.Find("screenInteract");
                Animator pup = globalState.pupScene.transform.Find("pup").GetComponent<Animator>();
                interactable stik = globalState.pupScene.transform.Find("stick").GetChild(0).GetComponent<interactable>();

                if (var2 >= 1 && stik.var2 >= 1 && stik.var3 >= 1)
                {
                    //if reaches here, satisfies: thrown and played ball, thrown and played stick
                    //trigger ending anim
                    pup.Play("dPlayBallEnding");
                    globalState.toggleAnimationGlobalClickable(false);

                    gameObject.SetActive(false); //deactivate ball

                    yield return new WaitForSeconds(5);
                    globalState.audio.fadeVolumeSFX(6, 6, 2, 0.2f);
                    globalState.audio.fadeVolumeSFX(6, 0, 2, 0);
                }

                else if (var1 == 0) //on lawn
                {
                    if ((!(var3 >= 3 && var2 == 0)) && (timesClicked == 1 || Random.Range(0f, 1f) < 0.5f)) //either first encounter or 50% chance will perform hold, or too many throws with no play
                    {
                        transform.parent.SetAsLastSibling(); //so that item sprite on top of hand

                        //forced fetch (starts with hand motion)
                        myAnimator.SetTrigger("action2"); //hold
                        hand.SetTrigger("action1"); //hold

                        //toggle on mouse based cam shift
                        hand.transform.parent.GetComponent<MouseBasedCamShift>().setActive(true);
                        transform.parent.GetComponent<MouseBasedCamShift>().setActive(true);

                        yield return new WaitForSeconds(2); //time for ball to be on hand

                        var1 = 1; //mark ball as being held
                        screenInteract.gameObject.SetActive(true);
                        screenInteract.GetComponent<interactable>().clickable = true; //activate click check for item throw

                        var3 += 1;
                    }
                    else
                    {
                        //play
                        myAnimator.SetTrigger("fadeOut");

                        pup.Play("dPlayBall");
                        var2 += 1; //indicates play ball happened (once more)
                    }
                }

                break;
            case "stickAnimator":
                //initially non-clickable, will be enabled after interactions with pup
                Animator hd = globalState.pupScene.transform.Find("hand").GetChild(0).GetComponent<Animator>();
                Transform screenInteractt = globalState.pupScene.transform.Find("screenInteract");
                Animator pupp = globalState.pupScene.transform.Find("pup").GetComponent<Animator>();

                if (var1 == 0) //on lawn
                {

                    //50% chance fetch 50% chance play, or too many plays with no throw (will avoid if too many throws with no play)
                    if ((!(var2 >= 3 && var3 == 0)) && ((var3 >= 3 && var2 == 0) || Random.Range(0f, 1f) < 0.5f))
                    {

                        transform.parent.SetAsLastSibling(); //so that item sprite on top of hand

                        myAnimator.SetTrigger("action2"); //hold
                        hd.SetTrigger("action1"); //hold

                        //toggle on mouse based cam shift
                        hd.transform.parent.GetComponent<MouseBasedCamShift>().setActive(true);
                        transform.parent.GetComponent<MouseBasedCamShift>().setActive(true);

                        yield return new WaitForSeconds(2);

                        var1 = 1; //mark stick as being held
                        screenInteractt.gameObject.SetActive(true);
                        screenInteractt.GetComponent<interactable>().clickable = true; //activate click check for item throw

                        var2 += 1; //indicates stick has been thrown in game (once more)
                    }
                    else
                    {
                        myAnimator.SetTrigger("fadeOut");

                        pupp.Play("dPlayTwig");

                        var3 += 1; //indicates play stick happened (once more)
                    }

                }
                break;

            case "screenInteract": //clickable screen signaling item throw
                interactable bl = globalState.pupScene.transform.Find("ball").GetChild(0).GetComponent<interactable>();
                interactable stk = globalState.pupScene.transform.Find("stick").GetChild(0).GetComponent<interactable>();

                Animator hdd = globalState.pupScene.transform.Find("hand").GetChild(0).GetComponent<Animator>();
                Animator pp = globalState.pupScene.transform.Find("pup").GetComponent<Animator>();

                hdd.SetTrigger("action2"); //throw

                //toggle off mouse based pos offset
                hdd.transform.parent.GetComponent<MouseBasedCamShift>().setActive(false);

                if (bl.var1 == 1)
                {
                    pp.Play("dFetchBall");
                    bl.GetComponent<Animator>().SetTrigger("action3");
                    bl.transform.parent.GetComponent<MouseBasedCamShift>().setActive(false);
                }
                else if (stk.var1 == 1)
                {
                    pp.Play("dFetchTwig");
                    stk.GetComponent<Animator>().SetTrigger("action3");
                    stk.transform.parent.GetComponent<MouseBasedCamShift>().setActive(false);
                }
                // else Debug.LogError("no item is being held");

                yield return new WaitForSeconds(3);

                hdd.transform.parent.SetAsLastSibling(); //reset hand sprite's layer status to the topmost
                gameObject.SetActive(false); //disable self

                break;

            /////////////////////
            case "rightHandFlowers":

                myAnimator.SetTrigger("fadeOut");
                GetComponent<interactable>().clickable = false;
                yield return new WaitForSeconds(2);

                Transform scene = globalState.gardenCloseupScene.transform;
                scene.Find("hand").GetComponent<Animator>().SetTrigger("action1"); //hand out
                scene.Find("herCloseup").GetComponent<interactable>().clickable = true;

                break;

            case "her_bench_head":
                transform.parent.GetComponent<Animator>().Play("girl_bench_disappear");
                HideAndSeek has = camMovement.edgeScroller.transform.GetComponent<HideAndSeek>();
                has.hideStatus = -1;
                has.found[0] = true;
                break;

            case "her_flo":
                myAnimator.SetTrigger("action4");

                HideAndSeek hs = camMovement.edgeScroller.transform.GetComponent<HideAndSeek>();
                hs.hideStatus = -1;
                hs.found[1] = true;

                //arrange for bush interact
                globalState.gardenScene.transform.Find("right").Find("flo_separated").GetComponent<Animator>().Play("plant3");
                Transform p2 = globalState.gardenScene.transform.Find("right").Find("plants").Find("p2");
                p2.GetComponent<Animator>().Play("plant2");
                p2.GetComponent<Collider2D>().enabled = true;
                break;

            case "p2": //garden scene hide reveal
                globalState.audio.playSFX(7, 2);

                globalState.gardenScene.transform.Find("right").Find("flo_separated").GetComponent<Animator>().Play("plantIdle");
                GetComponent<Animator>().Play("plantIdle");

                HideAndSeek hass = FindObjectOfType<HideAndSeek>();
                hass.girl_bush.gameObject.SetActive(true);
                hass.girl_bush.SetTrigger("action2");

                hass.found[2] = true;

                GetComponent<Collider2D>().enabled = false;
                globalState.gardenScene.transform.Find("right").Find("flo_separated").GetChild(0).GetComponent<imgSwitcher>().switchToImgState(1);
                break;
            case "door":
                globalState.audio.playSFX(7, 1);
                if (!transform.Find("open_door").gameObject.activeSelf)
                {
                    transform.Find("open_door").gameObject.SetActive(true);
                    GetComponent<imgSwitcher>().switchToImgState(1);
                }
                else
                {
                    transform.Find("open_door").gameObject.SetActive(false);
                    GetComponent<imgSwitcher>().switchToImgState(0);
                }
                break;
            case "her_sign":
                HideAndSeek hss = FindObjectOfType<HideAndSeek>();

                myAnimator.SetTrigger("action1");
                hss.found[3] = true;
                GetComponent<Collider2D>().enabled = false;

                break;
            case "her": //post hide and seek

                GetComponent<Collider2D>().enabled = false;
                //camMovement.enable.darkCover.enabled = true;
                //camMovement.enable.darkCover.Play("idle");

                camMovement.edgeScroller.disableEdgeScroller();

                yield return new WaitForSeconds(1.5f);

                camMovement.camHolder.enabled = false; //to prevent locking of cam's position anymore
                camMovement.vfx.Play("blink");
                camMovement.cam.Play("camGardenWalkUp");

                yield return new WaitForSeconds(1.5f);
                camMovement.enable.darkCover.Play("fadeIn");

                yield return new WaitForSeconds(3f);
                globalState.audio.fadeVolumeSFX(7, 7, 3, 0); //fade out bgm

                //transition to closeup subscene
                yield return new WaitForSeconds(4f);

                camMovement.enable.setUpLevel(7, true);
                camMovement.enable.darkCover.Play("fadeOut");

                break;
            case "herCloseup":
                if (var1 == 0)
                {
                    globalState.toggleAnimationGlobalClickable(false);

                    GetComponent<Animator>().SetTrigger("action1"); //turn
                    GetComponent<interactable>().clickable = false;

                    var1 = 1;

                    Transform closeupScene = globalState.gardenCloseupScene.transform;
                    Animator handCloseup = closeupScene.Find("hand").GetComponent<Animator>(),
                        mucha = closeupScene.Find("mucha_filter").GetComponent<Animator>(), klimt = closeupScene.Find("klimt").GetComponent<Animator>();

                    handCloseup.GetComponent<animEventLink>().deactivateMouseBasedCamShift(handCloseup.gameObject);

                    yield return new WaitForSeconds(1f);
                    handCloseup.SetTrigger("action2"); //quick hide

                    yield return new WaitForSeconds(3f);
                    camMovement.vfx.Play("blink");
                    yield return new WaitForSeconds(7f);

                    mucha.gameObject.SetActive(true);
                    mucha.Play("muchaFadeIn");

                    globalState.audio.fadeVolumeSFX(7, 9, 3, 0);
                    globalState.audio.fadeVolumeSFX(7, 10, 3, 1f);
                    globalState.audio.fadeVolumeSFX(7, 13, 1.5f, 0.9f); //heartbeats louder

                    yield return new WaitForSeconds(10f);
                    handCloseup.SetTrigger("action1"); //offer flower

                    yield return new WaitForSeconds(3f);
                    GetComponent<interactable>().clickable = true;

                    globalState.toggleAnimationGlobalClickable(true);
                }
                else
                {
                    globalState.toggleAnimationGlobalClickable(false);
                    Animator handCloseup = globalState.gardenCloseupScene.transform.Find("hand").GetComponent<Animator>();

                    GetComponent<interactable>().clickable = false;
                    handCloseup.GetComponent<animEventLink>().deactivateMouseBasedCamShift(handCloseup.gameObject);
                    handCloseup.SetTrigger("fadeOut");
                    globalState.audio.fadeVolumeSFX(7, 10, 3, 0);
                    yield return new WaitForSeconds(4f);
                    globalState.audio.playSFX(7, 11);

                    yield return new WaitForSeconds(2f);
                    GetComponent<Animator>().SetTrigger("action2"); //wear flower
                    yield return new WaitForSeconds(11f);

                    //end scene transition
                    //end of scene
                    gameControl.GetComponent<BlurManager>().levelPassEffect(7);
                    globalState.toggleAnimationGlobalClickable(true);
                }

                break;
            /////////////////////
            case "sunImage":
                if (var1 < 3)
                {
                    transform.parent.parent.GetComponent<Animator>().Play("sunset" + (var1 + 1)); //find the actual sun GO which carries the animator
                    var1 += 1;
                    globalState.toggleAnimationGlobalClickable(false);

                    if (var1 == 3) clickable = false; //disable sun interact after fades out
                }
                break;

            /////////////////////
            case "faceaway":
                transform.parent.GetComponent<animEventLink>().faceawayInteract();

                break;

            case "fish1":

                if (var1 == 0)
                {
                    transform.parent.parent.GetComponent<Animator>().SetTrigger("action1");
                }
                else
                {
                    clickable = false;

                    //fish anim
                    transform.parent.parent.GetComponent<Animator>().SetTrigger("action2");

                    yield return new WaitForSeconds(1);

                    Animator h = globalState.bickerScene.transform.Find("her").GetComponent<Animator>();
                    h.Play("girlNormalsitQuickPeek");
                    //her peek
                    Animator nspp_closeup = globalState.bickerScene.transform.Find("newspaper_closeup").GetComponent<Animator>();

                    yield return new WaitForSeconds(1f);
                    nspp_closeup.Play("nsppEvade");
                    //newspaper evade anim

                    yield return new WaitForSeconds(2);

                    h.Play("girlCasualSit");

                    yield return new WaitForSeconds(10);

                    h.Play("girlLeanback");

                    yield return new WaitForSeconds(3);
                    activateMouseBasedCamShift(nspp_closeup.gameObject);
                    yield return new WaitForSeconds(3);

                    h.Play("girlCasualSit");
                    //put away fishtank (local pos -533, -21)
                    Transform fshtank = globalState.bickerScene.transform.Find("fishtank");
                    fshtank.transform.localPosition = new Vector2(-533, -21);

                    yield return new WaitForSeconds(4);

                    //cough
                    h.Play("girlCasualsitCough");

                    yield return new WaitForSeconds(0.5f);

                    nspp_closeup.Play("nsppCoughReaction");
                    camMovement.cam.Play("camBickerQuickGlance");

                    //switch fish sprite
                    Transform f1 = transform, f2 = globalState.bickerScene.transform.Find("fishtank/fish/fish/fish2");
                    f1.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    f2.gameObject.SetActive(true);

                    Transform pt1 = globalState.bickerScene.transform.Find("potPlant/pattern1"), pt2 = globalState.bickerScene.transform.Find("potPlant/pattern2");
                    pt1.gameObject.SetActive(false);
                    pt2.gameObject.SetActive(true);

                    //fruit sprite change back
                    Transform mdr = globalState.bickerScene.transform.Find("fruitPlatter/mandarin"), appl = globalState.bickerScene.transform.Find("fruitPlatter/apple");
                    mdr.gameObject.SetActive(true);
                    appl.gameObject.SetActive(false);

                    yield return new WaitForSeconds(4);
                    activateMouseBasedCamShift(nspp_closeup.gameObject);

                    yield return new WaitForSeconds(2);

                    h.Play("girlRead");
                    globalState.audio.playSFX(8, 15);
                    //candle to (-148, -216)
                    globalState.bickerScene.transform.Find("topBook").gameObject.SetActive(false);
                    globalState.bickerScene.transform.Find("candle").transform.localPosition = new Vector2(-148, -216);

                    yield return new WaitUntil(() => mouseAtCornerBottomLeft(nspp_closeup.gameObject)); //wait til mouse close enough to goal (peek through nspp)

                    deactivateMouseBasedCamShift(nspp_closeup.gameObject);

                    //nspp off anim, wait on mandarin click
                    nspp_closeup.Play("nsppExit");
                    globalState.bickerScene.transform.Find("fruitPlatter/mandarin").GetComponent<interactable>().clickable = true;
                }
                break;

            case "fish2":
                transform.parent.parent.GetComponent<Animator>().SetTrigger("action3"); //fish interact

                break;
            case "mandarin":
                GetComponent<Animator>().SetTrigger("fadeOut");
                //peel sfx
                globalState.audio.playSFX(8, 7);
                yield return new WaitForSeconds(6);

                Transform mandarin_peeled = globalState.bickerScene.transform.Find("mandarin_peeled");
                mandarin_peeled.gameObject.SetActive(true);
                Animator mdrp = mandarin_peeled.GetComponent<Animator>();
                mdrp.SetTrigger("fadeIn");
                globalState.globalClickable = false;
                yield return new WaitForSeconds(1.5f);
                mdrp.enabled = false;
                globalState.globalClickable = true;

                //peek

                break;
            case "screenInteract2": //activated every time a slice is clicked
                clickable = false;

                if (globalState.mandarinConsumed != 6)
                {
                    //eat animation, turn off si2
                    GameObject mdrSlice = globalState.bickerScene.transform.Find("slice_closeup").gameObject;
                    deactivateMouseBasedCamShift(mdrSlice);
                    mdrSlice.GetComponent<Animator>().SetTrigger("action2");

                    yield return new WaitForSeconds(1.5f);

                    if (var1 == 1)
                    {
                        //means this is the 4th slice, will set sprite to turned after anim done
                        globalState.bickerScene.transform.Find("her").GetComponent<Animator>().Play("girlTurned");
                    }
                    else if (var1 == 2)
                    {
                        Animator herr = globalState.bickerScene.transform.Find("her").GetComponent<Animator>();
                        herr.Play("girlTurnedTiltHead"); //staring at last piece 
                        yield return new WaitForSeconds(0.5f);
                        herr.Play("girlTurnedCloser");
                        yield return new WaitForSeconds(0.3f);
                        herr.Play("girlTurnedCloserTiltHead");
                    }

                    gameObject.SetActive(false);
                }
                else
                { //trigger ending
                    globalState.toggleAnimationGlobalClickable(false);
                    camMovement.mouseBasedCamShift.setActive(false); //deactivate cam shift

                    GameObject mdrSlice = globalState.bickerScene.transform.Find("slice_closeup").gameObject;
                    deactivateMouseBasedCamShift(mdrSlice);
                    mdrSlice.GetComponent<Animator>().SetTrigger("action3");

                    Animator hr = globalState.bickerScene.transform.Find("her").GetComponent<Animator>();
                    yield return new WaitForSeconds(1.5f);

                    hr.Play("girlGrab");

                }

                break;

            /////////////////////
            case "ripple":
                var1 += 1;
                myAnimator.Play("rippleInteract");
                globalState.audio.playSFX(9, 6 + var1); //distant bell
                globalState.audio.playSFX(9, 24); //windbell

                Transform leafBatch = globalState.parkScene.transform.Find("flat/her/leaves" + var1);
                leafBatch.gameObject.SetActive(true);
                globalState.toggleAnimationGlobalClickable(false);

                foreach (Transform leaf in leafBatch)
                {
                    
                    StartCoroutine(Global.Chain(this,
                    Global.WaitForSeconds(Random.Range(0f, 0.7f)),
                    Global.Do(() =>
                    {
                        //print( leaf.name);
                        leaf.GetComponent<Animator>().Play("leafFall" + Random.Range(1, 5));
                    })));

                }

                yield return new WaitForSeconds(1f);

                globalState.audio.playSFX(9, 0);

                yield return new WaitForSeconds(5f);

                globalState.toggleAnimationGlobalClickable(true);

                if (var1 < 3)
                {
                    //continue

                }
                else
                {
                    clickable = false;

                    globalState.parkScene.transform.Find("flat/her").GetComponent<Animator>().SetTrigger("fadeIn");

                    //enable individual leaves interact
                    for (int r = 1; r <= 3; r++)
                    {

                        Transform currBatch = globalState.parkScene.transform.Find("flat/her/leaves" + r);

                        foreach (Transform leaf in currBatch)
                        {
                            leaf.Find("leafLine").GetComponent<interactable>().clickable = true;

                        }
                    }

                }

                break;

            case "leafLine":
                clickable = false;
                GetComponent<Collider2D>().enabled = false;
                globalState.audio.playSFX(0, Random.Range(4, 8)); //fall sfx

                Animator l = transform.parent.GetComponent<Animator>();
                l.Play("leafShow");
                l.Play("leafFallDown" + Random.Range(1, 5));
                globalState.globalClickable = false;

                yield return new WaitForSeconds(1f);

                globalState.globalClickable = true;
                globalState.leavesFall += 1;
                if (globalState.leavesFall >= 30) //all clicked
                {
                    yield return new WaitForSeconds(1);
                    //some sfx reaction

                    yield return new WaitForSeconds(1);
                    animEventLink roses = globalState.parkScene.transform.Find("roses").GetComponent<animEventLink>();

                    globalState.enable.darkCover.SetTrigger("fadeInWhite");
                    globalState.audio.playSFX(9, 30);
                    yield return new WaitForSeconds(2);

                    roses.gameObject.SetActive(true);
                    roses.roses();
                }
                break;

            /////////////////////
            case "grave":

                globalState.globalClickable = false;

                if (var1 == 1) //if at offer flower checkpoint
                {
                    Transform bouquet = globalState.graveyardScene.transform.Find("flower");
                    yield return new WaitForSeconds(1);

                    globalState.toggleAnimationGlobalClickable(false);

                    bouquet.gameObject.SetActive(true);
                    Animator bq = bouquet.GetComponent<Animator>();
                    bq.SetTrigger("fadeIn");
                    globalState.audio.playSFX(10, 8);

                    globalState.audio.fadeVolumeSFX(10, 0, 3, 0.2f);
                    globalState.audio.fadeVolumeSFX(10, 1, 3, 0.2f);

                    yield return new WaitForSeconds(4);

                    globalState.audio.playSFX(10, 9, 0.1f);
                    globalState.audio.fadeVolumeSFX(10, 9, 8, 1);

                    yield return new WaitForSeconds(9.5f);

                    bq.SetTrigger("action1"); //flower petals

                    yield return new WaitForSeconds(3);

                    camMovement.cam.Play("camShiftGraveyardSky2");

                    yield return new WaitForSeconds(9);

                    //end of scene
                    gameControl.GetComponent<BlurManager>().levelPassEffect(10);

                    globalState.toggleAnimationGlobalClickable(true);
                }
                else
                {

                    if (!globalState.graveyardConditionMetTriggered)
                    {
                        globalState.audio.playSFX(10, 6, 1);
                        camMovement.cam.Play("camGraveInteract1");

                        globalState.graveClicked = true;

                        yield return new WaitForSeconds(3);

                        if (globalState.graveyardLeaves >= 5)
                        {
                            StartCoroutine(graveyardConditionMetCoroutine());
                        }

                    }
                    else
                    {
                        globalState.audio.playSFX(10, 6, 0.5f);
                        camMovement.cam.Play("camGraveInteract2");
                        yield return new WaitForSeconds(3);
                        camMovement.cam.Play("camShiftGraveyardSky");
                        yield return new WaitForSeconds(3);
                    }

                }

                globalState.globalClickable = true;

                break;

            case "doggo":
                //sfx
                clickable = false;

                yield return new WaitForSeconds(2);

                globalState.graveyardScene.transform.Find("dark_cover").GetComponent<Animator>().Play("s1s2transition"); //will auto-transition to s2
                //disable lightening
                globalState.graveyardScene.transform.Find("sky").GetComponent<weather>().terminateLightning();

                yield return new WaitForSeconds(3);

                Transform lvs = globalState.graveyardScene.transform.Find("leavesInteractable");
                foreach (Transform lf in lvs)
                {
                    interactable ita = lf.GetComponent<interactable>();
                    ita.var1 = 1;
                    ita.clickable = true; //clicking will toggle on col

                }

                myAnimator.SetTrigger("fadeOut");
                yield return new WaitForSeconds(2);
                GetComponent<imgSwitcher>().switchToImgState(1);
                myAnimator.SetTrigger("fadeIn");

                globalState.graveyardConditionMetTriggered = true;

                break;

            /////////////////////
            case "glasses":
                globalState.globalClickable = false;

                //sfx
                clickable = false;
                myAnimator.SetTrigger("fadeOut");

                gameControl.GetComponent<BlurManager>().centerBlur.setNewScale(0.1f, 0.1f);

                yield return new WaitForSeconds(1);

                globalState.audio.playSFX(11, 0);

                yield return new WaitForSeconds(1);

                Transform bk = globalState.homeScene.transform.Find("book");
                bk.Find("textsBlur").gameObject.SetActive(false);
                bk.Find("texts").gameObject.SetActive(true);

                yield return new WaitForSeconds(2);

                //sfx 
                globalState.audio.playSFX(11, 1);
                Transform dg = globalState.homeScene.transform.Find("dog_closeby");
                dg.gameObject.SetActive(true);
                dg.GetComponent<Animator>().SetTrigger("fadeIn");
                dg.GetComponent<Animator>().SetTrigger("action1"); //hop in

                yield return new WaitForSeconds(2);

                globalState.globalClickable = true;

                break;

            case "dog_closeby":
                clickable = false;
                globalState.globalClickable = false;

                yield return new WaitForSeconds(1);
                GetComponent<imgSwitcher>().switchToImgState(1);
                //sfx
                globalState.audio.playSFX(11, 2);

                yield return new WaitForSeconds(3.5f);
                myAnimator.SetTrigger("fadeOut");

                yield return new WaitForSeconds(3);
                camMovement.camHolder.enabled = true;
                camMovement.camHolder.Play("camShift0to1");

                yield return new WaitForSeconds(4);

                camMovement.camHolder.Play("camShift1to0");

                yield return new WaitForSeconds(2);
                camMovement.camHolder.enabled = false;

                Transform pen = globalState.homeScene.transform.Find("pen"),
                    mug = globalState.homeScene.transform.Find("mug"), mobile = globalState.homeScene.transform.Find("mobile");

                pen.GetComponent<interactable>().clickable = true;
                mug.GetComponent<interactable>().clickable = true;
                mobile.GetComponent<interactable>().clickable = true;

                globalState.globalClickable = true;

                break;
            case "pen":
                globalState.penTriggered = true;

                myAnimator.SetTrigger("action1");
                globalState.audio.playSFX(11, 3);

                //check for condition met after interact
                if (globalState.checkHomeSceneItemCondition())
                {
                    Transform phone = globalState.homeScene.transform.Find("phone");
                    phone.gameObject.SetActive(true);
                    phone.GetComponent<animEventLink>().initiatePhoneCheck();
                }
                break;

            case "mobile":

                globalState.mobileTriggered = true;

                myAnimator.SetTrigger("action1");
                globalState.audio.playSFX(11, 7);

                if (globalState.checkHomeSceneItemCondition())
                {
                    Transform phone = globalState.homeScene.transform.Find("phone");
                    phone.gameObject.SetActive(true);
                    phone.GetComponent<animEventLink>().initiatePhoneCheck();
                }
                break;

            case "mug":

                globalState.mugTriggered = true;

                globalState.globalClickable = false;

                Transform mg = globalState.homeScene.transform.Find("mug"),
                    mug_hand = globalState.homeScene.transform.Find("hand_mug");
                mug_hand.gameObject.SetActive(true);
                mug_hand.GetComponent<Animator>().SetTrigger("action1"); //hand out and in 

                mg.GetComponent<Animator>().SetTrigger("fadeOut");

                yield return new WaitForSeconds(6f);

                mug_hand.gameObject.SetActive(false);
                mg.GetComponent<Animator>().SetTrigger("fadeIn");
                globalState.audio.playSFX(11, 5);

                globalState.globalClickable = true;

                if (globalState.checkHomeSceneItemCondition())
                {
                    Transform phone = globalState.homeScene.transform.Find("phone");
                    phone.gameObject.SetActive(true);
                    phone.GetComponent<animEventLink>().initiatePhoneCheck();
                }

                yield return new WaitForSeconds(1f);

                break;
            /////////////////////
            case "mmBox":
                globalState.enable.UICanvas.SetActive(true);
                globalState.enable.menuUIButton.SetActive(false);

                globalState.enable.openMemorabiliaUI();
                break;
            case "MenuUI":
                globalState.enable.closeMenuUIWindow();
                break;
            case "MemorabiliaUI":
                if (globalState.enable.mm.showingDetail)
                {
                    //return to full item list if showing item detail
                    // globalState.enable.mm.itemOnDisplay.itemOnClick();
                    globalState.enable.mm.itemOnDisplay.itemOnClick();
                }
                else
                {
                    globalState.enable.closeMemorabiliaUI();
                }

                break;
            case "LanguageSelectionUI":
                globalState.enable.closeLanguageSelectionUI();
                break;
        }

        string parentName = transform.parent.name;

        if (name[0] == 'f' && parentName.Equals("flowers"))
        {
            //lawn flower instance
            //myAnimator.Play("empty");

            float rd = Random.Range(0f, 1f);

            if (rd <= 0.333f)
            {
                myAnimator.Play("lawnFlo1");
                var1 = 1;
            }
            else if (rd <= 0.666f)
            {
                myAnimator.Play("lawnFlo2");
                var1 = 1;
            }
            else
            {
                myAnimator.Play("lawnFlo3");
                var1 = 1;
            }

        }
        else if (parentName.Equals("pup")) //one of pup sprites
        {
            globalState.audio.playSFX(6, Random.Range(1, 4));

            if (name.Equals("d1") || name.Equals("d1.5"))
            {
                transform.parent.GetComponent<Animator>().Play("dSit");
                transform.parent.Find("d2").GetComponent<interactable>().var2 = 1; //mark as visited

                globalState.StartCoroutine(Global.Chain(globalState, Global.Do(() => globalState.globalClickable = false),
                    Global.WaitForSeconds(1.5f),
                    Global.Do(() => globalState.globalClickable = true)));

            }
            else if (name.Equals("d2"))
            {
                float rand = Random.Range(0f, 1f);

                transform.parent.GetComponent<Animator>().Play((rand > 0.5f) ? "dStand" : "dRollover");
                if (rand > 0.5f) transform.parent.Find("d3").GetComponent<interactable>().var2 = 1;  //mark as visited
                else transform.parent.Find("d1").GetComponent<interactable>().var2 = 1;  //mark as visited

                globalState.StartCoroutine(Global.Chain(globalState, Global.Do(() => globalState.globalClickable = false),
                    Global.WaitForSeconds(1.5f),
                    Global.Do(() => globalState.globalClickable = true)));

            }
            else if (name.Equals("d3"))
            {
                transform.parent.GetComponent<Animator>().Play("dSit");
                transform.parent.Find("d2").GetComponent<interactable>().var2 = 1;  //mark as visited

                globalState.StartCoroutine(Global.Chain(globalState, Global.Do(() => globalState.globalClickable = false),
                    Global.WaitForSeconds(1.5f),
                    Global.Do(() => globalState.globalClickable = true)));
            }

            interactable d1 = transform.parent.Find("d1").GetComponent<interactable>(), d2 = transform.parent.Find("d2").GetComponent<interactable>(),
            d3 = transform.parent.Find("d3").GetComponent<interactable>();

            //if all three poses visited, unlock clickable on the two items
            if (d1.var3 != 5 && d1.var2 == 1 && d2.var2 == 1 && d3.var2 == 1)
            {
                Transform ball = globalState.pupScene.transform.Find("ball"), stick = globalState.pupScene.transform.Find("stick");

                ball.gameObject.SetActive(true); stick.gameObject.SetActive(true);

                ball.GetChild(0).GetComponent<Animator>().SetTrigger("fadeIn");
                stick.GetChild(0).GetComponent<Animator>().SetTrigger("fadeIn");

                ball.GetChild(0).GetComponent<interactable>().clickable = true;
                stick.GetChild(0).GetComponent<interactable>().clickable = true;

                d1.var3 = 5; //prevent this code from being reached again
            }

        }
        else if (name[0] == 'n' && parentName.Equals("notes"))
        {
            int noteIndex = int.Parse(name[1].ToString());

            NotesRecord notesRecord = transform.parent.parent.GetComponent<NotesRecord>();
            notesRecord.recordNote(noteIndex);

        }
        else if (parentName.Equals("rose") || parentName.Equals("hibiscus"))
        {
            if (var1 == 0)
            {
                myAnimator.SetTrigger("action1");
                globalState.gardenSceneFlowerCount += 1;

                var1 += 1;

                if (globalState.gardenSceneFlowerCount >= 14) //total num flo; effect
                {
                    //trigger effect

                    camMovement.mouseBasedCamShift.setActive(false);

                    Transform center = globalState.gardenScene.transform.Find("center"), left = globalState.gardenScene.transform.Find("left"),
                        right = globalState.gardenScene.transform.Find("right");

                    Transform up_front = center.Find("up_front"), flowers1 = center.Find("flowers1"), bottomLeft = center.Find("bottomLeft"),
                        muchaFilter = center.Find("mucha_filter");
                    muchaFilter.gameObject.SetActive(false);

                    Transform treeflower = center.Find("treeflower");
                    Transform l1 = up_front.Find("l1"), l2 = up_front.Find("l2"), l3 = up_front.Find("l3"), l4 = up_front.Find("l4");

                    yield return new WaitForSeconds(5.5f);

                    globalState.audio.playSFX(7, 16, 0.2f); //warm pad
                    globalState.audio.fadeVolumeSFX(7, 16, 2, 0.6f);

                    left.SetAsLastSibling();
                    right.SetAsLastSibling(); //so that center is behind those two 

                    treeflower.GetComponent<Animator>().SetTrigger("fadeIn"); //override original fadeIn; fade in color
                    l1.GetComponent<Animator>().SetTrigger("fadeIn");
                    l2.GetComponent<Animator>().SetTrigger("fadeIn");
                    l3.GetComponent<Animator>().SetTrigger("fadeIn");
                    l4.GetComponent<Animator>().SetTrigger("fadeIn");
                    flowers1.GetComponent<Animator>().SetTrigger("fadeIn");
                    bottomLeft.GetComponent<Animator>().SetTrigger("fadeIn");

                    yield return new WaitForSeconds(7);

                    muchaFilter.gameObject.SetActive(true);
                    muchaFilter.GetComponent<Animator>().SetTrigger("fadeIn");

                    yield return new WaitForSeconds(4);

                    Animator hand = center.Find("hand").GetComponent<Animator>();
                    imgSwitcher her = center.Find("her").GetComponent<imgSwitcher>();

                    hand.SetTrigger("action2"); //fist

                    yield return new WaitForSeconds(2);
                    camMovement.vfx.Play("blink");

                    yield return new WaitForSeconds(1);
                    camMovement.cam.Play("camGardenNod");

                    yield return new WaitForSeconds(1f);
                    her.switchToImgState(1);

                    yield return new WaitForSeconds(1f);
                    her.switchToImgState(0);

                    yield return new WaitForSeconds(1f);
                    her.GetComponent<Animator>().SetTrigger("fadeOut");
                    her.transform.Find("her_shadow").GetComponent<Animator>().SetTrigger("fadeOut");

                    yield return new WaitForSeconds(3f);
                    camMovement.vfx.Play("blink2x");

                    yield return new WaitForSeconds(5f);

                    camMovement.edgeScroller.enableEdgeScroller();
                    camMovement.edgeScroller.transform.GetComponent<HideAndSeek>().startHideAndSeek();
                    globalState.interactHint(false); //move

                }
            }
            else
            {
                int rand = Random.Range(2, 5);

                myAnimator.SetTrigger("action" + rand);

            }
        }
        else if (parentName.Equals("stars"))
        {
            GetComponent<AudioSource>().Play();
            clickable = false; //disable star clicking check
            starsManager starsManage = FindObjectOfType<starsManager>();

            myAnimator.Play("starFound");
            starsManage.currActiveStarIndex += 1;
            starsManage.startStarCheck();

        }
        else if (parentName.Equals("mandarin_peeled")) //mandarin slice
        {
            globalState.globalClickable = false;
            globalState.mandarinConsumed += 1; //increment count

            if (globalState.mandarinConsumed == 5)
            {//if at 5th piece

                Animator hhh = globalState.bickerScene.transform.Find("her").GetComponent<Animator>();
                Transform si_2 = globalState.bickerScene.transform.Find("screenInteract2");

                StartCoroutine(Global.Chain(this, Global.Do(() =>
                {
                    si_2.GetComponent<interactable>().var1 = 2;
                    //her turn, then face forward

                    hhh.Play("girlTurnedTiltHead");
                }), Global.WaitForSeconds(2.5f),
                    Global.Do(() => { hhh.Play("girlTurned"); })));
            }

            clickable = false;
            myAnimator.SetTrigger("fadeOut");

            yield return new WaitForSeconds(1);
            globalState.audio.playSFX(8, 8);
            yield return new WaitForSeconds(1);

            //play closeup anim
            Transform closeup = globalState.bickerScene.transform.Find("slice_closeup");

            closeup.gameObject.SetActive(true);
            closeup.GetComponent<Animator>().SetTrigger("action1"); //fork out

            Transform si2 = globalState.bickerScene.transform.Find("screenInteract2");
            si2.gameObject.SetActive(true);
            si2.GetComponent<interactable>().clickable = true;
            //globalState.screenInteract2On = true;

            switch (globalState.mandarinConsumed)
            {
                case 1:
                    //her turn
                    Animator h = globalState.bickerScene.transform.Find("her").GetComponent<Animator>();
                    h.Play("girlReadQuickPeek");

                    break;
                case 3:
                    Animator hh = globalState.bickerScene.transform.Find("her").GetComponent<Animator>();
                    hh.Play("girlReadQuickPeek");

                    yield return new WaitForSeconds(2);

                    hh.Play("girlReadQuickPeek");
                    break;
                case 4:
                    si2.GetComponent<interactable>().var1 = 1;
                    break;
                case 6:
                    Animator hhh = globalState.bickerScene.transform.Find("her").GetComponent<Animator>();
                    hhh.Play("girlTurnedCloser");
                    si2.GetComponent<interactable>().var1 = 3; //identify anim
                    break;
            }

            yield return new WaitForSeconds(0.5f);
            globalState.globalClickable = true;

        }
        else if (name.Equals("img") && transform.parent.parent.name.Equals("objects"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (Mathf.Abs(rb.rotation) < 3500) //TODO change to 5000
            {
                int sign = (rb.rotation > 0) ? 1 : -1;
                rb.AddTorque(120000 * sign);
            }
            else //light up
            {
                globalState.audio.playSFX(9, Random.Range(12, 17)); //play random bell sound

                clickable = false;
                GetComponent<Animator>().Play("brighten");
                globalState.klimtRotate += 1;

                Animator pt = globalState.parkScene.transform.Find("klimt/bg/pattern").GetComponent<Animator>();

                switch (globalState.klimtRotate)
                {

                    case 3:
                        pt.Play("opacity0_2");
                        break;
                    case 7:
                        pt.Play("opacity0_4");
                        break;
                    case 11:
                        pt.Play("opacity0_9");
                        break;

                    case 14:
                        animEventLink collage = globalState.parkScene.transform.Find("collage").GetComponent<animEventLink>();

                        StartCoroutine(Global.Chain(this, Global.WaitForSeconds(2), Global.Do(() =>
                        {
                            globalState.enable.darkCover.SetTrigger("fadeInWhite");
                            //sfx
                            globalState.audio.playSFX(9, 30); //deep thud
                        }),
                            Global.WaitForSeconds(3),
                            Global.Do(() =>
                            {
                                collage.gameObject.SetActive(true);
                                collage.collage();
                            })));

                        break;
                }

            }

        }
        else if (parentName.Equals("leavesInteractable"))
        {
            globalState.globalClickable = false;

            if (var1 == 0)
            {

                myAnimator.SetTrigger("action1");
                var1 = 1;
                clickable = false;
                globalState.graveyardLeaves += 1;

                if (globalState.graveyardLeaves == 5 && globalState.graveClicked)
                {
                    StartCoroutine(graveyardConditionMetCoroutine());

                }

            }
            else if (var1 == 1)
            {
                //this is after lightning ends, clicking will toggle on color
                clickable = false;
                var1 = 2;

                Transform c = transform.Find("c");
                c.gameObject.SetActive(true);
                c.GetComponent<Animator>().SetTrigger("fadeIn");
                transform.Find("cbw").GetComponent<Animator>().SetTrigger("fadeOut");

                globalState.leavesColored += 1;

                if (globalState.leavesColored >= 8)
                {
                    //all leaves clicked, show env cols
                    camMovement.cam.SetTrigger("stormEnd");
                    yield return new WaitForSeconds(2);
                    //sfx

                    globalState.audio.fadeVolumeSFX(10, 0, 5, 0.1f);
                    globalState.audio.fadeVolumeSFX(10, 1, 5, 0.1f);

                    Transform tree = globalState.graveyardScene.transform.Find("tree"), leavesCol = globalState.graveyardScene.transform.Find("leavesCol"),
                        sky = globalState.graveyardScene.transform.Find("sky"), darkScreen = globalState.graveyardScene.transform.Find("dark_cover"),
                        leaves = globalState.graveyardScene.transform.Find("leaves"), blown = globalState.graveyardScene.transform.Find("blown_leaves"),
                        rain = globalState.graveyardScene.transform.Find("rain"), lz = globalState.graveyardScene.transform.Find("grave/lines"),
                        gve = globalState.graveyardScene.transform.Find("grave"), lighterGray = globalState.graveyardScene.transform.Find("lighter_darkness");

                    leaves.Find("c").gameObject.SetActive(true);
                    leaves.Find("c").GetComponent<Animator>().SetTrigger("fadeInSlow");
                    tree.Find("c").gameObject.SetActive(true);
                    tree.Find("c").GetComponent<Animator>().SetTrigger("fadeInSlow");

                    foreach (Transform li in transform.parent)
                    {
                        //light up non-interacted leaf
                        Transform li_c = li.transform.Find("c");
                        if (!li_c.gameObject.activeSelf)
                        {
                            li_c.gameObject.SetActive(true);
                            li_c.GetComponent<Animator>().SetTrigger("fadeIn");
                            li.transform.Find("cbw").GetComponent<Animator>().SetTrigger("fadeOut");
                        }
                    }

                    sky.Find("storm").gameObject.SetActive(false);

                    foreach (Transform bll in blown)
                    {
                        bll.GetComponent<imgSwitcher>().switchToImgState(1);
                        bll.GetComponent<Animator>().SetTrigger("fadeIn");
                    }

                    rain.gameObject.SetActive(false);

                    yield return new WaitForSeconds(4);

                    leavesCol.gameObject.SetActive(true);
                    leavesCol.GetComponent<Animator>().SetTrigger("fadeInSlow");

                    darkScreen.GetComponent<Animator>().Play("s2Out");

                    yield return new WaitForSeconds(3);

                    lz.GetComponent<Animator>().SetTrigger("fadeOut");
                    lighterGray.GetComponent<Animator>().SetTrigger("fadeOut");

                    yield return new WaitForSeconds(3);

                    gve.GetComponent<interactable>().var1 = 1; //after this, click will trigger flower action
                }
            }

            globalState.globalClickable = true;

        }

    }

    IEnumerator graveyardConditionMetCoroutine()
    {
        if (!globalState.graveyardConditionMetTriggered)
        {

            globalState.globalClickable = false;
            //sfx
            globalState.audio.playSFX(10, 7);

            yield return new WaitForSeconds(2);

            Transform dg = globalState.graveyardScene.transform.Find("doggo");
            dg.gameObject.SetActive(true);
            globalState.audio.fadeVolumeSFX(10, 0, 3, 0.6f);
            globalState.audio.fadeVolumeSFX(10, 1, 3, 0.6f);

            dg.GetComponent<Animator>().SetTrigger("fadeIn");
            yield return new WaitForSeconds(2);

            globalState.globalClickable = true;

        }
    }

    //another copy in animEventLink
    public static bool mouseAtCornerBottomLeft(GameObject go)
    {
        bool mobile = enabler.isMobile();

        //print(Camera.main.WorldToScreenPoint(go.transform.position));
        Vector2 point = mobile ? Camera.main.WorldToScreenPoint(go.transform.position) : Input.mousePosition;

        float dist = Vector2.Distance(point, new Vector2(0, 0));
        float minDist = (float)Screen.width / 1600 * (mobile ? 470 : 80);

     //   print("min: " + minDist + " d: " + dist);
        //return false;
        return (dist < minDist);
    }

    //called at the end of animations
    public void activateMouseBasedCamShift(GameObject go)
    {
        go.GetComponent<Animator>().enabled = false;
        go.GetComponent<MouseBasedCamShift>().setActive(true);
    }

    public void deactivateMouseBasedCamShift(GameObject go) //and enable animator
    {
        go.GetComponent<Animator>().enabled = true;
        go.GetComponent<MouseBasedCamShift>().setActive(false);
    }
}
