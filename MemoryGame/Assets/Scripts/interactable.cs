using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class interactable : MonoBehaviour, IPointerClickHandler
{
    private int timesClicked = 0; //this should keep track of effective clicks
    bool clickable = false;
    Animator myAnimator;
    int numInteractions;

    void Start()
    {
        myAnimator = GetComponent<Animator>();

        //test
        clickable = true;
        numInteractions = 4;
    }

    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        print("clicked: " + eventData.pointerPress.name);
        if (clickable)
        {
            timesClicked += 1;
            checkBehavior();
        }
    }

    public void fadeIn()
    {
        myAnimator.SetTrigger("fadeIn");
    }

    public void fadeOut()
    {
        myAnimator.SetTrigger("fadeOut");
    }

    //should be overidden by children?
    public void checkBehavior()
    {
        if(timesClicked <= numInteractions)
        {
            print("checking behavior: " + timesClicked);
            myAnimator.SetTrigger("action" + timesClicked.ToString());
        }
    }

    public void setClickableTrue() { clickable = true; print("actually being set"); }

    public void setClickableFalse() { clickable = false; }
}
