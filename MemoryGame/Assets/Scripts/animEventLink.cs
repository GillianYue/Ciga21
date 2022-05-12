using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class animEventLink : MonoBehaviour
{
    public globalStateStore globalState;
    public enabler enable;
    public CamMovement camMovement;

    private void Awake()
    {
        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();
        if (enable == null) enable = FindObjectOfType<enabler>();
        if (camMovement == null) camMovement = FindObjectOfType<CamMovement>();

    }

    private void Start()
    {
        if (name[0] == 'l' && transform.parent.name.Equals("up_front"))
        {

            GetComponent<Animator>().SetTrigger("action" + name[1]);
            //   GetComponent<Animator>().SetTrigger("fadeIn"); //fades in lavendar color

        }
        else if (transform.childCount > 0 && transform.GetChild(0).name.Equals("bush"))
        {
            StartCoroutine(Global.Chain(this,
                    Global.WaitForSeconds(Random.Range(0f, 2f)),
                    Global.Do(() =>
                    {
                        GetComponent<Animator>().Play("bush" + (Random.Range(1, 3)));
                    })));

        }
        else if (name[0] == 'f' && transform.parent.name.Equals("foxtail"))
        {
            GetComponent<Animator>().SetTrigger("action" + Random.Range(1, 5));
        }
        else if (name.Equals("bushesCloseup"))
        {
            GetComponent<Animator>().SetTrigger("action1"); //default action
        }

    }

    //called from animation after screen fades out
    public void treeSceneEndTransition()
    {
        StartCoroutine(treeSceneEndTransitionCoroutine());

    }

    IEnumerator treeSceneEndTransitionCoroutine()
    {
        yield return new WaitForSeconds(4);

        enable.setUpLevel(3, true);
        globalState.audio.playSFX(3, 9, 0.1f); //tree wind
        globalState.audio.fadeVolumeSFX(3, 9, 6, 1);

        yield return new WaitForSeconds(7); //wait for anim to fade into new scene view
        camMovement.cam.Play("naturalBreathe");

        yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(4);

        camMovement.vfx.Play("blink");

        yield return new WaitForSeconds(3);

        globalState.treeBottomScene.transform.Find("tree").GetComponent<Animator>().Play("treeBottom"); //fade in top layer sprite

        yield return new WaitForSeconds(2);

        camMovement.vfx.Play("blink");

        yield return new WaitForSeconds(10);

        //end of scene
        enable.GetComponent<BlurManager>().levelPassEffect(3);
    }

    //called from the end of fetch item animations
    public void pupDropItem(int itemIndex)
    {

        globalState.audio.playSFX(6, 9);

        if (itemIndex == 0) //ball
        {
            Transform ball = globalState.pupScene.transform.Find("ball").GetChild(0);
            ball.gameObject.SetActive(true);
            Animator ba = ball.GetComponent<Animator>();
            ba.SetTrigger("action1"); //restore default pos
            ba.SetTrigger("fadeIn"); //fadein

            interactable bl = ball.GetComponent<interactable>();
            bl.var1 = 0;

        }
        else if (itemIndex == 1) //stick
        {
            Transform stick = globalState.pupScene.transform.Find("stick").GetChild(0);
            stick.gameObject.SetActive(true);
            Animator sa = stick.GetComponent<Animator>();
            sa.SetTrigger("action1");
            sa.SetTrigger("fadeIn"); //fadein

            interactable stk = stick.GetComponent<interactable>();
            stk.var1 = 0;
        }
        else if (itemIndex == 2) //still ball, but signals ending anim for scene
        {
            Transform ball = globalState.pupScene.transform.Find("ball").GetChild(0);
            ball.transform.localPosition = new Vector2(0, 0);
            ball.gameObject.SetActive(true);
            Animator ba = ball.GetComponent<Animator>();
            ba.SetTrigger("action4"); //roll away
        }
    }

    public void pupSceneCamShift()
    {
        camMovement.cam.Play("camPupSceneShift");
        globalState.audio.fadeVolumeSFX(6, 6, 6, 0);
    }

    public void pupSceneCamShiftEnd()
    {
        globalState.pupScene.transform.Find("her").GetComponent<imgSwitcher>().switchToNextImgState();
        globalState.globalClickable = true;

        //TODO scene transitions and stuff

        StartCoroutine(Global.Chain(this, Global.WaitForSeconds(2),
            Global.Do(() =>
                    {
                        camMovement.vfx.Play("focusOnHer");
                    }), Global.WaitForSeconds(4.5f), Global.Do(() =>
                    {
                        //end of scene
                        enable.GetComponent<BlurManager>().levelPassEffect(6);
                    })));
    }

    public void sunsetDone()
    {
        StartCoroutine(sunsetDoneCoroutine());
    }

    public void girlFall()
    {
        camMovement.cam.Play("camBickerFall");
    }

    public void girlFallMidSetup()
    {
        Animator her = globalState.bickerScene.transform.Find("her_closeup").GetComponent<Animator>();
        her.gameObject.SetActive(true);
        globalState.bickerScene.transform.Find("her").gameObject.SetActive(false);

        globalState.bickerScene.transform.Find("slice_closeup").GetComponent<Animator>().SetTrigger("action4");
        her.SetTrigger("action1");

        StartCoroutine(Global.Chain(this, Global.WaitForSeconds(4),
                        Global.Do(() =>
                        {
                            //sfx laughter
                        }), Global.WaitForSeconds(1), Global.Do(() =>
                        {
                            //end of scene
                            enable.GetComponent<BlurManager>().levelPassEffect(8);
                        })));
    }

    public void deactivateGO() { gameObject.SetActive(false); }

    //called at the end of animations
    public void activateMouseBasedCamShift()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<MouseBasedCamShift>().setActive(true);
    }

    public void deactivateMouseBasedCamShift(GameObject go) //and enable animator
    {
        go.GetComponent<Animator>().enabled = true;
        go.GetComponent<MouseBasedCamShift>().setActive(false);
    }

    IEnumerator sunsetDoneCoroutine()
    {

        yield return new WaitForSeconds(2);
        //play sfx, footsteps and voice

        yield return new WaitForSeconds(2);

        camMovement.camHolder.Play("camShiftRight"); //turn right
        yield return new WaitForSeconds(2);

        Transform f1 = globalState.seaScene.transform.Find("beach/friend1"), f2 = globalState.seaScene.transform.Find("beach/friend2"),
            f3 = globalState.seaScene.transform.Find("beach/friend3");

        f3.gameObject.SetActive(true);
        f3.GetComponent<Animator>().SetTrigger("hide"); //hide sprite for friend 3
        f3.Find("light").gameObject.SetActive(false); //hide reflection

        Transform stand = f3.Find("friend3Standing");
        stand.gameObject.SetActive(true);
        stand.GetComponent<Animator>().SetTrigger("fadeIn");
        globalState.audio.playSFX(5, 0);

        yield return new WaitForSeconds(3);
        f2.GetComponent<imgSwitcher>().switchToImgState(1);
        f2.Find("light").gameObject.SetActive(false);
        f2.Find("lightAway").gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        f1.GetComponent<imgSwitcher>().switchToImgState(2); //looking at 3
        f1.Find("light").gameObject.SetActive(false);
        f1.Find("lightAway").gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        stand.GetComponent<Animator>().SetTrigger("fadeOut");
        f2.GetComponent<imgSwitcher>().switchToImgState(2); //take beer
        yield return new WaitForSeconds(1.5f);

        f1.GetComponent<imgSwitcher>().switchToImgState(3); //take beer
        Transform friendBeer = f1.Find("beer");
        friendBeer.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);
        Transform hand = globalState.seaScene.transform.Find("hand_beer");
        hand.gameObject.SetActive(true);
        Animator handAnim = hand.GetChild(0).GetComponent<Animator>();
        handAnim.SetTrigger("action1"); //hand out

        globalState.audio.playSFX(5, 8); //faint laughters

        yield return new WaitForSeconds(1.5f);

        globalState.audio.playSFX(5, 1);
        f3.GetComponent<Animator>().SetTrigger("fadeIn"); //fade in f3 sprite
        Transform f3Light = f3.Find("light");
        f3Light.gameObject.SetActive(true);
        f3Light.GetComponent<Animator>().SetTrigger("fadeIn"); //will auto-transition to light flicker

        f1.GetComponent<imgSwitcher>().switchToImgState(1);
        f1.Find("light").gameObject.SetActive(true);
        f1.Find("lightAway").gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        f3.GetComponent<imgSwitcher>().switchToImgState(1);

        yield return new WaitForSeconds(2);
        handAnim.SetTrigger("action3");
        yield return new WaitForSeconds(1);

        camMovement.camHolder.Play("camShiftLeftBack");
        yield return new WaitForSeconds(4);

        camMovement.camHolder.enabled = false;
        camMovement.cam.Play("camBeerDrink");
        //TODO sfx
        globalState.audio.playSFX(5, 2);
        yield return new WaitForSeconds(6);

        camMovement.camHolder.enabled = true;

        camMovement.camHolder.Play("camShiftDown");

        yield return new WaitForSeconds(4);
        camMovement.camHolder.Play("camShiftUp");

        yield return new WaitForSeconds(3);
        //TODO sfx to the right
        camMovement.camHolder.Play("camShiftRight"); //turn right

        yield return new WaitForSeconds(1);
        friendBeer.GetComponent<Animator>().SetTrigger("action1"); //raise

        yield return new WaitForSeconds(2);

        handAnim.SetTrigger("action1"); //out
        yield return new WaitForSeconds(2);

        handAnim.enabled = false;
        MouseBasedCamShift camShift = handAnim.GetComponent<MouseBasedCamShift>();
        camShift.setActive(true); //enable mouse based movement

        yield return new WaitUntil(() =>
        {
            return (Vector2.Distance(handAnim.transform.position, friendBeer.position) < 410);
        }); //wait til close enough

        handAnim.GetComponent<MouseBasedCamShift>().setActive(false);
        handAnim.enabled = true;

        //TODO sfx of chatter
        //blur
        handAnim.SetTrigger("action2"); //cheers
        friendBeer.GetComponent<Animator>().SetTrigger("action2");

        yield return new WaitForSeconds(3);
        f1.GetComponent<imgSwitcher>().switchToImgState(3);
        f1.Find("light").gameObject.SetActive(false);
        f1.Find("lightAway").gameObject.SetActive(true);

        yield return new WaitForSeconds(2);
        camMovement.camHolder.Play("camShiftLeftBack");

        enable.darkCover.enabled = true;

        yield return new WaitForSeconds(2);
        camMovement.vfx.Play("blink");
        yield return new WaitForSeconds(1);

        enable.darkCover.SetTrigger("fadeInSlow"); //very slowly fades to black
        Transform sea = globalState.seaScene.transform.Find("sea"),
            myself = globalState.seaScene.transform.Find("myself");
        myself.Find("myself_light").gameObject.SetActive(false); //disable reflection

        globalState.audio.fadeVolumeSFX(5, 8, 1, 0);
        globalState.audio.fadeVolumeSFX(5, 6, 3, 0.15f);

        yield return new WaitForSeconds(3);
        globalState.audio.fadeVolumeSFX(5, 4, 5, 0);
        yield return new WaitForSeconds(2);

        //set up for night

        sea.Find("dusk/duskSky").gameObject.SetActive(false);
        sea.Find("dusk/sun/sunMask").gameObject.SetActive(false);
        sea.Find("dusk/sun/sunGlowMask").gameObject.SetActive(false);
        sea.Find("dusk/sun/glowOnSea").gameObject.SetActive(false);

        sea.Find("night").gameObject.SetActive(true);
        sea.Find("waves").GetComponent<Animator>().Play("wave1Hold"); //wave anim transition

        //disable sunset reflections
        friendBeer.gameObject.SetActive(false);
        hand.gameObject.SetActive(false);
        f3.Find("light").gameObject.SetActive(false);
        f2.Find("light").gameObject.SetActive(false);
        f1.Find("light").gameObject.SetActive(false);
        f2.Find("lightAway").gameObject.SetActive(false);
        f1.Find("lightAway").gameObject.SetActive(false);

        //adjust character sprites
        f1.GetComponent<imgSwitcher>().switchToImgState(2);
        f2.GetComponent<imgSwitcher>().switchToImgState(1);
        f3.GetComponent<imgSwitcher>().switchToImgState(0);

        camMovement.camHolder.GetComponent<MouseBasedCamShift>().setActive(false);

        camMovement.camHolder.Play("camShiftDown"); //so that when eyes open is staring at self

        yield return new WaitForSeconds(3);
        enable.darkCover.SetTrigger("fadeOutSlow");

        yield return new WaitForSeconds(3);
        camMovement.vfx.Play("blink");
        yield return new WaitForSeconds(1);

        camMovement.camHolder.Play("camShiftUp");
        yield return new WaitForSeconds(6);

        camMovement.camHolder.Play("camShiftRight");

        yield return new WaitForSeconds(5);
        f2.GetComponent<imgSwitcher>().switchToImgState(0);
        yield return new WaitForSeconds(3);

        f2.GetComponent<imgSwitcher>().switchToImgState(3);
        yield return new WaitForSeconds(1);
        //sfx of voice "stars"
        globalState.audio.playSFX(5, 7);
        yield return new WaitForSeconds(2);

        camMovement.camHolder.Play("camShiftLeftBack");
        yield return new WaitForSeconds(3);

        //hide friends, no longer needed in game
        f1.gameObject.SetActive(false);
        f2.gameObject.SetActive(false);
        f3.gameObject.SetActive(false);

        Transform handPoint = globalState.seaScene.transform.Find("hand_point");
        handPoint.gameObject.SetActive(true);
        handPoint.GetComponent<Animator>().Play("handPointOut");

        globalState.seaScene.transform.Find("sea/night/stars").gameObject.SetActive(true);
        starsManager stars = FindObjectOfType<starsManager>();
        stars.startStarCheck(); //activate first star

        yield return new WaitForSeconds(2);

        globalState.audio.playSFX(5, 5); //stars bgm
    }

    //called from faceaway interactable script, this script on "her" GO
    public void faceawayInteract()
    {
        StartCoroutine(faceawayInteractCoroutine());
    }

    IEnumerator faceawayInteractCoroutine()
    {
        Animator h = GetComponent<Animator>();
        transform.Find("faceaway").GetComponent<interactable>().clickable = false;
        h.Play("girlShrug");

        //disgruntled sfx
        yield return new WaitForSeconds(5);

        camMovement.cam.Play("camGrabNspp");
        yield return new WaitForSeconds(1);
        Transform m_nspp = globalState.bickerScene.transform.Find("newspaper");
        m_nspp.GetComponent<Animator>().SetTrigger("fadeOut");

        Animator nspp_closeup = globalState.bickerScene.transform.Find("newspaper_closeup").GetComponent<Animator>();

        yield return new WaitForSeconds(2);
        nspp_closeup.gameObject.SetActive(true);
        nspp_closeup.Play("nsppOut"); //out

        yield return new WaitForSeconds(2);

        //toggle mouse shift
        nspp_closeup.enabled = false;
        nspp_closeup.GetComponent<MouseBasedCamShift>().setActive(true);

        yield return new WaitForSeconds(2);

        h.Play("girlAwayQuickPeek");

        yield return new WaitForSeconds(1.5f);

        globalState.interactHint(false); //move hint

        yield return new WaitUntil(() => interactable.mouseAtCornerBottomLeft(nspp_closeup.gameObject)); //wait until mouse scrolls to position that reveals face

        //peek at "us" when discovered
        h.Play("girlAwayPeek");

        yield return new WaitForSeconds(1.5f);

        //subtle change of sprite here
        Transform mdrn = globalState.bickerScene.transform.Find("fruitPlatter/mandarin"), apple = globalState.bickerScene.transform.Find("fruitPlatter/apple");
        mdrn.gameObject.SetActive(false);
        apple.gameObject.SetActive(true);

        //cam + nspp anim, zoom in as if reading closely, do this for a while and zoom out and check on her (nspp down a bit, blink)
        nspp_closeup.GetComponent<MouseBasedCamShift>().setActive(false);
        nspp_closeup.enabled = true;

        nspp_closeup.Play("nsppFocus"); //focus
        camMovement.cam.Play("camNsppFocus");

        yield return new WaitForSeconds(4);

        camMovement.vfx.Play("blink");

        //move fishtank and change sprite

        h.Play("girlNormalsit"); //staring at fishtank
        Transform fshtank = globalState.bickerScene.transform.Find("fishtank");
        fshtank.transform.localPosition = new Vector2(-30, -110);

        yield return new WaitForSeconds(4); //wait for cam and nspp anim above to end

        globalState.interactHint(false); //move
        yield return new WaitUntil(() => interactable.mouseAtCornerBottomLeft(nspp_closeup.gameObject));
        deactivateMouseBasedCamShift(nspp_closeup.gameObject);

        //yield return new WaitForSeconds(1.5f);
        //down
        nspp_closeup.Play("nsppDown");

        yield return new WaitForSeconds(3);
        //wait for fish interact

        yield return new WaitForSeconds(2);

        globalState.bickerScene.transform.Find("fishtank/fish/fish/fish1").GetComponent<interactable>().var1 = 1; //toggle interact mode
    }

    public void blink() { camMovement.vfx.Play("blink"); }

    public void rippleInteract()
    {
        globalState.globalClickable = false;

    }

    //since under animEventLink, is necessarily called by an animation event
    public void setGlobalClickableTrue() {
        globalState.toggleAnimationGlobalClickable(true);
    }

    public void setGlobalClickableFalse() {
        globalState.toggleAnimationGlobalClickable(false);
    }

    public void glitchEffectEnd() { StartCoroutine(glitchEffectEndCoroutine()); }

    public IEnumerator glitchEffectEndCoroutine()
    {
        Animator her = globalState.parkScene.transform.Find("Her").GetComponent<Animator>(),
        hand3 = globalState.parkScene.transform.Find("MyHand3").GetComponent<Animator>();

        Transform hosp = globalState.parkScene.transform.Find("hosp"), glitch = globalState.parkScene.transform.Find("glitch");
        Transform screen = hosp.Find("screen");
        Animator sr = screen.Find("screenR").GetComponent<Animator>();

        her.Play("opaque"); //toggle back Image & Image (1)
        her.Play("herLinesAway");

        yield return new WaitForSeconds(1);
        Time.timeScale = 0.8f;

        yield return new WaitForSeconds(1);
        Time.timeScale = 1.2f;

        globalState.blurManager.centerBlur.setNewScale(3, 0.1f);
        yield return new WaitForSeconds(1);
        Time.timeScale = 1;

        yield return new WaitForSeconds(3);
        globalState.blurManager.centerBlur.setNewScale(0.2f, 0.1f);

        yield return new WaitForSeconds(2);
        hand3.gameObject.SetActive(true);
        hand3.SetTrigger("action4"); //reach
        camMovement.enable.darkCover.SetTrigger("fadeIn");
        //wait then sfx tunnel footsteps

        yield return new WaitForSeconds(2);
        globalState.audio.fadeVolumeSFX(9, 1, 2, 0);
        globalState.audio.fadeVolumeSFX(9, 10, 2, 0);
        globalState.audio.fadeVolumeSFX(9, 22, 2, 0);

        yield return new WaitForSeconds(0.5f);
        globalState.audio.playSFX(9, 29); //run down corridot

        yield return new WaitForSeconds(3);

        her.gameObject.SetActive(false);
        Transform kmt = globalState.parkScene.transform.Find("klimt");
        kmt.gameObject.SetActive(true);
        kmt.GetComponent<animEventLink>().klimt();

        sr.gameObject.SetActive(false);
        hand3.gameObject.SetActive(false);
        glitch.gameObject.SetActive(false);
    }

    public void klimt() { StartCoroutine(klimtCoroutine()); }

    IEnumerator klimtCoroutine()
    {
        camMovement.cam.SetTrigger("stopBreathe");

        yield return new WaitForSeconds(4);

        Animator bg = transform.Find("bg/pattern").GetComponent<Animator>(), fl = transform.Find("her/flowers").GetComponent<Animator>(),
            cloth = transform.Find("her/clothMask").GetComponent<Animator>();

        fl.Play("onOffGlitch");

        Transform objs = transform.Find("objects");

        globalState.toggleAnimationGlobalClickable(true);

        foreach (Transform obj in objs)
        {
            obj.transform.GetChild(0).GetComponent<Rigidbody2D>().AddTorque(Random.Range(20000, 100000) * ((Random.Range(0, 2) > 0) ? 1 : -1));
        }

        camMovement.enable.darkCover.SetTrigger("fadeOut");

        globalState.audio.playSFX(9, 20, 0.1f); //scary violins
        globalState.audio.fadeVolumeSFX(9, 20, 3, 1);

        yield return new WaitForSeconds(2);

        
    }

    public void collage()
    {
        StartCoroutine(collageCoroutine());
    }

    IEnumerator collageCoroutine()
    {
        globalState.audio.playSFX(9, 31, 0.1f); //clock with reverb
        globalState.audio.fadeVolumeSFX(9, 31, 3, 1);

        transform.Find("clothMask").GetComponent<Animator>().Play("clothGlitch2");

        globalState.parkScene.transform.Find("klimt").gameObject.SetActive(false);

        enable.darkCover.SetTrigger("fadeOutWhite");
        yield return new WaitForSeconds(2);

    }

    public void flat()
    {
        StartCoroutine(flatCoroutine());
    }

    IEnumerator flatCoroutine()
    {
        Transform hr = transform.Find("her");
        hr.gameObject.SetActive(true);
        hr.GetComponent<Animator>().SetTrigger("hide");

        globalState.audio.playSFX(9, 30); //deep thud
        globalState.audio.fadeVolumeSFX(9, 20, 5, 0); //fade out scary violins

        globalState.enable.darkCover.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(2);
        globalState.parkScene.transform.Find("collage").gameObject.SetActive(false);

        globalState.enable.darkCover.SetTrigger("fadeOutWhite");
        yield return new WaitForSeconds(2);

    }

    public void roses()
    {
        StartCoroutine(rosesCoroutine());
    }

    IEnumerator rosesCoroutine()
    {

        globalState.globalClickable = false;
        camMovement.vfx.Play("noises");
        globalState.parkScene.transform.Find("flat").gameObject.SetActive(false);

        globalState.enable.darkCover.SetTrigger("fadeOutWhite");
        yield return new WaitForSeconds(2);

        globalState.globalClickable = true;
    }

    public void rosesFadeOut()
    {
        StartCoroutine(rosesFadeOutCoroutine());
    }

    IEnumerator rosesFadeOutCoroutine()
    {
        Transform roses = globalState.parkScene.transform.Find("roses/rosesRotate"), rosesScene = globalState.parkScene.transform.Find("roses"),
            glitch = globalState.parkScene.transform.Find("glitch"), herGlitch = globalState.parkScene.transform.Find("glitch/her"),
            screen = globalState.parkScene.transform.Find("hosp/screen"), herHosp = globalState.parkScene.transform.Find("hosp/Her"),
            hosp = globalState.parkScene.transform.Find("hosp/bg"), herRoses = globalState.parkScene.transform.Find("roses/her");

        //setup
        herHosp.Find("face").gameObject.SetActive(false);
        globalState.toggleAnimationGlobalClickable(false);

        int count = 0;
        foreach (Transform r in roses)
        {
            count += 1;

            StartCoroutine(Global.Chain(this,
                Global.WaitForSeconds(count * 0.6f),
                Global.Do(() =>
                {
                    r.GetComponent<Animator>().Play("roseRotateDisappear");
                })));
        }

        yield return new WaitForSeconds(6);
        herRoses.GetComponent<imgSwitcher>().switchToImgState(1);
        globalState.audio.playSFX(9, 11);

        yield return new WaitForSeconds(1);

        GameObject sr = screen.Find("screenR").gameObject;
        sr.SetActive(false);
        Animator sw = screen.Find("screenW").GetComponent<Animator>();
        screen.gameObject.SetActive(true);
        sw.gameObject.SetActive(true);
        sw.Play("screenFadeIn");

        yield return new WaitForSeconds(4);

        globalState.audio.playSFX(9, 2, 0.1f);
        globalState.audio.fadeVolumeSFX(9, 2, 10, 0.8f);
        globalState.audio.fadeVolumeSFX(9, 31, 3, 0.3f); //fade out clock

        yield return new WaitForSeconds(8);

        rosesScene.gameObject.SetActive(false);

        hosp.GetComponent<Image>().enabled = true;
        herHosp.gameObject.SetActive(true);
        Transform fc = herHosp.Find("face");
        fc.gameObject.SetActive(false);

        camMovement.vfx.Play("noisesFadeOut");

        hosp.GetComponent<Animator>().Play("hspFadeAction1");

        yield return new WaitForSeconds(3);

        yield return new WaitForSeconds(2);

        Animator hh = herHosp.GetComponent<Animator>();
        hh.SetTrigger("fadeIn"); //universe 
        hh.SetTrigger("action1");

        yield return new WaitForSeconds(13);
        herHosp.GetComponent<Animator>().SetTrigger("fadeOut"); //only fade out bg

        yield return new WaitForSeconds(3);

        //fade in hosp bg
        hosp.GetComponent<Animator>().Play("hospFadeIn");
        yield return new WaitForSeconds(9);

        fc.gameObject.SetActive(true);
        fc.GetComponent<Animator>().SetTrigger("fadeInSlow"); //fade in face
        yield return new WaitForSeconds(5);

        sw.gameObject.SetActive(false);

        globalState.audio.playSFX(9, 18); //reverse cymbal
        globalState.audio.fadeVolumeSFX(9, 31, 2, 0f); //fade out clock
        yield return new WaitForSeconds(2);
        sr.GetComponent<Animator>().enabled = false;
        sr.transform.localPosition = new Vector2(371, 227);
        sr.SetActive(true);
        //play sfx machine long beep
        globalState.audio.playSFX(9, 23);

        yield return new WaitForSeconds(4);

        globalState.audio.fadeVolumeSFX(9, 23, 5, 0);
        //lv pass
        yield return new WaitForSeconds(3);

        //end of scene
        enable.GetComponent<BlurManager>().levelPassEffect(9);
        globalState.toggleAnimationGlobalClickable(true);
    }

    public void playSfx(string levelUnderlineIndex) //e.g. 11_2
    {
        string[] splits = levelUnderlineIndex.Split('_');
        int lv = int.Parse(splits[0]), sfx = int.Parse(splits[1]);
        globalState.audio.playSFX(lv, sfx);
    }

    public void playRandomThunderSound()
    {
        globalState.audio.playSFX(10, Random.Range(2, 6));
    }

    public void initiatePhoneCheck() { StartCoroutine(phoneCheckCoroutine()); }

    IEnumerator phoneCheckCoroutine()
    {
        globalState.toggleAnimationGlobalClickable(false);

        yield return new WaitForSeconds(2);
        camMovement.vfx.Play("blink");

        yield return new WaitForSeconds(1);
        //sfx phone beep
        globalState.audio.playSFX(11, 4);
        Animator mob = globalState.homeScene.transform.Find("mobile").GetComponent<Animator>();
        mob.SetTrigger("action2");

        yield return new WaitForSeconds(1.5f);

        mob.SetTrigger("fadeOut");

        globalState.homeScene.transform.Find("phone/texts").GetComponent<setDateTimeTexts>().setDateTimeText(); //sets date time, to the right language as well

        yield return new WaitForSeconds(3f);

        GetComponent<Animator>().SetTrigger("action1"); //phone check anim
    }

    //called from phone anim
    public void bookPatternChange()
    {
        Transform bk = globalState.homeScene.transform.Find("book");
        bk.Find("texts").gameObject.SetActive(false);
        bk.Find("textsBtfl").gameObject.SetActive(true);

        globalState.homeScene.transform.Find("btfl").gameObject.SetActive(true);
        globalState.homeScene.transform.Find("dog_away").gameObject.SetActive(false);
    }

    //also called from phone anim
    public void btflFlyAction()
    {

        globalState.homeScene.transform.Find("btfl").GetComponent<Animator>().SetTrigger("action1");
        StartCoroutine(flyActionSequence());

    }

    IEnumerator flyActionSequence()
    {
        yield return new WaitForSeconds(4);

        camMovement.camHolder.enabled = true;
        camMovement.camHolder.Play("camShift0to1");

        Transform couch = globalState.homeScene.transform.Find("couch");
        couch.Find("patternYellow").gameObject.SetActive(false);
        couch.GetComponent<Animator>().SetTrigger("action1"); //scrolling of second pattern

        globalState.homeScene.transform.Find("plant").GetComponent<imgSwitcher>().switchToImgState(1);

        yield return new WaitForSeconds(3);

        camMovement.vfx.Play("blink");
        yield return new WaitForSeconds(1);

        camMovement.camHolder.enabled = false;
        camMovement.cam.Play("lrGlanceRightside");

        yield return new WaitForSeconds(2);

        globalState.homeScene.transform.Find("btfl").GetComponent<Animator>().SetTrigger("action2"); //reappearance and exit

    }

    //called from btfl anim
    public void handReachOut()
    {
        Transform hd = globalState.homeScene.transform.Find("MyHand");
        hd.gameObject.SetActive(true);
        hd.GetComponent<Animator>().SetTrigger("action1"); //reach out

        StartCoroutine(Global.Chain(this, Global.WaitForSeconds(2), Global.Do(() =>
        {
            //scene ending 

            //sfx hurried footsteps out
            //rushed exit
            camMovement.enable.darkCover.SetTrigger("fadeIn");

        }), Global.WaitForSeconds(5), Global.Do(() =>
        {
            globalState.revealAndHideStuff(11, false); //hide curr scene GOs

            enable.setUpLevel(11, true); //subscene logic
        })));
    }

    public void b1FadeIn()
    {
        Transform b1 = transform.Find("b1");
        b1.gameObject.SetActive(true);
        Animator a = b1.GetComponent<Animator>();

        a.SetTrigger("fadeIn");

    }

    public void b8FadeIn()
    {
        Transform b8 = transform.Find("b8");
        b8.gameObject.SetActive(true);
        Animator a = b8.GetComponent<Animator>();

        a.SetTrigger("fadeIn");
        a.SetTrigger("action1");

    }

    public void pollockDone() { globalState.mirrorScene.transform.Find("girl").gameObject.SetActive(false); }

    public void triggerStreetDialogue()
    {
        globalState.audio.fadeVolumeSFX(11, 8, 2, 0.4f); //ambience sound lower volume

        GetComponent<Animator>().enabled = false;
        //dialogue
        StartDialogueClickThrough dlg = globalState.streetScene.transform.Find("StartDialogue").GetComponent<StartDialogueClickThrough>();
        dlg.gameObject.SetActive(true);
        dlg.enableStartDialogue();

        globalState.toggleAnimationGlobalClickable(true);
    }

    public void fadeOut() { GetComponent<Animator>().SetTrigger("fadeOut"); }

    public void endingFadeOutThings()
    {
        enable.startCanvas.transform.Find("water").GetComponent<Animator>().SetTrigger("fadeOut");
    }


    public void fadeInStartButton()
    {
        enable.fadeInStartButton();
    }

    public void enableButton()
    {
        GetComponent<Button>().interactable = true;
    }
}
