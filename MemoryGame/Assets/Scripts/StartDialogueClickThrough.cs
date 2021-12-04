using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogueClickThrough : MonoBehaviour
{
    public Animator[] textAnimators;
    public Animator backPanel;

    public int counter;
    public bool animating;

    public enabler myEnabler;

    public int dialogueType; //0 start, 1 streetScene

    public bool textLock; //prevent two texts from being triggered in too short of an interval

    private void Awake()
    {
    }

    void Start()
    {

        foreach (Animator a in textAnimators)
        {
            a.GetComponent<Text>().color = new Color(1, 1, 1, 0); //hide all texts
        }

        backPanel.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        if(dialogueType == 0) backPanel.gameObject.SetActive(false);

        counter = 0;


    }


    void FixedUpdate()
    {

            if (!animating && counter > 0 && Input.GetMouseButtonDown(0)) //show next line of text
            {
                if (dialogueType == 1)
                {
                        if (counter == 6 || counter == 9)
                        {
                            StartCoroutine(waitAndHideTexts(counter)); //hide prev page of texts
                        }
                        else
                        {
                            StartCoroutine(fadeInText(counter));
                        }
                }
                else
                {
                        if (counter == 4 || counter == 7 || counter == 9)
                        {
                            StartCoroutine(waitAndHideTexts(counter)); //hide prev page of texts
                        }
                        else
                        {
                            StartCoroutine(fadeInText(counter));
                        }
            }


            }
    }

    IEnumerator waitAndHideTexts(int c)
    {
        animating = true;

        if (!textLock)
        {
            StartCoroutine(triggerTextLock());

            if (dialogueType == 1)
            {
                switch (c)
                {
                    case 6:
                        for (int i = 0; i < 5; i++)
                        {
                            textAnimators[i].SetTrigger("fadeOutText");
                        }
                        myEnabler.audio.fadeVolumeSFX(11, 8, 5, 0);
                        yield return new WaitForSeconds(6f);
                        //TODO sfx
                        myEnabler.audio.playSFX(7, 1);
                        yield return new WaitForSeconds(3f);
                        textAnimators[5].SetTrigger("fadeInText");
                        yield return new WaitForSeconds(1f);
                        break;
                    case 9:

                        for (int i = 5; i < 8; i++)
                        {
                            textAnimators[i].SetTrigger("fadeOutText");
                        }

                        Transform mirrorScene = myEnabler.globalState.mirrorScene.transform;

                        yield return new WaitForSeconds(3f);

                        myEnabler.audio.playSFX(0, 17, 0.2f); //ambience white noise
                        myEnabler.audio.fadeVolumeSFX(0, 17, 2, 1);

                        mirrorScene.gameObject.SetActive(true);
                        Transform mirror = mirrorScene.Find("mirror"), things = mirrorScene.Find("things");
                        mirror.gameObject.SetActive(false); things.gameObject.SetActive(false);
                        backPanel.SetTrigger("fadeOutSlow"); //text panel fade 

                        yield return new WaitForSeconds(3f);
                        myEnabler.audio.playSFX(11, 9, 0.7f);

                        mirror.gameObject.SetActive(true); things.gameObject.SetActive(true);
                        mirror.GetComponent<Animator>().SetTrigger("fadeIn");
                        things.GetComponent<Animator>().SetTrigger("fadeIn");

                        yield return new WaitForSeconds(11f);
                        Transform girl = mirrorScene.Find("girl");
                        girl.gameObject.SetActive(true);
                        // girl.GetComponent<Animator>().SetTrigger("fadeIn");
                        // yield return new WaitForSeconds(4f);

                        girl.GetComponent<imgSwitcher>().switchToImgState(1);

                        girl.GetComponent<Animator>().SetTrigger("action1"); //pollock
                                                                             //will trigger fade out by end of animation

                        yield return new WaitForSeconds(12f);
                        Transform me = mirrorScene.Find("me");
                        me.gameObject.SetActive(true);
                        me.GetComponent<Animator>().SetTrigger("fadeIn");
                        me.GetComponent<Animator>().SetTrigger("action1");

                        yield return new WaitForSeconds(8f);

                        myEnabler.GetComponent<BlurManager>().levelPassEffect(11);

                        yield return new WaitForSeconds(2f);
                        backPanel.gameObject.SetActive(false);

                        me.gameObject.SetActive(false);
                        mirror.gameObject.SetActive(false);
                        things.gameObject.SetActive(false);
                        break;
                }
            }
            else if (dialogueType == 0)
            {
                switch (c)
                {
                    case 4:
                        for (int i = 0; i < 3; i++)
                        {
                            textAnimators[i].SetTrigger("fadeOutText");
                        }
                        yield return new WaitForSeconds(2f);
                        textAnimators[3].SetTrigger("fadeInText");
                        yield return new WaitForSeconds(1f);
                        break;
                    case 7:
                        for (int i = 3; i < 6; i++)
                        {
                            textAnimators[i].SetTrigger("fadeOutText");
                        }
                        yield return new WaitForSeconds(5f);
                        textAnimators[6].SetTrigger("fadeInText");
                        yield return new WaitForSeconds(3f);
                        break;
                    case 9:
                        for (int i = 6; i < 8; i++)
                        {
                            textAnimators[i].SetTrigger("fadeOutText");
                        }

                        yield return new WaitForSeconds(1);

                        myEnabler.darkCover.SetTrigger("fadeIn");

                        myEnabler.audio.playSFX(0, 1); //routine

                        yield return new WaitForSeconds(22);

                        backPanel.SetTrigger("fadeOutSlow"); //text panel fade 

                        yield return new WaitForSeconds(1f);

                        myEnabler.darkCover.SetTrigger("fadeOut");
                        yield return new WaitForSeconds(1f);
                        myEnabler.startGame();

                        yield return new WaitForSeconds(2f);
                        backPanel.gameObject.SetActive(false);
                        break;
                }
            }


            counter++;

        }//text lock 

        animating = false;
    }

    IEnumerator fadeInText(int c)
    {

        animating = true;

        if (!textLock)
        {
            StartCoroutine(triggerTextLock());

            if (dialogueType == 0 && c == 8) yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(0.3f);

            textAnimators[c - 1].SetTrigger("fadeInText");

            yield return new WaitForSeconds(1f);

            if (dialogueType == 0 && c == 8) yield return new WaitForSeconds(3f);

            counter++;

            if (dialogueType == 1 && c == 3) //auto-advance to 3
            {
                yield return new WaitForSeconds(2f);

                textAnimators[3].SetTrigger("fadeInText");

                yield return new WaitForSeconds(1f);
                counter++;
            }

        }
        animating = false;
    }

    IEnumerator triggerTextLock()
    {
        textLock = true;
        yield return new WaitForSeconds(0.5f);

        textLock = false;
    }

    //should be tied to start button
    public void enableStartDialogue()
    {
        StartCoroutine(backPanelFadeIn());
    }

    IEnumerator backPanelFadeIn()
    {
        animating = true;

        myEnabler.globalState.globalClickable = false;

        backPanel.SetTrigger("fadeIn"); //pure colored background
        yield return new WaitForSeconds(1.5f);
        counter = 1;
        

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(fadeInText(counter)); //text first line auto entry

        yield return new WaitForSeconds(1.5f);


        myEnabler.globalState.globalClickable = true;
        animating = false;
    }
}
