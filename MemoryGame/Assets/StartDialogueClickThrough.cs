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

    void Start()
    {
        foreach(Animator a in textAnimators)
        {
            a.GetComponent<Text>().color = new Color(1, 1, 1, 0); //hide all texts
        }

        backPanel.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        backPanel.gameObject.SetActive(false);

        counter = 0;
    }


    void Update()
    {
        if(counter > 0 && Input.GetMouseButtonDown(0) && !animating)
        {
            if(counter == 4 || counter == 7 || counter == 9)
            {
                StartCoroutine(waitAndHideTexts(counter));
            }
            else
            {
                StartCoroutine(fadeInText(counter));
            }
            
        }
    }

    IEnumerator waitAndHideTexts(int c)
    {
        animating = true;
        switch (c)
        {
            case 4:
                for(int i=0; i<3; i++)
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
                backPanel.SetTrigger("fadeOut");
                yield return new WaitForSeconds(2f);
                backPanel.gameObject.SetActive(false);
                myEnabler.startGame();
                break;
        }

        counter++;
        animating = false;
    }

    IEnumerator fadeInText(int c)
    {
        animating = true;
        if(c==8) yield return new WaitForSeconds(1f);
        else yield return new WaitForSeconds(0.3f);
        textAnimators[c-1].SetTrigger("fadeInText");
        yield return new WaitForSeconds(1f);
        counter++;
        animating = false;
    }

    //should be tied to start button
    public void enableStartDialogue()
    {
        StartCoroutine(backPanelFadeIn());
    }

    IEnumerator backPanelFadeIn()
    {
        animating = true;
        backPanel.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1.5f);
        counter = 1;
        animating = false;
    }
}
