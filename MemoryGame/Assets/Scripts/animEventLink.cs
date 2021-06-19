﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(name[0] == 'l' && transform.parent.name.Equals("up_front"))
        {

            GetComponent<Animator>().SetTrigger("action" + name[1]);
         //   GetComponent<Animator>().SetTrigger("fadeIn"); //fades in lavendar color


        }else if (transform.GetChild(0).name.Equals("bush"))
        {
            StartCoroutine(Global.Chain(this,
                    Global.WaitForSeconds(Random.Range(0f, 2f)),
                    Global.Do(() => {
                        GetComponent<Animator>().Play("bush" + (Random.Range(1, 3)));
                    })));

        }else if(name[0] == 'f' && transform.parent.name.Equals("foxtail"))
        {
            GetComponent<Animator>().SetTrigger("action" + Random.Range(1, 5));
        }else if (name.Equals("bushesCloseup"))
        {
            GetComponent<Animator>().SetTrigger("action1");
        }
    }

    //called from animation after screen fades out
    public void treeSceneEndTransition()
    {
        StartCoroutine(treeSceneEndTransitionCoroutine());

    }

    IEnumerator treeSceneEndTransitionCoroutine()
    {
        
        enable.setUpLevel(3, true);
        yield return new WaitForSeconds(7); //wait for anim to fade into new scene view
        camMovement.cam.Play("naturalBreathe");
        yield return new WaitForSeconds(5);

        camMovement.vfx.Play("blink");

        yield return new WaitForSeconds(2);

        globalState.treeBottomScene.transform.Find("tree").GetComponent<Animator>().Play("treeBottom"); //fade in top layer sprite

        yield return new WaitForSeconds(3);

        camMovement.vfx.Play("blink");
    }

    //called from the end of fetch item animations
    public void pupDropItem(int itemIndex)
    {
        print(itemIndex + " fetched");

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
        else if(itemIndex == 1) //stick
        {
            Transform stick = globalState.pupScene.transform.Find("stick").GetChild(0);
            stick.gameObject.SetActive(true);
            Animator sa = stick.GetComponent<Animator>();
            sa.SetTrigger("action1");
            sa.SetTrigger("fadeIn"); //fadein

            interactable stk = stick.GetComponent<interactable>();
            stk.var1 = 0;
        }else if(itemIndex == 2) //still ball, but signals ending anim for scene
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
                    })));
    }

    public void sunsetDone()
    {
        StartCoroutine(sunsetDoneCoroutine());
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

        yield return new WaitForSeconds(3);
        f2.GetComponent<imgSwitcher>().switchToImgState(1);

        yield return new WaitForSeconds(1.5f);
        f1.GetComponent<imgSwitcher>().switchToImgState(2); //looking at 3

        yield return new WaitForSeconds(3);
        stand.GetComponent<Animator>().SetTrigger("fadeOut");
        f2.GetComponent<imgSwitcher>().switchToImgState(2); //take beer
        yield return new WaitForSeconds(1.5f);
        f1.GetComponent<imgSwitcher>().switchToImgState(3); //take beer
        Transform friendBeer = f1.Find("beer");
        friendBeer.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);
        Transform hand = globalState.seaScene.transform.Find("hand_beer");
        hand.gameObject.SetActive(true);
        Animator handAnim = hand.GetChild(0).GetComponent<Animator>();
        handAnim.SetTrigger("action1"); //hand out

        yield return new WaitForSeconds(1.5f);
        f3.GetComponent<Animator>().SetTrigger("fadeIn"); //fade in f3 sprite
        f3.Find("light").GetComponent<Animator>().SetTrigger("fadeIn"); //will auto-transition to light flicker
        yield return new WaitForSeconds(1.5f);
        f3.GetComponent<imgSwitcher>().switchToImgState(1); 

        yield return new WaitForSeconds(3);
        camMovement.camHolder.Play("camShiftLeftBack");
        yield return new WaitForSeconds(4);

        camMovement.camHolder.enabled = false;
        camMovement.cam.Play("camBeerDrink"); 
        //TODO sfx
        yield return new WaitForSeconds(3);

        camMovement.camHolder.enabled = true;

        camMovement.camHolder.Play("camShiftDown");

        yield return new WaitForSeconds(4);
        camMovement.camHolder.Play("camShiftUp");

        yield return new WaitForSeconds(3);
        //TODO sfx to the right
        camMovement.camHolder.Play("camShiftRight"); //turn right

        handAnim.SetTrigger("action1"); //out

        yield return new WaitForSeconds(1);
        friendBeer.GetComponent<Animator>().SetTrigger("action1"); //raise

        yield return new WaitForSeconds(3);

        handAnim.enabled = false;
        handAnim.GetComponent<MouseBasedCamShift>().active = true; //enable mouse based movement

        yield return new WaitUntil(() => { return (Vector2.Distance(hand.position, friendBeer.position) < 50); });
        //TODO check above

        //TODO sfx of chatter
        //blur

        yield return new WaitForSeconds(5);
        camMovement.camHolder.Play("camShiftLeftBack");
        yield return new WaitForSeconds(3);
        enable.darkCover.SetTrigger("fadeInSlow"); //very slowly fades to black

        yield return new WaitForSeconds(5);

        //set up for night
        Transform sea = globalState.transform.Find("sea");
        sea.Find("dusk").gameObject.SetActive(false);
        sea.Find("night").gameObject.SetActive(true);
        sea.Find("waves").GetComponent<Animator>().Play("wave1Hold"); //wave anim transition

        //disable sunset reflections
        friendBeer.gameObject.SetActive(false);
        hand.gameObject.SetActive(false);
        f3.Find("light").gameObject.SetActive(false);
        f2.Find("light").gameObject.SetActive(false);
        f1.Find("light").gameObject.SetActive(false);

        camMovement.camHolder.Play("camShiftDown"); //so that when eyes open is staring at self

        yield return new WaitForSeconds(3);
        enable.darkCover.SetTrigger("fadeOutSlow");

        yield return new WaitForSeconds(3);
        camMovement.vfx.Play("blink");
        yield return new WaitForSeconds(1);

        camMovement.camHolder.Play("camShiftUp");
        yield return new WaitForSeconds(3);

        camMovement.camHolder.Play("camShiftRight");
        //TODO maybe sfx of voice "stars"
        yield return new WaitForSeconds(3);

        camMovement.camHolder.Play("camShiftLeftBack");
        yield return new WaitForSeconds(3);

        //TODO start stars interact
    }

    public void setGlobalClickableTrue() { globalState.globalClickable = true; }

    public void setGlobalClickableFalse() { globalState.globalClickable = false; }


}
