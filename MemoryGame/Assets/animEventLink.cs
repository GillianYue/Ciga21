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
            Transform ball = globalState.pupScene.transform.Find("ball");
            ball.gameObject.SetActive(true);
            Animator ba = ball.GetComponent<Animator>();
            ba.SetTrigger("action1"); //restore default pos
            ba.SetTrigger("fadeIn"); //fadein

            interactable bl = globalState.pupScene.transform.Find("ball").GetComponent<interactable>();
            bl.var1 = 0;

        }
        else if(itemIndex == 1) //stick
        {
            Transform stick = globalState.pupScene.transform.Find("stick");
            stick.gameObject.SetActive(true);
            Animator sa = stick.GetComponent<Animator>();
            sa.SetTrigger("action1");
            sa.SetTrigger("fadeIn"); //fadein

            interactable stk = globalState.pupScene.transform.Find("stick").GetComponent<interactable>();
            stk.var1 = 0;
        }
    }

    public void setGlobalClickableTrue() { globalState.globalClickable = true; }

    public void setGlobalClickableFalse() { globalState.globalClickable = false; }
}
