﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class interactable : MonoBehaviour
{
    private int timesClicked = 0; //this should keep track of effective clicks
    public bool clickable = true;
    Animator myAnimator;
    public int numInteractions;
    public GameObject changeIntoPrefab, affectsGO; //applies when type is animThenImgChange

    public enum InteractType { animThenImgChange, anim, imgSwitcher, instrument, clickInspect, custom };
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
                    Global.Do(() => { 
                        if(var1 == 0) //if flower already clicked before this, will set var1 to 1
                        myAnimator.Play("lawnFloIdle");    } )));
            
        }

        if(name.Equals("ballAnimator") || name.Equals("stickAnimator"))
        {
            myAnimator.SetTrigger("action1"); //idle state
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

                }
                break;
            case InteractType.imgSwitcher: //change base images to the next pair (with effects)
                GetComponent<imgSwitcher>().myTriggerAction();
                break;
            case InteractType.instrument: 
                if(gameControl.GetComponent<globalStateStore>().globalCounter < 2) //phone and speaker not triggered sprite swap yet
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
                    if (timesClicked < numInteractions)
                    {
                        //TODO start puzzle session; if already activated, cancel all and replay solution

                        

                        myAnimator.SetTrigger("action" + timesClicked.ToString());
                        //play sound effect (solution)

                        NotesRecord notesRecord = GetComponent<NotesRecord>();
                        //if starts another instrument, will deactivate prev instrument
                        NotesRecord.currActiveInstrument = notesRecord.instrumentIndex;
                        notesRecord.resetAllNoteStatus();

                    }
                    else
                    {
                        instrumentStartPlaying();
                        //sound
                    }
                }

                break;
            case InteractType.clickInspect: //start canvas obj
                //focus on object clicked (cam focus + pos shift)
                CamMovement cam = gameControl.GetComponent<enabler>().cam;
                cam.camFocusOnObject(transform.position);

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
                print("fork triggered snake");
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

        if(sqr.var1 >= 4) {
            sqr.onClick();
        }
    }

    //gets called at the end of leaf3's action2 clip
    public void checkWormAppearance()
    {
        if(var1 == 1)
        {
            affectsGO.GetComponent<Animator>().SetTrigger("action1");

        }
    }

    IEnumerator snakeDisappear()
    {
        myAnimator.SetTrigger("action1"); //hiss
        yield return new WaitForSeconds(2);
        GameObject pasta = swapSpriteToTarget();
        yield return new WaitForSeconds(3);
        gameControl.GetComponent<BlurManager>().scene1Clear1();
        gameControl.GetComponent<BlurManager>().backBlur.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void instrumentStartPlaying()
    {
        globalStateStore gs = gameControl.GetComponent<globalStateStore>(); 

        myAnimator.SetInteger("state", 2);
        gs.globalCounter += 1;

        switch (name)
        {
            case "guitar": gs.guitar = true;
                gameControl.GetComponent<AudioManager>().playSFX(4, 9);
                break;
            case "drums": gs.drums = true; gameControl.GetComponent<AudioManager>().playSFX(4, 11); break;
            case "accordion": gs.accordion = true; gameControl.GetComponent<AudioManager>().playSFX(4, 10); break;
        }

        if(gs.drums && gs.guitar && gs.accordion && !gs.hasScrolled) //everything triggered for l2
        {
            gameControl.GetComponent<BlurManager>().levelPassEffect(4);
            gs.hasScrolled = true;
        }
    }

    public void awayFadeInFinished()
    {
        clickable = true;
        timesClicked = 1;
    }

    public void lookOverFinished()
    {
        gameControl.GetComponent<BlurManager>().levelPassEffect(9);
    }






    IEnumerator customBehavior()
    {
        switch (name)
        {
            case "soccer":
                if (var1 == 0)
                {
                    print("play child sigh sfx");
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        myAnimator.SetTrigger("action" + ((Random.Range(0f, 1f) < 0.5f)? "2":"3")); //slight bounce or rotate

                    }
                }
                else if (var1 == 1)
                {
                    myAnimator.SetTrigger("action4"); // roll away

                    yield return new WaitForSeconds(10);

                    //TODO sfx, footstep, muffled sound

                    GameObject ma = globalState.vaseScene.transform.Find("mom").gameObject,
                    blkt = globalState.vaseScene.transform.Find("blanket").gameObject,
                    vase = globalState.vaseScene.transform.Find("broken_vase").gameObject,
                    plate = globalState.vaseScene.transform.Find("plate").gameObject,
                    nurse = globalState.vaseScene.transform.Find("nurse").gameObject,
                    sofa = globalState.vaseScene.transform.Find("sofa").gameObject;

                    ma.SetActive(true);
                    ma.GetComponent<Animator>().SetTrigger("fadeIn");

                    yield return new WaitForSeconds(3);

                    ma.GetComponent<imgSwitcher>().switchToNextImgState(); //head tilt
                    yield return new WaitForSeconds(2);
                    ma.GetComponent<imgSwitcher>().switchToImgState(0);
                    yield return new WaitForSeconds(3);

                    //TODO fade in of mask
                    //blanket masks
                    
                    blkt.transform.Find("mask1").gameObject.SetActive(true);
                    blkt.GetComponent<Animator>().SetTrigger("action1"); //mask anim

                    yield return new WaitForSeconds(6);

                    ma.GetComponent<imgSwitcher>().switchToImgState(1); //head tilt
                    yield return new WaitForSeconds(3);
                    ma.GetComponent<imgSwitcher>().switchToImgState(2); //reach
                    yield return new WaitForSeconds(1);
                    blkt.GetComponent<Animator>().SetTrigger("fadeOut");
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
                    yield return new WaitForSeconds(1);
                    camMovement.cam.Play("nervousBreathe");
                    yield return new WaitForSeconds(7);

                    //TODO faint vfx

                    //flash
                    ma.SetActive(false);
                    vase.SetActive(false);
                    sofa.SetActive(false);
                    nurse.SetActive(true);
                    plate.SetActive(true);
                    yield return new WaitForSeconds(0.2f);

                    print("s1");

                    ma.SetActive(true);
                    vase.SetActive(true);
                    sofa.SetActive(true);
                    nurse.SetActive(false);
                    plate.SetActive(false);
                    yield return new WaitForSeconds(2f);

                    print("s2");

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
                    yield return new WaitForSeconds(2); //TODO sfx
                    camMovement.vfx.Play("blink");
                    yield return new WaitForSeconds(6);

                    ma.GetComponent<Animator>().SetTrigger("fadeIn");
                    Transform bin = globalState.vaseScene.transform.Find("bin");
                    bin.gameObject.SetActive(true);
                    bin.GetComponent<Animator>().SetTrigger("fadeIn");
                    camMovement.cam.Play("naturalBreathe");

                    yield return new WaitForSeconds(3);

                    ma.GetComponent<imgSwitcher>().switchToImgState(3); //glove reach
                    yield return new WaitForSeconds(4);

                    vase.GetComponent<Animator>().SetTrigger("fadeOut");
                    ma.GetComponent<imgSwitcher>().switchToImgState(1);

                    yield return new WaitForSeconds(4);
                    ma.GetComponent<imgSwitcher>().switchToImgState(0);


                    yield return new WaitForSeconds(8);
                    camMovement.cam.Play("vaseSceneEndZoom"); 
                    //sfx thud
                    yield return new WaitForSeconds(4);
                    ma.GetComponent<imgSwitcher>().switchToImgState(4);
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
                            Transform flo = transform.Find("flowers");
                            flo.GetComponent<Animator>().enabled = false;
                            flo.Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            flo.Find("Image (1)").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }), 
                            Global.WaitForSeconds(2f),
                            //TODO blink, slight shake of cam
                           camMovement.glanceAndMoveBack(new Vector2(-200, 60), 0.1f),
                            
                            Global.Do(() => { 
                                globalState.vaseScene.transform.Find("sofa").GetComponent<interactable>().clickable = true; //enable sofa click
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
                if(Random.Range(0f, 1f) < 0.5f)
                {
                    myAnimator.SetTrigger("action1");
                }
                else
                {
                    myAnimator.SetTrigger("action2");
                }
                break;

            case "bird":
                if(var1 == 0)
                {
                    myAnimator.SetTrigger("action1");
                    //sfx
                }else if(var1 == 1) //feedable
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
                    myAnimator.SetTrigger("action1");
                    rech.GetComponent<Animator>().SetTrigger("action2");
                    camMovement.cam.Play("camEggReach");

                    yield return new WaitForSeconds(0.5f);
                    bird.GetComponent<Animator>().SetTrigger("action1");
                }else if(var1 == 1) //fed and clicks egg
                {
                    //something magical
                    print("something magical");
                    
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
                    gameObject.SetActive(false); //deactivate ball
                }

                else if (var1 == 0) //on lawn
                {
                    if((!(var3 >= 3 && var2 == 0)) && (timesClicked == 1 || Random.Range(0f, 1f) < 0.5f)) //either first encounter or 50% chance will perform hold, or too many throws with no play
                    {
                        transform.parent.SetAsLastSibling(); //so that item sprite on top of hand

                        //forced fetch (starts with hand motion)
                        myAnimator.SetTrigger("action2"); //hold
                        hand.SetTrigger("action1"); //hold

                        //toggle on mouse based cam shift
                        hand.transform.parent.GetComponent<MouseBasedCamShift>().active = true;
                        transform.parent.GetComponent<MouseBasedCamShift>().active = true;

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
                    if ( (!(var2 >= 3 && var3 == 0)) && ((var3 >= 3 && var2 == 0) || Random.Range(0f, 1f) < 0.5f))
                    {

                        transform.parent.SetAsLastSibling(); //so that item sprite on top of hand

                        myAnimator.SetTrigger("action2"); //hold
                        hd.SetTrigger("action1"); //hold

                        //toggle on mouse based cam shift
                        hd.transform.parent.GetComponent<MouseBasedCamShift>().active = true;
                        transform.parent.GetComponent<MouseBasedCamShift>().active = true;

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
                hdd.transform.parent.GetComponent<MouseBasedCamShift>().active = false;


                if (bl.var1 == 1)
                {
                    pp.Play("dFetchBall");
                    bl.GetComponent<Animator>().SetTrigger("action3");
                    bl.transform.parent.GetComponent<MouseBasedCamShift>().active = false;
                }
                else if (stk.var1 == 1)
                {
                    pp.Play("dFetchTwig");
                    stk.GetComponent<Animator>().SetTrigger("action3");
                    stk.transform.parent.GetComponent<MouseBasedCamShift>().active = false;
                }
                else Debug.LogError("no item is being held");

                yield return new WaitForSeconds(3);

                hdd.transform.parent.SetAsLastSibling(); //reset hand sprite's layer status to the topmost
                gameObject.SetActive(false); //disable self

                break;

            /////////////////////
            case "rightHandFlowers":
                myAnimator.SetTrigger("fadeOut");
                //TODO hand anim out, show hand anim



                break;
        }


        if(name[0] == 'f' && transform.parent.name.Equals("flowers"))
        {
            //lawn flower instance
            myAnimator.Play("empty");

            float rd = Random.Range(0f, 1f);


            if (rd <= 0.333f)
            {
                myAnimator.Play("lawnFlo1");
                var1 = 1;
            }
            else if(rd <= 0.666f)
            {
                myAnimator.Play("lawnFlo2");
                var1 = 1;
            }
            else
            {
                myAnimator.Play("lawnFlo3");
                var1 = 1;
            }

            

        }else if (transform.parent.name.Equals("pup")) //one of pup sprites
        {

            if (name.Equals("d1") || name.Equals("d1.5"))
            {
                transform.parent.GetComponent<Animator>().Play("dSit");
                transform.parent.Find("d2").GetComponent<interactable>().var2 = 1; //mark as visited

                globalState.StartCoroutine(Global.Chain(globalState, Global.Do(()=> globalState.globalClickable = false), 
                    Global.WaitForSeconds(2.5f), 
                    Global.Do(()=> globalState.globalClickable = true)));

            }
            else if (name.Equals("d2"))
            {
                float rand = Random.Range(0f, 1f);

                transform.parent.GetComponent<Animator>().Play((rand > 0.5f)? "dStand" : "dRollover");
                if (rand > 0.5f) transform.parent.Find("d3").GetComponent<interactable>().var2 = 1;  //mark as visited
                else transform.parent.Find("d1").GetComponent<interactable>().var2 = 1;  //mark as visited

                globalState.StartCoroutine(Global.Chain(globalState, Global.Do(() => globalState.globalClickable = false),
                    Global.WaitForSeconds(2.5f),
                    Global.Do(() => globalState.globalClickable = true)));

            }
            else if (name.Equals("d3"))
            {
                transform.parent.GetComponent<Animator>().Play("dSit");
                transform.parent.Find("d2").GetComponent<interactable>().var2 = 1;  //mark as visited

                globalState.StartCoroutine(Global.Chain(globalState, Global.Do(() => globalState.globalClickable = false),
                    Global.WaitForSeconds(2.5f),
                    Global.Do(() => globalState.globalClickable = true)));
            }

            interactable d1 = transform.parent.Find("d1").GetComponent<interactable>(), d2 = transform.parent.Find("d2").GetComponent<interactable>(),
            d3 = transform.parent.Find("d3").GetComponent<interactable>();

            //if all three poses visited, unlock clickable on the two items
            if(d1.var3 != 5 && d1.var2 == 1 && d2.var2 == 1 && d3.var2 == 1)
            {
                Transform ball = globalState.pupScene.transform.Find("ball"), stick = globalState.pupScene.transform.Find("stick");

                ball.gameObject.SetActive(true); stick.gameObject.SetActive(true);

                ball.GetChild(0).GetComponent<Animator>().SetTrigger("fadeIn");
                stick.GetChild(0).GetComponent<Animator>().SetTrigger("fadeIn");

                ball.GetChild(0).GetComponent<interactable>().clickable = true;
                stick.GetChild(0).GetComponent<interactable>().clickable = true;

                d1.var3 = 5; //prevent this code from being reached again
            }

        }else if (name[0] == 'n' && (transform.parent.name.Equals("guitar") || transform.parent.name.Equals("drums") ||
            transform.parent.name.Equals("accordion")))
        {
            int noteIndex = int.Parse(name[1].ToString());

            NotesRecord notesRecord = transform.parent.GetComponent<NotesRecord>();
            notesRecord.recordNote(noteIndex);



        }
    }












}
