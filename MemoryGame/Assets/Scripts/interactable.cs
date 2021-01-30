using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class interactable : MonoBehaviour, IPointerClickHandler
{
    private int timesClicked = 0; //this should keep track of effective clicks
    public bool clickable = true;
    Animator myAnimator;
    public int numInteractions;
    public GameObject changeIntoPrefab; //applies when type is animThenImgChange

    public enum InteractType { animThenImgChange, anim };
    public InteractType interactType;

    void Start()
    {
        myAnimator = GetComponent<Animator>();

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
                myAnimator.SetTrigger("action1");
                break;
            case InteractType.animThenImgChange:
                if (timesClicked <= numInteractions)
                {
                    print("checking behavior: " + timesClicked);
                    myAnimator.SetTrigger("action" + timesClicked.ToString());

                }
                break;
        }

    }

    public void swapSpriteToTarget()
    {
        GameObject target = Instantiate(changeIntoPrefab, transform.parent, false);
        target.transform.localPosition = transform.localPosition;
        target.SetActive(false);
        setChildrenInvisible(target);

        myAnimator.SetTrigger("fadeOut");
        target.GetComponent<Animator>().SetTrigger("fadeIn");
    }

    public void setClickableTrue() { clickable = true; print("actually being set"); }

    public void setClickableFalse() { clickable = false; }

    static void setChildrenInvisible(GameObject obj)
    {
        Color col0 = obj.transform.GetChild(0).GetComponent<Image>().color,
            col1 = obj.transform.GetChild(1).GetComponent<Image>().color;

        obj.transform.GetChild(0).GetComponent<Image>().color = new Color(col0.r, col0.g, col0.b, 0);
        obj.transform.GetChild(1).GetComponent<Image>().color = new Color(col1.r, col1.g, col1.b, 0);

        obj.SetActive(true);
    }
}
