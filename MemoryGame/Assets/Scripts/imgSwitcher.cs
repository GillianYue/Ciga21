using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// used for GOs with sprites that have multiple visual states
/// 
/// default starts with the first pair (state 0)
/// </summary>
public class imgSwitcher : MonoBehaviour
{
    //each state has two
    public Sprite[] stateImgs;

    private Image img1, img2;
    private int currIndex = 0;

    GameObject gameControl;

    void Start()
    {
        img1 = transform.Find("Image").GetComponent<Image>();
        img2 = transform.Find("Image (1)").GetComponent<Image>();

        gameControl = GameObject.FindGameObjectWithTag("GameController");

        switchToImgState(0);
    }


    void Update()
    {
        
    }


    /// <param name="s"> which "pair" of images to use for this state s </param>
    public void switchToImgState(int s)
    {
        int idx1 = 2 * s, idx2 = 2 * s + 1;

        if(img1 == null)
        {
            img1 = transform.GetChild(0).GetComponent<Image>();
            img2 = transform.GetChild(1).GetComponent<Image>();
        }

        img1.sprite = stateImgs[idx1];
        img2.sprite = stateImgs[idx2];
        img1.overrideSprite = stateImgs[idx1];
        img2.overrideSprite = stateImgs[idx2];

    }

    public void switchToNextImgState()
    {
        
        if(stateImgs.Length >= (currIndex + 1) * 2 + 1)
        {
            currIndex += 1;
            switchToImgState(currIndex);
        }
    }


    public void myTriggerAction()
    {
        switch (name)
        {
            case "dad":
                if (currIndex == 1)
                {
                    GameObject pasta = GameObject.Find("Pasta(Clone)");
                    if (pasta != null) pasta.GetComponent<interactable>().clickable = true;
                    StartCoroutine(pointAtPasta());
                    currIndex += 1;
                }
                else switchToNextImgState();

                break;
            case "Pasta(Clone)":
                switchToNextImgState();
                gameControl.GetComponent<globalStateStore>().playSFX(1, 6);
                if (currIndex == 3) StartCoroutine(pastaFinish());
                break;

        }
    }

    public void clearOverride()
    {
        img1.overrideSprite = null;
        img2.overrideSprite = null;

        img1.sprite = stateImgs[currIndex * 2];
        img2.sprite = stateImgs[currIndex * 2 + 1];
    }

    IEnumerator pointAtPasta()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetTrigger("action1");
    }


    IEnumerator pastaFinish()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("dad").GetComponent<imgSwitcher>().myTriggerAction();

        //TODO more
        gameControl.GetComponent<BlurManager>().scene1Clear2();
        yield return new WaitForSeconds(4);
        gameControl.GetComponent<BlurManager>().levelPassEffect(1);
    }


}
