using System.Collections;
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

    public void setGlobalClickableTrue() { globalState.globalClickable = true; }

    public void setGlobalClickableFalse() { globalState.globalClickable = false; }
}
