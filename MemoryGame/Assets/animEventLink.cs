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
        yield return new WaitForSeconds(3); //wait for anim to fade into new scene view

        camMovement.cam.Play("blink");

        yield return new WaitForSeconds(2);

        globalState.treeBottomScene.transform.Find("tree").GetComponent<Animator>().Play("treeBottom"); //fade in top layer sprite
    }


    public void setGlobalClickableTrue() { globalState.globalClickable = true; }

    public void setGlobalClickableFalse() { globalState.globalClickable = false; }
}
